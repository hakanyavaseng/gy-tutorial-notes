using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DatabaseFirst
{
    public partial class GYSALES_RDMSContext : DbContext
    {
        public GYSALES_RDMSContext()
        {
        }

        public GYSALES_RDMSContext(DbContextOptions<GYSALES_RDMSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<District> Districts { get; set; } = null!;
        public virtual DbSet<Invoice> Invoices { get; set; } = null!;
        public virtual DbSet<Invoicedetail> Invoicedetails { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Orderdetail> Orderdetails { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Town> Towns { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=GYSALES_RDMS; Integrated Security=SSPI");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("ADDRESS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Addresstext)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESSTEXT");

                entity.Property(e => e.Cityid).HasColumnName("CITYID");

                entity.Property(e => e.Countryid).HasColumnName("COUNTRYID");

                entity.Property(e => e.Districtid).HasColumnName("DISTRICTID");

                entity.Property(e => e.Postalcode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("POSTALCODE");

                entity.Property(e => e.Townid).HasColumnName("TOWNID");

                entity.Property(e => e.Userid).HasColumnName("USERID");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("CITIES");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.City1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CITY");

                entity.Property(e => e.Countryid).HasColumnName("COUNTRYID");

                entity.Property(e => e.Region)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("REGION");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("COUNTRIES");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Country1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY");
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.ToTable("DISTRICTS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.District1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DISTRICT");

                entity.Property(e => e.Townid).HasColumnName("TOWNID");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("INVOICES");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Addressid).HasColumnName("ADDRESSID");

                entity.Property(e => e.Cargoficheno)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CARGOFICHENO");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("DATE_");

                entity.Property(e => e.Orderid).HasColumnName("ORDERID");

                entity.Property(e => e.Totalprice).HasColumnName("TOTALPRICE");
            });

            modelBuilder.Entity<Invoicedetail>(entity =>
            {
                entity.ToTable("INVOICEDETAILS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount).HasColumnName("AMOUNT");

                entity.Property(e => e.Invoiceid).HasColumnName("INVOICEID");

                entity.Property(e => e.Itemid).HasColumnName("ITEMID");

                entity.Property(e => e.Linetotal).HasColumnName("LINETOTAL");

                entity.Property(e => e.Orderdetailid).HasColumnName("ORDERDETAILID");

                entity.Property(e => e.Unitprice).HasColumnName("UNITPRICE");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("ITEMS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Brand)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BRAND");

                entity.Property(e => e.Category1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORY1");

                entity.Property(e => e.Category2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORY2");

                entity.Property(e => e.Category3)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORY3");

                entity.Property(e => e.Itemcode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ITEMCODE");

                entity.Property(e => e.Itemname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ITEMNAME");

                entity.Property(e => e.Unitprice).HasColumnName("UNITPRICE");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("ORDERS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Addressid).HasColumnName("ADDRESSID");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("DATE_");

                entity.Property(e => e.Status).HasColumnName("STATUS_");

                entity.Property(e => e.Totalprice).HasColumnName("TOTALPRICE");

                entity.Property(e => e.Userid).HasColumnName("USERID");
            });

            modelBuilder.Entity<Orderdetail>(entity =>
            {
                entity.ToTable("ORDERDETAILS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount).HasColumnName("AMOUNT");

                entity.Property(e => e.Itemid).HasColumnName("ITEMID");

                entity.Property(e => e.Linetotal).HasColumnName("LINETOTAL");

                entity.Property(e => e.Orderid).HasColumnName("ORDERID");

                entity.Property(e => e.Unitprice).HasColumnName("UNITPRICE");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("PAYMENTS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Approvecode)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("APPROVECODE");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("DATE_");

                entity.Property(e => e.Isok).HasColumnName("ISOK");

                entity.Property(e => e.Orderid).HasColumnName("ORDERID");

                entity.Property(e => e.Paymenttotal).HasColumnName("PAYMENTTOTAL");

                entity.Property(e => e.Paymenttype).HasColumnName("PAYMENTTYPE");
            });

            modelBuilder.Entity<Town>(entity =>
            {
                entity.ToTable("TOWNS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cityid).HasColumnName("CITYID");

                entity.Property(e => e.Town1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TOWN");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USERS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Birthdate)
                    .HasColumnType("date")
                    .HasColumnName("BIRTHDATE");

                entity.Property(e => e.Createddate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATEDDATE");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("GENDER");

                entity.Property(e => e.Namesurname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NAMESURNAME");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD_");

                entity.Property(e => e.Telnr1)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("TELNR1");

                entity.Property(e => e.Telnr2)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("TELNR2");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME_");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
