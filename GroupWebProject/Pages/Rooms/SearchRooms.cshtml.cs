using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupWebProject.Data;
using GroupWebProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.Sqlite;

namespace GroupWebProject.Pages.Rooms
{
    public class SearchRoomsModel : PageModel
    {
        private readonly GroupWebProject.Data.ApplicationDbContext _context;

        public SearchRoomsModel(GroupWebProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Room> Rooms { get;set; }

        [BindProperty]
        public RoomSearch RoomSearch { get; set; }

        

        public void OnGet()
        {
            ViewData["BedCount"] = new SelectList(new[]
                        {
                            new { BedCount = "1", Name = "1 bed" },
                            new { BedCount = "2", Name = "2 bed" },
                            new { BedCount = "3", Name = "3 bed" },
                        },
                        "BedCount", "Name", 1);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["BedCount"] = new SelectList(new[]
{
                            new { BedCount = "1", Name = "1 bed" },
                            new { BedCount = "2", Name = "2 bed" },
                            new { BedCount = "3", Name = "3 bed" },
                        },
                        "BedCount", "Name");

            if (!ModelState.IsValid)
            {
                return Page();
            }


            var search = new SqliteParameter("search", RoomSearch.BedCount);

            var searchQuery = _context.Room.FromSqlRaw("SELECT * FROM [Room] " +
                "WHERE [Room].BedCount = @search", search);

            /* The structure of your single SQL query should be as follows: first,
            use a main query to find all rooms satisfying the bed count requirement; then, 
            use a subquery to find all rooms that have overlapping with the customer’s 
            intended period; finally, use the NOT IN operator to exclude those rooms found
            by thesubquery from the main query. */

            //find avaible bookins
            //select rooms rooms where  
            //find rooms with bedcount

            //SELECT [Room].* FROM [Room] WHERE [Room].BedCount = (roomsearch.bed),

            //SELECT BookingRoom.id 



            //   await _context.RoomQuery.ToListAsync();

            Rooms = await searchQuery.ToListAsync();

            return Page();
        }
    }
}
