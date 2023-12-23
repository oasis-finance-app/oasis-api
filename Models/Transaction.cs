﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oasis.Enums;

namespace Oasis.Models
{
	public class Transaction
	{
		[Key]
		public int TransactionId { get; set; }

		[Required]
		public DateTime Date { get; set; }

		[Required]
		public decimal Amount { get; set; }

		[Required]
		public TransactionType Type { get; set; }

		[StringLength(250)]
		public string? Description { get; set; }

		public int BankAccountId { get; set; }
		
		public required BankAccount BankAccount { get; set; }
	}
}
