using System;
using Microsoft.EntityFrameworkCore;
namespace buybaseapi.Models
{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactInformation> ContactInformations { get; set; }

    }
}
