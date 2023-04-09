using CodeExcercise.Common.Configuration;
using CodeExcercise.Common.Models.DTO;
using CodeExcercise.Database.Context.MemoryDatabase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CodeExcercise.Database.Context
{
    /// <inheritdoc />
    public class CustomerContext : DbContext
    {
        private readonly SqlConnectionConfiguration sqlConfiguration;

        /// <inheritdoc />
        public CustomerContext(IOptions<SqlConnectionConfiguration> sqlConfiguration)
        {
            this.sqlConfiguration = sqlConfiguration.Value;
        }
        
        /// <summary>
        /// Customers
        /// </summary>
        public DbSet<DatabaseCustomer> Customers { get; set; } = null!;

        /// <inheritdoc />
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            optionsBuilder.UseInMemoryDatabase(sqlConfiguration.DatabaseName!);
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DatabaseCustomer>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<DatabaseCustomer>()
                .Property(c => c.Id)
                .HasValueGenerator<OrderIdValueGenerator>()
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<DatabaseCustomer>()
                .Property(c => c.Ident)
                .HasColumnType("uniqueidentifier")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("newid()")
                .HasValueGenerator<GuidValueGenerator>()
                .IsRequired();
            modelBuilder.Entity<DatabaseCustomer>()
                .Property(c => c.Firstname)
                .HasColumnType("nvarchar")
                .HasMaxLength(64)
                .IsRequired();
            modelBuilder.Entity<DatabaseCustomer>()
                .Property(c => c.Surname)
                .HasColumnType("nvarchar")
                .HasMaxLength(120)
                .IsRequired();
        }
    }
}