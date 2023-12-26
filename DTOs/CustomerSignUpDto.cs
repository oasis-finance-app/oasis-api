using System.ComponentModel.DataAnnotations;

public class CustomerSignUpDto
{
  [Required]
  [EmailAddress]
  public required string Email { get; set; }

  [Required]
  [StringLength(100, MinimumLength = 6)]
  public required string Name { get; set; }

  [Required]
  [DataType(DataType.Password)]
  public required string Password { get; set; }
}