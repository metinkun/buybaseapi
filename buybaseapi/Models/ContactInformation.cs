using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace buybaseapi.Models
{
    public class ContactInformation
    {
        public enum ContactInformationType
        {
            Phone,
            Email,
            Location
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public ContactInformationType Type { get; set; }

        public string Content { get; set; }

        [Required]
        public Guid ContactId { get; set; }

    }
}
