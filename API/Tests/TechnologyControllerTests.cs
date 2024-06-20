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

public class TechnologyControllerTests
{
    private ApplicationDbContext CreateMockDbContext(List<Technology>? technologies = null)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var dbContext = new ApplicationDbContext(options);
        if (technologies != null)
        {
            dbContext.Technology.AddRange(technologies);
            dbContext.SaveChanges();
        }
        return dbContext;
    }

    [Fact]
    public async Task GetTechnologies_ReturnsTechnologies_WhenTechnologiesExist()
    {
        // Arrange
        var technologies = new List<Technology>
        {
            new Technology { Id = 1, Name = "Tech 1", SkillLevel = "Beginner" },
            new Technology { Id = 2, Name = "Tech 2", SkillLevel = "Advanced" }
        };
        var dbContext = CreateMockDbContext(technologies);
        var controller = new TechnologyController(dbContext);

        // Act
        var result = await controller.GetTechnologies();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedTechnologies = Assert.IsAssignableFrom<IEnumerable<Technology>>(okResult.Value);
        Assert.Equal(2, returnedTechnologies.Count());
    }

    [Fact]
    public async Task GetTechnologies_ReturnsNotFound_WhenNoTechnologiesExist()
    {
        // Arrange
        var dbContext = CreateMockDbContext();
        var controller = new TechnologyController(dbContext);

        // Act
        var result = await controller.GetTechnologies();

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetTechnology_ReturnsTechnology_WhenTechnologyExists()
    {
        // Arrange
        var technology = new Technology { Id = 1, Name = "Tech 1", SkillLevel = "Beginner" };
        var dbContext = CreateMockDbContext(new List<Technology> { technology });
        var controller = new TechnologyController(dbContext);

        // Act
        var result = await controller.GetTechnology(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedTechnology = Assert.IsType<Technology>(okResult.Value);
        Assert.Equal(technology.Id, returnedTechnology.Id);
        Assert.Equal(technology.Name, returnedTechnology.Name);
        Assert.Equal(technology.SkillLevel, returnedTechnology.SkillLevel);
    }

    [Fact]
    public async Task GetTechnology_ReturnsNotFound_WhenTechnologyDoesNotExist()
    {
        // Arrange
        var dbContext = CreateMockDbContext();
        var controller = new TechnologyController(dbContext);

        // Act
        var result = await controller.GetTechnology(1);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task PostTechnology_CreatesTechnology_AndReturnsCreatedAtAction()
    {
        // Arrange
        var dbContext = CreateMockDbContext();
        var controller = new TechnologyController(dbContext);
        var newTechnology = new PostTechnology
        {
            Name = "New Tech",
            SkillLevel = "Intermediate"
        };

        // Act
        var result = await controller.PostTechnology(newTechnology);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal("GetTechnology", createdAtActionResult.ActionName);
        var technology = Assert.IsType<Technology>(createdAtActionResult.Value);
        Assert.Equal(newTechnology.Name, technology.Name);
        Assert.Equal(newTechnology.SkillLevel, technology.SkillLevel);

        // Check if the technology was added to the database
        var technologiesInDb = await dbContext.Technology.ToListAsync();
        Assert.Single(technologiesInDb);
        Assert.Equal(newTechnology.Name, technologiesInDb[0].Name);
        Assert.Equal(newTechnology.SkillLevel, technologiesInDb[0].SkillLevel);
    }

    [Fact]
    public async Task PutTechnology_UpdatesTechnology_AndReturnsNoContent()
    {
        // Arrange
        var technology = new Technology { Id = 1, Name = "Old Tech", SkillLevel = "Beginner" };
        var dbContext = CreateMockDbContext(new List<Technology> { technology });
        var controller = new TechnologyController(dbContext);
        var updatedTechnology = new PostTechnology
        {
            Name = "Updated Tech",
            SkillLevel = "Advanced"
        };
        // Act
        var result = await controller.PutTechnology(1, updatedTechnology);

        // Assert
        Assert.IsType<NoContentResult>(result);

        // Check if the technology was updated in the database
        var technologyInDb = await dbContext.Technology.FindAsync(1);
        Assert.Equal(updatedTechnology.Name, technologyInDb.Name);
        Assert.Equal(updatedTechnology.SkillLevel, technologyInDb.SkillLevel);
    }

    [Fact]
    public async Task PutTechnology_ReturnsNotFound_WhenTechnologyDoesNotExist()
    {
        // Arrange
        var dbContext = CreateMockDbContext();
        var controller = new TechnologyController(dbContext);
        var updatedTechnology = new PostTechnology
        {
            Name = "Updated Tech",
            SkillLevel = "Advanced"
        };

        // Act
        var result = await controller.PutTechnology(1, updatedTechnology);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteTechnology_DeletesTechnology_AndReturnsTechnology()
    {
        // Arrange
        var technology = new Technology { Id = 1, Name = "Tech 1", SkillLevel = "Beginner" };
        var dbContext = CreateMockDbContext(new List<Technology> { technology });
        var controller = new TechnologyController(dbContext);

        // Act
        var result = await controller.DeleteTechnology(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var deletedTechnology = Assert.IsType<Technology>(okResult.Value);
        Assert.Equal(technology.Id, deletedTechnology.Id);

        // Check if the technology was removed from the database
        var technologiesInDb = await dbContext.Technology.ToListAsync();
        Assert.Empty(technologiesInDb);
    }

    [Fact]
    public async Task DeleteTechnology_ReturnsNotFound_WhenTechnologyDoesNotExist()
    {
        // Arrange
        var dbContext = CreateMockDbContext();
        var controller = new TechnologyController(dbContext);

        // Act
        var result = await controller.DeleteTechnology(1);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
}
