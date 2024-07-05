﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Project.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<SliderShop> SliderShops { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity => {
                entity.HasKey(e => e.PK_iUserID);
            });

            modelBuilder.Entity<Store>(entity => {
                entity.HasKey(e => e.PK_iStoreID);
            });

            modelBuilder.Entity<SliderShop>(entity => {
                entity.HasKey(e => e.PK_iSliderShopID);
            });

            modelBuilder.Entity<Category>(entity => {
                entity.HasKey(e => e.PK_iCategoryID);
            });

            modelBuilder.Entity<Product>(entity => {
                entity.HasKey(e => e.PK_iProductID);
            });

            modelBuilder.Entity<Favorite>(entity => {
                entity.HasKey(e => e.PK_iFavoriteID);
            });

            modelBuilder.Entity<User>(entity => {
                entity.HasKey(e => e.PK_iUserID);
            });

            modelBuilder.Entity<Cart>(entity => {
                entity.HasNoKey();
            });

            modelBuilder.Entity<Order>(entity => {
                entity.HasNoKey();
            });

            modelBuilder.Entity<CartDetail>(entity => {
                entity.HasNoKey();
            });
        }
    }
}
