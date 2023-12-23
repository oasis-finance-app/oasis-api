using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Oasis.Enums;

namespace Oasis.Models
{
    public class BankAccount
	{
		[Key]
		public int BankAccountId { get; set; }

		[Required]
		public required string AccountName { get; set; }

		[Required]
		public BankName Bank { get; set; }

		[StringLength(100)]
		public string? OtherBankName { get; set; }
		
		public int CustomerId { get; set; }
		public required Customer Customer { get; set; }

		public List<Transaction>? Transactions { get; set; }
	}
}
