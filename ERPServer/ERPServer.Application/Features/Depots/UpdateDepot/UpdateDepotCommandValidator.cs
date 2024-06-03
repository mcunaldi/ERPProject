using FluentValidation;

namespace ERPServer.Application.Features.Depots.UpdateDepot;
internal class UpdateDepotCommandValidator : AbstractValidator<UpdateDepotCommand>
{
    public UpdateDepotCommandValidator()
    {
        RuleFor(p => p.Name).MinimumLength(3);
    }
}
