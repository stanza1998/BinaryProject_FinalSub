using Microsoft.EntityFrameworkCore;


namespace BinaryCityProject.Models
{
    public class BinaryCity_DbContext : DbContext
    {
        public BinaryCity_DbContext(DbContextOptions<BinaryCity_DbContext> options)
        : base(options)
        {

        }

        public DbSet<Contact> Contact { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Client_Contact_List> Client_Contact_List { get; set; }
        public DbSet<Contact_Client_List> Contact_Client_List { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Modeling RelationShips
            modelBuilder.Entity<Client>()
                .HasKey(s => s.Client_Index);

            modelBuilder.Entity<Contact>()
                .HasKey(s => s.Contact_Index);

            modelBuilder.Entity<Client_Contact_List>()
                .HasKey(s => s.CC_Index);

            modelBuilder.Entity<Contact_Client_List>()
                .HasKey(s => s.CC_Index);

         






        }
    }
}