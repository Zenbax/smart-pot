namespace Cloud.Services;

public class CustomAuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public CustomAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Check if the request is for the AuthController
        if (context.Request.Path.StartsWithSegments("/auth"))
        {
            // If it is, call the next middleware and return
            await _next(context);
            return;
        }

        // If it's not for the AuthController, perform the custom authentication check
        if (!context.User.Identity.IsAuthenticated)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("401 error Unauthorized: please add a Header with the key \"Authorization\" and with the value \"Bearer <token>\"");
            return;
        }

        await _next(context);
    }
}