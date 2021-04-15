using Microsoft.EntityFrameworkCore;
using desafioStone.Models;

namespace desafioStone.Data
{
    public class DesafioStoneEntities : DbContext
    {
        public DesafioStoneEntities(DbContextOptions<DesafioStoneEntities> options)
            : base(options)
        {

        }
            
        public DbSet<FuncionarioViewModel> Funcionarios { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Password=Suggar.111;Persist Security Info=True;User ID=sa;Initial Catalog=DESAFIOSTONE;Data Source=.");
            //optionsBuilder.UseSqlServer("Integrated Security=SSPI;Persist Security Info=False;User ID=sa;Initial Catalog=DESAFIO_STONE1;Data Source=SU0094N\\SQLEXPRESS01");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<FuncionarioViewModel>()
                .ToTable("Funcionario");

            modelBuilder.Entity<FuncionarioViewModel>()
                .Property(p => p.SalarioBruto)
                .HasColumnType("decimal(18,4)");
        }
    }
}
