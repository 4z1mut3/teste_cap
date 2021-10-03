using Microsoft.EntityFrameworkCore;
using teste_cap_07.Models;

namespace teste_cap_07.Data
{   
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options)
        {

        }

        public DbSet<MovimentoProduto> movimentoProdutos {get;set;}
    }
}