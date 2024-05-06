using System.Drawing;
using System.Windows.Forms;

namespace ADMINISTRATIVNI_Modul
{
    partial class Login
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
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblError = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbUsername
            // 
            this.tbUsername.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.tbUsername.Location = new System.Drawing.Point(461, 339);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(367, 39);
            this.tbUsername.TabIndex = 10;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblUsername.Location = new System.Drawing.Point(461, 309);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(135, 32);
            this.lblUsername.TabIndex = 11;
            this.lblUsername.Text = "Username:";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblPassword.Location = new System.Drawing.Point(461, 411);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(129, 32);
            this.lblPassword.TabIndex = 13;
            this.lblPassword.Text = "Password:";
            // 
            // tbPassword
            // 
            this.tbPassword.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.tbPassword.Location = new System.Drawing.Point(461, 441);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '^';
            this.tbPassword.Size = new System.Drawing.Size(367, 39);
            this.tbPassword.TabIndex = 12;
            this.tbPassword.UseSystemPasswordChar = true;
            this.tbPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPassword_KeyDown);
            // 
            // btnLogin
            // 
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnLogin.Location = new System.Drawing.Point(542, 517);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(195, 55);
            this.btnLogin.TabIndex = 14;
            this.btnLogin.Text = "Log in";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.BackColor = System.Drawing.Color.Red;
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.lblError.Location = new System.Drawing.Point(487, 229);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(312, 46);
            this.lblError.TabIndex = 15;
            this.lblError.Text = "! Invalid Credentials";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1353, 812);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.tbUsername);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TextBox tbUsername;
        private Label lblUsername;
        private Label lblPassword;
        private TextBox tbPassword;
        private Button btnLogin;
        private Label lblError;
    }
}