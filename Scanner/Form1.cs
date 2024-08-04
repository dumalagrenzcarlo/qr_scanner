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
using ZXing.Common;
using ZXing.QrCode;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text.RegularExpressions;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.Media;
using System.Security.AccessControl;
using System.Security.Principal;

namespace Scanner
{
    public partial class Form1 : Form
    {
        private FilterInfoCollection CaptureDevice;
        private VideoCaptureDevice FinalFrame;
        private Task<List<Students>?> studentData;
        private List<Attendance> attendances = new List<Attendance>();

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

            CheckFolderPermission();

            //check if there is internet connection first
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                MessageBox.Show("Internet is needed to fetch student data. Please check your internet connection and try again.");
                return;
            } 

            // Check if website URL is set in app.config  
            if (string.IsNullOrEmpty(schoolURL) && string.IsNullOrEmpty(authCode))
            {
                //Setup setup = new Setup();
                //setup.StartPosition = FormStartPosition.CenterScreen;
                //setup.BringToFront(); // Brings the form to the front
                //setup.Show(); // Shows the form
                //setup.Focus();
                MessageBox.Show("Setup is incomplete. Please click on \"Settings\" and add your school url and authentication code.");

                //add a message in the listbox to say you need to setup 
                //// Show a popup to ask for the website URL and authentication code
                //string userInput = Interaction.InputBox("Please enter the School Application URL:", "Enter School URL", "");
                //string authCodeInput = Interaction.InputBox("Please enter the Authentication Code:", "Enter Authentication Code", ""); 
                //disable the camera button
                button1.Enabled = false;
                button1.Text = "SETUP INVALID";
                button1.BackColor = Color.White;
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

        public bool IsDirectoryWritable(string dirPath, bool throwIfFails = false)
        {
            try
            {
                using (FileStream fs = File.Create(
                    Path.Combine(
                        dirPath,
                        Path.GetRandomFileName()
                    ),
                    1,
                    FileOptions.DeleteOnClose)
                )
                { }
                return true;
            }
            catch
            {
                if (throwIfFails)
                    throw;
                else
                    return false;
            }
        }
        private void CheckFolderPermission()
        {
            var exePath = System.AppDomain.CurrentDomain.BaseDirectory;
            var dailyRecordsDir = Path.Combine(exePath, "DailyRecords");

            bool writable = IsDirectoryWritable(dailyRecordsDir);

            if (!writable)
            {
                MessageBox.Show("You do not have read and write permission for the installation folder. Please make sure the installation folder has the required permissions for this app.");
            }

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

                    if (showmsg)
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

                jsonData.Add(attendances);

                //if (jsonData.ToList().Any(s => s.type == attendances.type && s.lrn == attendances.lrn))
                //{
                //    return;
                //}
                //else
                //{

                //}

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

        CancellationTokenSource cancellationToken;
        public void onStartScan(CancellationToken sourcetoken)
        {
            Task.Factory.StartNew(new Action(() =>
            {
                while (true)
                {
                    if (sourcetoken.IsCancellationRequested)
                    {
                        return;
                    }
                    Thread.Sleep(50);
                    BarcodeReader Reader = new BarcodeReader();
                    pictureBox1.BeginInvoke(new Action(() =>
                    {
                        if (pictureBox1.Image != null)
                        {
                            try
                            {
                                var results = Reader.DecodeMultiple((Bitmap)pictureBox1.Image);
                                if (results != null)
                                {
                                    foreach (Result result in results)
                                    {
                                        //listBox1.Items.Add(result.ToString() + $"- Type: {result.BarcodeFormat.ToString()}");
                                        if (result.ResultPoints.Length > 0)
                                        {
                                            var offsetX = pictureBox1.SizeMode == PictureBoxSizeMode.Zoom
                                               ? (pictureBox1.Width - pictureBox1.Image.Width) / 2 :
                                               0;
                                            var offsetY = pictureBox1.SizeMode == PictureBoxSizeMode.Zoom
                                               ? (pictureBox1.Height - pictureBox1.Image.Height) / 2 :
                                               0;
                                            var rect = new Rectangle((int)result.ResultPoints[0].X + offsetX, (int)result.ResultPoints[0].Y + offsetY, 1, 1);
                                            foreach (var point in result.ResultPoints)
                                            {
                                                if (point.X + offsetX < rect.Left)
                                                    rect = new Rectangle((int)point.X + offsetX, rect.Y, rect.Width + rect.X - (int)point.X - offsetX, rect.Height);
                                                if (point.X + offsetX > rect.Right)
                                                    rect = new Rectangle(rect.X, rect.Y, rect.Width + (int)point.X - (rect.X - offsetX), rect.Height);
                                                if (point.Y + offsetY < rect.Top)
                                                    rect = new Rectangle(rect.X, (int)point.Y + offsetY, rect.Width, rect.Height + rect.Y - (int)point.Y - offsetY);
                                                if (point.Y + offsetY > rect.Bottom)
                                                    rect = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height + (int)point.Y - (rect.Y - offsetY));
                                            }
                                            using (var g = pictureBox1.CreateGraphics())
                                            {
                                                using (Pen pen = new Pen(Color.Green, 5))
                                                {
                                                    g.DrawRectangle(pen, rect);

                                                    pen.Color = Color.Yellow;
                                                    pen.DashPattern = new float[] { 5, 5 };
                                                    g.DrawRectangle(pen, rect);
                                                }
                                                g.DrawString(result.ToString(), new Font("Tahoma", 16f), Brushes.Blue, new System.Drawing.Point(rect.X - 60, rect.Y - 50));
                                            }
                                        }


                                    }

                                }
                            }
                            catch (Exception)
                            {
                            }
                        }

                    }));

                }
            }), sourcetoken);
        }

        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        { 
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
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
            SyncronizeDataToWeb(null, false);
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
            //change the date 

            label3.Text = DateTime.Now.ToString("ddd, MMM dd, yyyy");
            label4.Text = DateTime.Now.ToLongTimeString();
            if (pictureBox1.Image != null)
            {
                BarcodeReader reader = new BarcodeReader();

                //reader.AutoRotate = true;
                //reader.Options = new DecodingOptions { TryHarder = true };

                Result result = reader.Decode((Bitmap)pictureBox1.Image);
                try
                {
                    if (result == null) { return; }
                    string decoded = result.ToString().Trim();

                    if (result.ResultPoints.Length > 0)
                    {
                        var offsetX = pictureBox1.SizeMode == PictureBoxSizeMode.Zoom
                           ? (pictureBox1.Width - pictureBox1.Image.Width) / 2 :
                           0;
                        var offsetY = pictureBox1.SizeMode == PictureBoxSizeMode.Zoom
                           ? (pictureBox1.Height - pictureBox1.Image.Height) / 2 :
                           0;
                        var rect = new Rectangle((int)result.ResultPoints[0].X + offsetX, (int)result.ResultPoints[0].Y + offsetY, 1, 1);
                        foreach (var point in result.ResultPoints)
                        {
                            if (point.X + offsetX < rect.Left)
                                rect = new Rectangle((int)point.X + offsetX, rect.Y, rect.Width + rect.X - (int)point.X - offsetX, rect.Height);
                            if (point.X + offsetX > rect.Right)
                                rect = new Rectangle(rect.X, rect.Y, rect.Width + (int)point.X - (rect.X - offsetX), rect.Height);
                            if (point.Y + offsetY < rect.Top)
                                rect = new Rectangle(rect.X, (int)point.Y + offsetY, rect.Width, rect.Height + rect.Y - (int)point.Y - offsetY);
                            if (point.Y + offsetY > rect.Bottom)
                                rect = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height + (int)point.Y - (rect.Y - offsetY));
                        }
                        using (var g = pictureBox1.CreateGraphics())
                        {
                            using (Pen pen = new Pen(Color.Green, 5))
                            {
                                g.DrawRectangle(pen, rect);

                                pen.Color = Color.Yellow;
                                pen.DashPattern = new float[] { 5, 5 };
                                g.DrawRectangle(pen, rect);
                            }
                            g.DrawString(result.ToString(), new Font("Tahoma", 16f), Brushes.Blue, new System.Drawing.Point(rect.X - 60, rect.Y - 50));
                        }
                    }

                    // Console.WriteLine(decoded);
                    if (decoded != "")
                    {

                        //get the checked radio button 
                        //var checkedRadio = panel4.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Name;

                        //now we check if the decoded string matches the student LRN
                        if (studentData.Result != null && studentData.Result.Any(student => student.lrn == decoded))
                        {
                            var student = studentData.Result.Where(s => s.lrn == decoded).FirstOrDefault();

                            //now we update the json file and log the attendance 
                            Attendance attendance = new Attendance
                            {
                                lrn = student?.lrn,
                                currentdate = DateTime.Now.ToString("yyyy-MM-dd"),
                                time = DateTime.Now.ToString("HH:mm:ss"),
                                student_id = student?.student_id,
                                //type = checkedRadio.ToString() 
                            };
                            //check if the student has tapped to in under 10 minutes
                            var existingAttendance = attendances.FirstOrDefault(a => a.student_id == attendance.student_id &&
                                             (DateTime.Now - DateTime.Parse(a.time)).TotalMinutes <= 10);
                            if (existingAttendance != null)
                            {
                                // The attendance record already exists
                                //clear the image and show the x image
                                label7.Text = "Already recorded";
                                pictureBox3.Image = Properties.Resources._734189_middle;
                                pictureBox4.Image = Properties.Resources.warning_5632161;
                                //add the student data to listBox1
                                string listBoxMessage = string.Format("{0} not recorded", student?.lrn);
                                listBox1.Items.Add(listBoxMessage);
                            }
                            else
                            {
                                //if there is a match, show the profile picture and show the OK image
                                pictureBox3.ImageLocation = string.Format("{0}/{1}", schoolURL, student?.profile_photo);
                                pictureBox4.Image = Properties.Resources.ok;
                                label7.Text = (decoded) + " recorded";

                                // Save the new attendance record to the list
                                attendances.Add(attendance);
                                WriteToJSON(attendance);
                                //add the student data to listBox1
                                string listBoxMessage = string.Format("{0} recorded sucessfully", student?.lrn);
                                listBox1.Items.Add(listBoxMessage);
                            }
                        }
                        else
                        {
                            //clear the image and show the x image
                            label7.Text = "LRN not found";
                            pictureBox3.Image = Properties.Resources._734189_middle;
                            pictureBox4.Image = Properties.Resources.x;

                            string listBoxMessage = "LRN not found";
                            listBox1.Items.Add(listBoxMessage);
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

                //cancellationToken = new CancellationTokenSource();
                //var sourcetoken = cancellationToken.Token;
                //onStartScan(sourcetoken);

            }
            else if (button1.Text.ToLower() == "stop camera")
            {
                //cancellationToken.Cancel();
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
            //open Setup form after click
            Setup setup = new Setup();
            setup.Show();
             
            //// Show a popup to ask for the website URL and authentication code
            //string userInput = Interaction.InputBox("Please enter the School Application URL:", "Enter School URL", "");
            //string authCodeInput = Interaction.InputBox("Please enter the Authentication Code:", "Enter Authentication Code", "");

            //// Save the website URL and auth code in app.config
            //SaveAppSettings("SchoolWebsiteURL", userInput);
            //SaveAppSettings("AuthCode", authCodeInput);

            //Application.Restart();
            //Environment.Exit(0);
        }
    }
}