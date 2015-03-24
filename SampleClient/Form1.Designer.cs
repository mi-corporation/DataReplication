namespace Mi_Co_DataSync_Example_App
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
            this.button1 = new System.Windows.Forms.Button();
            this.SearchText = new System.Windows.Forms.TextBox();
            this.Search = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.personName = new System.Windows.Forms.Label();
            this.personAddress = new System.Windows.Forms.Label();
            this.personPhone = new System.Windows.Forms.Label();
            this.personCityStateZip = new System.Windows.Forms.Label();
            this.personAddress2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.RSResourceName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.RSCompanyName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.RSBaseURL = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.MEASPassword = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.labelMEASUsername = new System.Windows.Forms.Label();
            this.MEASUsername = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.MEASCustomerName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.MEASAuthServiceURL = new System.Windows.Forms.TextBox();
            this.messages = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(43, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Register";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SearchText
            // 
            this.SearchText.Location = new System.Drawing.Point(6, 41);
            this.SearchText.Name = "SearchText";
            this.SearchText.Size = new System.Drawing.Size(257, 20);
            this.SearchText.TabIndex = 31;
            // 
            // Search
            // 
            this.Search.Location = new System.Drawing.Point(286, 22);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(141, 39);
            this.Search.TabIndex = 6;
            this.Search.Text = "Search";
            this.Search.UseVisualStyleBackColor = true;
            this.Search.Click += new System.EventHandler(this.Search_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Filter Last Name Search";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(9, 67);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(418, 229);
            this.dataGridView1.TabIndex = 9;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // personName
            // 
            this.personName.AutoSize = true;
            this.personName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.personName.Location = new System.Drawing.Point(18, 326);
            this.personName.Name = "personName";
            this.personName.Size = new System.Drawing.Size(0, 25);
            this.personName.TabIndex = 10;
            // 
            // personAddress
            // 
            this.personAddress.AutoSize = true;
            this.personAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.personAddress.Location = new System.Drawing.Point(18, 377);
            this.personAddress.Name = "personAddress";
            this.personAddress.Size = new System.Drawing.Size(0, 25);
            this.personAddress.TabIndex = 11;
            // 
            // personPhone
            // 
            this.personPhone.AutoSize = true;
            this.personPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.personPhone.Location = new System.Drawing.Point(18, 530);
            this.personPhone.Name = "personPhone";
            this.personPhone.Size = new System.Drawing.Size(0, 25);
            this.personPhone.TabIndex = 12;
            // 
            // personCityStateZip
            // 
            this.personCityStateZip.AutoSize = true;
            this.personCityStateZip.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.personCityStateZip.Location = new System.Drawing.Point(18, 479);
            this.personCityStateZip.Name = "personCityStateZip";
            this.personCityStateZip.Size = new System.Drawing.Size(0, 25);
            this.personCityStateZip.TabIndex = 13;
            // 
            // personAddress2
            // 
            this.personAddress2.AutoSize = true;
            this.personAddress2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.personAddress2.Location = new System.Drawing.Point(18, 428);
            this.personAddress2.Name = "personAddress2";
            this.personAddress2.Size = new System.Drawing.Size(0, 25);
            this.personAddress2.TabIndex = 14;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.RSResourceName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.RSCompanyName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.RSBaseURL);
            this.groupBox1.Location = new System.Drawing.Point(12, 249);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(306, 150);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Replication Server";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Resource Name";
            // 
            // RSResourceName
            // 
            this.RSResourceName.Location = new System.Drawing.Point(9, 117);
            this.RSResourceName.Name = "RSResourceName";
            this.RSResourceName.Size = new System.Drawing.Size(291, 20);
            this.RSResourceName.TabIndex = 30;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Customer Name";
            // 
            // RSCompanyName
            // 
            this.RSCompanyName.Location = new System.Drawing.Point(9, 74);
            this.RSCompanyName.Name = "RSCompanyName";
            this.RSCompanyName.Size = new System.Drawing.Size(291, 20);
            this.RSCompanyName.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Replication Server Base URL";
            // 
            // RSBaseURL
            // 
            this.RSBaseURL.Location = new System.Drawing.Point(9, 32);
            this.RSBaseURL.Name = "RSBaseURL";
            this.RSBaseURL.Size = new System.Drawing.Size(291, 20);
            this.RSBaseURL.TabIndex = 28;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.SearchText);
            this.groupBox2.Controls.Add(this.personAddress2);
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Controls.Add(this.personCityStateZip);
            this.groupBox2.Controls.Add(this.Search);
            this.groupBox2.Controls.Add(this.personPhone);
            this.groupBox2.Controls.Add(this.personName);
            this.groupBox2.Controls.Add(this.personAddress);
            this.groupBox2.Location = new System.Drawing.Point(324, 40);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(452, 584);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data Replication Client API - Search";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 299);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(195, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Person Details Lookup (select a person)";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Location = new System.Drawing.Point(12, 405);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(306, 107);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Data Replication Sync";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.MEASPassword);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.labelMEASUsername);
            this.groupBox4.Controls.Add(this.MEASUsername);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.MEASCustomerName);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.MEASAuthServiceURL);
            this.groupBox4.Location = new System.Drawing.Point(12, 40);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(306, 203);
            this.groupBox4.TabIndex = 23;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Mi-Enterprise Apps Server";
            // 
            // MEASPassword
            // 
            this.MEASPassword.Location = new System.Drawing.Point(9, 171);
            this.MEASPassword.Name = "MEASPassword";
            this.MEASPassword.PasswordChar = '*';
            this.MEASPassword.Size = new System.Drawing.Size(291, 20);
            this.MEASPassword.TabIndex = 27;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 155);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 27;
            this.label8.Text = "Password";
            // 
            // labelMEASUsername
            // 
            this.labelMEASUsername.AutoSize = true;
            this.labelMEASUsername.Location = new System.Drawing.Point(6, 106);
            this.labelMEASUsername.Name = "labelMEASUsername";
            this.labelMEASUsername.Size = new System.Drawing.Size(55, 13);
            this.labelMEASUsername.TabIndex = 25;
            this.labelMEASUsername.Text = "Username";
            // 
            // MEASUsername
            // 
            this.MEASUsername.Location = new System.Drawing.Point(9, 122);
            this.MEASUsername.Name = "MEASUsername";
            this.MEASUsername.Size = new System.Drawing.Size(291, 20);
            this.MEASUsername.TabIndex = 26;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Customer Name";
            // 
            // MEASCustomerName
            // 
            this.MEASCustomerName.Location = new System.Drawing.Point(9, 77);
            this.MEASCustomerName.Name = "MEASCustomerName";
            this.MEASCustomerName.Size = new System.Drawing.Size(291, 20);
            this.MEASCustomerName.TabIndex = 24;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Auth Service URL";
            // 
            // MEASAuthServiceURL
            // 
            this.MEASAuthServiceURL.Location = new System.Drawing.Point(9, 32);
            this.MEASAuthServiceURL.Name = "MEASAuthServiceURL";
            this.MEASAuthServiceURL.Size = new System.Drawing.Size(291, 20);
            this.MEASAuthServiceURL.TabIndex = 22;
            // 
            // messages
            // 
            this.messages.AutoSize = true;
            this.messages.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messages.ForeColor = System.Drawing.Color.Red;
            this.messages.Location = new System.Drawing.Point(18, 8);
            this.messages.Name = "messages";
            this.messages.Size = new System.Drawing.Size(0, 20);
            this.messages.TabIndex = 16;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(43, 49);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Get Data";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 643);
            this.Controls.Add(this.messages);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Data Replication Client - \"People Search\"";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox SearchText;
        private System.Windows.Forms.Button Search;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label personName;
        private System.Windows.Forms.Label personAddress;
        private System.Windows.Forms.Label personPhone;
        private System.Windows.Forms.Label personCityStateZip;
        private System.Windows.Forms.Label personAddress2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox RSResourceName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox RSCompanyName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox RSBaseURL;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox MEASAuthServiceURL;
        private System.Windows.Forms.Label labelMEASUsername;
        private System.Windows.Forms.TextBox MEASUsername;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox MEASCustomerName;
        private System.Windows.Forms.TextBox MEASPassword;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label messages;
        private System.Windows.Forms.Button button2;
    }
}

