using IAM.AuditService.Data;
using IAM.AuditService.Models;
using IAM.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace IAM.AuditService.Services;

public class AuditStore
{
    private readonly AuditDbContext _context;

    public AuditStore(AuditDbContext context)
    {
        _context = context;
    }

    public async Task AppendAsync(string aggregateId, string eventType, object payload)
    {
        var lastEntry = await _context.AuditEntries
            .OrderByDescending(x => x.Timestamp)
            .FirstOrDefaultAsync();

        var entry = new AuditEntry
        {
            AggregateId = aggregateId,
            EventType = eventType,
            Payload = System.Text.Json.JsonSerializer.Serialize(payload),
            PreviousHash = lastEntry?.Hash ?? string.Empty
        };

        entry.Hash = entry.CalculateHash(entry.PreviousHash);

        _context.AuditEntries.Add(entry);
        await _context.SaveChangesAsync();
    }
}
