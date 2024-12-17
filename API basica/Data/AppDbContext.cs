using API_basica.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace API_basica.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<HistoricoSaida> HistoricoSaida { get; set; }

        public override int SaveChanges()
        {       
            return base.SaveChanges();  
        }

    }
}
