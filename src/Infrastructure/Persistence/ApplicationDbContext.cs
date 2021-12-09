

using Microsoft.EntityFrameworkCore;

using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Domain.Common;
using System.Reflection;
using Infrastructure.Identity;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly IDateTime _dateTime;
    private readonly IDomainEventService _domainEventService;


    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IOptions<Duende.IdentityServer.EntityFramework.Options.OperationalStoreOptions> operationalStoreOptions,
        IDomainEventService domainEventService,
    IDateTime dateTime) : base(options, operationalStoreOptions)
    {
        _domainEventService = domainEventService;
        _dateTime = dateTime;
    }

    public DbSet<MeterReadItem> MeterReadItems => Set<MeterReadItem>();
    public DbSet<AccountItem> AccountItems => Set<AccountItem>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    //entry.Entity.CreatedBy = _currentUserService.UserId;
                    entry.Entity.CreatedBy = "123";
                    entry.Entity.Created = _dateTime.Now;
                    break;

                case EntityState.Modified:
                    //entry.Entity.LastModifiedBy = _currentUserService.UserId;
                    entry.Entity.CreatedBy = "123";
                    entry.Entity.LastModified = _dateTime.Now;
                    break;
            }
        }

        var events = ChangeTracker.Entries<IHasDomainEvent>()
                .Select(x => x.Entity.DomainEvents)
                .SelectMany(x => x)
                .Where(domainEvent => !domainEvent.IsPublished)
                .ToArray();

        var result = await base.SaveChangesAsync(cancellationToken);

        await DispatchEvents(events);

        return result;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Entity<AccountItem>()
       .HasKey(c => c.Id);

        builder.Entity<MeterReadItem>()
        .HasKey(c => c.Id);

        base.OnModelCreating(builder);
    }

    private async Task DispatchEvents(DomainEvent[] events)
    {
        foreach (var @event in events)
        {
            @event.IsPublished = true;
            await _domainEventService.Publish(@event);
        }
    }
}
