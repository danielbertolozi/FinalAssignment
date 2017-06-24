﻿using System;
using Microsoft.EntityFrameworkCore;

namespace FinalAssignment.Models
{
	public class DatabaseContext : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Filename=Database.db");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}

	}
}
