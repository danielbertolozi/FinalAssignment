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

			entityBuilder.Property(t => t.Classification);
			entityBuilder.Property(t => t.DateTime);

			entityBuilder.ToTable("Consults");
			entityBuilder.Property(t => t.ConsultKey).HasColumnName("ConsultKey");
			entityBuilder.Property(t => t.Classification).HasColumnName("Classification");
			entityBuilder.Property(t => t.DateTime).HasColumnName("DateTime");

			entityBuilder.HasOne(t => t.Medics)
			             .WithMany(t => t.Consults)
			             .HasForeignKey(t => t.ConsultKey);

			entityBuilder.HasOne(t => t.Patients)
			             .WithMany(t => t.Consults)
			             .HasForeignKey(t => t.ConsultKey);
		}
	}
}
