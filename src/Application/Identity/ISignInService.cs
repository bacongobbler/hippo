using Hippo.Application.Common.Models;
using MediatR;

namespace Hippo.Application.Identity;

public interface ISignInService
{
    /// <summary>
    /// Attempts to sign in the current user using the provided password.
    /// </summary>
    Task<Result> PasswordSignInAsync(string username, string password, bool rememberMe = false);

    /// <summary>
    /// Signs the current user out of the application.
    /// </summary>
    Task<Unit> SignOutAsync();
}
