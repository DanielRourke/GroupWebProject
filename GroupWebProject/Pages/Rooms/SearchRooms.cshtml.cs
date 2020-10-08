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
           ViewData["BedCount"]  = new SelectList(new[]
                        {
                            new { BedCount = "1", Name = "1 bed" },
                            new { BedCount = "2", Name = "2 beds" },
                            new { BedCount = "3", Name = "3 beds" },
                        },
                        "BedCount", "Name", 1);

        }

        public async Task<IActionResult> OnPostAsync()
        {
            int selected = 1;
            if (RoomSearch != null)
            {
                selected = RoomSearch.BedCount;
            }

            ViewData["BedCount"] = new SelectList(new[]
            {
                            new { BedCount = "1", Name = "1 bed" },
                            new { BedCount = "2", Name = "2 beds" },
                            new { BedCount = "3", Name = "3 beds" },
                        },
            "BedCount", "Name", selected);



            if (!ModelState.IsValid)
            {
                ViewData["Error"] = "You did something you were not meant to do";
                return Page();
            }
            else if (RoomSearch.CheckIn < DateTime.Today)
            {
                ViewData["Error"] = "Check in Date Must be in the future";
                return Page();
            }
            else if (RoomSearch.CheckIn > RoomSearch.CheckOut)
            {
                ViewData["Error"] = "Check Out Date Must after Check In Date";
                return Page();
            }

            //raw sql
            var bedCount = new SqliteParameter("bedCount", RoomSearch.BedCount);
            var checkIn = new SqliteParameter("checkIn", RoomSearch.CheckIn);
            var checkOut = new SqliteParameter("checkOut", RoomSearch.CheckOut);



            String query = "SELECT [Room].* FROM Room " +
                            "WHERE [Room].BedCount = @bedCount ";

            String subQuery = "(SELECT [Room].ID " +
                              "FROM [Room] " +
                              "INNER JOIN [Booking] " +
                              "ON [Room].ID = [Booking].RoomId " +
                              "WHERE @checkIn < Booking.Checkout " +
                              "AND Booking.CheckIn < @checkOut ) ";



            String notQuery = query + " AND [Room].ID NOT IN " + subQuery;


            var searchQuery = _context.Room.FromSqlRaw(notQuery, bedCount, checkIn ,checkOut);


            Rooms = await searchQuery.ToListAsync();

            return Page();
        }
    }
}
