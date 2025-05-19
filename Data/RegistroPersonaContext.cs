using System;
using System.Collections.Generic;
using Backend_Analisis.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_Analisis.Data;

public partial class RegistroPersonaContext : DbContext
{
    public RegistroPersonaContext()
    {
    }

    public RegistroPersonaContext(DbContextOptions<RegistroPersonaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Persona__3214EC074109B418");

            entity.ToTable("Persona");

            entity.HasIndex(e => new { e.PrimerNombre, e.PrimerApellido }, "IX_Persona_Nombre");

            entity.Property(e => e.FechaDeNacimiento).HasColumnName("Fecha de nacimiento");
            entity.Property(e => e.Genero).HasMaxLength(20);
            entity.Property(e => e.PrimerApellido)
                .HasMaxLength(50)
                .HasColumnName("Primer Apellido");
            entity.Property(e => e.PrimerNombre)
                .HasMaxLength(50)
                .HasColumnName("Primer Nombre");
            entity.Property(e => e.SegundoApellido)
                .HasMaxLength(50)
                .HasColumnName("Segundo Apellido");
            entity.Property(e => e.SegundoNombre)
                .HasMaxLength(50)
                .HasColumnName("Segundo Nombre");
            entity.Property(e => e.TipoDeSangre)
                .HasMaxLength(3)
                .HasColumnName("Tipo de sangre");

            entity.HasOne(d => d.Region).WithMany(p => p.Personas)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Persona_Region");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Region__3214EC07339026BA");

            entity.ToTable("Region");

            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
