using Microsoft.EntityFrameworkCore;
using TaskCalender.Models;

namespace TaskCalender.Data
{
    public class TaskCalenderContext : DbContext
    {
        public TaskCalenderContext(DbContextOptions<TaskCalenderContext> options)
            : base(options)
        {
        }

        public DbSet<Relation> Relations { get; set; }
        public DbSet<Principal> Principals { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderInspector> OrderInspectors { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<SettingValue> SettingValues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Table mappings
            modelBuilder.Entity<Relation>().ToTable("Relation");
            modelBuilder.Entity<Principal>().ToTable("Principals");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<OrderInspector>().ToTable("OrderInspectors");
            modelBuilder.Entity<Setting>().ToTable("Settings");
            modelBuilder.Entity<SettingValue>().ToTable("SettingValues");

            // Principal -> Relation (many-to-one)
            modelBuilder.Entity<Principal>()
                .HasOne(p => p.Relation)
                .WithMany(r => r.Principals)
                .HasForeignKey(p => p.Relation_rel_Id);

            // OrderInspector -> Order (many-to-one)
            modelBuilder.Entity<OrderInspector>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderInspectors)
                .HasForeignKey(oi => oi.Order_ord_Id);

            // OrderInspector -> Relation (Inspector) (many-to-one)
            modelBuilder.Entity<OrderInspector>()
                .HasOne(oi => oi.Inspector)
                .WithMany(r => r.OrderInspectors)
                .HasForeignKey(oi => oi.Inspector_rel_Id);

            // SettingValue -> Setting (many-to-one)
            modelBuilder.Entity<SettingValue>()
                .HasOne(sv => sv.Setting)
                .WithMany(s => s.SettingValues)
                .HasForeignKey(sv => sv.Settings_set_Id);
        }
    }
} 