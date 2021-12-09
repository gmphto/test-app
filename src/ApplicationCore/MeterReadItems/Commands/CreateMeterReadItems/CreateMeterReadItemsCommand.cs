using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Data;

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using System.Linq;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using FluentValidation.Results;

namespace Application.MeterReadItems.Commands.CreateMeterReadItems;

public class CreateMeterReadItemsCommand : IRequest<MeterReadItemsDto>
{
    public List<MeterReadItem>? Items { get; set; }

}

public class CreateReadingItemCommandHandler : IRequestHandler<CreateMeterReadItemsCommand, MeterReadItemsDto>
{

    private readonly IApplicationDbContext _context;

    public CreateReadingItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<MeterReadItemsDto> Handle(CreateMeterReadItemsCommand request, CancellationToken cancellationToken)
    {
        if (request.Items == null)   throw new ArgumentNullException(nameof(request.Items));
        var result = new MeterReadItemsDto();

        // NOTE: Could be moved
        var validator = new CreateMeterReadItemsCommandValidator(_context);
        foreach (var item in request.Items)
        {

            var isValid = validator.Validate(item).IsValid;
            if (isValid)
            {
                item.Account = await _context.AccountItems.FindAsync(new object[] { item.Id }, cancellationToken);
                _context.MeterReadItems.Add(item);
                await _context.SaveChangesAsync(cancellationToken);
                result.Succesful += 1;
            }else
            {
                result.Failures += 1;
            }
        }

        return result;
    }
}
