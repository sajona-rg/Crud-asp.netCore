using CrudNet8MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudNet8MVC.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> opciones) : base (opciones)
        {
            
        }

        //Here we add the models (each model corresponds to a table in the database)
        public DbSet<Contact> Contact { get; set; }
    }
}
