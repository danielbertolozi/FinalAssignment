﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalAssignment.Models.Mappings
{
	public class AvailabilityMap
	{
		public AvailabilityMap(EntityTypeBuilder<Availability> entityBuilder)
		{
			entityBuilder.HasKey(t => t.AvailabilityKey);

			entityBuilder.Property(t => t.AvailabilityKey)
			             .ValueGeneratedOnAdd();

			entityBuilder.Property(t => t.DayOfWeek);
			entityBuilder.Property(t => t.EndTime);
			entityBuilder.Property(t => t.StartTime);
			entityBuilder.Property(t => t.IsAllDay);

			entityBuilder.ToTable("Availability");
			entityBuilder.Property(t => t.AvailabilityKey).HasColumnName("AvailabilityKey");
			entityBuilder.Property(t => t.DayOfWeek).HasColumnName("DayOfWeek");
			entityBuilder.Property(t => t.EndTime).HasColumnName("EndTime");
			entityBuilder.Property(t => t.StartTime).HasColumnName("StartTime");
			entityBuilder.Property(t => t.IsAllDay).HasColumnName("IsAllDay");

			entityBuilder.HasOne(t => t.Medics)
						 .WithMany(t => t.Availability)
			             .HasForeignKey(t => t.MedicKey);
		}
	}
}
