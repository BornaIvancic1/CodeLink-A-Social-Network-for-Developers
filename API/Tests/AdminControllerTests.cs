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

public class AdminControllerTests
{
    private ApplicationDbContext CreateMockDbContext(List<Admin>? admins = null)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var dbContext = new ApplicationDbContext(options);
        if (admins != null)
        {
            dbContext.Admin.AddRange(admins);
            dbContext.SaveChanges();
        }
        return dbContext;
    }

    [Fact]
    public async Task GetAdmins_ReturnsAdmins_WhenAdminsExist()
    {
        // Arrange
        var admins = new List<Admin>
        {
            new Admin { Id = 1, Username = "admin1", Email = "admin1@example.com" },
            new Admin { Id = 2, Username = "admin2", Email = "admin2@example.com" }
        };
        var dbContext = CreateMockDbContext(admins);
        var controller = new AdminController(dbContext);

        // Act
        var result = await controller.GetAdmins();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedAdmins = Assert.IsAssignableFrom<IEnumerable<Admin>>(okResult.Value);
        Assert.Equal(2, returnedAdmins.Count());
    }

    [Fact]
    public async Task GetAdmins_ReturnsNotFound_WhenNoAdminsExist()
    {
        // Arrange
        var dbContext = CreateMockDbContext();
        var controller = new AdminController(dbContext);

        // Act
        var result = await controller.GetAdmins();

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetAdmin_ReturnsAdmin_WhenAdminExists()
    {
        // Arrange
        var admin = new Admin { Id = 1, Username = "admin1", Email = "admin1@example.com" };
        var dbContext = CreateMockDbContext(new List<Admin> { admin });
        var controller = new AdminController(dbContext);

        // Act
        var result = await controller.GetAdmin(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedAdmin = Assert.IsType<Admin>(okResult.Value);
        Assert.Equal(admin.Id, returnedAdmin.Id);
        Assert.Equal(admin.Username, returnedAdmin.Username);
        Assert.Equal(admin.Email, returnedAdmin.Email);
    }

    [Fact]
    public async Task GetAdmin_ReturnsNotFound_WhenAdminDoesNotExist()
    {
        // Arrange
        var dbContext = CreateMockDbContext();
        var controller = new AdminController(dbContext);

        // Act
        var result = await controller.GetAdmin(1);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task PostAdmin_CreatesAdmin_AndReturnsCreatedAtAction()
    {
        // Arrange
        var dbContext = CreateMockDbContext();
        var controller = new AdminController(dbContext);
        var newAdmin = new PostAdmin
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            UserProfilePictureBase64 = "base64string",
            Username = "johndoe",
            PhoneNumber = "1234567890",
            PwdHash = "password"
        };

        // Act
        var result = await controller.PostAdmin(newAdmin);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal("GetAdmin", createdAtActionResult.ActionName);
        var admin = Assert.IsType<Admin>(createdAtActionResult.Value);
        Assert.Equal(newAdmin.Username, admin.Username);
        Assert.Equal(newAdmin.Email, admin.Email);

        // Check if the admin was added to the database
        var adminsInDb = await dbContext.Admin.ToListAsync();
        Assert.Single(adminsInDb);
        Assert.Equal(newAdmin.Username, adminsInDb[0].Username);
        Assert.Equal(newAdmin.Email, adminsInDb[0].Email);
    }

    [Fact]
    public async Task PostAdmin_ReturnsConflict_WhenAdminWithSameUsernameOrEmailExists()
    {
        // Arrange
        var existingAdmin = new Admin { Id = 1, Username = "johndoe", Email = "john.doe@example.com" };
        var dbContext = CreateMockDbContext(new List<Admin> { existingAdmin });
        var controller = new AdminController(dbContext);
        var newAdmin = new PostAdmin
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            UserProfilePictureBase64 = "base64string",
            Username = "johndoe",
            PhoneNumber = "1234567890",
            PwdHash = "password"
        };

        // Act
        var result = await controller.PostAdmin(newAdmin);

        // Assert
        var conflictResult = Assert.IsType<ConflictObjectResult>(result.Result);
        Assert.Equal("User with the same username or email already exists.", conflictResult.Value);
    }

    [Fact]
    public async Task PutAdmin_UpdatesAdmin_AndReturnsNoContent()
    {
        // Arrange
        var existingAdmin = new Admin { Id = 1, Username = "admin1", Email = "admin1@example.com" };
        var dbContext = CreateMockDbContext(new List<Admin> { existingAdmin });
        var controller = new AdminController(dbContext);
        var updatedAdmin = new PutAdmin
        {
            FirstName = "Jane",
            LastName = "Doe",
            Email = "jane.doe@example.com",
            UserProfilePictureBase64 = "newbase64string",
            Username = "janedoe",
            PhoneNumber = "0987654321"
        };

        // Act
        var result = await controller.PutAdmin(1, updatedAdmin);

        // Assert
        Assert.IsType<NoContentResult>(result);

        // Check if the admin was updated in the database
        var adminInDb = await dbContext.Admin.FindAsync(1);
        Assert.Equal(updatedAdmin.Username, adminInDb.Username);
        Assert.Equal(updatedAdmin.Email, adminInDb.Email);
    }

    [Fact]
    public async Task PutAdmin_ReturnsNotFound_WhenAdminDoesNotExist()
    {
        // Arrange
        var dbContext = CreateMockDbContext();
        var controller = new AdminController(dbContext);
        var updatedAdmin = new PutAdmin
        {
            FirstName = "Jane",
            LastName = "Doe",
            Email = "jane.doe@example.com",
            UserProfilePictureBase64 = "newbase64string",
            Username = "janedoe",
            PhoneNumber = "0987654321"
        };

        // Act
        var result = await controller.PutAdmin(1, updatedAdmin);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Admin not found.", notFoundResult.Value);
    }

    [Fact]
    public async Task DeleteAdmin_RemovesAdmin_AndReturnsAdmin()
    {
        // Arrange
        var admin = new Admin { Id = 1, Username = "admin1", Email = "admin1@example.com" };
        var dbContext = CreateMockDbContext(new List<Admin> { admin });
        var controller = new AdminController(dbContext);

        // Act
        var result = await controller.DeleteAdmin(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedAdmin = Assert.IsType<Admin>(okResult.Value);
        Assert.Equal(admin.Id, returnedAdmin.Id);

        // Check if the admin was removed from the database
        var adminsInDb = await dbContext.Admin.ToListAsync();
        Assert.Empty(adminsInDb);
    }

    [Fact]
    public async Task DeleteAdmin_ReturnsNotFound_WhenAdminDoesNotExist()
    {
        // Arrange
        var dbContext = CreateMockDbContext();
        var controller = new AdminController(dbContext);

        // Act
        var result = await controller.DeleteAdmin(1);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task Login_ReturnsAdmin_WhenCredentialsAreValid()
    {
        // Arrange
        var salt = Encryption.CreateSalt(8);
        var hashedPassword = Encryption.GenerateHash("password", salt);
        var admin = new Admin { Id = 1, Email = "admin@example.com", PwdSalt = salt, PwdHash = hashedPassword };
        var dbContext = CreateMockDbContext(new List<Admin> { admin });
        var controller = new AdminController(dbContext);

        // Act
        var result = await controller.Login("admin@example.com", "password");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedAdmin = Assert.IsType<Admin>(okResult.Value);
        Assert.Equal(admin.Id, returnedAdmin.Id);
    }

    [Fact]
    public async Task Login_ReturnsNotFound_WhenAdminDoesNotExist()
    {
        // Arrange
        var dbContext = CreateMockDbContext();
        var controller = new AdminController(dbContext);

        // Act
        var result = await controller.Login("nonexistent@example.com", "password");

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task Login_ReturnsUnauthorized_WhenPasswordIsInvalid()
    {
        // Arrange
        var salt = Encryption.CreateSalt(8);
        var hashedPassword = Encryption.GenerateHash("password", salt);
        var admin = new Admin { Id = 1, Email = "admin@example.com", PwdSalt = salt, PwdHash = hashedPassword };
        var dbContext = CreateMockDbContext(new List<Admin> { admin });
        var controller = new AdminController(dbContext);

        // Act
        var result = await controller.Login("admin@example.com", "wrongpassword");

        // Assert
        Assert.IsType<UnauthorizedObjectResult>(result.Result);
    }
}
