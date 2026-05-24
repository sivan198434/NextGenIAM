using IAM.IdentityService.Data;
using IAM.IdentityService.Models;
using IAM.Shared.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IAM.IdentityService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IdentitiesController : ControllerBase
{
    private readonly IdentityDbContext _context;
    private readonly IPublishEndpoint _publishEndpoint;

    public IdentitiesController(IdentityDbContext context, IPublishEndpoint publishEndpoint)
    {
        _context = context;
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Identity identity)
    {
        identity.Id = Guid.NewGuid();
        _context.Identities.Add(identity);
        await _context.SaveChangesAsync();

        await _publishEndpoint.Publish(new ProvisioningRequestedEvent
        {
            CorrelationId = Guid.NewGuid(),
            IdentityId = identity.Id.ToString(),
            Action = "Create",
            Attributes = new Dictionary<string, string>
            {
                { "Username", identity.Username },
                { "Email", identity.Email }
            }
        });

        return Ok(identity);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _context.Identities.ToListAsync());
    }
}
