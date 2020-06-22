using System.Collections.Generic;
using System.Linq;
using db.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApiPgGql.Controllers
{
	[ApiController]
	[Route("api/[controller]/[action]")]
	public class AccountController : ControllerBase
	{
		private readonly YogaDbContext _context;

		public AccountController(YogaDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		[Route("/api/accounts")]
		public List<Account> GetAll()
		{
			return _context.Account.ToList();
		}

		[HttpGet]
		[Route("/api/reservations/{id?}")]
		public List<Reservation> GetAccountReservations(string id)
		{
			return _context.Reservation.Where(r => r.AccountId.ToString() == id).ToList();
		}
	}
}