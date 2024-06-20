using API.Controllers;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class FriendControllerTests
{
    private ApplicationDbContext CreateMockDbContext(
        List<Friend>? friends = null,
        List<User>? users = null)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var dbContext = new ApplicationDbContext(options);
        if (friends != null) dbContext.Friend.AddRange(friends);
        if (users != null) dbContext.User.AddRange(users);
        dbContext.SaveChanges();
        return dbContext;
    }

    [Fact]
    public async Task GetFriends_ReturnsFriends_WhenFriendsExist()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Id = 1 },
            new User { Id = 2 },
            new User { Id = 3 } // Not a friend
        };
        var friends = new List<Friend>
        {
            new Friend { UserId = 1, FriendId = 2 }
        };
        var dbContext = CreateMockDbContext(friends, users);
        var controller = new FriendController(dbContext);

        // Act
        var result = await controller.GetFriends(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedFriends = Assert.IsAssignableFrom<List<User>>(okResult.Value);
        Assert.Single(returnedFriends);
        Assert.Equal(2, returnedFriends[0].Id);
    }

    [Fact]
    public async Task GetFriends_ReturnsNotFound_WhenNoFriendsExist()
    {
        // Arrange
        var dbContext = CreateMockDbContext(users: new List<User>()); // No friends
        var controller = new FriendController(dbContext);

        // Act
        var result = await controller.GetFriends(1);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal("No friends found for the given user.", notFoundResult.Value);
    }
    [Fact]
    public async Task AddFriend_AddsFriendship_WhenItDoesNotExist()
    {
        // Arrange
        var dbContext = CreateMockDbContext();
        var controller = new FriendController(dbContext);
        var formCollection = new FormCollection(
            new Dictionary<string, StringValues>
            {
            { "userId", "1" },
            { "friendId", "2" }
            });

        // Act
        var result = await controller.AddFriend(1, 2); // Pass parameters directly without [FromForm]

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Friend added successfully.", okResult.Value);
        var friendshipsInDb = await dbContext.Friend.ToListAsync();
        Assert.Single(friendshipsInDb);
        Assert.Equal(1, friendshipsInDb[0].UserId);
        Assert.Equal(2, friendshipsInDb[0].FriendId);
    }

    [Fact]
    public async Task AddFriend_ReturnsConflict_WhenFriendshipAlreadyExists()
    {
        // Arrange
        var existingFriendship = new Friend { UserId = 1, FriendId = 2 };
        var dbContext = CreateMockDbContext(new List<Friend> { existingFriendship });
        var controller = new FriendController(dbContext);

        // Act
        var result = await controller.AddFriend(1, 2); // Pass parameters directly without [FromForm]

        // Assert
        var conflictResult = Assert.IsType<ConflictObjectResult>(result);
        Assert.Equal("Friendship already exists.", conflictResult.Value);
    }

    [Fact]
    public async Task RemoveFriend_RemovesFriendship_WhenItExists()
    {
        // Arrange
        var existingFriendship = new Friend { UserId = 1, FriendId = 2 };
        var dbContext = CreateMockDbContext(new List<Friend> { existingFriendship });
        var controller = new FriendController(dbContext);

        // Act
        var result = await controller.RemoveFriend(1, 2); // Pass parameters directly without [FromForm]

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Friend removed successfully.", okResult.Value);
        var friendshipsInDb = await dbContext.Friend.ToListAsync();
        Assert.Empty(friendshipsInDb);
    }

    [Fact]
    public async Task RemoveFriend_ReturnsNotFound_WhenFriendshipDoesNotExist()
    {
        // Arrange
        var dbContext = CreateMockDbContext();
        var controller = new FriendController(dbContext);

        // Act
        var result = await controller.RemoveFriend(1, 2); // Pass parameters directly without [FromForm]

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Friendship not found.", notFoundResult.Value);
    }
}