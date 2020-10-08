using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GroupWebProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GroupWebProject.Pages.Customers
{
    [Authorize(Roles = "Customers")]
    public class MyDetailsModel : PageModel
    {

        private readonly GroupWebProject.Data.ApplicationDbContext _context;

        public MyDetailsModel(GroupWebProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CustomerDetails CustomerDetails { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            string _email = User.FindFirst(ClaimTypes.Name).Value;

            Customer Customer = await _context.Customer.FirstOrDefaultAsync(m => m.Email == _email);

            if (Customer == null)
            {
                ViewData["ExistInDB"] = "false";
            }
            else
            {
                ViewData["ExistInDB"] = "true";
                CustomerDetails = new CustomerDetails
                {
                    GivenName = Customer.GivenName,
                    Surname = Customer.Surname,
                    Postcode = Customer.Postcode
                };
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string _email = User.FindFirst(ClaimTypes.Name).Value;

            Customer Customer = await _context.Customer.FirstOrDefaultAsync(m => m.Email == _email);

            if (Customer == null)
            {
                ViewData["ExistInDB"] = "false";
                Customer = new Customer
                {
                    Email = _email
                };
            }
            else
            {
                ViewData["ExistInDB"] = "true";
            }

            Customer.GivenName = CustomerDetails.GivenName;
            Customer.Surname = CustomerDetails.Surname;
            Customer.Postcode = CustomerDetails.Postcode;

            if ((string)ViewData["ExistInDB"] == "true")
            {
                _context.Attach(Customer).State = EntityState.Modified;
            }
            else
            {
                _context.Customer.Add(Customer);
            }



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(Customer.Email))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            ViewData["SuccessDB"] = "success";
            return Page();
        }

        private bool CustomerExists(string id)
        {
            return _context.Customer.Any(e => e.Email == id);
        }
    }
}
