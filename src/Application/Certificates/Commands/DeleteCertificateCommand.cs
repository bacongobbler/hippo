using System.ComponentModel.DataAnnotations;
using Hippo.Application.Common.Exceptions;
using Hippo.Application.Common.Interfaces;
using Hippo.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hippo.Application.Certificates.Commands;

public class DeleteCertificateCommand : IRequest
{
    [Required]
    public Guid Id { get; set; }
}

public class DeleteCertificateCommandHandler : IRequestHandler<DeleteCertificateCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteCertificateCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteCertificateCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Certificates
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(Certificate), request.Id);
        }

        _context.Certificates.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
