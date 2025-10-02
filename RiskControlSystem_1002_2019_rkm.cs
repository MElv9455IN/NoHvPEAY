// 代码生成时间: 2025-10-02 20:19:57
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Define the Risk entity
public class Risk {
    public int RiskId { get; set; }
    public string Description { get; set; }
    public DateTime OccurrenceDate { get; set; }
}

// Define the RiskControlSystem context
public class RiskControlContext : DbContext {
    public DbSet<Risk> Risks { get; set; }

    public RiskControlContext(DbContextOptions<RiskControlContext> options) : base(options) { }
}

// Define the RiskControlService class
public class RiskControlService {
    private readonly RiskControlContext _context;

    public RiskControlService(RiskControlContext context) {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // Add a new risk to the system
    public async Task<Risk> AddRiskAsync(Risk newRisk) {
        if (newRisk == null) {
            throw new ArgumentNullException(nameof(newRisk));
        }
        _context.Risks.Add(newRisk);
        await _context.SaveChangesAsync();
        return newRisk;
    }

    // Get all risks from the system
    public async Task<List<Risk>> GetAllRisksAsync() {
        return await _context.Risks.ToListAsync();
    }

    // Update a risk in the system
    public async Task<Risk> UpdateRiskAsync(int riskId, Risk updatedRisk) {
        var risk = await _context.Risks.FindAsync(riskId);
        if (risk == null) {
            throw new KeyNotFoundException($"Risk with ID {riskId} not found.");
        }
        risk.Description = updatedRisk.Description;
        risk.OccurrenceDate = updatedRisk.OccurrenceDate;
        await _context.SaveChangesAsync();
        return risk;
    }

    // Delete a risk from the system
    public async Task DeleteRiskAsync(int riskId) {
        var risk = await _context.Risks.FindAsync(riskId);
        if (risk == null) {
            throw new KeyNotFoundException($"Risk with ID {riskId} not found.");
        }
        _context.Risks.Remove(risk);
        await _context.SaveChangesAsync();
    }
}
