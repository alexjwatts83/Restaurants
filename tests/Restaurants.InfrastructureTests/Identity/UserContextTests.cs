using Shouldly;

namespace Restaurants.Infrastructure.Identity.Tests;

public class UserContextTests
{
    [Fact()]
    public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
    {
        // arrange
        var dateOfBirth = new DateOnly(1990, 1, 1);

        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

        var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier, "1"),
                new(ClaimTypes.Email, "test@test.com"),
                new(ClaimTypes.Role, UserRoles.Admin),
                new(ClaimTypes.Role, UserRoles.User),
                new(AppClaimTypes.Nationality, "German"),
                new(AppClaimTypes.DateOfBirth, dateOfBirth.ToString("yyyy-MM-dd"))
            };

        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));

        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
        {
            User = user
        });

        var userContext = new UserContext(httpContextAccessorMock.Object);

        // act
        var currentUser = userContext.GetCurrentUser();


        // asset
        currentUser.ShouldNotBeNull();
        currentUser.Id.ShouldBe("1");
        currentUser.Email.ShouldBe("test@test.com");
        currentUser.Roles.ShouldContain(UserRoles.Admin, UserRoles.User);
        currentUser.Nationality.ShouldBe("German");
        currentUser.DateOfBirth.ShouldBe(dateOfBirth);
    }

    [Fact]
    public void GetCurrentUser_WithUserContextNotPresent_ThrowsInvalidOperationException()
    {
        // Arrange
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

        httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null);

        var userContext = new UserContext(httpContextAccessorMock.Object);

        // act
        Action action = () => userContext.GetCurrentUser();

        // assert 
        action.ShouldThrow<InvalidOperationException>().Message.ShouldBe("User context is not present");
    }
}