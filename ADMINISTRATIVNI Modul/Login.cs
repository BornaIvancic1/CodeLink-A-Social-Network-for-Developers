using ADMINISTRATIVNI_Modul.Dal;
using API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADMINISTRATIVNI_Modul
{
    public partial class Login : Form
    {
        RepoFactory repoFactory = new RepoFactory();
        string username;
        string password;
        Admin admin;

        public Login()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            lblError.Visible = false;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                GetCredentials();
                admin = await repoFactory.adminRepository.GetAdminLoginAsync(username, password);
                if (admin.Email != null)
                {
                    Thread newThread = new Thread(CreateNewAdminForm);
                    newThread.Start();
                    this.Close();
                }
                else 
                {
                    lblError.Visible = true;
                }
            }
            catch (Exception)
            {
                lblError.Visible = true;
            }
        }

        private void CreateNewAdminForm()
        {
            Form newForm = new MainMenu(admin, repoFactory);
            Application.Run(newForm);
        }

        private void GetCredentials()
        {
            username = tbUsername.Text;
            password = tbPassword.Text;
        }

        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }
    }
}
