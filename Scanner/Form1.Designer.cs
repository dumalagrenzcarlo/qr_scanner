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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
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
            listBox1 = new ListBox();
            pictureBox4 = new PictureBox();
            label6 = new Label();
            label7 = new Label();
            panel4 = new Panel();
            menuStrip1 = new MenuStrip();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            last3DaysToolStripMenuItem = new ToolStripMenuItem();
            last7DaysToolStripMenuItem = new ToolStripMenuItem();
            last14DaysToolStripMenuItem = new ToolStripMenuItem();
            last30DaysToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            panel4.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Image = Properties.Resources.camera_placeholder;
            pictureBox1.InitialImage = null;
            pictureBox1.Location = new Point(11, 37);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(866, 360);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top;
            button1.BackColor = Color.Black;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Point);
            button1.ForeColor = Color.White;
            button1.Location = new Point(7, 68);
            button1.Name = "button1";
            button1.Size = new Size(426, 45);
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
            comboBox1.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point);
            comboBox1.FormattingEnabled = true;
            comboBox1.IntegralHeight = false;
            comboBox1.Location = new Point(163, 12);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(270, 40);
            comboBox1.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.Gray;
            label1.Location = new Point(9, 10);
            label1.Name = "label1";
            label1.Size = new Size(97, 32);
            label1.TabIndex = 5;
            label1.Text = "DATE:";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.Gray;
            label2.Location = new Point(9, 65);
            label2.Name = "label2";
            label2.Size = new Size(88, 32);
            label2.TabIndex = 6;
            label2.Text = "TIME:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(127, 10);
            label3.Name = "label3";
            label3.Size = new Size(126, 32);
            label3.TabIndex = 7;
            label3.Text = "#######";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(127, 65);
            label4.Name = "label4";
            label4.Size = new Size(126, 32);
            label4.TabIndex = 8;
            label4.Text = "#######";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(label5);
            panel1.Location = new Point(306, 403);
            panel1.Name = "panel1";
            panel1.Size = new Size(573, 324);
            panel1.TabIndex = 9;
            // 
            // label5
            // 
            label5.Dock = DockStyle.Fill;
            label5.Font = new Font("Microsoft Sans Serif", 22F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ForeColor = Color.Black;
            label5.Location = new Point(0, 0);
            label5.Name = "label5";
            label5.Size = new Size(571, 322);
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
            pictureBox2.Location = new Point(13, 403);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(286, 324);
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
            panel2.Location = new Point(883, 37);
            panel2.Name = "panel2";
            panel2.Size = new Size(449, 109);
            panel2.TabIndex = 11;
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label9.AutoSize = true;
            label9.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label9.ForeColor = Color.Gray;
            label9.Location = new Point(16, 17);
            label9.Name = "label9";
            label9.Size = new Size(142, 32);
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
            pictureBox3.Location = new Point(11, 78);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(214, 197);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 12;
            pictureBox3.TabStop = false;
            pictureBox3.Click += pictureBox3_Click;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(listBox1);
            panel3.Controls.Add(pictureBox4);
            panel3.Controls.Add(pictureBox3);
            panel3.Controls.Add(label6);
            panel3.Controls.Add(label7);
            panel3.Location = new Point(883, 285);
            panel3.Name = "panel3";
            panel3.Size = new Size(449, 442);
            panel3.TabIndex = 13;
            panel3.Paint += panel3_Paint;
            // 
            // listBox1
            // 
            listBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listBox1.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            listBox1.ItemHeight = 29;
            listBox1.Location = new Point(11, 281);
            listBox1.Name = "listBox1";
            listBox1.RightToLeft = RightToLeft.No;
            listBox1.Size = new Size(422, 149);
            listBox1.TabIndex = 13;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = Properties.Resources.ok;
            pictureBox4.InitialImage = null;
            pictureBox4.Location = new Point(231, 78);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(202, 197);
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.TabIndex = 11;
            pictureBox4.TabStop = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label6.ForeColor = Color.Gray;
            label6.Location = new Point(9, 33);
            label6.Name = "label6";
            label6.Size = new Size(78, 32);
            label6.TabIndex = 9;
            label6.Text = "LRN:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label7.ForeColor = Color.Black;
            label7.Location = new Point(114, 33);
            label7.Name = "label7";
            label7.Size = new Size(126, 32);
            label7.TabIndex = 10;
            label7.Text = "#######";
            // 
            // panel4
            // 
            panel4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(label9);
            panel4.Controls.Add(comboBox1);
            panel4.Controls.Add(button1);
            panel4.Location = new Point(883, 150);
            panel4.Name = "panel4";
            panel4.Size = new Size(449, 129);
            panel4.TabIndex = 16;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { settingsToolStripMenuItem, toolStripMenuItem1 });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1344, 33);
            menuStrip1.TabIndex = 17;
            menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(92, 29);
            settingsToolStripMenuItem.Text = "Settings";
            settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { last3DaysToolStripMenuItem, last7DaysToolStripMenuItem, last14DaysToolStripMenuItem, last30DaysToolStripMenuItem });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(212, 29);
            toolStripMenuItem1.Text = "Synchronize to Website";
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // last3DaysToolStripMenuItem
            // 
            last3DaysToolStripMenuItem.Name = "last3DaysToolStripMenuItem";
            last3DaysToolStripMenuItem.Size = new Size(212, 34);
            last3DaysToolStripMenuItem.Text = "Last 3 days";
            last3DaysToolStripMenuItem.Click += last3DaysToolStripMenuItem_Click;
            // 
            // last7DaysToolStripMenuItem
            // 
            last7DaysToolStripMenuItem.Name = "last7DaysToolStripMenuItem";
            last7DaysToolStripMenuItem.Size = new Size(212, 34);
            last7DaysToolStripMenuItem.Text = "Last 7 days";
            last7DaysToolStripMenuItem.Click += last7DaysToolStripMenuItem_Click;
            // 
            // last14DaysToolStripMenuItem
            // 
            last14DaysToolStripMenuItem.Name = "last14DaysToolStripMenuItem";
            last14DaysToolStripMenuItem.Size = new Size(212, 34);
            last14DaysToolStripMenuItem.Text = "Last 14 days";
            last14DaysToolStripMenuItem.Click += last14DaysToolStripMenuItem_Click;
            // 
            // last30DaysToolStripMenuItem
            // 
            last30DaysToolStripMenuItem.Name = "last30DaysToolStripMenuItem";
            last30DaysToolStripMenuItem.Size = new Size(212, 34);
            last30DaysToolStripMenuItem.Text = "Last 30 days";
            last30DaysToolStripMenuItem.Click += last30DaysToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1344, 732);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(pictureBox2);
            Controls.Add(panel1);
            Controls.Add(pictureBox1);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
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
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem last3DaysToolStripMenuItem;
        private ToolStripMenuItem last7DaysToolStripMenuItem;
        private ToolStripMenuItem last14DaysToolStripMenuItem;
        private ToolStripMenuItem last30DaysToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ListBox listBox1;
    }
}