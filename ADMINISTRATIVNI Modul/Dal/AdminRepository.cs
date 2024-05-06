using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ADMINISTRATIVNI_Modul.Dal
{
    public class AdminRepository
    {
        private readonly HttpClient client;
        public AdminRepository(HttpClient client)
        {
            this.client = client;
        }
        public async Task<IEnumerable<UserItem>> GetAdminsAsync()
        {
            IEnumerable<UserItem> adminsItems = new List<UserItem>();

            try
            {
                var response = await client.GetAsync("Admin");

                if (response.IsSuccessStatusCode)
                {
                    var admins = await response.Content.ReadAsAsync<IEnumerable<Admin>>();
                    adminsItems = FormatDataForApp(admins);
                }
            }
            catch (Exception)
            {
                // all good, never happens :)
            }

            return adminsItems;
        }

        public async Task<User> GetAdminAsync(int id)
        {
            Admin admin = null;
            User userFormat = null;
            try
            {
                var response = await client.GetAsync($"Admin/{id}");

                if (response.IsSuccessStatusCode)
                {
                    admin = await response.Content.ReadAsAsync<Admin>();
                }

                userFormat = ConvertToUserFromAdmin(admin);
            }
            catch (Exception)
            {
                // all good, never happens :)
            }

            return userFormat;
        }

        public async Task CreateAdminAsync(User user)
        {
            Admin admin = ConvertToAdminFromUser(user);
            
            try
            {
                AdminPut adminPut = new AdminPut()
                {
                    firstName = admin.FirstName,
                    lastName = admin.LastName,
                    email = admin.Email,
                    phoneNumber = admin.PhoneNumber,
                    username = admin.Username,
                    userProfilePictureBase64 = admin.UserProfilePictureBase64,
                    password = admin.PwdHash
                };

                string userJson = JsonSerializer.Serialize(adminPut);
                var content = new StringContent(userJson, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("Admin", content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                // No need to worry
            }
        }

        public async Task UpdateAdminAsync(User user)
        {
            Admin admin = ConvertToAdminFromUser(user);

            try
            {
                AdminPut adminPut = new AdminPut()
                {
                    firstName = admin.FirstName,
                    lastName = admin.LastName,
                    email = admin.Email,
                    phoneNumber = admin.PhoneNumber,
                    username = admin.Username,
                    userProfilePictureBase64 = admin.UserProfilePictureBase64,
                    password = admin.PwdHash
                };

                string userJson = JsonSerializer.Serialize(adminPut);
                var content = new StringContent(userJson, Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"Admin/{admin.Id}", content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                // All good here
            }
        }

        public async Task DeleteAdminAsync(int adminId)
        {
            try
            {
                var response = await client.DeleteAsync($"Admin/{adminId}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                // Stay Calm and forget it ever happend
            }
        }

        public async Task<Admin> GetAdminLoginAsync(string email, string pwd)
        {
            Admin admin = new Admin();
            try
            {
                var response = await client.GetAsync($"Admin/login/{email}/{pwd}");

                if (response.IsSuccessStatusCode)
                {
                    admin = await response.Content.ReadAsAsync<Admin>();
                }
            }
            catch (Exception)
            {
                // all good, never happens :)
            }

            return admin;
        }

        private Admin ConvertToAdminFromUser(User user)
        {
            Admin admin = new Admin
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Username = user.Username,
                UserProfilePictureBase64 = user.UserProfilePictureBase64,
                PwdHash = user.PwdHash
            };
            return admin;
        }

        private User ConvertToUserFromAdmin(Admin admin)
        {
            User user = new User
            {
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email,
                PhoneNumber = admin.PhoneNumber,
                Username = admin.Username,
                UserProfilePictureBase64 = admin.UserProfilePictureBase64,
                PwdHash = admin.PwdHash,
                KnownTechnologies = new List<Technology>()
            };
            return user;
        }

        private IEnumerable<UserItem> FormatDataForApp(IEnumerable<Admin> admins)
        {
            List<UserItem> usersItems = new List<UserItem>();

            foreach (var admin in admins)
            {
                UserItem userItem = FormatDataForApp(admin);
                usersItems.Add(userItem);
            }
            return usersItems;
        }

        private UserItem FormatDataForApp(Admin admin)
        {
            UserItem userItem = new UserItem();
            userItem.pnl_Content.Tag = admin.Id;
            userItem.lbl_firstname_content.Text = admin.FirstName;
            userItem.lbl_lastname_content.Text = admin.LastName;
            userItem.lbl_email_content.Text = admin.Email;
            userItem.lbl_phone_content.Text = admin.PhoneNumber;
            userItem.lbl_username_content.Text = admin.Username;
            userItem.lbl_Admin.Enabled = true;
            userItem.lbl_Admin.Text = "Admin";
            if (admin.UserProfilePictureBase64.Length >= 1000)
            {
                userItem.pb_Picture.Image = ImageConverter.Base64ToImage(admin.UserProfilePictureBase64);
            }
            

            return userItem;
        }
    }
}
