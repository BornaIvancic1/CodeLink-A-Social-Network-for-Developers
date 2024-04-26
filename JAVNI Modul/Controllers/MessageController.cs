using JAVNI_Modul.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JAVNI_Modul.Controllers
{
    public class MessageController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public MessageController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var loggedInUserId = int.Parse(HttpContext.Session.GetString("Id"));

            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7156/api/User");

            if (response.IsSuccessStatusCode)
            {
                var usersJson = await response.Content.ReadAsStringAsync();
                var users = JsonSerializer.Deserialize<List<User>>(usersJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

       
                var filteredUsers = users.Where(u => u.Id != loggedInUserId).ToList();

                var userId = HttpContext.Session.GetString("Id");
                ViewBag.UserId = userId;
           
                return View(filteredUsers);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
