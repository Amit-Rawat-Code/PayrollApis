using Microsoft.EntityFrameworkCore;
using PayrollApis.DtosModels;

namespace PayrollApis.Models
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>().HasIndex(e=>e.EmpCode).IsUnique();

            modelBuilder.Entity<Employee>()
                .Property(e => e.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<Employee>()
                .Property(e => e.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Department>()
                .Property(e => e.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<Department>()
                .Property(e => e.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Designation>()
                .Property(e => e.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<Designation>()
                .Property(e => e.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<EmployeeOfficialDetail>()
                .Property(e => e.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<EmployeeOfficialDetail>()
                .Property(e => e.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<ReportingManagerDto>().HasNoKey().ToView(null);

            modelBuilder.Entity<EmployeeBankDetail>()
                .Property(e => e.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<EmployeeBankDetail>()
                .Property(e => e.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<EmployeeBankDetail>()
                .HasIndex(e => e.EmployeeId)
                .IsUnique();

            modelBuilder.Entity<EmployeeStatuary>()
                .Property(e => e.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<EmployeeStatuary>()
                .Property(e => e.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<EmployeeStatuary>()
                .HasIndex(e => e.EmployeeId)
                .IsUnique();

            modelBuilder.Entity<EmployeeSalaryStructure>()
                .Property(e => e.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<EmployeeSalaryStructure>()
                .Property(e => e.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<EmployeeSalaryStructure>()
                .HasIndex(e => e.EmployeeId)
                .IsUnique();

            modelBuilder.Entity<EmployeeSalaryTransaction>()
                .Property(e => e.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<EmployeeSalaryTransaction>()
                .Property(e => e.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<EmployeeSalaryTransaction>()
                .HasKey(e => e.SalaryTransactionId);

            modelBuilder.Entity<EmployeeSalaryTransaction>()
                .HasIndex(e => new { e.EmployeeId, e.Month, e.Year })
                .IsUnique();
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<EmployeeOfficialDetail> EmployeeOfficialDetails { get; set; }
        public DbSet<ReportingManagerDto> EmployeeHierarchy { get; set; }
        public DbSet<EmployeeBankDetail> EmployeeBankDetails { get; set; }
        public DbSet<EmployeeStatuary> EmployeeStatuarys { get; set; }
        public DbSet<EmployeeSalaryStructure> EmployeeSalaryStructures { get; set; }
        public DbSet<EmployeeSalaryTransaction> EmployeeSalaryTransactions { get; set; }
    }
}
