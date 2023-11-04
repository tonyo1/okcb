using Microsoft.Azure.Cosmos.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Name_Here;
using Name_Here.Models;

namespace Cosmos.ModelBuilding;

public class AppDBContext : DbContext
{
    readonly IConfiguration config;

    public AppDBContext() { }
    public AppDBContext(IConfiguration config)
    {
        this.config = config;
    }

    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }

    #region Configuration
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseCosmos(accountEndpoint: config.AzureEndpoint,
                accountKey:config.AzureSecret,
                databaseName: config.AzureDBName); 
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region DefaultContainer
        modelBuilder
            .HasDefaultContainer("AppData");
        #endregion

        var appusers = modelBuilder
                    .Entity<AppUser>();

        appusers
            .HasDiscriminator(e => e.PartitionKey);
           
       
        appusers 
            .HasPartitionKey(o => o.Email)  
            .Property(d => d.ETag)
            .IsETagConcurrency()         ;

        appusers.HasKey(e => e.Email);


        var userProfs = modelBuilder
                    .Entity<UserProfile>();

        userProfs.HasDiscriminator(e => e.PartitionKey);

        userProfs
            .HasPartitionKey(o => o.Email)
            .Property(d => d.ETag)
            .IsETagConcurrency();

        userProfs.HasKey(e => e.Email);

    }
}
