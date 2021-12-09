using Application.Common.Interfaces;
using Domain.Entities;
using FluentValidation;

namespace Application.MeterReadItems.Commands.CreateMeterReadItems;

public class CreateMeterReadItemsCommandValidator : AbstractValidator<MeterReadItem>
{
    private readonly IApplicationDbContext _context;

    public CreateMeterReadItemsCommandValidator(IApplicationDbContext context)
    {
        _context = context;


        RuleFor(item => item.Id)
            .Must(BeAssociatedWithAccount).WithMessage("Reading must be associated with an account.");

        RuleFor(item => item.Value)
            .Must(BeInNumberFormat).WithMessage("Reading must have a valid MeterReadValue number format.");

        RuleFor(item => item.Id)
            .Must(BeUniqueReading).WithMessage("Reading already exists.");
    }

    public bool BeUniqueReading(int accountId)
    {
        return _context.MeterReadItems
            .All(l => l.Id != accountId);
    }

    public bool BeAssociatedWithAccount(int accountId)
    {
        bool test = _context.AccountItems.Any(l => l.Id == accountId);
        return test;
    }

    private static bool BeInNumberFormat(string x)
    {
        if (IsNumeric(x) && IsNotNegative(x)) return IsFiveDigit(x);
        return false;
    }
    private static bool IsFiveDigit(string x) => x.Length >= 5;
    private static bool IsNumeric(string x) => int.TryParse(x, out int _);
    private static bool IsNotNegative(string x) => int.Parse(x) > 0;

}