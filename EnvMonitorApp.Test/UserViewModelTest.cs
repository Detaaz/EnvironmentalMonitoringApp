using Xunit;
using EnvironmentalMonitoringApp.ViewModels;
using EnvironmentalMonitoringApp.Models;
using EnvironmentalMonitoringApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using EnvironmentalMonitoringApp;


namespace EnvMonitorApp.Test;

// This doesn't work, I tried a few things but the issue lies with not being able to call async. I'm probably missing something but I'm not sure what

public class UserViewModelTest
{
    private EnvMonitorDbContext InMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<EnvMonitorDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        var context = new EnvMonitorDbContext(options);
        return context;
    }

    [Fact]
    public void Save_RoleChange_ModifyDatabaseRecord()
    {
        // Arrange

        var context = InMemoryDbContext();
        var user = new Users
        {
            user_id = 1,
            first_name = "Ben",
            last_name = "Test",
            email = "Ben@Test.com",
            role = "Environmental Scientist",
            login_id = 1
        };

        context.Users.Add(user);
        context.SaveChanges();

        var viewModel = new UserViewModel(context, user)
        {
            SelectedRole = "Administrator"
        };

        // Act

        viewModel.Save();
        var updatedUser = context.Users.Find(1);

        // Assert
        Assert.Equal("Administrator", updatedUser.role);
    }
}