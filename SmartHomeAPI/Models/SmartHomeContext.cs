﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SmartHomeAPI.Models
{
    public partial class SmartHomeContext : DbContext
    {
        public SmartHomeContext()
        {
        }

        public SmartHomeContext(DbContextOptions<SmartHomeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<Mobile> Mobiles { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>(entity =>
            {
                entity.Property(e => e.Type).HasMaxLength(50);

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Devices)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK_Devices_Rooms");
            });

            modelBuilder.Entity<Mobile>(entity =>
            {
                entity.Property(e => e.AppId).HasMaxLength(50);

                entity.Property(e => e.DeviceName).HasMaxLength(50);

                entity.Property(e => e.Uuid)
                    .HasMaxLength(50)
                    .HasColumnName("UUID");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rooms_RoomTypes");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rooms_Users");
            });

            modelBuilder.Entity<RoomType>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.Property(e => e.DateOfBirth).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Gender).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Setting)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.SettingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Settings");

                entity.HasMany(d => d.Mobiles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UsersMobile",
                        l => l.HasOne<Mobile>().WithMany().HasForeignKey("MobileId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UsersMobiles_Mobiles"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UsersMobiles_Users"),
                        j =>
                        {
                            j.HasKey("UserId", "MobileId");

                            j.ToTable("UsersMobiles");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}