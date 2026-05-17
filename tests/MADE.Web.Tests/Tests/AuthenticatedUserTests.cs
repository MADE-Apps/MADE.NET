using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using MADE.Web.Identity;
using NUnit.Framework;
using Shouldly;

namespace MADE.Web.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class AuthenticatedUserTests
{
    public class WhenCreatingWithClaims
    {
        [Test]
        public void ShouldSetSubjectFromClaims()
        {
            // Arrange
            var claims = new List<Claim> { new(AuthenticatedUser.SubjectClaimType, "user-123") };
            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims));

            // Act
            var user = new AuthenticatedUser(principal);

            // Assert
            user.Subject.ShouldBe("user-123");
        }

        [Test]
        public void ShouldSetEmailFromClaims()
        {
            // Arrange
            var claims = new List<Claim> { new(AuthenticatedUser.EmailClaimType, "user@example.com") };
            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims));

            // Act
            var user = new AuthenticatedUser(principal);

            // Assert
            user.Email.ShouldBe("user@example.com");
        }

        [Test]
        public void ShouldSetRolesFromClaims()
        {
            // Arrange
            var claims = new List<Claim>
            {
                new(AuthenticatedUser.RoleClaimType, "Admin"),
                new(AuthenticatedUser.RoleClaimType, "User"),
            };
            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims));

            // Act
            var user = new AuthenticatedUser(principal);

            // Assert
            user.Roles.ShouldNotBeNull();
            user.Roles.ShouldContain("Admin");
            user.Roles.ShouldContain("User");
        }

        [Test]
        public void ShouldExposeClaimsPrincipal()
        {
            // Arrange
            var principal = new ClaimsPrincipal(new ClaimsIdentity());

            // Act
            var user = new AuthenticatedUser(principal);

            // Assert
            user.ClaimsPrincipal.ShouldBe(principal);
        }

        [Test]
        public void ShouldExposeAllClaims()
        {
            // Arrange
            var claims = new List<Claim>
            {
                new(AuthenticatedUser.SubjectClaimType, "user-123"),
                new(AuthenticatedUser.EmailClaimType, "user@example.com"),
                new("custom", "value"),
            };
            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims));

            // Act
            var user = new AuthenticatedUser(principal);

            // Assert
            user.Claims.ShouldNotBeNull();
            user.Claims.Count.ShouldBe(3);
        }

        [Test]
        public void ShouldReturnNullSubjectWhenClaimMissing()
        {
            // Arrange
            var principal = new ClaimsPrincipal(new ClaimsIdentity());

            // Act
            var user = new AuthenticatedUser(principal);

            // Assert
            user.Subject.ShouldBeNull();
        }

        [Test]
        public void ShouldReturnNullEmailWhenClaimMissing()
        {
            // Arrange
            var principal = new ClaimsPrincipal(new ClaimsIdentity());

            // Act
            var user = new AuthenticatedUser(principal);

            // Assert
            user.Email.ShouldBeNull();
        }
    }
}
