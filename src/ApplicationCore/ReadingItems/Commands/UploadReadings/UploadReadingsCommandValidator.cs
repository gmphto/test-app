using Application.Common.Interfaces;
using FluentValidation;

namespace Application.ReadingItems.Commands.UploadReadings;

public class UploadReadingsCommandValidator : AbstractValidator<UploadReadingsCommand>
{
    private readonly IApplicationDbContext _context;

    public UploadReadingsCommandValidator(IApplicationDbContext context)
    {
        _context = context;
    }
}