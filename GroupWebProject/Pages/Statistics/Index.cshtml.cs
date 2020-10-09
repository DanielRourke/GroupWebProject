using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupWebProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GroupWebProject.Pages.Statistics
{

    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly GroupWebProject.Data.ApplicationDbContext _context;

        public IndexModel(GroupWebProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<RoomBookingStatistic> RoomBookingStats;

        public IList<CustomerPostcodeStatistic> CustomerPostcodeStatistics;
        public async Task<PageResult> OnGetAsync()
        {
            var bookingGroup = _context.Booking.GroupBy(b => b.RoomID);
            RoomBookingStats = await bookingGroup.Select(r => new RoomBookingStatistic
            {
                Room = r.Key,
                BookingCount = r.Count()
            }).ToListAsync();


            var CustomerGroup = _context.Customer.GroupBy(c => c.Postcode);

            CustomerPostcodeStatistics = await CustomerGroup.Select(c => new CustomerPostcodeStatistic
            {
                Postcode = c.Key,
                CustomerCount = c.Count()
            }).ToListAsync();


            return Page();
        }
    }
}
