using Microsoft.AspNetCore.Identity;
using Moq;
using MyMovieCollection.Core.Models;
using MyMovieCollection.Core.Repositories;
using MyMovieCollection.Core.Services;

namespace MyMovieCollection.Tests;

public class UserServiceTests
{
    [Fact]
    public async Task GetUserByIdAsync_SetIdNull_ThrowsArgumentNullException()
    {
        string userId = null;
        var store = new Mock<IUserStore<User>>();
        var userManagerMock = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
        var userService = new UserService(userManagerMock.Object);

        await Assert.ThrowsAsync<ArgumentNullException>(() => userService.GetUserByIdAsync(userId));
    }

    [Fact]
    public async Task GetUserByIdAsync_SetNotRealId_ThrowsArgumentNullException()
    {
        var userId = "test";
        var store = new Mock<IUserStore<User>>();
        var userManagerMock = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
        userManagerMock.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync((User?)null);
        var userService = new UserService(userManagerMock.Object);

        await Assert.ThrowsAsync<NullReferenceException>(() => userService.GetUserByIdAsync(userId));
    }

    
}