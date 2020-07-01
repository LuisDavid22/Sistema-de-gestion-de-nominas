using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Sistema_de_gestion_de_nominas.Models
{
    public partial class nominaDBContext : DbContext
    {
        public nominaDBContext()
        {
        }

        public nominaDBContext(DbContextOptions<nominaDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Payroll> Payroll { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-HEFCM55;Initial Catalog=nominaDB;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Genre)
                    .HasColumnName("genre")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.GrossSalary)
                    .HasColumnName("gross_salary")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Payroll>(entity =>
            {
                entity.ToTable("payroll");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_id");

                entity.Property(e => e.GrossSalary)
                    .HasColumnName("gross_salary")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.NetIncome)
                    .HasColumnName("net_income")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.RetentionAfp).HasColumnName("retention_afp");

                entity.Property(e => e.RetentionArs).HasColumnName("retention_ars");

                entity.Property(e => e.RetentionIsr).HasColumnName("retention_isr");

                entity.Property(e => e.RetentionTotal).HasColumnName("retention_total");

                entity.Property(e => e.TaxableSalary)
                    .HasColumnName("taxable_salary")
                    .HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Payroll)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_payroll_payroll");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
