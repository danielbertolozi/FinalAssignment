﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalAssignment.Models.Mappings
{
	public class MedicsMap
	{
		public MedicsMap(EntityTypeBuilder<Medics> entityBuilder)
		{
			entityBuilder.HasKey(t => t.MedicKey);

			entityBuilder.Property(t => t.MedicKey)
			             .ValueGeneratedOnAdd();

			entityBuilder.Property(t => t.Address);
			entityBuilder.Property(t => t.BirthDate);
			entityBuilder.Property(t => t.Deleted);
			entityBuilder.Property(t => t.Email);
			entityBuilder.Property(t => t.Genre);
			entityBuilder.Property(t => t.Name);
			entityBuilder.Property(t => t.Password);
			entityBuilder.Property(t => t.Phone);

			entityBuilder.ToTable("Medics");
			entityBuilder.Property(t => t.Address).HasColumnName("Address");
			entityBuilder.Property(t => t.BirthDate).HasColumnName("BirthDate");
			entityBuilder.Property(t => t.Deleted).HasColumnName("Deleted");
			entityBuilder.Property(t => t.Email).HasColumnName("Email");
			entityBuilder.Property(t => t.Genre).HasColumnName("Genre");
			entityBuilder.Property(t => t.MedicKey).HasColumnName("MedicKey");
			entityBuilder.Property(t => t.Name).HasColumnName("Name");
			entityBuilder.Property(t => t.Password).HasColumnName("Password");
			entityBuilder.Property(t => t.Phone).HasColumnName("Phone");
		}
	}
}
