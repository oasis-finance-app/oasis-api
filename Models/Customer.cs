using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oasis.Models
{
	public class Customer
	{
		[Key]
		public int CustomerId { get; set; }

		[Required]
		[StringLength(100, MinimumLength = 6)]
		public required string Name { get; set; }

		[Required]
		[EmailAddress]
		public required string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public required string Password { get; set; }

		public ICollection<BankAccount>? BankAccounts { get; set; }
	}
}
