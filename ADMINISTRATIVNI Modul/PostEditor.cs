using ADMINISTRATIVNI_Modul.Dal;
using API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADMINISTRATIVNI_Modul
{
    public partial class PostEditor : Form
    {
        bool edit;
        Post post = new Post();
        Admin currentUser;
        RepoFactory repoFactory;
        public PostEditor(RepoFactory repoFactory, Admin admin)
        {
            
            InitializeComponent();
            currentUser = admin;
            edit = false;    
            this.repoFactory = repoFactory;
        }

        public PostEditor(RepoFactory repoFactory, Admin admin, int postID)
        {
            InitializeComponent();
            currentUser = admin;
            edit = true;
            RenameButton();
            this.repoFactory = repoFactory;
            GetPost(postID);
            
        }

        private async void GetPost(int postID)
        {
            post = await repoFactory.postRepository.GetPostAsync(postID);
            LoadFormData();
        }

        private void LoadFormData()
        {
            tb_Title.Text = post.Title;
            tb_Description.Text = post.Description;
            if (post.PostImage.Length >= 1000)
            {
                byte[] imageBytes = Convert.FromBase64String(post.PostImage);
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    Bitmap bitmap = new Bitmap(ms);
                    pb_Picture.Image = bitmap;
                }
            }
        }

        private void RenameButton()
        {
            btn_CreateBlog.Text = "Update Post";
        }

        private void btn_UploadPicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog opnfd = new OpenFileDialog();
            opnfd.Filter = "Image Files (*.jpg;*.jpeg;.*.gif;)|*.jpg;*.jpeg;.*.gif";
            Invoke((Action)(() =>
            {
                if (opnfd.ShowDialog() == DialogResult.OK)
                {
                    pb_Picture.Image = new Bitmap(opnfd.FileName);
                }
            }));
        }

        private async void btn_CreatePost_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                MessageBox.Show("Title can not be empty", "Missing Title",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            UpdatePostData();

            if (edit) 
            {
                if (await repoFactory.postRepository.UpdatePostAsync(post))
                {
                    MessageBox.Show("Action was successful", "Success", MessageBoxButtons.OK);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Action was not successful, try again later.", "Failed", MessageBoxButtons.OK,MessageBoxIcon.Error);
                }

            }
            else 
            {
                if (await repoFactory.postRepository.CreatePostAsync(post))
                {
                    MessageBox.Show("Action was successful", "Success", MessageBoxButtons.OK);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Action was not successful, try again later.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void UpdatePostData()
        {
            post.Title = tb_Description.Text;
            post.Description = tb_Description.Text;
            try
            {
                post.PostImage = Dal.ImageConverter.ImageToBase64(pb_Picture.Image);
            }
            catch (Exception)
            {
                post.PostImage = "";
            }           
            post.AdminId = currentUser.Id;

        }

        private bool ValidateForm()
        {
            if (tb_Title.Text!=null || tb_Title.Text.Trim() != "")
            {
                return true;
            }
            return false;
        }
    }
}
