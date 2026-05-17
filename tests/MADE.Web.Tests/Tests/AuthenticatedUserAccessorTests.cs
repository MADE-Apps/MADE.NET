using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using MADE.Web.Identity;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace MADE.Web.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class AuthenticatedUserAccessorTests
{
    public class WhenAccessingClaimsPrincipal
    {
        [Test]
        public void ShouldReturnUserFromHttpContext()
        {
            // Arrange
            var claims = new List<Claim> { new(AuthenticatedUser.SubjectClaimType, "user-456") };
            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims));

            var httpContext = new DefaultHttpContext { User = principal };
            var mockAccessor = new Mock<IHttpContextAccessor>();
            mockAccessor.Setup(x => x.HttpContext).Returns(httpContext);

            var accessor = new AuthenticatedUserAccessor(mockAccessor.Object);

            // Act
            var claimsPrincipal = accessor.ClaimsPrincipal;

            // Assert
            claimsPrincipal.ShouldBe(principal);
        }

        [Test]
        public void ShouldReturnAuthenticatedUserModel()
        {
            // Arrange
            var claims = new List<Claim>
            {
                new(AuthenticatedUser.SubjectClaimType, "user-789"),
                new(AuthenticatedUser.EmailClaimType, "test@example.com"),
            };
            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims));

            var httpContext = new DefaultHttpContext { User = principal };
            var mockAccessor = new Mock<IHttpContextAccessor>();
            mockAccessor.Setup(x => x.HttpContext).Returns(httpContext);

            var accessor = new AuthenticatedUserAccessor(mockAccessor.Object);

            // Act
            var user = accessor.AuthenticatedUser;

            // Assert
            user.Subject.ShouldBe("user-789");
            user.Email.ShouldBe("test@example.com");
        }
    }
}
