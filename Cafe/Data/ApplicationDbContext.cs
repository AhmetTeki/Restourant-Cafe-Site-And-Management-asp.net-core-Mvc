﻿using Cafe.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Catagory> Catagory { get; set; }
        public DbSet<Menu> Menu { get; set; }
		public DbSet<Rezervasyon> Rezervasyon { get; set; }
		public DbSet<Galeri> Galeri { get; set; }
		public DbSet<About> About { get; set; }
        public DbSet<Blog> Blog { get; set; }
		public DbSet<Contact> Contact { get; set; }
		public DbSet<ApplicationUser> ApplicationUser { get; set; }
	}
}