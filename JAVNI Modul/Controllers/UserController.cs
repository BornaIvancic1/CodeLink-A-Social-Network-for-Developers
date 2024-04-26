
using JAVNI_Modul.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JAVNI_Modul.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public IActionResult Account()
        {
            var viewModel = new User();


            viewModel.Id = int.Parse(HttpContext.Session.GetString("Id"));
            viewModel.Username = HttpContext.Session.GetString("Username");
            viewModel.FirstName = HttpContext.Session.GetString("FirstName");
            viewModel.LastName = HttpContext.Session.GetString("LastName");
            viewModel.Email = HttpContext.Session.GetString("Email");
            viewModel.PhoneNumber = HttpContext.Session.GetString("Phone");
          
            var knownTechnologiesJson = HttpContext.Session.GetString("KnownTechnologies");
            viewModel.KnownTechnologies = JsonSerializer.Deserialize<List<Technology>>(knownTechnologiesJson);


            viewModel.UserProfilePictureBase64= HttpContext.Session.GetString("UserProfilePictureBase64");


            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new User());
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            var client = _httpClientFactory.CreateClient();
            var apiUrl = $"https://localhost:7156/api/User/login/{user.Email}/{user.PwdHash}";
            var response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var userData = await response.Content.ReadFromJsonAsync<User>();

                var session = HttpContext.Session;
                session.SetString("Id", userData.Id.ToString());
                session.SetString("Username", userData.Username);
                session.SetString("FirstName", userData.FirstName);
                session.SetString("LastName", userData.LastName);
                session.SetString("Email", userData.Email);
                session.SetString("Phone", userData.PhoneNumber);

               
                var knownTechnologiesJson = JsonSerializer.Serialize(userData.KnownTechnologies);
                session.SetString("KnownTechnologies", knownTechnologiesJson);

             
                session.SetString("UserProfilePictureBase64", userData.UserProfilePictureBase64);

                return RedirectToAction("Index", "Post");

            }
            else
            {
                ModelState.AddModelError("", "Invalid email or password");
                return View("Login");
            }
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> FriendList()
        {
            var loggedInUserId = int.Parse(HttpContext.Session.GetString("Id"));

            var client = _httpClientFactory.CreateClient();

 
            var usersResponse = await client.GetAsync("https://localhost:7156/api/User");

            List<User> users = null;
            if (usersResponse.IsSuccessStatusCode)
            {
                var usersJson = await usersResponse.Content.ReadAsStringAsync();
                users = JsonSerializer.Deserialize<List<User>>(usersJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

           
                users.RemoveAll(u => u.Id == loggedInUserId);
            }

         
            var friendsResponse = await client.GetAsync($"https://localhost:7156/api/Friend/GetFriends/{loggedInUserId}");

            List<User> friends = null;
            if (friendsResponse.IsSuccessStatusCode)
            {
                var friendsJson = await friendsResponse.Content.ReadAsStringAsync();
                friends = JsonSerializer.Deserialize<List<User>>(friendsJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            ViewBag.UserId = loggedInUserId;
            ViewBag.Users = users;
            ViewBag.Friends = friends;

            return View();
        }




        [HttpGet]
        public IActionResult ChangePassword()
        {
            var userId = HttpContext.Session.GetString("Id");
            ViewBag.UserId = userId;
            return View();
        }




        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}