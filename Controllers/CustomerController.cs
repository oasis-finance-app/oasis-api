using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Oasis.Models;
using Oasis.Context;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Oasis.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CustomerController : ControllerBase
  {
    private readonly EntityContext _context;

    public CustomerController(EntityContext context)
    {
      _context = context;
    }

    [HttpPost("sign-in")]
    public async Task<ActionResult> Login(CustomerLoginDto loginDto)
    {
      var Customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == loginDto.Email);

      if(Customer == null)
      {
        return Unauthorized();
      }

      var hasher = new PasswordHasher<Customer>();
      var result = hasher.VerifyHashedPassword(null, Customer.Password, loginDto.Password);

      if(result == PasswordVerificationResult.Failed)
      {
        return Unauthorized();
      }

      var token = GenerateJwtToken(Customer);
      return Ok(new { token });
    }

    [HttpPost("sign-up")]
    public async Task<ActionResult<Customer>> SignUp(CustomerSignUpDto signUpDto)
    {
      if(!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var hasher = new PasswordHasher<Customer>();

      var customer = new Customer 
      {
        Email = signUpDto.Email,
        Name = signUpDto.Name,
        Password = signUpDto.Password
      };
      customer.Password = hasher.HashPassword(customer, signUpDto.Password);


      _context.Customers.Add(customer);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetCustomer(int id)
    {
      var customer = await _context.Customers.FindAsync(id);

      if (customer == null)
      {
          return NotFound();
      }

      return customer;
    }

    private string GenerateJwtToken(Customer customer)
    {
      var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1c00a1c21bab75249256cfbe41192cd7"));
      var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

      var claims = new[]
      {
          new Claim(JwtRegisteredClaimNames.Sub, customer.Email),
          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
          new Claim("id", customer.CustomerId.ToString())
      };

      var token = new JwtSecurityToken(
          // issuer: "localhost",
          // audience: "localhost",
          claims: claims,
          expires: DateTime.Now.AddDays(7),
          signingCredentials: credentials
      );

      return new JwtSecurityTokenHandler().WriteToken(token);
    }
  }
}

public class CustomerLoginDto
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}
