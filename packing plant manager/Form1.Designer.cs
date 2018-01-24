namespace packing_plant_manager
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Wymagana metoda obsługi projektanta — nie należy modyfikować 
        /// zawartość tej metody z edytorem kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.loggingBox = new System.Windows.Forms.ListBox();
            this.server = new System.Windows.Forms.TextBox();
            this.login = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.saveLoginData = new System.Windows.Forms.Button();
            this.packingStation = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.printer = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnUnlock = new System.Windows.Forms.Button();
            this.sumatra_checkbox = new System.Windows.Forms.CheckBox();
            this.connection = new System.ComponentModel.BackgroundWorker();
            this.label7 = new System.Windows.Forms.Label();
            this.port_number = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.serwerIP = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(308, 161);
            this.btnStart.Margin = new System.Windows.Forms.Padding(6);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(275, 42);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(308, 214);
            this.btnStop.Margin = new System.Windows.Forms.Padding(6);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(275, 42);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::packing_plant_manager.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(22, 22);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(561, 127);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // loggingBox
            // 
            this.loggingBox.FormattingEnabled = true;
            this.loggingBox.ItemHeight = 24;
            this.loggingBox.Location = new System.Drawing.Point(24, 565);
            this.loggingBox.Margin = new System.Windows.Forms.Padding(6);
            this.loggingBox.Name = "loggingBox";
            this.loggingBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.loggingBox.Size = new System.Drawing.Size(558, 412);
            this.loggingBox.TabIndex = 4;
            // 
            // server
            // 
            this.server.Location = new System.Drawing.Point(114, 161);
            this.server.Margin = new System.Windows.Forms.Padding(6);
            this.server.Name = "server";
            this.server.Size = new System.Drawing.Size(180, 29);
            this.server.TabIndex = 5;
            // 
            // login
            // 
            this.login.Location = new System.Drawing.Point(114, 209);
            this.login.Margin = new System.Windows.Forms.Padding(6);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(180, 29);
            this.login.TabIndex = 6;
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(114, 257);
            this.password.Margin = new System.Windows.Forms.Padding(6);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(180, 29);
            this.password.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 530);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 25);
            this.label1.TabIndex = 8;
            this.label1.Text = "Logi:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 174);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 25);
            this.label2.TabIndex = 9;
            this.label2.Text = "Serwer:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 222);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 25);
            this.label3.TabIndex = 10;
            this.label3.Text = "Login:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 270);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 25);
            this.label4.TabIndex = 11;
            this.label4.Text = "Hasło:";
            // 
            // saveLoginData
            // 
            this.saveLoginData.Location = new System.Drawing.Point(308, 462);
            this.saveLoginData.Margin = new System.Windows.Forms.Padding(6);
            this.saveLoginData.Name = "saveLoginData";
            this.saveLoginData.Size = new System.Drawing.Size(273, 42);
            this.saveLoginData.TabIndex = 12;
            this.saveLoginData.Text = "Zapisz dane logowania";
            this.saveLoginData.UseVisualStyleBackColor = true;
            this.saveLoginData.Click += new System.EventHandler(this.saveLoginData_Click);
            // 
            // packingStation
            // 
            this.packingStation.FormattingEnabled = true;
            this.packingStation.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.packingStation.Location = new System.Drawing.Point(114, 305);
            this.packingStation.Margin = new System.Windows.Forms.Padding(6);
            this.packingStation.Name = "packingStation";
            this.packingStation.Size = new System.Drawing.Size(180, 32);
            this.packingStation.TabIndex = 13;
            this.packingStation.TextChanged += new System.EventHandler(this.packingStation_TextChanged);
            this.packingStation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.packingStation_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 319);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 25);
            this.label5.TabIndex = 14;
            this.label5.Text = "Posada:";
            // 
            // printer
            // 
            this.printer.Location = new System.Drawing.Point(114, 356);
            this.printer.Margin = new System.Windows.Forms.Padding(6);
            this.printer.Name = "printer";
            this.printer.Size = new System.Drawing.Size(180, 29);
            this.printer.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 360);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 25);
            this.label6.TabIndex = 16;
            this.label6.Text = "Zebra:";
            // 
            // btnUnlock
            // 
            this.btnUnlock.Location = new System.Drawing.Point(308, 412);
            this.btnUnlock.Margin = new System.Windows.Forms.Padding(6);
            this.btnUnlock.Name = "btnUnlock";
            this.btnUnlock.Size = new System.Drawing.Size(273, 42);
            this.btnUnlock.TabIndex = 19;
            this.btnUnlock.Text = "Zablokuj";
            this.btnUnlock.UseVisualStyleBackColor = true;
            this.btnUnlock.Click += new System.EventHandler(this.btnUnlock_Click);
            // 
            // sumatra_checkbox
            // 
            this.sumatra_checkbox.AutoSize = true;
            this.sumatra_checkbox.Location = new System.Drawing.Point(114, 395);
            this.sumatra_checkbox.Margin = new System.Windows.Forms.Padding(4);
            this.sumatra_checkbox.Name = "sumatra_checkbox";
            this.sumatra_checkbox.Size = new System.Drawing.Size(151, 29);
            this.sumatra_checkbox.TabIndex = 20;
            this.sumatra_checkbox.Text = "SumatraPDF";
            this.sumatra_checkbox.UseVisualStyleBackColor = true;
            // 
            // connection
            // 
            this.connection.WorkerSupportsCancellation = true;
            this.connection.DoWork += new System.ComponentModel.DoWorkEventHandler(this.connection_DoWork);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 438);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 25);
            this.label7.TabIndex = 21;
            this.label7.Text = "Port:";
            // 
            // port_number
            // 
            this.port_number.Location = new System.Drawing.Point(114, 434);
            this.port_number.Margin = new System.Windows.Forms.Padding(6);
            this.port_number.Name = "port_number";
            this.port_number.Size = new System.Drawing.Size(180, 29);
            this.port_number.TabIndex = 22;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 484);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 25);
            this.label8.TabIndex = 23;
            this.label8.Text = "Serwer:";
            // 
            // serwerIP
            // 
            this.serwerIP.Location = new System.Drawing.Point(116, 481);
            this.serwerIP.Margin = new System.Windows.Forms.Padding(6);
            this.serwerIP.Name = "serwerIP";
            this.serwerIP.Size = new System.Drawing.Size(180, 29);
            this.serwerIP.TabIndex = 24;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(605, 991);
            this.Controls.Add(this.serwerIP);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.port_number);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.sumatra_checkbox);
            this.Controls.Add(this.btnUnlock);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.printer);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.packingStation);
            this.Controls.Add(this.saveLoginData);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.password);
            this.Controls.Add(this.login);
            this.Controls.Add(this.server);
            this.Controls.Add(this.loggingBox);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Pakowalnia";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListBox loggingBox;
        private System.Windows.Forms.TextBox server;
        private System.Windows.Forms.TextBox login;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button saveLoginData;
        private System.Windows.Forms.ComboBox packingStation;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox printer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnUnlock;
        private System.Windows.Forms.CheckBox sumatra_checkbox;
        private System.ComponentModel.BackgroundWorker connection;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox port_number;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox serwerIP;
    }
}

