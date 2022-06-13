using Hippo.Application.Common.Interfaces;
using Hippo.Application.Common.Security;
using Hippo.Core.Entities;
using Hippo.Core.Events;
using MediatR;

namespace Hippo.Application.Apps.Commands;

[Authorize(Roles = UserRole.Administrator)]
[Authorize(Policy = UserPolicy.CanPurge)]
public class PurgeAppsCommand : IRequest { }

public class PurgeAppsCommandHandler : IRequestHandler<PurgeAppsCommand>
{
    private readonly IApplicationDbContext _context;

    public PurgeAppsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(PurgeAppsCommand request, CancellationToken cancellationToken)
    {
        foreach (var entity in _context.Apps)
        {
            entity.AddDomainEvent(new DeletedEvent<App>(entity));
        }

        _context.Apps.RemoveRange(_context.Apps);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
