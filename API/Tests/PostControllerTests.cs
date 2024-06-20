using API.Controllers;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class PostControllerTests
{
    private ApplicationDbContext CreateMockDbContext(List<Post>? posts = null)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var dbContext = new ApplicationDbContext(options);
        if (posts != null)
        {
            dbContext.Post.AddRange(posts);
            dbContext.SaveChanges();
        }
        return dbContext;
    }

    [Fact]
    public async Task GetPosts_ReturnsPosts_WhenPostsExist()
    {
        // Arrange
        var posts = new List<Post>
        {
            new Post { Id = 1, Title = "Post 1", Description = "Description 1" },
            new Post { Id = 2, Title = "Post 2", Description = "Description 2" }
        };
        var dbContext = CreateMockDbContext(posts);
        var controller = new PostController(dbContext);

        // Act
        var result = await controller.GetPosts();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedPosts = Assert.IsAssignableFrom<IEnumerable<Post>>(okResult.Value);
        Assert.Equal(2, returnedPosts.Count());
    }

    [Fact]
    public async Task GetPosts_ReturnsNotFound_WhenNoPostsExist()
    {
        // Arrange
        var dbContext = CreateMockDbContext();
        var controller = new PostController(dbContext);

        // Act
        var result = await controller.GetPosts();

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetPost_ReturnsPost_WhenPostExists()
    {
        // Arrange
        var post = new Post { Id = 1, Title = "Post 1", Description = "Description 1" };
        var dbContext = CreateMockDbContext(new List<Post> { post });
        var controller = new PostController(dbContext);

        // Act
        var result = await controller.GetPost(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedPost = Assert.IsType<Post>(okResult.Value);
        Assert.Equal(post.Id, returnedPost.Id);
        Assert.Equal(post.Title, returnedPost.Title);
        Assert.Equal(post.Description, returnedPost.Description);
    }

    [Fact]
    public async Task GetPost_ReturnsNotFound_WhenPostDoesNotExist()
    {
        // Arrange
        var dbContext = CreateMockDbContext();
        var controller = new PostController(dbContext);

        // Act
        var result = await controller.GetPost(1);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task PostPost_CreatesPost_AndReturnsCreatedAtAction()
    {
        // Arrange
        var dbContext = CreateMockDbContext();
        var controller = new PostController(dbContext);
        var newPost = new PostPost
        {
            UserId = 1,
            PostImage = "image.jpg",
            Title = "New Post",
            Description = "New Description"
        };

        // Act
        var result = await controller.PostPost(newPost);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal("GetPost", createdAtActionResult.ActionName);
        var post = Assert.IsType<Post>(createdAtActionResult.Value);
        Assert.Equal(newPost.Title, post.Title);
        Assert.Equal(newPost.Description, post.Description);

        // Check if the post was added to the database
        var postsInDb = await dbContext.Post.ToListAsync();
        Assert.Single(postsInDb);
        Assert.Equal(newPost.Title, postsInDb[0].Title);
        Assert.Equal(newPost.Description, postsInDb[0].Description);
    }

    [Fact]
    public async Task PostPost_ReturnsBadRequest_WhenUserIdIsNotProvided()
    {
        // Arrange
        var dbContext = CreateMockDbContext();
        var controller = new PostController(dbContext);
        var newPost = new PostPost
        {
            PostImage = "image.jpg",
            Title = "New Post",
            Description = "New Description"
        };

        // Act
        var result = await controller.PostPost(newPost);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("UserId or AdminId must be provided.", badRequestResult.Value);
    }

    [Fact]
    public async Task PostPostAdmin_CreatesPost_AndReturnsCreatedAtAction()
    {
        // Arrange
        var dbContext = CreateMockDbContext();
        var controller = new PostController(dbContext);
        var newPost = new PostPostAdmin
        {
            AdminId = 1,
            PostImage = "image.jpg",
            Title = "New Post",
            Description = "New Description"
        };

        // Act
        var result = await controller.PostPostAdmin(newPost);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal("GetPost", createdAtActionResult.ActionName);
        var post = Assert.IsType<Post>(createdAtActionResult.Value);
        Assert.Equal(newPost.Title, post.Title);
        Assert.Equal(newPost.Description, post.Description);

        // Check if the post was added to the database
        var postsInDb = await dbContext.Post.ToListAsync();
        Assert.Single(postsInDb);
        Assert.Equal(newPost.Title, postsInDb[0].Title);
        Assert.Equal(newPost.Description, postsInDb[0].Description);
    }

    [Fact]
    public async Task PostPostAdmin_ReturnsBadRequest_WhenAdminIdIsNotProvided()
    {
        // Arrange
        var dbContext = CreateMockDbContext();
        var controller = new PostController(dbContext);
        var newPost = new PostPostAdmin
        {
            PostImage = "image.jpg",
            Title = "New Post",
            Description = "New Description"
        };

        // Act
        var result = await controller.PostPostAdmin(newPost);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("UserId or AdminId must be provided.", badRequestResult.Value);
    }

    [Fact]
    public async Task PutPost_UpdatesPost_AndReturnsNoContent()
    {
        // Arrange
        var existingPost = new Post { Id = 1, Title = "Old Title", Description = "Old Description" };
        var dbContext = CreateMockDbContext(new List<Post> { existingPost });
        var controller = new PostController(dbContext);
        var updatedPost = new PutPost
        {
            Id = 1,
            PostImage = "newimage.jpg",
            Title = "Updated Title",
            Description = "Updated Description"
        };

        // Act
        var result = await controller.PutPost(updatedPost);

        // Assert
        Assert.IsType<NoContentResult>(result);

        // Check if the post was updated in the database
        var postInDb = await dbContext.Post.FindAsync(1);
        Assert.Equal(updatedPost.Title, postInDb.Title);
        Assert.Equal(updatedPost.Description, postInDb.Description);
        Assert.Equal(updatedPost.PostImage, postInDb.PostImage);
    }

    [Fact]
    public async Task PutPost_ReturnsNotFound_WhenPostDoesNotExist()
    {
        // Arrange
        var dbContext = CreateMockDbContext();
        var controller = new PostController(dbContext);
        var updatedPost = new PutPost
        {
            Id = 1,
            PostImage = "newimage.jpg",
            Title = "Updated Title",
            Description = "Updated Description"
        };

        // Act
        var result = await controller.PutPost(updatedPost);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Post not found.", notFoundResult.Value);
    }

    [Fact]
    public async Task PutAdminPost_UpdatesPost_AndReturnsNoContent()
    {
        // Arrange
        var existingPost = new Post { Id = 1, Title = "Old Title", Description = "Old Description" };
        var dbContext = CreateMockDbContext(new List<Post> { existingPost });
        var controller = new PostController(dbContext);
        var updatedPost = new PutAdminPost
        {
            Id = 1,
            AdminId = 1,
            PostImage = "newimage.jpg",
            Title = "Updated Title",
            Description = "Updated Description"
        };

        // Act
        var result = await controller.PutAdminPost(updatedPost);

        // Assert
        Assert.IsType<NoContentResult>(result);

        // Check if the post was updated in the database
        var postInDb = await dbContext.Post.FindAsync(1);
        Assert.Equal(updatedPost.Title, postInDb.Title);
        Assert.Equal(updatedPost.Description, postInDb.Description);
        Assert.Equal(updatedPost.PostImage, postInDb.PostImage);
        Assert.Equal(updatedPost.AdminId, postInDb.AdminId);
    }

    [Fact]
    public async Task PutAdminPost_ReturnsNotFound_WhenPostDoesNotExist()
    {
        // Arrange
        var dbContext = CreateMockDbContext();
        var controller = new PostController(dbContext);
        var updatedPost = new PutAdminPost
        {
            Id = 1,
            AdminId = 1,
            PostImage = "newimage.jpg",
            Title = "Updated Title",
            Description = "Updated Description"
        };

        // Act
        var result = await controller.PutAdminPost(updatedPost);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Post not found.", notFoundResult.Value);
    }

    [Fact]
    public async Task DeletePost_DeletesPost_AndReturnsPost()
    {
        // Arrange
        var post = new Post { Id = 1, Title = "Post 1", Description = "Description 1" };
        var dbContext = CreateMockDbContext(new List<Post> { post });
        var controller = new PostController(dbContext);

        // Act
        var result = await controller.DeletePost(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var deletedPost = Assert.IsType<Post>(okResult.Value);
        Assert.Equal(post.Id, deletedPost.Id);

        // Check if the post was removed from the database
        var postsInDb = await dbContext.Post.ToListAsync();
        Assert.Empty(postsInDb);
    }

    [Fact]
    public async Task DeletePost_ReturnsNotFound_WhenPostDoesNotExist()
    {
        // Arrange
        var dbContext = CreateMockDbContext();
        var controller = new PostController(dbContext);

        // Act
        var result = await controller.DeletePost(1);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
}
