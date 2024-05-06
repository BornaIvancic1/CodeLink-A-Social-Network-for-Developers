using API.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADMINISTRATIVNI_Modul.Dal
{
    public class PostRepository
    {
        private readonly HttpClient client;
        public PostRepository(HttpClient client) 
        {
            this.client = client;
        }
        public async Task<IEnumerable<PostItem>> GetPostsAsync()
        {
            IEnumerable<PostItem> postsItems = new List<PostItem>();

            try
            {
                var response = await client.GetAsync("Post");

                if (response.IsSuccessStatusCode)
                {
                    var posts = await response.Content.ReadAsAsync<IList<Post>>();
                    postsItems = FormatDataForApp(posts);
                }
            }
            catch (Exception)
            {
                // all good, never happens :)
            }

            return postsItems;
        }

        public async Task<Post> GetPostAsync(int id)
        {
            Post postItem = null;

            try
            {
                var response = await client.GetAsync($"Post/{id}");

                if (response.IsSuccessStatusCode)
                {
                    postItem = await response.Content.ReadAsAsync<Post>();
                }
            }
            catch (Exception)
            {
                // all good, never happens :)
            }

            return postItem;
        }

        public async Task CreatePostAsync(Post post)
        {
            try
            {
                var formData = new MultipartFormDataContent();

                formData.Add(new StringContent(post.AdminId.ToString()), "AdminId");
                formData.Add(new StringContent(post.Title), "Title");
                formData.Add(new StringContent(post.Description), "Description");
                formData.Add(new StringContent(post.PostImage), "PostImage");

                var response = await client.PostAsync("Post/AdminPost", formData);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                // No need to worry
            }
        }

        public async Task UpdatePostAsync(Post post)
        {
            try
            {
                var formData = new MultipartFormDataContent();

                formData.Add(new StringContent(post.Id.ToString()), "Id");
                formData.Add(new StringContent(post.AdminId.ToString()), "AdminId");
                formData.Add(new StringContent(post.Title), "Title");
                formData.Add(new StringContent(post.Description), "Description");
                formData.Add(new StringContent(post.PostImage), "PostImage");

                var response = await client.PutAsync($"Post/AdminPostUpdate", formData);
                response.EnsureSuccessStatusCode();

            }
            catch (Exception)
            {
                // All good here
            }
        }

        public async Task DeletePostAsync(int postId)
        {
            try
            {
                var response = await client.DeleteAsync($"Post/{postId}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                // Stay Calm and forget it ever happend
            }
        }

        private IEnumerable<PostItem> FormatDataForApp(IEnumerable<Post> posts)
        {
            List<PostItem> postsItems = new List<PostItem>();

            foreach (Post post in posts)
            {
                PostItem postItem = FormatDataForApp(post);                
                postsItems.Add(postItem);
            }

            return postsItems;
        }

        private PostItem FormatDataForApp(Post post)
        {
            PostItem postItem = new PostItem();
            postItem.pnl_Content.Tag = post.Id;
            postItem.lbl_ID_Content.Text = post.Id.ToString();
            postItem.lbl_title_content.Text = post.Title;
            postItem.lbl_Description_Content.Text = post.Description;
            postItem.lbl_Date_Content.Text = post.PublishingDate.ToString();
            postItem.lbl_PosterID_Content.Text = post.UserId != null ? post.UserId.ToString() : post.AdminId.ToString();
            if (post.PostImage.Length >= 1000)
            {
                postItem.pb_Picture.Image = ImageConverter.Base64ToImage(post.PostImage);
            }
            return postItem;
        }
    }
}
