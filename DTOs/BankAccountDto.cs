using System.ComponentModel.DataAnnotations;
using Oasis.Models;

namespace Oasis.DTOs;


public class BankAccountDto
{
	[Key]
	public int BankAccountId { get; set; }

	[Required]
	public string AccountName { get; set; }

	[Required]
	public int BankId { get; set; }
	public Bank? Bank { get; set; }

	public string? OtherBankName { get; set; }

}
