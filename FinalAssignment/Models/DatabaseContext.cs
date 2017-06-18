using System;
using Microsoft.EntityFrameworkCore;

namespace FinalAssignment.Models
{
	public class DatabaseContext : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			// TODO here goes all the PGSQL Stuff, will do it later
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}

	}
}
