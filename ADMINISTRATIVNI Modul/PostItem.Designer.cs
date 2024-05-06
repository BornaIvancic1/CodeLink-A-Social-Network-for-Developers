using System.Drawing;
using System.Windows.Forms;

namespace ADMINISTRATIVNI_Modul
{
    partial class PostItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnl_Content = new System.Windows.Forms.Panel();
            this.lbl_Description_Content = new System.Windows.Forms.Label();
            this.lbl_title_content = new System.Windows.Forms.Label();
            this.lbl_Description = new System.Windows.Forms.Label();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.lbl_PosterID_Content = new System.Windows.Forms.Label();
            this.lbl_Date_Content = new System.Windows.Forms.Label();
            this.lbl_PosterID = new System.Windows.Forms.Label();
            this.lbl_Date = new System.Windows.Forms.Label();
            this.pb_Picture = new System.Windows.Forms.PictureBox();
            this.lbl_ID_Content = new System.Windows.Forms.Label();
            this.lbl_Image = new System.Windows.Forms.Label();
            this.lbl_ID = new System.Windows.Forms.Label();
            this.pnl_Content.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Picture)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_Content
            // 
            this.pnl_Content.Controls.Add(this.lbl_Description_Content);
            this.pnl_Content.Controls.Add(this.lbl_title_content);
            this.pnl_Content.Controls.Add(this.lbl_Description);
            this.pnl_Content.Controls.Add(this.lbl_Title);
            this.pnl_Content.Controls.Add(this.lbl_PosterID_Content);
            this.pnl_Content.Controls.Add(this.lbl_Date_Content);
            this.pnl_Content.Controls.Add(this.lbl_PosterID);
            this.pnl_Content.Controls.Add(this.lbl_Date);
            this.pnl_Content.Controls.Add(this.pb_Picture);
            this.pnl_Content.Controls.Add(this.lbl_ID_Content);
            this.pnl_Content.Controls.Add(this.lbl_Image);
            this.pnl_Content.Controls.Add(this.lbl_ID);
            this.pnl_Content.Location = new System.Drawing.Point(3, 3);
            this.pnl_Content.Name = "pnl_Content";
            this.pnl_Content.Size = new System.Drawing.Size(352, 192);
            this.pnl_Content.TabIndex = 0;
            this.pnl_Content.UseWaitCursor = true;
            // 
            // lbl_Description_Content
            // 
            this.lbl_Description_Content.AutoSize = true;
            this.lbl_Description_Content.Location = new System.Drawing.Point(0, 36);
            this.lbl_Description_Content.Name = "lbl_Description_Content";
            this.lbl_Description_Content.Size = new System.Drawing.Size(50, 16);
            this.lbl_Description_Content.TabIndex = 45;
            this.lbl_Description_Content.Text = "content";
            this.lbl_Description_Content.UseWaitCursor = true;
            // 
            // lbl_title_content
            // 
            this.lbl_title_content.AutoSize = true;
            this.lbl_title_content.Location = new System.Drawing.Point(45, 3);
            this.lbl_title_content.Name = "lbl_title_content";
            this.lbl_title_content.Size = new System.Drawing.Size(50, 16);
            this.lbl_title_content.TabIndex = 44;
            this.lbl_title_content.Text = "content";
            this.lbl_title_content.UseWaitCursor = true;
            // 
            // lbl_Description
            // 
            this.lbl_Description.AutoSize = true;
            this.lbl_Description.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Description.Location = new System.Drawing.Point(0, 19);
            this.lbl_Description.Name = "lbl_Description";
            this.lbl_Description.Size = new System.Drawing.Size(95, 17);
            this.lbl_Description.TabIndex = 43;
            this.lbl_Description.Text = "Description:";
            this.lbl_Description.UseWaitCursor = true;
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Title.Location = new System.Drawing.Point(0, 2);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(50, 17);
            this.lbl_Title.TabIndex = 42;
            this.lbl_Title.Text = "Title: ";
            this.lbl_Title.UseWaitCursor = true;
            // 
            // lbl_PosterID_Content
            // 
            this.lbl_PosterID_Content.AutoSize = true;
            this.lbl_PosterID_Content.Location = new System.Drawing.Point(309, 174);
            this.lbl_PosterID_Content.Name = "lbl_PosterID_Content";
            this.lbl_PosterID_Content.Size = new System.Drawing.Size(18, 16);
            this.lbl_PosterID_Content.TabIndex = 41;
            this.lbl_PosterID_Content.Text = "id";
            this.lbl_PosterID_Content.UseWaitCursor = true;
            // 
            // lbl_Date_Content
            // 
            this.lbl_Date_Content.AutoSize = true;
            this.lbl_Date_Content.Location = new System.Drawing.Point(228, 37);
            this.lbl_Date_Content.Name = "lbl_Date_Content";
            this.lbl_Date_Content.Size = new System.Drawing.Size(50, 16);
            this.lbl_Date_Content.TabIndex = 40;
            this.lbl_Date_Content.Text = "content";
            this.lbl_Date_Content.UseWaitCursor = true;
            // 
            // lbl_PosterID
            // 
            this.lbl_PosterID.AutoSize = true;
            this.lbl_PosterID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_PosterID.Location = new System.Drawing.Point(228, 173);
            this.lbl_PosterID.Name = "lbl_PosterID";
            this.lbl_PosterID.Size = new System.Drawing.Size(75, 17);
            this.lbl_PosterID.TabIndex = 39;
            this.lbl_PosterID.Text = "PosterID:";
            this.lbl_PosterID.UseWaitCursor = true;
            // 
            // lbl_Date
            // 
            this.lbl_Date.AutoSize = true;
            this.lbl_Date.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Date.Location = new System.Drawing.Point(228, 20);
            this.lbl_Date.Name = "lbl_Date";
            this.lbl_Date.Size = new System.Drawing.Size(123, 17);
            this.lbl_Date.TabIndex = 38;
            this.lbl_Date.Text = "Published Date:";
            this.lbl_Date.UseWaitCursor = true;
            // 
            // pb_Picture
            // 
            this.pb_Picture.Location = new System.Drawing.Point(231, 73);
            this.pb_Picture.Name = "pb_Picture";
            this.pb_Picture.Size = new System.Drawing.Size(100, 97);
            this.pb_Picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_Picture.TabIndex = 37;
            this.pb_Picture.TabStop = false;
            this.pb_Picture.UseWaitCursor = true;
            // 
            // lbl_ID_Content
            // 
            this.lbl_ID_Content.AutoSize = true;
            this.lbl_ID_Content.Location = new System.Drawing.Point(253, 0);
            this.lbl_ID_Content.Name = "lbl_ID_Content";
            this.lbl_ID_Content.Size = new System.Drawing.Size(18, 16);
            this.lbl_ID_Content.TabIndex = 36;
            this.lbl_ID_Content.Text = "id";
            this.lbl_ID_Content.UseWaitCursor = true;
            // 
            // lbl_Image
            // 
            this.lbl_Image.AutoSize = true;
            this.lbl_Image.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Image.Location = new System.Drawing.Point(228, 53);
            this.lbl_Image.Name = "lbl_Image";
            this.lbl_Image.Size = new System.Drawing.Size(56, 17);
            this.lbl_Image.TabIndex = 35;
            this.lbl_Image.Text = "Image:";
            this.lbl_Image.UseWaitCursor = true;
            // 
            // lbl_ID
            // 
            this.lbl_ID.AutoSize = true;
            this.lbl_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ID.Location = new System.Drawing.Point(228, -1);
            this.lbl_ID.Name = "lbl_ID";
            this.lbl_ID.Size = new System.Drawing.Size(33, 17);
            this.lbl_ID.TabIndex = 34;
            this.lbl_ID.Text = "ID: ";
            this.lbl_ID.UseWaitCursor = true;
            // 
            // PostItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pnl_Content);
            this.Name = "PostItem";
            this.Size = new System.Drawing.Size(358, 196);
            this.UseWaitCursor = true;
            this.pnl_Content.ResumeLayout(false);
            this.pnl_Content.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Picture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public Panel pnl_Content;
        public Label lbl_Description_Content;
        public Label lbl_title_content;
        private Label lbl_Description;
        private Label lbl_Title;
        public Label lbl_PosterID_Content;
        public Label lbl_Date_Content;
        private Label lbl_PosterID;
        private Label lbl_Date;
        public PictureBox pb_Picture;
        public Label lbl_ID_Content;
        private Label lbl_Image;
        private Label lbl_ID;
    }
}
