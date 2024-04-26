using JAVNI_Modul.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace JAVNI_Modul.Controllers
{
    public class PostController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public PostController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }


        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7156/api/Post");

            if (response.IsSuccessStatusCode)
            {
                var postsJson = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(postsJson))
                {
                    var posts = JsonSerializer.Deserialize<List<Post>>(postsJson, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return View(posts);
                }
                else
                {
                  
                    return View(new List<Post>());
                }
            }
            else
            {
                
                return View(new List<Post>());
            }
        }


        public async Task<IActionResult> Details(int id)
        {
            var client = _clientFactory.CreateClient();
            var apiUrl = $"https://localhost:7156/api/Post/{id}";

            var response = await client.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode((int)response.StatusCode);
                }
            }

            var userId = HttpContext.Session.GetString("Id");
            ViewBag.UserId = userId;

            var content = await response.Content.ReadAsStringAsync();
            var post = JsonSerializer.Deserialize<Post>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(post);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var userId = HttpContext.Session.GetString("Id");
            ViewBag.UserId = userId;
            var model = new PostPost(); 
            return View(model); 
        }

        
    }
}