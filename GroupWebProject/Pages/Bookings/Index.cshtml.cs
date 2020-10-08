using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupWebProject.Data;
using GroupWebProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace GroupWebProject.Pages.Bookings
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly GroupWebProject.Data.ApplicationDbContext _context;

        public IndexModel(GroupWebProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Booking> Booking { get;set; }

        public async Task OnGetAsync()
        {
            Booking = await _context.Booking
                .Include(b => b.TheCustomer)
                .Include(b => b.TheRoom).ToListAsync();
        }
    }
}
