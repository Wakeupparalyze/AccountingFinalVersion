using System;
using System.Collections.Generic;
using Accounting.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Accounting.DB
{
    public partial class accountingContext : DbContext
    {
        private static accountingContext instance;

        public accountingContext()
        {
        }

        public accountingContext(DbContextOptions<accountingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Budget> Budgets { get; set; } = null!;
        public virtual DbSet<Finance> Finances { get; set; } = null!;
        public virtual DbSet<IncomeOrExpense> IncomeOrExpenses { get; set; } = null!;
        public virtual DbSet<TypeExpense> TypeExpenses { get; set; } = null!;
        public virtual DbSet<TypeIncome> TypeIncomes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=accounting;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Budget>(entity =>
            {
                entity.ToTable("Budget");

                entity.Property(e => e.IdFinance).HasColumnName("Id_Finance");

                entity.Property(e => e.IdUser).HasColumnName("Id_User");

                entity.Property(e => e.Total).HasColumnType("money");

                entity.HasOne(d => d.IdFinanceNavigation)
                    .WithMany(p => p.Budgets)
                    .HasForeignKey(d => d.IdFinance)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Budget_Finance");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Budgets)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Budget_User");
            });

            modelBuilder.Entity<Finance>(entity =>
            {
                entity.ToTable("Finance");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.IdIncomeOrExpenses).HasColumnName("Id_IncomeOrExpenses");

                entity.Property(e => e.IdTypeExpenses).HasColumnName("Id_TypeExpenses");

                entity.Property(e => e.IdTypeIncome).HasColumnName("Id_TypeIncome");

                entity.Property(e => e.IdUser).HasColumnName("Id_User");

                entity.Property(e => e.Sum).HasColumnType("money");

                entity.HasOne(d => d.IdIncomeOrExpensesNavigation)
                    .WithMany(p => p.Finances)
                    .HasForeignKey(d => d.IdIncomeOrExpenses)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Finance_IncomeOrExpenses");

                entity.HasOne(d => d.IdTypeExpensesNavigation)
                    .WithMany(p => p.Finances)
                    .HasForeignKey(d => d.IdTypeExpenses)
                    .HasConstraintName("FK_Finance_TypeExpenses");

                entity.HasOne(d => d.IdTypeIncomeNavigation)
                    .WithMany(p => p.Finances)
                    .HasForeignKey(d => d.IdTypeIncome)
                    .HasConstraintName("FK_Finance_TypeIncome");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Finances)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Finance_User");
            });

            modelBuilder.Entity<IncomeOrExpense>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<TypeExpense>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<TypeIncome>(entity =>
            {
                entity.ToTable("TypeIncome");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Lastname).HasMaxLength(50);

                entity.Property(e => e.Login).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Patronymic).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        public static accountingContext Instance()
        {
            if (instance == null)
            {
                instance = new accountingContext();
            }
            return instance;
        }
    }
}
