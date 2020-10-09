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

namespace GroupWebProject.Pages.Rooms
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly GroupWebProject.Data.ApplicationDbContext _context;

        public IndexModel(GroupWebProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Room> Room { get;set; }

        public async Task OnGetAsync()
        {
            Room = await _context.Room.ToListAsync();
        }
    }
}
