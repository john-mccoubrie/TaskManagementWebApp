using Microsoft.EntityFrameworkCore;
using TaskManagementWebApp.Controllers;
using TaskManagementWebApp.Models;
using TaskManagementWebApp.Data;


namespace TaskManagementWebAppUnitTests
{
    [TestClass]
    public class ProjectTasksApiTests
    {
        [TestMethod]
        public async Task GetTasks_ReturnsAllTasks()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new ApplicationDbContext(options))
            {
                context.ProjectTasks.Add(new ProjectTask {
                    ProjectTaskId = 1,
                    Title = "Task 1",
                    Description = "Description for Task 1",
                    DueDate = DateTime.Now.AddDays(10),
                    AssignedUserId = "user1",
                    Status = 0
                });
                context.ProjectTasks.Add(new ProjectTask {
                    ProjectTaskId = 2,
                    Title = "Task 2",
                    Description = "Description for Task 2",
                    DueDate = DateTime.Now.AddDays(20),
                    AssignedUserId = "user2",
                    Status = 0
                });
                await context.SaveChangesAsync();
            }

            // Use a clean instance of the context to run the test
            using (var context = new ApplicationDbContext(options))
            {
                var controller = new ProjectTasksApiController(context);

                // Act
                var result = await controller.GetTasks();

                // Assert
                Assert.AreEqual(2, result.Value.Count());
            }
        }
    }
}