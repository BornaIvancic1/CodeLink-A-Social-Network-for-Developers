namespace ADMINISTRATIVNI_Modul
{
    partial class PostEditor
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
            this.lbl_Description = new System.Windows.Forms.Label();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.tb_Title = new System.Windows.Forms.TextBox();
            this.tb_Description = new System.Windows.Forms.RichTextBox();
            this.pb_Picture = new System.Windows.Forms.PictureBox();
            this.lbl_Image = new System.Windows.Forms.Label();
            this.btn_CreateBlog = new System.Windows.Forms.Button();
            this.btn_UploadPicture = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Picture)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Description
            // 
            this.lbl_Description.AutoSize = true;
            this.lbl_Description.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Description.Location = new System.Drawing.Point(12, 31);
            this.lbl_Description.Name = "lbl_Description";
            this.lbl_Description.Size = new System.Drawing.Size(95, 17);
            this.lbl_Description.TabIndex = 28;
            this.lbl_Description.Text = "Description:";
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Title.Location = new System.Drawing.Point(12, 9);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(50, 17);
            this.lbl_Title.TabIndex = 27;
            this.lbl_Title.Text = "Title: ";
            // 
            // tb_Title
            // 
            this.tb_Title.Location = new System.Drawing.Point(68, 6);
            this.tb_Title.Name = "tb_Title";
            this.tb_Title.Size = new System.Drawing.Size(357, 22);
            this.tb_Title.TabIndex = 29;
            // 
            // tb_Description
            // 
            this.tb_Description.Location = new System.Drawing.Point(15, 51);
            this.tb_Description.Name = "tb_Description";
            this.tb_Description.Size = new System.Drawing.Size(256, 180);
            this.tb_Description.TabIndex = 30;
            this.tb_Description.Text = "";
            // 
            // pb_Picture
            // 
            this.pb_Picture.Location = new System.Drawing.Point(277, 51);
            this.pb_Picture.Name = "pb_Picture";
            this.pb_Picture.Size = new System.Drawing.Size(148, 141);
            this.pb_Picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_Picture.TabIndex = 32;
            this.pb_Picture.TabStop = false;
            // 
            // lbl_Image
            // 
            this.lbl_Image.AutoSize = true;
            this.lbl_Image.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Image.Location = new System.Drawing.Point(274, 31);
            this.lbl_Image.Name = "lbl_Image";
            this.lbl_Image.Size = new System.Drawing.Size(56, 17);
            this.lbl_Image.TabIndex = 31;
            this.lbl_Image.Text = "Image:";
            // 
            // btn_CreateBlog
            // 
            this.btn_CreateBlog.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_CreateBlog.Location = new System.Drawing.Point(130, 237);
            this.btn_CreateBlog.Name = "btn_CreateBlog";
            this.btn_CreateBlog.Size = new System.Drawing.Size(180, 33);
            this.btn_CreateBlog.TabIndex = 33;
            this.btn_CreateBlog.Text = "Create Post";
            this.btn_CreateBlog.UseVisualStyleBackColor = true;
            this.btn_CreateBlog.Click += new System.EventHandler(this.btn_CreatePost_Click);
            // 
            // btn_UploadPicture
            // 
            this.btn_UploadPicture.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_UploadPicture.Location = new System.Drawing.Point(277, 198);
            this.btn_UploadPicture.Name = "btn_UploadPicture";
            this.btn_UploadPicture.Size = new System.Drawing.Size(148, 33);
            this.btn_UploadPicture.TabIndex = 34;
            this.btn_UploadPicture.Text = "Upload Picture";
            this.btn_UploadPicture.UseVisualStyleBackColor = true;
            this.btn_UploadPicture.Click += new System.EventHandler(this.btn_UploadPicture_Click);
            // 
            // PostEdditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 284);
            this.Controls.Add(this.btn_UploadPicture);
            this.Controls.Add(this.btn_CreateBlog);
            this.Controls.Add(this.pb_Picture);
            this.Controls.Add(this.lbl_Image);
            this.Controls.Add(this.tb_Description);
            this.Controls.Add(this.tb_Title);
            this.Controls.Add(this.lbl_Description);
            this.Controls.Add(this.lbl_Title);
            this.Name = "PostEdditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CodeLink Post Edditor";
            ((System.ComponentModel.ISupportInitialize)(this.pb_Picture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Description;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.TextBox tb_Title;
        private System.Windows.Forms.RichTextBox tb_Description;
        public System.Windows.Forms.PictureBox pb_Picture;
        private System.Windows.Forms.Label lbl_Image;
        private System.Windows.Forms.Button btn_CreateBlog;
        private System.Windows.Forms.Button btn_UploadPicture;
    }
}