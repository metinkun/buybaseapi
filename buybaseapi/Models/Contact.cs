using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace buybaseapi.Models
{
    public class Contact
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50 , ErrorMessage = "Max FirstName legnth is 50")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Max LastName legnth is 50")]
        public string LastName { get; set; }

        [MaxLength(50, ErrorMessage = "Max Firm legnth is 50")]
        public string Firm { get; set; }

        public ICollection<ContactInformation> Informations { get; set; } = new HashSet<ContactInformation>();

    }
}
