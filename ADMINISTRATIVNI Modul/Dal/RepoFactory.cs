using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ADMINISTRATIVNI_Modul.Dal
{
    public class RepoFactory
    {
        // swap to private
        public PostRepository postRepository;
        public UserRepository userRepository;
        public AdminRepository adminRepository;
        private readonly HttpClient client;
        public RepoFactory() 
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7156/api/");
            postRepository = new PostRepository(client);
            userRepository = new UserRepository(client);
            adminRepository = new AdminRepository(client);
        }

        // TODO: add accessors to repos after swaping to private
    }
}
