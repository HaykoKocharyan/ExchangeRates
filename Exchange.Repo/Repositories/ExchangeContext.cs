using Exchange.Repo.Entities;
using Microsoft.EntityFrameworkCore;
using System;

public class ExchangeContext : DbContext
{
    public DbSet<Rates> Rates { get; set; }
    public DbSet<Urls> Urls { get; set; }
    public ExchangeContext()
    {

    }
    public ExchangeContext(DbContextOptions<ExchangeContext> options) : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=Exchange;Username=postgres;Password=Hhkk0407",
            b => b.MigrationsAssembly("ExchangeRatesWebapi"));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rates>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnName("Id")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.UrlsId)
                .HasColumnName("UrlsId")
                .IsRequired();

            entity.Property(e => e.Buy)
                .HasColumnType("decimal(18,4)")
                .IsRequired();

            entity.Property(e => e.Sell)
                .HasColumnType("decimal(18,4)")
                .IsRequired();

            entity.Property(e => e.DateTime)
                .HasColumnType("timestamp")
                .HasDefaultValueSql("now()")
                .IsRequired();

            entity.HasOne(d => d.Urls)
                .WithMany(p => p.Rates)
                .HasForeignKey(d => d.UrlsId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Rates_Urls");
        });

        modelBuilder.Entity<Urls>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnName("Id")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.Bank_Name)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(e => e.Bank_Id)
                .IsRequired();

            entity.Property(e => e.Bank_Url)
                .HasMaxLength(60)
                .IsRequired();

            entity.Property(e => e.Currency)
                .IsRequired();

            entity.Property(e => e.Buy_Xpath)
                .HasMaxLength(250)
                .IsRequired();

            entity.Property(e => e.Sell_Xpath)
                .HasMaxLength(250)
                .IsRequired();
        });
    }
}
