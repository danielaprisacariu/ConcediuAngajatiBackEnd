﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProiectASP.Models
{
    public partial class StrangerThingsContext : DbContext
    {
        public StrangerThingsContext()
        {
        }

        public StrangerThingsContext(DbContextOptions<StrangerThingsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Angajat> Angajats { get; set; } = null!;
        public virtual DbSet<Concediu> Concedius { get; set; } = null!;
        public virtual DbSet<Departament> Departaments { get; set; } = null!;
        public virtual DbSet<Functie> Functies { get; set; } = null!;
        public virtual DbSet<StareConcediu> StareConcedius { get; set; } = null!;
        public virtual DbSet<TipConcediu> TipConcedius { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server =ts2112\\SQLEXPRESS; Database =StrangerThings; User Id =internship2022; Password =int; MultipleActiveResultSets = true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Angajat>(entity =>
            {
                entity.ToTable("Angajat");

                entity.HasIndex(e => e.Cnp, "UQ__Angajat__D8361757CB9A2C8A")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cnp)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("cnp");

                entity.Property(e => e.DataAngajare)
                    .HasColumnType("datetime")
                    .HasColumnName("dataAngajare");

                entity.Property(e => e.DataNasterii)
                    .HasColumnType("datetime")
                    .HasColumnName("dataNasterii");

                entity.Property(e => e.DepartamentId).HasColumnName("departamentId");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.EsteAdmin)
                    .HasColumnName("esteAdmin")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FunctieId).HasColumnName("functieId");

                entity.Property(e => e.ManagerId).HasColumnName("managerId");

                entity.Property(e => e.No)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("no");

                entity.Property(e => e.NrTelefon)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nrTelefon");

                entity.Property(e => e.Nume)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nume");

                entity.Property(e => e.Parola)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("parola");

               // entity.Ignore(e => e.Poza);
                entity.Property(e => e.Poza).HasColumnName("poza");

                entity.Property(e => e.Prenume)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("prenume");

                entity.Property(e => e.Serie)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("serie");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.InverseManager)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("FK__Angajat__manager__38996AB5");

                entity.HasOne(d => d.Departament)
                    .WithMany(p => p.Angajats)
                    .HasForeignKey(d => d.DepartamentId)
                    .HasConstraintName("FK_Angajat_departamentId");

                entity.HasOne(d => d.Functie)
                    .WithMany(p => p.Angajats)
                    .HasForeignKey(d => d.FunctieId)
                    .HasConstraintName("FK_Angajat_functieId");
            });

            modelBuilder.Entity<Concediu>(entity =>
            {
                entity.ToTable("Concediu");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AngajatId).HasColumnName("angajatId");

                entity.Property(e => e.Comentarii).HasColumnName("comentarii");

                entity.Property(e => e.DataInceput)
                    .HasColumnType("datetime")
                    .HasColumnName("dataInceput");

                entity.Property(e => e.DataSfarsit)
                    .HasColumnType("datetime")
                    .HasColumnName("dataSfarsit");

                entity.Property(e => e.InlocuitorId).HasColumnName("inlocuitorId");

                entity.Property(e => e.StareConcediuId).HasColumnName("stareConcediuId");

                entity.Property(e => e.TipConcediuId).HasColumnName("tipConcediuId");

                entity.HasOne(d => d.Angajat)
                    .WithMany(p => p.ConcediuAngajats)
                    .HasForeignKey(d => d.AngajatId)
                    .HasConstraintName("FK__Concediu__angaja__4222D4EF");

                entity.HasOne(d => d.Inlocuitor)
                    .WithMany(p => p.ConcediuInlocuitors)
                    .HasForeignKey(d => d.InlocuitorId)
                    .HasConstraintName("FK__Concediu__inlocu__403A8C7D");

                entity.HasOne(d => d.StareConcediu)
                    .WithMany(p => p.Concedius)
                    .HasForeignKey(d => d.StareConcediuId)
                    .HasConstraintName("FK__Concediu__stareC__412EB0B6");

                entity.HasOne(d => d.TipConcediu)
                    .WithMany(p => p.Concedius)
                    .HasForeignKey(d => d.TipConcediuId)
                    .HasConstraintName("FK__Concediu__tipCon__3F466844");
            });

            modelBuilder.Entity<Departament>(entity =>
            {
                entity.ToTable("Departament");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Denumire)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Functie>(entity =>
            {
                entity.ToTable("Functie");

                entity.Property(e => e.Denumire)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StareConcediu>(entity =>
            {
                entity.ToTable("StareConcediu");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cod)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cod");

                entity.Property(e => e.Nume)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nume");
            });

            modelBuilder.Entity<TipConcediu>(entity =>
            {
                entity.ToTable("TipConcediu");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cod)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cod");

                entity.Property(e => e.Nume)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nume");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
