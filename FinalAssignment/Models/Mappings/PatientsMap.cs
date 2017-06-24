﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalAssignment.Models.Mappings
{
	public class PatientsMap
	{
		public PatientsMap(EntityTypeBuilder<Patients> entityBuilder)
		{
			entityBuilder.HasKey(t => t.PatientKey);

			entityBuilder.Property(t => t.PatientKey)
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
			entityBuilder.Property(t => t.PatientKey).HasColumnName("PatientKey");
			entityBuilder.Property(t => t.Name).HasColumnName("Name");
			entityBuilder.Property(t => t.Password).HasColumnName("Password");
			entityBuilder.Property(t => t.Phone).HasColumnName("Phone");
		}
	}
}
