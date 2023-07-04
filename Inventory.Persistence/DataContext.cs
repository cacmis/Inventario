using Microsoft.EntityFrameworkCore;
using Inventory.Entities;
namespace Inventory.Persistence
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):
        base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<InventoryMovement> InventoryMovements { get; set; }
        public DbSet<InventoryStock> InventoryStocks { get; set; }
        public DbSet<MovementType> MovementTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
    }
}