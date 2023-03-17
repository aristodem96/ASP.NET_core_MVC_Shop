using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FatherShopImperiyaOdyagu.Models;

namespace FatherShopImperiyaOdyagu.Data
{
    public class ShopContext : DbContext
    {
        public ShopContext (DbContextOptions<ShopContext> options)
            : base(options)
        {
        }

        public DbSet<FatherShopImperiyaOdyagu.Models.Product> Product { get; set; } = default!;
        public DbSet<FatherShopImperiyaOdyagu.Models.Category> Category { get; set; }
        public DbSet<FatherShopImperiyaOdyagu.Models.Admin> Admin { get; set; }
    }
}
