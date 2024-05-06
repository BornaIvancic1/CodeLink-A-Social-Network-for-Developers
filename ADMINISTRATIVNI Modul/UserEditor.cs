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
    public partial class UserEditor : Form
    {
        RepoFactory repoFactory;
        bool edit;
        User user = new User();
        bool selectedAdmin = false;

        public UserEditor(RepoFactory repoFactory)
        {
            InitializeComponent();
            edit = false;
            this.repoFactory = repoFactory;
        }

        public UserEditor(RepoFactory repoFactory, int userID, bool selectedAdmin)
        {
            InitializeComponent();
            edit = true;
            RenameButton();
            this.repoFactory = repoFactory;
            this.selectedAdmin = selectedAdmin;
            GetUser(userID, selectedAdmin);
            //lbl_Password.Enabled = false;
            //lbl_Password1.Enabled = false;
            //tb_PasswordVerify.Enabled = false;
            //tb_password.Enabled = false;
        }

        private void RenameButton()
        {
            btn_CreateUser.Text = "Update User";
        }

        private async void GetUser(int userID, bool selectedAdmin)
        {
            if (selectedAdmin)
            {
                user = await repoFactory.adminRepository.GetAdminAsync(userID);
            }
            else
            {
                user = await repoFactory.userRepository.GetUserAsync(userID);
            }
            
            LoadFormData();
        }

        private void LoadFormData()
        {
            tb_Firstname.Text = user.FirstName;
            tb_Lastname.Text = user.LastName;
            tb_Email.Text = user.Email;
            tb_Phone.Text = user.PhoneNumber;
            tb_Username.Text = user.Username;
            pb_Picture.Image = Dal.ImageConverter.Base64ToImage(user.UserProfilePictureBase64);
            
            if (user.KnownTechnologies!=null)
            {
                foreach (var tech in user.KnownTechnologies)
                {
                    TechnologyItem technologyItem = new TechnologyItem();
                    technologyItem.tb_Tech.Text = tech.Name;
                    technologyItem.tb_Skill.Text = tech.SkillLevel;
                    flp_Technologies.Controls.Add(technologyItem);
                }
            }

            if (selectedAdmin)
            {
                cb_IsAdmin.Checked = true;
            }
        }

        private async void btn_CreateUser_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                MessageBox.Show("All fields need to be populated", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            UpdateUserData();

            if (cb_IsAdmin.Checked)
            {
                if (MessageBox.Show("Are you sure this user is an Administrator?", "Admin?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)==DialogResult.Yes)
                {
                    if (edit)
                    {
                        await repoFactory.adminRepository.UpdateAdminAsync(user);
                    }
                    else
                    {
                        await repoFactory.adminRepository.CreateAdminAsync(user);
                    }
                } 
            }
            else
            {
                if (edit)
                {
                    await repoFactory.userRepository.UpdateUserAsync(user);
                }
                else
                {
                    await repoFactory.userRepository.CreateUserAsync(user);
                }
            }

            
        }

        private void UpdateUserData()
        {
            user.FirstName = tb_Firstname.Text;
            user.LastName = tb_Lastname.Text;
            user.Email = tb_Email.Text;
            user.PhoneNumber = tb_Phone.Text;
            user.Username = tb_Username.Text;
            user.PwdHash = tb_password.Text;
            List<Technology> techs = new List<Technology>();
            foreach (TechnologyItem tech in flp_Technologies.Controls)
            {
                Technology technology = new Technology();
                technology.Name = tech.tb_Tech.Text;
                technology.SkillLevel = tech.tb_Skill.Text;
                techs.Add(technology);
            }
            try
            {
                user.UserProfilePictureBase64 = Dal.ImageConverter.ImageToBase64(pb_Picture.Image);
            }
            catch (Exception)
            {
                // no picture, all good
            }
        }

        private bool ValidateForm()
        {
            if (!edit && tb_password==tb_PasswordVerify)
            {
                return false;
            }
            if (tb_Firstname.Text.Trim()=="" || tb_Lastname.Text.Trim() == "" || 
                tb_Email.Text.Trim() == "" || tb_Phone.Text.Trim() == "" || 
                tb_Username.Text.Trim() == "") 
            {
                return false;
            }
            return true;
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

        private void btn_AddTech_Click(object sender, EventArgs e)
        {
            TechnologyItem techItem = new TechnologyItem();
            flp_Technologies.Controls.Add(techItem);
        }

    }
}
