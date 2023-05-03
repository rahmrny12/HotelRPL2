using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace HotelRPL2
{
    public partial class HotelRPLModel : DbContext
    {
        public HotelRPLModel()
            : base("name=DBHotelRPLModel")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<FDCheckOut> FDCheckOuts { get; set; }
        public virtual DbSet<FoodsAndDrink> FoodsAndDrinks { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemStatu> ItemStatus { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<ReservationCheckOut> ReservationCheckOuts { get; set; }
        public virtual DbSet<ReservationRequestItem> ReservationRequestItems { get; set; }
        public virtual DbSet<ReservationRoom> ReservationRooms { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.NIK)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Gender)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Photo)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .HasOptional(e => e.FDCheckOut)
                .WithRequired(e => e.Employee);

            modelBuilder.Entity<FoodsAndDrink>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<FoodsAndDrink>()
                .Property(e => e.Type)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<FoodsAndDrink>()
                .Property(e => e.Photo)
                .IsUnicode(false);

            modelBuilder.Entity<FoodsAndDrink>()
                .HasOptional(e => e.FDCheckOut)
                .WithRequired(e => e.FoodsAndDrink);

            modelBuilder.Entity<Item>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ItemStatu>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ItemStatu>()
                .HasMany(e => e.ReservationCheckOuts)
                .WithOptional(e => e.ItemStatu)
                .HasForeignKey(e => e.ItemStatusId);

            modelBuilder.Entity<Job>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Job>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.Job)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reservation>()
                .Property(e => e.BookingCode)
                .IsUnicode(false);

            modelBuilder.Entity<ReservationRoom>()
                .HasMany(e => e.FDCheckOuts)
                .WithRequired(e => e.ReservationRoom)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Room>()
                .Property(e => e.RoomNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .Property(e => e.RoomFloor)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<RoomType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<RoomType>()
                .Property(e => e.Photo)
                .IsUnicode(false);
        }
    }
}
