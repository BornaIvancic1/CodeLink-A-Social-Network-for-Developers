using ADMINISTRATIVNI_Modul.Dal;
using API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageConverter = ADMINISTRATIVNI_Modul.Dal.ImageConverter;

namespace ADMINISTRATIVNI_Modul
{
    public partial class MainMenu : Form
    {

        RepoFactory repoFactory;
        IEnumerable<PostItem> posts = new List<PostItem>();
        IEnumerable<UserItem> users = new List<UserItem>();
        IEnumerable<UserItem> admins = new List<UserItem>();
        Admin currentUser;
        int selectedPost = -1;
        int selectedUser = -1;
        bool selectedAdmin = false;
        Panel selectedPanel;

        public MainMenu(Admin admin, RepoFactory repoFactory)
        {
            InitializeComponent();
            currentUser = admin;
            this.repoFactory = repoFactory;
            btn_EditPost.Enabled = false;
            btn_DeletePost.Enabled = false;
            btn_EditUser.Enabled = false;
            btn_DeleteUser.Enabled = false;
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            tab_Control.SelectedIndex = 2; 
            //Start on ME tab, Starts loading data through handler tab_Control_SelectedIndexChanged
        }

        private void tab_Control_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((sender as TabControl).SelectedIndex)
            {
                case 0: //Posts
                    selectedUser = -1;
                    GetPosts();
                    break;
                case 1: //Users
                    selectedPost = -1;
                    GetUsers();
                    break;
                case 2: //Me
                    selectedUser = -1;
                    selectedPost = -1;
                    FillData();
                    break;
            }
        }

        private void FillData()
        {
            lbl_firstname_content.Text = currentUser.FirstName;
            lbl_lastname_content.Text = currentUser.LastName;
            lbl_email_content.Text = currentUser.Email;
            lbl_phone_content.Text = currentUser.PhoneNumber;
            lbl_username_content.Text = currentUser.Username;
            try
            {
                pb_Picture.Image = ImageConverter.Base64ToImage(currentUser.UserProfilePictureBase64);
            }
            catch (Exception)
            {
                //yeeeet
            }
        }
        #region Post
        private async void GetPosts()
        {
            flp_Post_Container.Controls.Clear();
            posts = await repoFactory.postRepository.GetPostsAsync();
            LoadPosts(posts);
        }

        private void LoadPosts(IEnumerable<PostItem> posts)
        {
            flp_Post_Container.Invoke(new Action(() =>
            {
                foreach (var post in posts)
                {
                    post.pnl_Content.Click += pnl_PostContent_Click;
                    flp_Post_Container.Controls.Add(post);
                }
                flp_Post_Container.PerformLayout();
            }));
        }
        private void btn_refreshposts_Click(object sender, EventArgs e)
        {
            GetPosts();
        }

        private void pnl_PostContent_Click(object sender, EventArgs e)
        {
            if (btn_EditPost.Enabled == false)
            {
                btn_EditPost.Enabled = true;
                btn_DeletePost.Enabled = true;
            }
            DeselectPanel();
            SelectedPanel((Panel)sender, false);
        }

        private void btn_NewPost_Click(object sender, EventArgs e)
        {
            Thread newThread = new Thread(() => CreateNewPostEditForm(-1));
            newThread.SetApartmentState(ApartmentState.STA);
            newThread.Start();
        }

        private void btn_EditPost_Click(object sender, EventArgs e)
        {
            Thread newThread = new Thread(() => CreateNewPostEditForm(selectedPost));
            newThread.SetApartmentState(ApartmentState.STA);
            newThread.Start();
        }

        private async void btn_DeletePost_Click(object sender, EventArgs e)
        {
            if (selectedPost != -1 && MessageBox.Show($"Are you sure you want to delete the post with the id {selectedPost}?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                await repoFactory.postRepository.DeletePostAsync(selectedPost);
                GetPosts();
            }
        }

        private void CreateNewPostEditForm(int postID)
        {
            Form newForm;
            if (postID == -1)
            {
                newForm = new PostEditor(repoFactory, currentUser);
            }
            else
            {
                newForm = new PostEditor(repoFactory, currentUser, postID);
            }
            Application.Run(newForm);
        }
        #endregion

        #region User
        private async void GetUsers()
        {
            flp_User_Container.Controls.Clear();
            users = await repoFactory.userRepository.GetUsersAsync();
            LoadUsers(users);
            admins = await repoFactory.adminRepository.GetAdminsAsync();
            LoadUsers(admins);
        }

        private void LoadUsers(IEnumerable<UserItem> users)
        {
            flp_User_Container.Invoke(new Action(() =>
            {
                foreach (var user in users)
                {
                    user.pnl_Content.Click += pnl_UserContent_Click;
                    flp_User_Container.Controls.Add(user);
                    flp_User_Container.PerformLayout();
                }
            }));
        }
        private void btn_refreshusers_Click(object sender, EventArgs e)
        {
            GetUsers();
        }

        private void pnl_UserContent_Click(object sender, EventArgs e)
        {
            if (btn_EditUser.Enabled == false)
            {
                btn_EditUser.Enabled = true;
                btn_DeleteUser.Enabled = true;
            }
            DeselectPanel();
            SelectedPanel((Panel)sender, true);
        }

        private void btn_CreateUser_Click(object sender, EventArgs e)
        {
            Thread newThread = new Thread(() => CreateNewUserEditForm(-1));
            newThread.SetApartmentState(ApartmentState.STA);
            newThread.Start();
        }

        private void btn_EditUser_Click(object sender, EventArgs e)
        {
            Thread newThread = new Thread(() => CreateNewUserEditForm(selectedUser));
            newThread.SetApartmentState(ApartmentState.STA);
            newThread.Start();
        }

        private async void btn_DeleteUser_Click(object sender, EventArgs e)
        {
            if (selectedUser != -1 && MessageBox.Show($"Are you sure you want to delete the user with the id {selectedUser}?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (admins.Any(x => (int)x.pnl_Content.Tag == selectedUser))
                {
                    //TODO: make it smarter
                    await repoFactory.adminRepository.DeleteAdminAsync(selectedUser);
                }
                await repoFactory.userRepository.DeleteUserAsync(selectedUser);
            }
        }
        private void CreateNewUserEditForm(int userID)
        {
            Form newForm;
            if (userID == -1)
            {
                newForm = new UserEditor(repoFactory);
            }
            else
            {
                newForm = new UserEditor(repoFactory, userID, selectedAdmin);
            }
            Application.Run(newForm);
        }

        #endregion

        private void SelectedPanel(Panel sender, bool user_or_post)
        {
            selectedPanel = sender;
            if (user_or_post)
            { //user
                selectedUser = (int)selectedPanel.Tag;
                if ((selectedPanel.Controls.Find("lbl_Admin", true).FirstOrDefault() as Label).Enabled == true)
                {
                    selectedAdmin = true;
                }
                else
                {
                    selectedAdmin = false;
                }
            }
            else
            { //post
                selectedPost = (int)selectedPanel.Tag;
            }
            var item = selectedPanel.Parent;
            item.BackColor = Color.FromArgb(50, Color.Aquamarine);
        }

        private void DeselectPanel()
        {
            if (selectedPanel != null) 
            {
                var item = selectedPanel.Parent;
                item.BackColor = Color.White;
            }
        }

    }
}
