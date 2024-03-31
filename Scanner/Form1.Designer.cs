namespace Scanner
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pictureBox1 = new PictureBox();
            button1 = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            comboBox1 = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            panel1 = new Panel();
            label5 = new Label();
            pictureBox2 = new PictureBox();
            panel2 = new Panel();
            label9 = new Label();
            pictureBox3 = new PictureBox();
            panel3 = new Panel();
            label6 = new Label();
            label7 = new Label();
            pictureBox4 = new PictureBox();
            amlogin = new RadioButton();
            amlogout = new RadioButton();
            panel4 = new Panel();
            pmlogin = new RadioButton();
            pmlogout = new RadioButton();
            panel5 = new Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Image = Properties.Resources.camera_placeholder;
            pictureBox1.InitialImage = null;
            pictureBox1.Location = new Point(21, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1131, 1066);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.BackColor = Color.Black;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold, GraphicsUnit.Point);
            button1.ForeColor = Color.White;
            button1.Location = new Point(10, 215);
            button1.Name = "button1";
            button1.Size = new Size(479, 111);
            button1.TabIndex = 1;
            button1.Text = "START CAMERA";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Tick += timer1_Tick_1;
            // 
            // comboBox1
            // 
            comboBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            comboBox1.DropDownHeight = 300;
            comboBox1.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Regular, GraphicsUnit.Point);
            comboBox1.FormattingEnabled = true;
            comboBox1.IntegralHeight = false;
            comboBox1.Location = new Point(206, 145);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(283, 48);
            comboBox1.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.Gray;
            label1.Location = new Point(9, 10);
            label1.Name = "label1";
            label1.Size = new Size(123, 40);
            label1.TabIndex = 5;
            label1.Text = "DATE:";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.Gray;
            label2.Location = new Point(11, 88);
            label2.Name = "label2";
            label2.Size = new Size(112, 40);
            label2.TabIndex = 6;
            label2.Text = "TIME:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(127, 10);
            label3.Name = "label3";
            label3.Size = new Size(164, 40);
            label3.TabIndex = 7;
            label3.Text = "#######";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(129, 88);
            label4.Name = "label4";
            label4.Size = new Size(164, 40);
            label4.TabIndex = 8;
            label4.Text = "#######";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(label5);
            panel1.Location = new Point(332, 1084);
            panel1.Name = "panel1";
            panel1.Size = new Size(820, 315);
            panel1.TabIndex = 9;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label5.Font = new Font("Microsoft Sans Serif", 28F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ForeColor = Color.Black;
            label5.Location = new Point(47, 33);
            label5.Name = "label5";
            label5.Size = new Size(738, 142);
            label5.TabIndex = 0;
            label5.Text = "Welcome to \r\n{SchoolName}!";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            label5.Click += label5_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            pictureBox2.BorderStyle = BorderStyle.FixedSingle;
            pictureBox2.Image = Properties.Resources._8ed868e675055b039502da400e3cc5c1;
            pictureBox2.Location = new Point(21, 1084);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(305, 315);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 10;
            pictureBox2.TabStop = false;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(label1);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label4);
            panel2.Location = new Point(1158, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(503, 147);
            panel2.TabIndex = 11;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label9.ForeColor = Color.Gray;
            label9.Location = new Point(18, 150);
            label9.Name = "label9";
            label9.Size = new Size(181, 40);
            label9.TabIndex = 9;
            label9.Text = "CAMERA:";
            label9.Click += label9_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox3.BackColor = Color.WhiteSmoke;
            pictureBox3.ErrorImage = Properties.Resources.user_profile;
            pictureBox3.Image = Properties.Resources.user_profile;
            pictureBox3.Location = new Point(61, 89);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(387, 357);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 12;
            pictureBox3.TabStop = false;
            pictureBox3.Click += pictureBox3_Click;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(pictureBox3);
            panel3.Controls.Add(label6);
            panel3.Controls.Add(label7);
            panel3.Location = new Point(1158, 915);
            panel3.Name = "panel3";
            panel3.Size = new Size(503, 484);
            panel3.TabIndex = 13;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label6.ForeColor = Color.Gray;
            label6.Location = new Point(8, 33);
            label6.Name = "label6";
            label6.Size = new Size(99, 40);
            label6.TabIndex = 9;
            label6.Text = "LRN:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label7.ForeColor = Color.Black;
            label7.Location = new Point(115, 33);
            label7.Name = "label7";
            label7.Size = new Size(164, 40);
            label7.TabIndex = 10;
            label7.Text = "#######";
            // 
            // pictureBox4
            // 
            pictureBox4.Image = Properties.Resources.ok;
            pictureBox4.InitialImage = null;
            pictureBox4.Location = new Point(155, 102);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(181, 180);
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.TabIndex = 11;
            pictureBox4.TabStop = false;
            // 
            // amlogin
            // 
            amlogin.AutoSize = true;
            amlogin.Checked = true;
            amlogin.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold, GraphicsUnit.Point);
            amlogin.Location = new Point(61, 25);
            amlogin.Name = "amlogin";
            amlogin.Size = new Size(146, 44);
            amlogin.TabIndex = 14;
            amlogin.TabStop = true;
            amlogin.Text = "AM IN";
            amlogin.UseVisualStyleBackColor = true;
            // 
            // amlogout
            // 
            amlogout.AutoSize = true;
            amlogout.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold, GraphicsUnit.Point);
            amlogout.Location = new Point(244, 25);
            amlogout.Name = "amlogout";
            amlogout.Size = new Size(188, 44);
            amlogout.TabIndex = 15;
            amlogout.Text = "AM OUT";
            amlogout.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            panel4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(label9);
            panel4.Controls.Add(pmlogin);
            panel4.Controls.Add(pmlogout);
            panel4.Controls.Add(amlogin);
            panel4.Controls.Add(amlogout);
            panel4.Controls.Add(comboBox1);
            panel4.Controls.Add(button1);
            panel4.Location = new Point(1158, 165);
            panel4.Name = "panel4";
            panel4.Size = new Size(503, 338);
            panel4.TabIndex = 16;
            // 
            // pmlogin
            // 
            pmlogin.AutoSize = true;
            pmlogin.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold, GraphicsUnit.Point);
            pmlogin.Location = new Point(61, 75);
            pmlogin.Name = "pmlogin";
            pmlogin.Size = new Size(146, 44);
            pmlogin.TabIndex = 16;
            pmlogin.Text = "PM IN";
            pmlogin.UseVisualStyleBackColor = true;
            // 
            // pmlogout
            // 
            pmlogout.AutoSize = true;
            pmlogout.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold, GraphicsUnit.Point);
            pmlogout.Location = new Point(244, 75);
            pmlogout.Name = "pmlogout";
            pmlogout.Size = new Size(188, 44);
            pmlogout.TabIndex = 17;
            pmlogout.Text = "PM OUT";
            pmlogout.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            panel5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel5.BorderStyle = BorderStyle.FixedSingle;
            panel5.Controls.Add(pictureBox4);
            panel5.Location = new Point(1158, 509);
            panel5.Name = "panel5";
            panel5.Size = new Size(503, 400);
            panel5.TabIndex = 17;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.White;
            ClientSize = new Size(1673, 1411);
            Controls.Add(panel5);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(pictureBox2);
            Controls.Add(panel1);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel5.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private ComboBox comboBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Panel panel1;
        private Label label5;
        private PictureBox pictureBox2;
        public Button button1;
        private Panel panel2;
        private PictureBox pictureBox3;
        private Panel panel3;
        private PictureBox pictureBox4;
        private Label label6;
        private Label label7;
        private Label label9;
        private RadioButton amin;
        private RadioButton amout;
        private Panel panel4;
        private RadioButton pmin;
        private RadioButton radioButton2;
        private RadioButton amlogin;
        private RadioButton amlogout;
        private RadioButton pmlogin;
        private RadioButton pmlogout;
        private Panel panel5;
    }
}