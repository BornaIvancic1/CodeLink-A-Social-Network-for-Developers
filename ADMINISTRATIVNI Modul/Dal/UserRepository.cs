using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ADMINISTRATIVNI_Modul.Dal
{
    public class UserRepository
    {
        private readonly HttpClient client;
        public UserRepository(HttpClient client)
        {
            this.client = client;
        }
        public async Task<IEnumerable<UserItem>> GetUsersAsync()
        {
            IEnumerable<UserItem> usersItems = new List<UserItem>();

            try
            {
                var response = await client.GetAsync("User");

                if (response.IsSuccessStatusCode)
                {
                    var users = await response.Content.ReadAsAsync<IList<User>>();
                    usersItems = FormatDataForApp(users);
                }
            }
            catch (Exception)
            {
                // all good, never happens :)
            }

            return usersItems;
        }

        public async Task<User> GetUserAsync(int id)
        {
            User user = null;

            try
            {
                var response = await client.GetAsync($"User/{id}");

                if (response.IsSuccessStatusCode)
                {
                    user = await response.Content.ReadAsAsync<User>();
                }
            }
            catch (Exception)
            {
                // all good, never happens :)
            }

            return user;
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            try
            {
                UserPut userPut = new UserPut()
                {
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    email = user.Email,
                    phoneNumber = user.PhoneNumber,
                    username = user.Username,
                    knownTechnologies = new List<Technology>(),
                    userProfilePictureBase64 = user.UserProfilePictureBase64,
                    password = user.PwdHash
                };

                string userJson = JsonSerializer.Serialize(userPut);
                var content = new StringContent(userJson, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("User", content);
                response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                // No need to worry
                return false;
            }

        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            try
            {
                UserPut userPut = new UserPut()
                {
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    email = user.Email,
                    phoneNumber = user.PhoneNumber,
                    username = user.Username,
                    knownTechnologies = new List<Technology>(),
                    userProfilePictureBase64 = user.UserProfilePictureBase64,
                };

                string userJson = JsonSerializer.Serialize(userPut);
                var content = new StringContent(userJson, Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"User/{user.Id}", content);
                response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                // All good here
                return false;
            }
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            try
            {
                var response = await client.DeleteAsync($"User/{userId}");
                response.EnsureSuccessStatusCode();

                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                // Stay Calm and forget it ever happend
                return false;
            }
        }

        private IEnumerable<UserItem> FormatDataForApp(IEnumerable<User> users)
        {
            List<UserItem> usersItems = new List<UserItem>();

            foreach (User user in users)
            {
                UserItem userItem = FormatDataForApp(user);        
                usersItems.Add(userItem);
            }
            return usersItems;
        }

        private UserItem FormatDataForApp(User user)
        {
            UserItem userItem = new UserItem();
            userItem.pnl_Content.Tag = user.Id;
            userItem.lbl_firstname_content.Text = user.FirstName;
            userItem.lbl_lastname_content.Text = user.LastName;
            userItem.lbl_email_content.Text = user.Email;
            userItem.lbl_phone_content.Text = user.PhoneNumber;
            userItem.lbl_username_content.Text = user.Username;
            userItem.lbl_Admin.Enabled = false;
            userItem.lbl_Admin.Text = "";
            try
            {
                userItem.lbl_technologies_content.Text = user.KnownTechnologies.ToList().ToString();
            }
            catch (Exception)
            {
                userItem.lbl_technologies_content.Text = "";
            }
            try
            {               
                userItem.pb_Picture.Image = ImageConverter.Base64ToImage(user.UserProfilePictureBase64);
            }
            catch (Exception)
            {
                //reeeeeeeeee
            }
            return userItem;
        }
    }
}
