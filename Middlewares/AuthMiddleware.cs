using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Oasis.Context;

public class AuthMiddleware
{
  private readonly RequestDelegate _next;

  public AuthMiddleware(RequestDelegate next)
  {
    _next = next;
  }

  public async Task Invoke(HttpContext context, EntityContext dbContext)
  {
    var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

    if (token != null)
      AttachUserToContext(context, dbContext, token);

    await _next(context);
  }

  private void AttachUserToContext(HttpContext context, EntityContext dbContext, string token)
  {
    try
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes("your_secret_key");
      tokenHandler.ValidateToken(token, new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
      }, out SecurityToken validatedToken);

      var jwtToken = (JwtSecurityToken)validatedToken;
      var customerId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

      context.Items["Customer"] = dbContext.Customers.Find(customerId);
    }
    catch
    {
    }
  }
}