using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupWebProject.Models
{
    public class CustomerPostcodeStatistic
    {
        [Display(Name="Postcode")]
        public String Postcode { get; set; }

        [Display(Name = "Number of Customers")]
        public int CustomerCount;

    }
}
