using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupWebProject.Data;
using GroupWebProject.Models;
using System.Security.Claims;

namespace GroupWebProject.Pages.Bookings
{
    
    public class MyBookingsModel : PageModel
    {
        private readonly GroupWebProject.Data.ApplicationDbContext _context;

        public MyBookingsModel(GroupWebProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Booking> Booking { get;set; }

        public async Task OnGetAsync(string sort = "check_in_asc")
        {

            IQueryable<Booking> Bookings = _context.Booking
                .Include(b => b.TheCustomer)
                .Include(b => b.TheRoom);

            string _email = User.FindFirst(ClaimTypes.Name).Value;

            Bookings = Bookings.Where(b => b.CustomerEmail == _email);

            switch (sort)
            {
                case "check_in_asc":
                    Bookings = Bookings.OrderBy(p => p.CheckIn);
                    break;
                case "check_in_desc":
                    Bookings = Bookings.OrderByDescending(p => p.CheckIn);
                    break;
                case "price_asc":
                    Bookings = Bookings.OrderBy(p => (double)p.Cost);
                    break;
                case "price_desc":
                    Bookings = Bookings.OrderByDescending(p => (double)p.Cost);
                    break;
                default:
                    Bookings = Bookings.OrderBy(p => p.ID);
                    break;
            }

            ViewData["NextCheckInOrder"] = sort == "check_in_asc" ? "check_in_desc" : "check_in_asc";
            ViewData["NextCostOrder"] = sort == "price_asc" ? "price_desc" : "price_asc";

            Booking = await Bookings.AsNoTracking().ToListAsync();

        }
    }
}
