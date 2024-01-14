using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oasis.Context;
using Oasis.DTOs;
using Oasis.Models;

namespace Oasis.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]

  public class BankAccountController : ControllerBase
  {
    private readonly EntityContext _context;

    public BankAccountController(EntityContext context)
    {
      _context = context;
    }

    [HttpGet]
    public ActionResult Get()
    {
      if (HttpContext.Items["Customer"] is not Customer customer)
      {
        return NotFound("Cliente não encontrado.");
      }

      var bankAccounts = _context.BankAccounts
        .Where(ba => ba.CustomerId == customer.CustomerId)
        .Select(ba => new BankAccountDto
        {
            BankAccountId = ba.BankAccountId,
            AccountName = ba.AccountName,
            Bank = (int)ba.Bank,
            OtherBankName = ba.OtherBankName,
            CustomerId = ba.CustomerId
        })
        .ToList();
      return Ok(bankAccounts);
    }

    [HttpGet("{id}")]
    public ActionResult Get(int id)
    {
      var bankAccount = _context.BankAccounts.Find(id);
      if (bankAccount == null)
      {
        return NotFound();
      }

      var responseDto = new BankAccountResponseDto
      {
        BankAccountId = bankAccount.BankAccountId,
        AccountName = bankAccount.AccountName,
        Bank = bankAccount.Bank,
        OtherBankName = bankAccount.OtherBankName
      };

      return Ok(responseDto);
    }

    [HttpPost]
    public ActionResult Post([FromBody] BankAccountCreateDto bankAccountDto)
    {
      var customer = HttpContext.Items["Customer"] as Customer;
      if (customer == null)
      {
        return NotFound("Cliente não encontrado.");
      }

      var bankAccount = new BankAccount
      {
        AccountName = bankAccountDto.AccountName,
        Bank = (Enums.BankName)bankAccountDto.Bank,
        OtherBankName = bankAccountDto.OtherBankName,
        Customer = customer
      };

      _context.BankAccounts.Add(bankAccount);
      _context.SaveChanges();
      return CreatedAtAction(nameof(Get), new { id = bankAccount.BankAccountId }, bankAccount);
    }

  }
}
