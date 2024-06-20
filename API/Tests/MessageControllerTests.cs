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

public class MessageControllerTests
{
    private ApplicationDbContext CreateMockDbContext(List<Message>? messages = null)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var dbContext = new ApplicationDbContext(options);
        if (messages != null)
        {
            dbContext.Message.AddRange(messages);
            dbContext.SaveChanges();
        }
        return dbContext;
    }

    [Fact]
    public async Task GetMessages_ReturnsMessages_WhenMessagesExist()
    {
        // Arrange
        var messages = new List<Message>
        {
            new Message { SenderId = 1, ReceiverId = 2, Content = "Hello" },
            new Message { SenderId = 2, ReceiverId = 1, Content = "Hi there" }
        };
        var dbContext = CreateMockDbContext(messages);
        var controller = new MessageController(dbContext);

        // Act
        var result = await controller.GetMessages(1, 2);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedMessages = Assert.IsAssignableFrom<IEnumerable<Message>>(okResult.Value);
        Assert.Equal(2, returnedMessages.Count());
    }

    [Fact]
    public async Task GetMessages_ReturnsEmptyList_WhenNoMessagesExist()
    {
        // Arrange
        var dbContext = CreateMockDbContext();
        var controller = new MessageController(dbContext);

        // Act
        var result = await controller.GetMessages(1, 2);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedMessages = Assert.IsAssignableFrom<IEnumerable<Message>>(okResult.Value);
        Assert.Empty(returnedMessages);
    }

    [Fact]
    public async Task SendMessage_CreatesMessage_AndReturnsCreatedAtAction()
    {
        // Arrange
        var dbContext = CreateMockDbContext();
        var controller = new MessageController(dbContext);

        // Act
        var result = await controller.SendMessage(1, 2, "Test message");

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal("GetMessages", createdAtActionResult.ActionName);
        var message = Assert.IsType<Message>(createdAtActionResult.Value);
        Assert.Equal(1, message.SenderId);
        Assert.Equal(2, message.ReceiverId);
        Assert.Equal("Test message", message.Content);

        // Check if the message was added to the database
        var messagesInDb = await dbContext.Message.ToListAsync();
        Assert.Single(messagesInDb);
        Assert.Equal("Test message", messagesInDb[0].Content);
    }

    [Fact]
    public async Task SendMessage_ReturnsBadRequest_WhenExceptionOccurs()
    {
        // Arrange
        var mockDbContext = new Mock<ApplicationDbContext>();
        mockDbContext.Setup(db => db.Message.Add(It.IsAny<Message>()))
            .Throws(new Exception("Database error"));
        var controller = new MessageController(mockDbContext.Object);

        // Act
        var result = await controller.SendMessage(1, 2, "Test message");

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Failed to send message", ((dynamic)badRequestResult.Value).message);
    }
}
