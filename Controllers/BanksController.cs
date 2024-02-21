using Microsoft.AspNetCore.Mvc;
using Oasis.Context;

namespace Oasis.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BanksController(EntityContext context) : ControllerBase
{
	[HttpGet]
	public ActionResult Get()
	{
		var banks = context.Banks
			.Select(b => new
			{
				Id = b.BankId,
				Name = b.Name
			})
			.ToList();
		return Ok(banks);
	}
}
