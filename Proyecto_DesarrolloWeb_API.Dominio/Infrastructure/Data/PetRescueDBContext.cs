using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Proyecto_DesarrolloWeb_API.Dominio.Core.Entities;

#nullable disable

namespace Proyecto_DesarrolloWeb_API.Dominio.Infrastructure.Data
{
    public partial class PetRescueDBContext : DbContext
    {
        public PetRescueDBContext()
        {
        }

        public PetRescueDBContext(DbContextOptions<PetRescueDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LostPet> LostPet { get; set; }
        public virtual DbSet<Pet> Pet { get; set; }
        public virtual DbSet<PetAdoption> PetAdoption { get; set; }
        public virtual DbSet<PetAge> PetAge { get; set; }
        public virtual DbSet<PetBreed> PetBreed { get; set; }
        public virtual DbSet<PetImage> PetImage { get; set; }
        public virtual DbSet<PetSize> PetSize { get; set; }
        public virtual DbSet<PetType> PetType { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("DESKTOP-S1DROK0\\SQLEXPRESS;Database=PetRescueDB;Trusted_Connection=true;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<LostPet>(entity =>
            {
                entity.Property(e => e.DateOfFound).HasColumnType("date");

                entity.Property(e => e.DateOfLoss).HasColumnType("date");

                entity.Property(e => e.DescriptionOfFound).HasMaxLength(1000);

                entity.Property(e => e.DescriptionOfLoss).HasMaxLength(1000);

                entity.Property(e => e.IdPet).HasColumnName("idPet");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.RegistrationDate).HasColumnType("date");

                entity.Property(e => e.Status).HasMaxLength(10);
            });

            modelBuilder.Entity<Pet>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.RegistrationDate).HasColumnType("date");

                entity.Property(e => e.Sex).HasMaxLength(6);

                entity.Property(e => e.Temperament).HasMaxLength(1000);

                entity.Property(e => e.Video).HasMaxLength(500);

                entity.HasOne(d => d.IdPetAgeNavigation)
                    .WithMany(p => p.Pet)
                    .HasForeignKey(d => d.IdPetAge)
                    .HasConstraintName("FK_Pet_PetAge");

                entity.HasOne(d => d.IdPetBreedNavigation)
                    .WithMany(p => p.Pet)
                    .HasForeignKey(d => d.IdPetBreed)
                    .HasConstraintName("FK_Pet_PetBreed");

                entity.HasOne(d => d.IdPetSizeNavigation)
                    .WithMany(p => p.Pet)
                    .HasForeignKey(d => d.IdPetSize)
                    .HasConstraintName("FK_Pet_PetSize");

                entity.HasOne(d => d.IdPetTypeNavigation)
                    .WithMany(p => p.Pet)
                    .HasForeignKey(d => d.IdPetType)
                    .HasConstraintName("FK_Pet_PetType");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Pet)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_Pet_User");
            });

            modelBuilder.Entity<PetAdoption>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.RegistrationDate).HasColumnType("date");

                entity.Property(e => e.Status).HasMaxLength(10);
            });

            modelBuilder.Entity<PetAge>(entity =>
            {
                entity.Property(e => e.AgeRange).HasMaxLength(50);
            });

            modelBuilder.Entity<PetBreed>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<PetImage>(entity =>
            {
                entity.Property(e => e.Photo).HasColumnType("image");

                entity.HasOne(d => d.IdPetNavigation)
                    .WithMany(p => p.PetImage)
                    .HasForeignKey(d => d.IdPet)
                    .HasConstraintName("FK_PetImage_Pet");
            });

            modelBuilder.Entity<PetSize>(entity =>
            {
                entity.Property(e => e.Size).HasMaxLength(50);
            });

            modelBuilder.Entity<PetType>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(30);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Contra).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.RegistrationDate).HasColumnType("date");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
