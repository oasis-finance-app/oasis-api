using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Oasis.DTOs;

public class BankAccountCreateDto
{
    [Required]
    [DefaultValue("Nubank")]
    public string AccountName { get; set; }

    [Required]
    [DefaultValue(1)]
    public int Bank { get; set; }

    public string OtherBankName { get; set; }
}

public class BankAccountResponseDto
{
    public int BankAccountId { get; set; }
    public string AccountName { get; set; }
    public Oasis.Enums.BankName Bank { get; set; }
    public string OtherBankName { get; set; }
}

public class BankAccountDto
{
    public int BankAccountId { get; set; }
    public string AccountName { get; set; }
    public int Bank { get; set; }
    public string? OtherBankName { get; set; }
    public int CustomerId { get; set; }
}
