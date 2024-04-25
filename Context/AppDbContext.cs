using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using apiprojeto.Models;

namespace apiprojeto.Context;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Colaborador> Colaboradors { get; set; }

    public virtual DbSet<Entrega> Entregas { get; set; }

    public virtual DbSet<Epi> Epis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=bancoprojeto;UserId=postgres;Password=senai901;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Colaborador>(entity =>
        {
            entity.HasKey(e => e.IdCol).HasName("colaborador_pkey");

            entity.ToTable("colaborador");

            entity.HasIndex(e => e.Cpf, "idx_cpf");

            entity.HasIndex(e => e.Ctps, "idx_ctps");

            entity.HasIndex(e => e.IdCol, "idx_id_col");

            entity.HasIndex(e => e.Cpf, "uk_cpf").IsUnique();

            entity.HasIndex(e => e.Ctps, "uk_ctps").IsUnique();

            entity.Property(e => e.IdCol)
                .ValueGeneratedNever()
                .HasColumnName("id_col");
            entity.Property(e => e.Cpf).HasColumnName("cpf");
            entity.Property(e => e.Ctps)
                .HasMaxLength(50)
                .HasColumnName("ctps");
            entity.Property(e => e.DataAdmisão).HasColumnName("data_admisão");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.NomeCol)
                .HasMaxLength(100)
                .HasColumnName("nome_col");
            entity.Property(e => e.Telefone).HasColumnName("telefone");
        });

        modelBuilder.Entity<Entrega>(entity =>
        {
            entity.HasKey(e => e.IdEnt).HasName("entrega_pkey");

            entity.ToTable("entrega");

            entity.HasIndex(e => e.IdEnt, "idx_id_ent");

            entity.Property(e => e.IdEnt)
                .ValueGeneratedNever()
                .HasColumnName("id_ent");
            entity.Property(e => e.DataValidade).HasColumnName("data_validade");
            entity.Property(e => e.DateEntrega).HasColumnName("date_entrega");
            entity.Property(e => e.IdCol).HasColumnName("id_col");
            entity.Property(e => e.IdEpi).HasColumnName("id_epi");

            entity.HasOne(d => d.IdColNavigation).WithMany(p => p.Entregas)
                .HasForeignKey(d => d.IdCol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_col");

            entity.HasOne(d => d.IdEpiNavigation).WithMany(p => p.Entregas)
                .HasForeignKey(d => d.IdEpi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("id_epi");
        });

        modelBuilder.Entity<Epi>(entity =>
        {
            entity.HasKey(e => e.IdEpi).HasName("epi_pkey");

            entity.ToTable("epi");

            entity.HasIndex(e => e.IdEpi, "idx_id_epi");

            entity.Property(e => e.IdEpi)
                .ValueGeneratedNever()
                .HasColumnName("id_epi");
            entity.Property(e => e.InsUso)
                .HasMaxLength(1000)
                .HasColumnName("ins_uso");
            entity.Property(e => e.NomeEpi)
                .HasMaxLength(100)
                .HasColumnName("nome_epi");
            entity.Property(e => e.Qtd).HasColumnName("qtd");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
