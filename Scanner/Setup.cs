using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Scanner
{
    public partial class Setup : Form
    {
        public Setup()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string schoolURL = textBox1.Text;
            string authCode = textBox2.Text;
            //// Save the website URL and auth code in app.config
            SaveAppSettings("SchoolWebsiteURL", schoolURL);
            SaveAppSettings("AuthCode", authCode);

            if (!IsValidAuthCode(schoolURL, authCode))
            {
                MessageBox.Show("Setup is not valid. Please try again.");
                return;
            }
            else
            {
                MessageBox.Show("Setup complete. You can now start recording attendance.");
                this.Close();
                Application.Restart();
                Environment.Exit(0);
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
    }
}
