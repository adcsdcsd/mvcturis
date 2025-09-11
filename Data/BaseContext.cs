using Microsoft.EntityFrameworkCore;
using Kempery.Models;

namespace Kempery.Data
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Multimedia> Multimedias { get; set; }
        

       
    }
}
