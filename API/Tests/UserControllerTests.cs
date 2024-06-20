using API.Controllers;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using RWA_Web_API_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class UserControllerTests
{
    private ApplicationDbContext CreateMockDbContext(List<User>? users = null, List<Technology>? technologies = null)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var dbContext = new ApplicationDbContext(options);
        if (users != null)
        {
            dbContext.User.AddRange(users);
            dbContext.SaveChanges();
        }
        if (technologies != null)
        {
            dbContext.Technology.AddRange(technologies);
            dbContext.SaveChanges();
        }
        return dbContext;
    }

    [Fact]
    public async Task GetUsers_ReturnsUsers_WhenUsersExist()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
            new User { Id = 2, FirstName = "Jane", LastName = "Doe", Email = "jane.doe@example.com" }
        };
        var dbContext = CreateMockDbContext(users);
        var controller = new UserController(dbContext);

        // Act
        var result = await controller.GetUsers();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedUsers = Assert.IsAssignableFrom<IEnumerable<User>>(okResult.Value);
        Assert.Equal(2, returnedUsers.Count());
    }

    [Fact]
    public async Task GetUsers_ReturnsNotFound_WhenNoUsersExist()
    {
        // Arrange
        var dbContext = CreateMockDbContext();
        var controller = new UserController(dbContext);

        // Act
        var result = await controller.GetUsers();

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetUser_ReturnsUser_WhenUserExists()
    {
        // Arrange
        var user = new User { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
        var dbContext = CreateMockDbContext(new List<User> { user });
        var controller = new UserController(dbContext);

        // Act
        var result = await controller.GetUser(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedUser = Assert.IsType<User>(okResult.Value);
        Assert.Equal(user.Id, returnedUser.Id);
        Assert.Equal(user.FirstName, returnedUser.FirstName);
        Assert.Equal(user.LastName, returnedUser.LastName);
        Assert.Equal(user.Email, returnedUser.Email);
    }

    [Fact]
    public async Task GetUser_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var dbContext = CreateMockDbContext();
        var controller = new UserController(dbContext);

        // Act
        var result = await controller.GetUser(1);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task PostUser_CreatesUser_AndReturnsCreatedAtAction()
    {
        // Arrange
        var dbContext = CreateMockDbContext();
        var controller = new UserController(dbContext);
        var newUser = new PostUser
        {
            FirstName = "New",
            LastName = "User",
            Email = "new.user@example.com",
            Username = "newuser",
            PhoneNumber = "1234567890",
            PwdHash = "password",
            KnownTechnologies = new List<PostTechnology> { new PostTechnology { Name = "Tech1", SkillLevel = "Beginner" } }
        };

        // Act
        var result = await controller.PostUser(newUser);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal("GetUser", createdAtActionResult.ActionName);
        var user = Assert.IsType<User>(createdAtActionResult.Value);
        Assert.Equal(newUser.FirstName, user.FirstName);
        Assert.Equal(newUser.LastName, user.LastName);
        Assert.Equal(newUser.Email, user.Email);
        Assert.Equal(newUser.Username, user.Username);

        // Check if the user was added to the database
        var usersInDb = await dbContext.User.ToListAsync();
        Assert.Single(usersInDb);
        Assert.Equal(newUser.FirstName, usersInDb[0].FirstName);
        Assert.Equal(newUser.LastName, usersInDb[0].LastName);
        Assert.Equal(newUser.Email, usersInDb[0].Email);
        Assert.Equal(newUser.Username, usersInDb[0].Username);
    }

    [Fact]
    public async Task PostUser_ReturnsConflict_WhenUserWithSameUsernameOrEmailExists()
    {
        // Arrange
        var existingUser = new User { Id = 1, Username = "existinguser", Email = "existing.user@example.com" };
        var dbContext = CreateMockDbContext(new List<User> { existingUser });
        var controller = new UserController(dbContext);
        var newUser = new PostUser
        {
            FirstName = "New",
            LastName = "User",
            Email = "existing.user@example.com",
            Username = "newuser",
            PhoneNumber = "1234567890",
            PwdHash = "password",
            KnownTechnologies = new List<PostTechnology> { new PostTechnology { Name = "Tech1", SkillLevel = "Beginner" } }
        };

        // Act
        var result = await controller.PostUser(newUser);

        // Assert
        var conflictResult = Assert.IsType<ConflictObjectResult>(result.Result);
        Assert.Equal("User with the same username or email already exists.", conflictResult.Value);
    }

    [Fact]
    public async Task PutUser_UpdatesUser_AndReturnsNoContent()
    {
        // Arrange
        var user = new User { Id = 1, FirstName = "Old", LastName = "User", Email = "old.user@example.com", Username = "olduser" };
        var dbContext = CreateMockDbContext(new List<User> { user });
        var controller = new UserController(dbContext);
        var updatedUser = new PutUser
        {
            FirstName = "Updated",
            LastName = "User",
            Email = "updated.user@example.com",
            Username = "updateduser",
            PhoneNumber = "0987654321",
            UserProfilePictureBase64 = "newImageBase64",
            KnownTechnologies = new List<PostTechnology> { new PostTechnology { Name = "Tech2", SkillLevel = "Advanced" } }
        };

        // Act
        var result = await controller.PutUser(1, updatedUser);

        // Assert
        Assert.IsType<NoContentResult>(result);

        // Check if the user was updated in the database
        var userInDb = await dbContext.User.Include(u => u.KnownTechnologies).FirstOrDefaultAsync(u => u.Id == 1);
        Assert.Equal(updatedUser.FirstName, userInDb.FirstName);
        Assert.Equal(updatedUser.LastName, userInDb.LastName);
        Assert.Equal(updatedUser.Email, userInDb.Email);
        Assert.Equal(updatedUser.Username, userInDb.Username);
        Assert.Equal(updatedUser.PhoneNumber, userInDb.PhoneNumber);
        Assert.Equal(updatedUser.UserProfilePictureBase64, userInDb.UserProfilePictureBase64);
        Assert.Single(userInDb.KnownTechnologies);
        Assert.Equal(updatedUser.KnownTechnologies[0].Name, userInDb.KnownTechnologies[0].Name);
    }

    [Fact]
    public async Task PutUser_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var dbContext = CreateMockDbContext();
        var controller = new UserController(dbContext);
        var updatedUser = new PutUser
        {
            FirstName = "Updated",
            LastName = "User",
            Email = "updated.user@example.com",
            Username = "updateduser",
            PhoneNumber = "0987654321",
            UserProfilePictureBase64 = "newImageBase64",
            KnownTechnologies = new List<PostTechnology> { new PostTechnology { Name = "Tech2", SkillLevel = "Advanced" } }
        };

        // Act
        var result = await controller.PutUser(1, updatedUser);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task DeleteUser_DeletesUser_AndReturnsUser()
    {
        // Arrange
        var user = new User { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
        var dbContext = CreateMockDbContext(new List<User> { user });
        var controller = new UserController(dbContext);

        // Act
        var result = await controller.DeleteUser(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var deletedUser = Assert.IsType<User>(okResult.Value);
        Assert.Equal(user.Id, deletedUser.Id);

        // Check if the user was removed from the database
        var usersInDb = await dbContext.User.ToListAsync();
        Assert.Empty(usersInDb);
    }

    [Fact]
    public async Task DeleteUser_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var dbContext = CreateMockDbContext();
        var controller = new UserController(dbContext);

        // Act
        var result = await controller.DeleteUser(1);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task Login_ReturnsUser_WhenCredentialsAreValid()
    {
        // Arrange
        string salt = Encryption.CreateSalt(8);
        string password = "password";
        var user = new User { Id = 1, Email = "user@example.com", PwdSalt = salt, PwdHash = Encryption.GenerateHash(password, salt) };
        var dbContext = CreateMockDbContext(new List<User> { user });
        var controller = new UserController(dbContext);

        // Act
        var result = await controller.Login("user@example.com", password);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var loggedInUser = Assert.IsType<User>(okResult.Value);
        Assert.Equal(user.Id, loggedInUser.Id);
    }

    [Fact]
    public async Task Login_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var dbContext = CreateMockDbContext();
        var controller = new UserController(dbContext);

        // Act
        var result = await controller.Login("user@example.com", "password");

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task Login_ReturnsUnauthorized_WhenPasswordIsInvalid()
    {
        // Arrange
        string salt = Encryption.CreateSalt(8);
        string password = "password";
        var user = new User { Id = 1, Email = "user@example.com", PwdSalt = salt, PwdHash = Encryption.GenerateHash(password, salt) };
        var dbContext = CreateMockDbContext(new List<User> { user });
        var controller = new UserController(dbContext);

        // Act
        var result = await controller.Login("user@example.com", "wrongpassword");

        // Assert
        Assert.IsType<UnauthorizedObjectResult>(result.Result);
    }

    [Fact]
    public async Task ChangePassword_ChangesPassword_WhenOldPasswordIsValid()
    {
        // Arrange
        string oldSalt = Encryption.CreateSalt(8);
        string oldPassword = "oldpassword";
        var user = new User { Id = 1, PwdSalt = oldSalt, PwdHash = Encryption.GenerateHash(oldPassword, oldSalt) };
        var dbContext = CreateMockDbContext(new List<User> { user });
        var controller = new UserController(dbContext);
        string newPassword = "newpassword";

        // Act
        var result = await controller.ChangePassword(1, oldPassword, newPassword);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Password changed successfully!", okResult.Value);

        // Check if the password was updated in the database
        var userInDb = await dbContext.User.FindAsync(1);
        Assert.True(Encryption.ValidatePassword(newPassword, userInDb.PwdSalt, userInDb.PwdHash));
    }

    [Fact]
    public async Task ChangePassword_ReturnsBadRequest_WhenOldPasswordIsInvalid()
    {
        // Arrange
        string oldSalt = Encryption.CreateSalt(8);
        string oldPassword = "oldpassword";
        var user = new User { Id = 1, PwdSalt = oldSalt, PwdHash = Encryption.GenerateHash(oldPassword, oldSalt) };
        var dbContext = CreateMockDbContext(new List<User> { user });
        var controller = new UserController(dbContext);
        string newPassword = "newpassword";

        // Act
        var result = await controller.ChangePassword(1, "wrongpassword", newPassword);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Invalid current password", badRequestResult.Value);
    }

    [Fact]
    public async Task ChangePassword_ReturnsUnauthorized_WhenUserDoesNotExist()
    {
        // Arrange
        var dbContext = CreateMockDbContext();
        var controller = new UserController(dbContext);
        string oldPassword = "oldpassword";
        string newPassword = "newpassword";

        // Act
        var result = await controller.ChangePassword(1, oldPassword, newPassword);

        // Assert
        Assert.IsType<UnauthorizedResult>(result);
    }

    [Fact]
    public async Task RegisterUser_CreatesUser_AndReturnsCreatedAtAction()
    {
        // Arrange
        var dbContext = CreateMockDbContext();
        var controller = new UserController(dbContext);
        var newUser = new RegisterUser
        {
            FirstName = "New",
            LastName = "User",
            Email = "new.user@example.com",
            Username = "newuser",
            PhoneNumber = "1234567890",
            PwdHash = "password",
            UserProfilePictureBase64 = "profilePictureBase64"
        };

        // Act
        var result = await controller.RegisterUser(newUser);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal("GetUser", createdAtActionResult.ActionName);
        var user = Assert.IsType<User>(createdAtActionResult.Value);
        Assert.Equal(newUser.FirstName, user.FirstName);
        Assert.Equal(newUser.LastName, user.LastName);
        Assert.Equal(newUser.Email, user.Email);
        Assert.Equal(newUser.Username, user.Username);

        // Check if the user was added to the database
        var usersInDb = await dbContext.User.ToListAsync();
        Assert.Single(usersInDb);
        Assert.Equal(newUser.FirstName, usersInDb[0].FirstName);
        Assert.Equal(newUser.LastName, usersInDb[0].LastName);
        Assert.Equal(newUser.Email, usersInDb[0].Email);
        Assert.Equal(newUser.Username, usersInDb[0].Username);
    }

    [Fact]
    public async Task RegisterUser_ReturnsConflict_WhenUserWithSameUsernameOrEmailExists()
    {
        // Arrange
        var existingUser = new User { Id = 1, Username = "existinguser", Email = "existing.user@example.com" };
        var dbContext = CreateMockDbContext(new List<User> { existingUser });
        var controller = new UserController(dbContext);
        var newUser = new RegisterUser
        {
            FirstName = "New",
            LastName = "User",
            Email = "existing.user@example.com",
            Username = "newuser",
            PhoneNumber = "1234567890",
            PwdHash = "password",
            UserProfilePictureBase64 = "profilePictureBase64"
        };

        // Act
        var result = await controller.RegisterUser(newUser);

        // Assert
        var conflictResult = Assert.IsType<ConflictObjectResult>(result);
        Assert.Equal("User with the same username or email already exists.", conflictResult.Value);
    }
}
