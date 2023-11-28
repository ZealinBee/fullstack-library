using IntegrifyLibrary.Domain;
using IntegrifyLibrary.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace IntegrifyLibrary.Controllers;
public class GoogleSignInController : ControllerBase {
     [HttpGet("/signin-google")]
     public IActionResult SignInGoogle(string code, string state)
    {
        // Handle the authorization code and state here
        // Example: Log the code and state to console
        Console.WriteLine($"Authorization Code: {code}");
        Console.WriteLine($"State: {state}");

        // Call a method to exchange the code for an access token
        // and then retrieve user information, check if the user exists, etc.
        // ...

        // Redirect to a different action or return a response
        return RedirectToAction("Index", "Home");
    }
}
