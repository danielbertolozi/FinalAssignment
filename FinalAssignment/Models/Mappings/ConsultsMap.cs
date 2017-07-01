﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalAssignment.Models.Mappings
{
	public class ConsultsMap
	{
		public ConsultsMap(EntityTypeBuilder<Consults> entityBuilder)
		{
			entityBuilder.HasKey(t => t.ConsultKey);

			entityBuilder.Property(t => t.ConsultKey)
			             .ValueGeneratedOnAdd();

			entityBuilder.Property(t => t.Title);
			entityBuilder.Property(t => t.Description);
			entityBuilder.Property(t => t.Duration);
			entityBuilder.Property(t => t.Date);
			entityBuilder.Property(t => t.Classification);

			entityBuilder.ToTable("Consults");
			entityBuilder.Property(t => t.ConsultKey).HasColumnName("ConsultKey");
			entityBuilder.Property(t => t.Title).HasColumnName("Title");
			entityBuilder.Property(t => t.Description).HasColumnName("Description");
			entityBuilder.Property(t => t.Duration).HasColumnName("Duration");
			entityBuilder.Property(t => t.Date).HasColumnName("Date");
			entityBuilder.Property(t => t.Classification).HasColumnName("Classification");

			entityBuilder.HasOne(t => t.Medic)
						 .WithMany(t => t.Consults)
			             .HasForeignKey(t => t.MedicKey);

			entityBuilder.HasOne(t => t.Patient)
						 .WithMany(t => t.Consults)
			             .HasForeignKey(t => t.PatientKey);
		}
	}
}
