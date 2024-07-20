using System.IO;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using ZXing.Aztec;
using System.Windows.Forms;
using System.Drawing;
using ZXing.Windows.Compatibility;
using static System.Windows.Forms.DataFormats;
using System.Reflection.PortableExecutable;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using RadioButton = System.Windows.Forms.RadioButton;
using System.Security.Policy;
using System.Net.Http;
using System.Configuration;
using Microsoft.VisualBasic;
using System;

namespace Scanner
{
    public partial class Form1 : Form
    {
        private FilterInfoCollection CaptureDevice;
        private VideoCaptureDevice FinalFrame;
        private Task<List<Students>?> studentData;

        public string authCode
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["AuthCode"];
            }
        }

        public string schoolURL
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["SchoolWebsiteURL"];
            }
        }

        public Form1()
        {
            InitializeComponent();

            this.Text = "Attendance Scanner System";

            //check if there is internet connection first
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                MessageBox.Show("Please check your internet connection and try again.");
                return;
            }

            // Check if website URL is set in app.config  
            if (string.IsNullOrEmpty(schoolURL) && string.IsNullOrEmpty(authCode))
            {
                // Show a popup to ask for the website URL and authentication code
                string userInput = Interaction.InputBox("Please enter the School Application URL:", "Enter School URL", "");
                string authCodeInput = Interaction.InputBox("Please enter the Authentication Code:", "Enter Authentication Code", "");

                // Save the website URL and auth code in app.config
                SaveAppSettings("SchoolWebsiteURL", userInput);
                SaveAppSettings("AuthCode", authCodeInput);
            }

            // Check if auth code matches, invalidate the request and show a popup message
            if (!IsValidAuthCode(schoolURL, authCode))
            {
                MessageBox.Show("Setup is incomplete. Please contact support and try again.");
                //disable the camera button
                button1.Enabled = false;
                button1.Text = "SETUP ERROR";
                button1.BackColor = Color.White;

                return;
            }
            else
            {
                // Continue with the rest of your code
                var school = GetSchoolData(schoolURL, authCode);

                if (school != null)
                {
                    label5.Text = school.school_name;
                    pictureBox2.ImageLocation = schoolURL + "/" + school.logo;
                }

                label5.TextAlign = ContentAlignment.MiddleCenter;
                studentData = GetStudentsData(schoolURL + "/autosync?token=" + authCode);
                SyncronizeDataToWeb();
            }

        }

        private void SaveAppSettings(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            KeyValueConfigurationElement setting = config.AppSettings.Settings[key];

            if (setting != null)
            {
                setting.Value = value;
            }
            else
            {
                config.AppSettings.Settings.Add(key, value);
            }

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private bool IsValidAuthCode(string schoolURL, string authCode)
        {
            if (schoolURL == null || authCode == null)
            {
                return false;
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = client.GetAsync(schoolURL + "/validatetoken?token=" + authCode).Result;

                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    if (jsonString != null)
                    {
                        return jsonString.Contains("Access Granted") ? true : false;
                    }
                }

                // Add your authentication code validation logic here
                // Return true if the auth code is valid, otherwise return false
                return false; // Placeholder, implement your validation logic
            }
            catch { return false; }
        }

        private School? GetSchoolData(string schoolURL, string authCode)
        {
            if (schoolURL == null || authCode == null)
            {
                return null;
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = client.GetAsync(schoolURL + "/getapplicationdata?token=" + authCode).Result;

                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    if (jsonString != null)
                    {
                        return jsonString.Contains("Access Denied") ? null : JsonConvert.DeserializeObject<School>(jsonString);
                    }
                }

                // Add your authentication code validation logic here
                // Return true if the auth code is valid, otherwise return false
                return null; // Placeholder, implement your validation logic
            }
            catch { return null; }
        }
        /// <summary>
        /// Sync the attendance data to the webserver
        /// </summary>
        public void SyncronizeDataToWeb(int? days = null, bool showmsg = false)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var day = days.HasValue ? days.Value : 1;
                    //default sync to 1 day
                    var daysToSync = Enumerable.Range(0, day).Select(x => DateTime.Today.AddDays(-x)).ToList();

                    foreach (var date in daysToSync)
                    {
                        var dailyRecordsPath = GetAttendanceOfAttendanceToday(date);
                        var jsonData = LoadJson(dailyRecordsPath);

                        if (jsonData is null)
                            continue;

                        string json = JsonConvert.SerializeObject(jsonData);
                        var content = new StringContent(json);

                        var syncUrl = schoolURL + "/autosync/syncronizeattendance?token=" + authCode;

                        var task = Task.Run(() => client.PostAsync(syncUrl, content));
                        task.Wait();
                        var response = task.Result.Content.ReadAsStringAsync().Result;
                        if (task.Result.StatusCode != HttpStatusCode.OK)
                        {
                            MessageBox.Show("Cannot synchronize data to webserver. Please contact system administrator and present this error. "
                                + Environment.NewLine + Environment.NewLine +
                                response, task.Result.ReasonPhrase);
                        }
                        else if (!response.Contains("Synchronization Complete"))
                        {
                            MessageBox.Show("Cannot synchronize data to webserver. Please contact system administrator and present this error." +
                                Environment.NewLine + Environment.NewLine +
                                "URL may not be accessible", task.Result.ReasonPhrase);
                        } 
                    }

                    if(showmsg)
                    {
                        MessageBox.Show("Synchronization to webserver completed");
                    }

                    if (days != null)
                    {
                        MessageBox.Show("Synchronization to webserver completed for the last " + day + " days");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //check path
        public string GetAttendancePath()
        {
            var exePath = System.AppDomain.CurrentDomain.BaseDirectory;
            var dailyRecordsDir = Path.Combine(exePath, "DailyRecords");
            
            //create the folder if it does not exist
            if (!Directory.Exists(dailyRecordsDir))
            {
                Directory.CreateDirectory(dailyRecordsDir);
            }

            return dailyRecordsDir;
        }

        public string GetAttendanceOfAttendanceToday(DateTime? date = null)
        {

            var dateToday = string.Format("{0}.json", DateTime.Now.ToString("dd-MMM-yyyy"));
            if (date.HasValue)
                dateToday = string.Format("{0}.json", date.Value.ToString("dd-MMM-yyyy"));

            var dailyRecordsPath = Path.Combine(GetAttendancePath(), dateToday);

            //if file does not exist, create it
            if (!File.Exists(dailyRecordsPath))
            {
                using (File.Create(dailyRecordsPath))
                {
                    // Do nothing, just create the file
                }
            }
            return dailyRecordsPath;
        }


        public void WriteToJSON(Attendance? attendances)
        {
            //if null do nothing
            if (attendances is null)
                return;

            //get date today
            var dailyRecordsPath = GetAttendanceOfAttendanceToday();


            //check if the path exists
            if (File.Exists(dailyRecordsPath))
            {
                //update the file
                var jsonData = LoadJson(dailyRecordsPath);
                //do 
                if (jsonData is null)
                    jsonData = new List<Attendance>();

                if (jsonData.ToList().Any(s => s.type == attendances.type && s.lrn == attendances.lrn))
                {
                    return;
                }
                else
                {
                    jsonData.Add(attendances);
                }

                //if not, insert a new one
                // open file stream
                using (StreamWriter file = File.CreateText(dailyRecordsPath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    //serialize object directly into file stream
                    serializer.Serialize(file, jsonData);
                }

            }
            else
            {
                //insert it as a list
                var list = new List<Attendance>();
                list.Add(attendances);
                //if not, insert a new one
                // open file stream
                using (StreamWriter file = File.CreateText(dailyRecordsPath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    //serialize object directly into file stream
                    serializer.Serialize(file, list);
                }
            }

        }

        public List<Attendance>? LoadJson(string path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Attendance>>(json);
            }
        }

        public static async Task<List<Students>?> GetStudentsData(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    response.EnsureSuccessStatusCode();
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    if (jsonString != null)
                    {
                        return JsonConvert.DeserializeObject<List<Students>>(jsonString);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            CaptureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo Device in CaptureDevice)
            {
                comboBox1.Items.Add(Device.Name);
            }

            comboBox1.SelectedIndex = 0;
            FinalFrame = new VideoCaptureDevice();
        }

        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
            //photo_was_taken = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
            Console.WriteLine("Scanner Strated");
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            ExitCamera();

            //Send the data to the webserver after closing
            SyncronizeDataToWeb(null, true);
            //this.Close();
        }

        public void ExitCamera()
        {
            if (FinalFrame != null)
            {
                if (FinalFrame.IsRunning)
                {
                    //FinalFrame.SignalToStop();
                    //FinalFrame = null;

                    FinalFrame.NewFrame -= new NewFrameEventHandler(FinalFrame_NewFrame);
                    //Thread.CurrentThread.Abort();
                    FinalFrame.SignalToStop();
                    FinalFrame.WaitForStop();
                }
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            //chagne the date 

            label3.Text = DateTime.Now.ToString("ddd, MMM dd, yyyy");
            label4.Text = DateTime.Now.ToLongTimeString();

            BarcodeReader Reader = new BarcodeReader();
            Reader.Options = new ZXing.Common.DecodingOptions
            {
                TryHarder = true,
            };

            if (pictureBox1.Image != null)
            {
                Result result = Reader.Decode((Bitmap)pictureBox1.Image);
                try
                {
                    if (result == null) { return; }
                    string decoded = result.ToString().Trim();
                    Console.WriteLine(decoded);
                    if (decoded != "")
                    {

                        //get the checked radio button 
                        var checkedRadio = panel4.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Name;

                        //now we check if the decoded string matches the student LRN
                        if (studentData.Result != null && studentData.Result.Any(student => student.lrn == decoded))
                        {
                            var student = studentData.Result.Where(s => s.lrn == decoded).FirstOrDefault();
                            //if there is a match, show the profile picture and show the OK image
                            label7.Text = (decoded);
                            pictureBox3.ImageLocation = string.Format("{0}/{1}", schoolURL, student?.profile_photo);
                            //"http://localhost:8080/uploads/students/19/1468593869-abra-evolution-pokemon.gif";

                            pictureBox4.Image = Properties.Resources.ok;

                            //now we update the json file and log the attendance
                            Attendance attendance = new Attendance
                            {
                                lrn = student?.lrn,
                                currentdate = DateTime.Now.ToString("yyyy-MM-dd"),
                                time = DateTime.Now.ToString("HH:mm:ss"),
                                student_id = student?.student_id,
                                type = checkedRadio.ToString()

                            };
                            WriteToJSON(attendance);
                        }
                        else
                        {
                            //clear the image and show the x image
                            label7.Text = "LRN not found";
                            pictureBox3.Image = Properties.Resources._734189_middle;
                            pictureBox4.Image = Properties.Resources.x;

                        }

                        //play the beep sound
                        Console.Beep(3000, 400);


                        //then run for another second
                        Thread.Sleep(1000);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception Occured", ex.ToString());
                }

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            //if (this.Text == "Start Camera")
            //{
            //    button1.Text = "Stop Camera";
            //}
            //else
            //{
            //    button1.Text = "Start Camera";

            //    FinalFrame = new VideoCaptureDevice(CaptureDevice[comboBox1.SelectedIndex].MonikerString);
            //    FinalFrame.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
            //    //stop the camera
            //    FinalFrame.Stop();
            //    pictureBox1.Image = null;
            //    //stop the timer
            //    timer1.Stop();
            //}

            if (button1.Text.ToLower() == "start camera")
            {
                button1.Text = "STOP CAMERA";
                FinalFrame = new VideoCaptureDevice(CaptureDevice[comboBox1.SelectedIndex].MonikerString);
                FinalFrame.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
                FinalFrame.Start();

            }
            else if (button1.Text.ToLower() == "stop camera")
            {
                button1.Text = "START CAMERA";
                ExitCamera();
                //stop the camera 
                pictureBox1.Image = Properties.Resources.placeholder_550x550;
                Image x = Properties.Resources.x;
                pictureBox4.Image = x;
                //stop the timer 
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void last3DaysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SyncronizeDataToWeb(3);
        }

        private void last7DaysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SyncronizeDataToWeb(7);
        }

        private void last14DaysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SyncronizeDataToWeb(14);
        }

        private void last30DaysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SyncronizeDataToWeb(30);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Show a popup to ask for the website URL and authentication code
            string userInput = Interaction.InputBox("Please enter the School Application URL:", "Enter School URL", "");
            string authCodeInput = Interaction.InputBox("Please enter the Authentication Code:", "Enter Authentication Code", "");

            // Save the website URL and auth code in app.config
            SaveAppSettings("SchoolWebsiteURL", userInput);
            SaveAppSettings("AuthCode", authCodeInput);

            Application.Restart();
            Environment.Exit(0);
        }
    }
}