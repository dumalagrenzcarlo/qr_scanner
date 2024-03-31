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

namespace Scanner
{
    public partial class Form1 : Form
    {
        private FilterInfoCollection CaptureDevice;
        private VideoCaptureDevice FinalFrame;
        Task<List<Students>?> studentData;
        string schoolLogoPath;
        string? scannerName = System.Configuration.ConfigurationManager.AppSettings["SchoolScannerName"];
        string? schoolName = System.Configuration.ConfigurationManager.AppSettings["SchoolName"];
        string? schoolLogoURL = System.Configuration.ConfigurationManager.AppSettings["SchoolLogoURL"];
        string? schoolURL = System.Configuration.ConfigurationManager.AppSettings["SchoolWebsiteURL"];
        string? authCode = System.Configuration.ConfigurationManager.AppSettings["AuthCode"];
        public Form1()
        {

            InitializeComponent();
            //change the Title
            this.Text = scannerName;

            label5.Text = schoolName;
            label5.TextAlign = ContentAlignment.MiddleCenter;
            pictureBox2.ImageLocation = schoolLogoURL;


            //get the list of students in the website first and add to memory 
            studentData = GetStudentsData(schoolURL + "/autosync?token=" + authCode);
            SyncronizeDataToWeb();
        }

        /// <summary>
        /// Sync the attendance data to the webserver
        /// </summary>
        public void SyncronizeDataToWeb()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var dailyRecordsPath = GetAttendanceDataPath();
                    var jsonData = LoadJson(dailyRecordsPath);

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
                    else
                    {
                        MessageBox.Show("Synchronization to webserver completed");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //check path
        public string GetAttendanceDataPath()
        { 
            //get date today
            var exePath = System.AppDomain.CurrentDomain.BaseDirectory;
            var dateToday = string.Format("{0}.json", DateTime.Now.ToString("dd-MMM-yyyy"));

            var dailyRecordsPath = Path.Combine(exePath, "DailyRecords", dateToday);

            if (!Directory.Exists(Path.Combine(exePath, "DailyRecords")))
            {
                //create the base path
                Directory.CreateDirectory(Path.Combine(exePath, "DailyRecords"));
            }

            return dailyRecordsPath;

        }

        public void WriteToJSON(Attendance? attendances)
        {
            //if null do nothing
            if (attendances is null)
                return;

            //get date today
            var dailyRecordsPath = GetAttendanceDataPath();
             

            //check if the path exists
            if (File.Exists(dailyRecordsPath))
            {
                //update the file
                var jsonData = LoadJson(dailyRecordsPath);
                //do 
                if (jsonData is null)
                    return;

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
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    var jsonString = await response.Content.ReadAsStringAsync();
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
            SyncronizeDataToWeb();
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
    }
}