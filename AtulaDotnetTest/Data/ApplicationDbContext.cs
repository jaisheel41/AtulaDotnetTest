using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AtulaDotnetTest.Models;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().ToTable("products");
        modelBuilder.Entity<Category>().ToTable("categories");

        // Seeding data into the Categories table
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Table" },
            new Category { Id = 2, Name = "Chair" },
            new Category { Id = 3, Name = "Sofa" }
        );

        // Seeding data into the Products table as per the assessment file requirements
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Sku = "SKUA", Name = "Lorem Table" },
            new Product { Id = 2, Sku = "SKUB", Name = "Ipsum Table" },
            new Product { Id = 3, Sku = "SKUC", Name = "Dolor Table" },
            new Product { Id = 4, Sku = "SKUD", Name = "Sit Chair" },
            new Product { Id = 5, Sku = "SKUE", Name = "Amet Chair" },
            new Product { Id = 6, Sku = "SKUF", Name = "Consectetur Chair" },
            new Product { Id = 7, Sku = "SKUG", Name = "Adipiscing Sofa" },
            new Product { Id = 8, Sku = "SKUH", Name = "Elit Sofa" },
            new Product { Id = 9, Sku = "SKUI", Name = "Mauris Sofa" }
        );

        modelBuilder.Entity<Product>()
            .HasMany(p => p.Categories)
            .WithMany(c => c.Products)
            .UsingEntity<Dictionary<string, object>>(
                "ProductCategories", 
                j => j.HasOne<Category>().WithMany().HasForeignKey("CategoryId"),
                j => j.HasOne<Product>().WithMany().HasForeignKey("ProductId"),
                j =>
                {
                    j.HasKey("ProductId", "CategoryId"); // Composite primary key
                    j.ToTable("ProductCategories"); 

                    // Seeded data between categories and products
                    j.HasData(
                        new { ProductId = 1, CategoryId = 1 }, // Lorem Table -> Table
                        new { ProductId = 2, CategoryId = 1 }, // Ipsum Table -> Table
                        new { ProductId = 3, CategoryId = 1 }, // Dolor Table -> Table
                        new { ProductId = 4, CategoryId = 2 }, // Sit Chair -> Chair
                        new { ProductId = 5, CategoryId = 2 }, // Amet Chair -> Chair
                        new { ProductId = 6, CategoryId = 2 }, // Consectetur Chair -> Chair
                        new { ProductId = 7, CategoryId = 3 }, // Adipiscing Sofa -> Sofa
                        new { ProductId = 8, CategoryId = 3 }, // Elit Sofa -> Sofa
                        new { ProductId = 9, CategoryId = 3 }  // Mauris Sofa -> Sofa
                    );
                });
    }
}
