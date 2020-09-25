using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GroupWebProject.Models
{
    public class CustomerDetails
    {
        /// <summary>
        /// Given Name of customer
        /// </summary>
        [Required]
        [Display(Name = "Given Name")]
        [RegularExpression(@"[A-Z][a-z'-]{2,20}")]
        public string GivenName { get; set; }

        /// <summary>
        /// Surname of customer
        /// </summary>
        [Required]
        [RegularExpression(@"[A-Z][a-z'-]{2,20}")]
        public string Surname { get; set; }


        /// <summary>
        /// Combination of Given Name and Surename
        /// </summary>
        [NotMapped] // not mapping this property to database, but exist in memory
        public string FullName => $"{GivenName} {Surname}";

        /// <summary>
        /// Post Code
        /// </summary>
        [Required]
        [RegularExpression(@"^[0-8][0-9]{3}$",
            ErrorMessage = "PostCode must be 4 digits" +
            " and must not start with 9")]
        public string Postcode { get; set; }
    }
}
