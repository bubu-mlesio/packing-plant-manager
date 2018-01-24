namespace ServerToUpdateData
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.server = new System.Windows.Forms.TextBox();
            this.user = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.sumatrapdf = new System.Windows.Forms.CheckBox();
            this.send_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.logbox = new System.Windows.Forms.ListBox();
            this.btn_stop = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.port_number = new System.Windows.Forms.TextBox();
            this.save_checkbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // server
            // 
            this.server.Location = new System.Drawing.Point(17, 189);
            this.server.Multiline = true;
            this.server.Name = "server";
            this.server.Size = new System.Drawing.Size(151, 29);
            this.server.TabIndex = 0;
            // 
            // user
            // 
            this.user.Location = new System.Drawing.Point(17, 266);
            this.user.Multiline = true;
            this.user.Name = "user";
            this.user.Size = new System.Drawing.Size(151, 29);
            this.user.TabIndex = 1;
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(17, 337);
            this.password.Multiline = true;
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(151, 29);
            this.password.TabIndex = 2;
            // 
            // sumatrapdf
            // 
            this.sumatrapdf.AutoSize = true;
            this.sumatrapdf.Checked = true;
            this.sumatrapdf.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sumatrapdf.Location = new System.Drawing.Point(17, 122);
            this.sumatrapdf.Name = "sumatrapdf";
            this.sumatrapdf.Size = new System.Drawing.Size(151, 29);
            this.sumatrapdf.TabIndex = 3;
            this.sumatrapdf.Text = "SumatraPDF";
            this.sumatrapdf.UseVisualStyleBackColor = true;
            // 
            // send_btn
            // 
            this.send_btn.Location = new System.Drawing.Point(187, 12);
            this.send_btn.Name = "send_btn";
            this.send_btn.Size = new System.Drawing.Size(160, 92);
            this.send_btn.TabIndex = 4;
            this.send_btn.Text = "Nasłuchuj";
            this.send_btn.UseVisualStyleBackColor = true;
            this.send_btn.Click += new System.EventHandler(this.send_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Serwer:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 238);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "Użytkownik:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 309);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "Hasło:";
            // 
            // logbox
            // 
            this.logbox.FormattingEnabled = true;
            this.logbox.ItemHeight = 24;
            this.logbox.Location = new System.Drawing.Point(187, 122);
            this.logbox.Name = "logbox";
            this.logbox.Size = new System.Drawing.Size(343, 244);
            this.logbox.TabIndex = 8;
            // 
            // btn_stop
            // 
            this.btn_stop.Location = new System.Drawing.Point(370, 12);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(160, 92);
            this.btn_stop.TabIndex = 9;
            this.btn_stop.Text = "Stop";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 25);
            this.label4.TabIndex = 10;
            this.label4.Text = "Port:";
            // 
            // port_number
            // 
            this.port_number.Location = new System.Drawing.Point(13, 41);
            this.port_number.Name = "port_number";
            this.port_number.Size = new System.Drawing.Size(100, 29);
            this.port_number.TabIndex = 11;
            this.port_number.Text = "2202";
            // 
            // save_checkbox
            // 
            this.save_checkbox.AutoSize = true;
            this.save_checkbox.Location = new System.Drawing.Point(13, 87);
            this.save_checkbox.Name = "save_checkbox";
            this.save_checkbox.Size = new System.Drawing.Size(118, 29);
            this.save_checkbox.TabIndex = 12;
            this.save_checkbox.Text = "Zapisać?";
            this.save_checkbox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 392);
            this.Controls.Add(this.save_checkbox);
            this.Controls.Add(this.port_number);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_stop);
            this.Controls.Add(this.logbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.send_btn);
            this.Controls.Add(this.sumatrapdf);
            this.Controls.Add(this.password);
            this.Controls.Add(this.user);
            this.Controls.Add(this.server);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox server;
        private System.Windows.Forms.TextBox user;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.CheckBox sumatrapdf;
        private System.Windows.Forms.Button send_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox logbox;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox port_number;
        private System.Windows.Forms.CheckBox save_checkbox;
    }
}

