namespace ADMINISTRATIVNI_Modul
{
    partial class UserEditor
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
            this.btn_UploadPicture = new System.Windows.Forms.Button();
            this.btn_CreateUser = new System.Windows.Forms.Button();
            this.pb_Picture = new System.Windows.Forms.PictureBox();
            this.lbl_Image = new System.Windows.Forms.Label();
            this.tb_Firstname = new System.Windows.Forms.TextBox();
            this.lbl_Technologies = new System.Windows.Forms.Label();
            this.lbl_FirstName = new System.Windows.Forms.Label();
            this.tb_Lastname = new System.Windows.Forms.TextBox();
            this.lbl_Lastname = new System.Windows.Forms.Label();
            this.tb_Email = new System.Windows.Forms.TextBox();
            this.lbl_Email = new System.Windows.Forms.Label();
            this.tb_Phone = new System.Windows.Forms.TextBox();
            this.lbl_Phone = new System.Windows.Forms.Label();
            this.tb_Username = new System.Windows.Forms.TextBox();
            this.lbl_Username = new System.Windows.Forms.Label();
            this.flp_Technologies = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_AddTech = new System.Windows.Forms.Button();
            this.cb_IsAdmin = new System.Windows.Forms.CheckBox();
            this.tb_password = new System.Windows.Forms.TextBox();
            this.lbl_Password = new System.Windows.Forms.Label();
            this.tb_PasswordVerify = new System.Windows.Forms.TextBox();
            this.lbl_Password1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Picture)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_UploadPicture
            // 
            this.btn_UploadPicture.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_UploadPicture.Location = new System.Drawing.Point(428, 174);
            this.btn_UploadPicture.Name = "btn_UploadPicture";
            this.btn_UploadPicture.Size = new System.Drawing.Size(148, 33);
            this.btn_UploadPicture.TabIndex = 42;
            this.btn_UploadPicture.Text = "Upload Picture";
            this.btn_UploadPicture.UseVisualStyleBackColor = true;
            this.btn_UploadPicture.Click += new System.EventHandler(this.btn_UploadPicture_Click);
            // 
            // btn_CreateUser
            // 
            this.btn_CreateUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_CreateUser.Location = new System.Drawing.Point(199, 360);
            this.btn_CreateUser.Name = "btn_CreateUser";
            this.btn_CreateUser.Size = new System.Drawing.Size(180, 33);
            this.btn_CreateUser.TabIndex = 41;
            this.btn_CreateUser.Text = "Create User";
            this.btn_CreateUser.UseVisualStyleBackColor = true;
            this.btn_CreateUser.Click += new System.EventHandler(this.btn_CreateUser_Click);
            // 
            // pb_Picture
            // 
            this.pb_Picture.Location = new System.Drawing.Point(428, 27);
            this.pb_Picture.Name = "pb_Picture";
            this.pb_Picture.Size = new System.Drawing.Size(148, 141);
            this.pb_Picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_Picture.TabIndex = 40;
            this.pb_Picture.TabStop = false;
            // 
            // lbl_Image
            // 
            this.lbl_Image.AutoSize = true;
            this.lbl_Image.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Image.Location = new System.Drawing.Point(425, 7);
            this.lbl_Image.Name = "lbl_Image";
            this.lbl_Image.Size = new System.Drawing.Size(56, 17);
            this.lbl_Image.TabIndex = 39;
            this.lbl_Image.Text = "Image:";
            // 
            // tb_Firstname
            // 
            this.tb_Firstname.Location = new System.Drawing.Point(110, 8);
            this.tb_Firstname.Name = "tb_Firstname";
            this.tb_Firstname.Size = new System.Drawing.Size(312, 22);
            this.tb_Firstname.TabIndex = 37;
            // 
            // lbl_Technologies
            // 
            this.lbl_Technologies.AutoSize = true;
            this.lbl_Technologies.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Technologies.Location = new System.Drawing.Point(9, 206);
            this.lbl_Technologies.Name = "lbl_Technologies";
            this.lbl_Technologies.Size = new System.Drawing.Size(110, 17);
            this.lbl_Technologies.TabIndex = 36;
            this.lbl_Technologies.Text = "Technologies:";
            // 
            // lbl_FirstName
            // 
            this.lbl_FirstName.AutoSize = true;
            this.lbl_FirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_FirstName.Location = new System.Drawing.Point(9, 11);
            this.lbl_FirstName.Name = "lbl_FirstName";
            this.lbl_FirstName.Size = new System.Drawing.Size(94, 17);
            this.lbl_FirstName.TabIndex = 35;
            this.lbl_FirstName.Text = "First name: ";
            // 
            // tb_Lastname
            // 
            this.tb_Lastname.Location = new System.Drawing.Point(110, 36);
            this.tb_Lastname.Name = "tb_Lastname";
            this.tb_Lastname.Size = new System.Drawing.Size(312, 22);
            this.tb_Lastname.TabIndex = 44;
            // 
            // lbl_Lastname
            // 
            this.lbl_Lastname.AutoSize = true;
            this.lbl_Lastname.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Lastname.Location = new System.Drawing.Point(9, 39);
            this.lbl_Lastname.Name = "lbl_Lastname";
            this.lbl_Lastname.Size = new System.Drawing.Size(93, 17);
            this.lbl_Lastname.TabIndex = 43;
            this.lbl_Lastname.Text = "Last name: ";
            // 
            // tb_Email
            // 
            this.tb_Email.Location = new System.Drawing.Point(110, 64);
            this.tb_Email.Name = "tb_Email";
            this.tb_Email.Size = new System.Drawing.Size(312, 22);
            this.tb_Email.TabIndex = 46;
            // 
            // lbl_Email
            // 
            this.lbl_Email.AutoSize = true;
            this.lbl_Email.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Email.Location = new System.Drawing.Point(9, 67);
            this.lbl_Email.Name = "lbl_Email";
            this.lbl_Email.Size = new System.Drawing.Size(57, 17);
            this.lbl_Email.TabIndex = 45;
            this.lbl_Email.Text = "Email: ";
            // 
            // tb_Phone
            // 
            this.tb_Phone.Location = new System.Drawing.Point(110, 92);
            this.tb_Phone.Name = "tb_Phone";
            this.tb_Phone.Size = new System.Drawing.Size(312, 22);
            this.tb_Phone.TabIndex = 48;
            // 
            // lbl_Phone
            // 
            this.lbl_Phone.AutoSize = true;
            this.lbl_Phone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Phone.Location = new System.Drawing.Point(9, 95);
            this.lbl_Phone.Name = "lbl_Phone";
            this.lbl_Phone.Size = new System.Drawing.Size(64, 17);
            this.lbl_Phone.TabIndex = 47;
            this.lbl_Phone.Text = "Phone: ";
            // 
            // tb_Username
            // 
            this.tb_Username.Location = new System.Drawing.Point(110, 120);
            this.tb_Username.Name = "tb_Username";
            this.tb_Username.Size = new System.Drawing.Size(312, 22);
            this.tb_Username.TabIndex = 50;
            // 
            // lbl_Username
            // 
            this.lbl_Username.AutoSize = true;
            this.lbl_Username.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Username.Location = new System.Drawing.Point(9, 123);
            this.lbl_Username.Name = "lbl_Username";
            this.lbl_Username.Size = new System.Drawing.Size(91, 17);
            this.lbl_Username.TabIndex = 49;
            this.lbl_Username.Text = "Username: ";
            // 
            // flp_Technologies
            // 
            this.flp_Technologies.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flp_Technologies.Location = new System.Drawing.Point(12, 234);
            this.flp_Technologies.Name = "flp_Technologies";
            this.flp_Technologies.Size = new System.Drawing.Size(410, 120);
            this.flp_Technologies.TabIndex = 51;
            // 
            // btn_AddTech
            // 
            this.btn_AddTech.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddTech.Location = new System.Drawing.Point(199, 200);
            this.btn_AddTech.Name = "btn_AddTech";
            this.btn_AddTech.Size = new System.Drawing.Size(180, 28);
            this.btn_AddTech.TabIndex = 52;
            this.btn_AddTech.Text = "Add Technology";
            this.btn_AddTech.UseVisualStyleBackColor = true;
            this.btn_AddTech.Click += new System.EventHandler(this.btn_AddTech_Click);
            // 
            // cb_IsAdmin
            // 
            this.cb_IsAdmin.AutoSize = true;
            this.cb_IsAdmin.Location = new System.Drawing.Point(451, 275);
            this.cb_IsAdmin.Name = "cb_IsAdmin";
            this.cb_IsAdmin.Size = new System.Drawing.Size(107, 20);
            this.cb_IsAdmin.TabIndex = 53;
            this.cb_IsAdmin.Text = "Administrator";
            this.cb_IsAdmin.UseVisualStyleBackColor = true;
            // 
            // tb_password
            // 
            this.tb_password.Location = new System.Drawing.Point(147, 148);
            this.tb_password.Name = "tb_password";
            this.tb_password.Size = new System.Drawing.Size(275, 22);
            this.tb_password.TabIndex = 55;
            this.tb_password.UseSystemPasswordChar = true;
            // 
            // lbl_Password
            // 
            this.lbl_Password.AutoSize = true;
            this.lbl_Password.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Password.Location = new System.Drawing.Point(9, 151);
            this.lbl_Password.Name = "lbl_Password";
            this.lbl_Password.Size = new System.Drawing.Size(132, 17);
            this.lbl_Password.TabIndex = 54;
            this.lbl_Password.Text = "Temp Password: ";
            // 
            // tb_PasswordVerify
            // 
            this.tb_PasswordVerify.Location = new System.Drawing.Point(147, 174);
            this.tb_PasswordVerify.Name = "tb_PasswordVerify";
            this.tb_PasswordVerify.Size = new System.Drawing.Size(275, 22);
            this.tb_PasswordVerify.TabIndex = 57;
            this.tb_PasswordVerify.UseSystemPasswordChar = true;
            // 
            // lbl_Password1
            // 
            this.lbl_Password1.AutoSize = true;
            this.lbl_Password1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Password1.Location = new System.Drawing.Point(9, 177);
            this.lbl_Password1.Name = "lbl_Password1";
            this.lbl_Password1.Size = new System.Drawing.Size(132, 17);
            this.lbl_Password1.TabIndex = 56;
            this.lbl_Password1.Text = "Temp Password: ";
            // 
            // UserEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 403);
            this.Controls.Add(this.tb_PasswordVerify);
            this.Controls.Add(this.lbl_Password1);
            this.Controls.Add(this.tb_password);
            this.Controls.Add(this.lbl_Password);
            this.Controls.Add(this.cb_IsAdmin);
            this.Controls.Add(this.btn_AddTech);
            this.Controls.Add(this.flp_Technologies);
            this.Controls.Add(this.tb_Username);
            this.Controls.Add(this.lbl_Username);
            this.Controls.Add(this.tb_Phone);
            this.Controls.Add(this.lbl_Phone);
            this.Controls.Add(this.tb_Email);
            this.Controls.Add(this.lbl_Email);
            this.Controls.Add(this.tb_Lastname);
            this.Controls.Add(this.lbl_Lastname);
            this.Controls.Add(this.btn_UploadPicture);
            this.Controls.Add(this.btn_CreateUser);
            this.Controls.Add(this.pb_Picture);
            this.Controls.Add(this.lbl_Image);
            this.Controls.Add(this.tb_Firstname);
            this.Controls.Add(this.lbl_Technologies);
            this.Controls.Add(this.lbl_FirstName);
            this.Name = "UserEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UserEdditor";
            ((System.ComponentModel.ISupportInitialize)(this.pb_Picture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_UploadPicture;
        private System.Windows.Forms.Button btn_CreateUser;
        public System.Windows.Forms.PictureBox pb_Picture;
        private System.Windows.Forms.Label lbl_Image;
        private System.Windows.Forms.TextBox tb_Firstname;
        private System.Windows.Forms.Label lbl_Technologies;
        private System.Windows.Forms.Label lbl_FirstName;
        private System.Windows.Forms.TextBox tb_Lastname;
        private System.Windows.Forms.Label lbl_Lastname;
        private System.Windows.Forms.TextBox tb_Email;
        private System.Windows.Forms.Label lbl_Email;
        private System.Windows.Forms.TextBox tb_Phone;
        private System.Windows.Forms.Label lbl_Phone;
        private System.Windows.Forms.TextBox tb_Username;
        private System.Windows.Forms.Label lbl_Username;
        private System.Windows.Forms.FlowLayoutPanel flp_Technologies;
        private System.Windows.Forms.Button btn_AddTech;
        private System.Windows.Forms.CheckBox cb_IsAdmin;
        private System.Windows.Forms.TextBox tb_password;
        private System.Windows.Forms.Label lbl_Password;
        private System.Windows.Forms.TextBox tb_PasswordVerify;
        private System.Windows.Forms.Label lbl_Password1;
    }
}