// 代码生成时间: 2025-10-05 15:59:50
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

// Define the Entity class for Experiment
public class Experiment
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int VersionACount { get; set; }
    public int VersionBCount { get; set; }
}

// Define the DbContext for the A/B Testing
public class ABTestingContext : DbContext
{
    public ABTestingContext() : base("name=ABTestingContext")
    {
    }

    public DbSet<Experiment> Experiments { get; set; }
}

// Define the ABTestingService class
public class ABTestingService
{
    private readonly ABTestingContext _context;

    public ABTestingService(ABTestingContext context)
    {
        _context = context;
    }

    // Method to create a new experiment
    public Experiment CreateExperiment(string name, string description)
    {
        Experiment experiment = new Experiment
        {
            Name = name,
            Description = description,
            VersionACount = 0,
            VersionBCount = 0
        };

        _context.Experiments.Add(experiment);
        _context.SaveChanges();
        return experiment;
    }

    // Method to update the count of a version
    public void UpdateVersionCount(int experimentId, bool isVersionA)
    {
        Experiment experiment = _context.Experiments.FirstOrDefault(e => e.Id == experimentId);
        if (experiment == null)
        {
            throw new Exception("Experiment not found.");
        }

        if (isVersionA)
        {
            experiment.VersionACount++;
        }
        else
        {
            experiment.VersionBCount++;
        }

        _context.SaveChanges();
    }

    // Method to retrieve experiments
    public IQueryable<Experiment> GetExperiments()
    {
        return _context.Experiments;
    }
}

// Define the Configuration class for Entity Framework migrations
public class Configuration : DbMigrationsConfiguration<ABTestingContext>
{
    public Configuration()
    {
        AutomaticMigrationsEnabled = true;
        AutomaticMigrationDataLossAllowed = true;
    }
}