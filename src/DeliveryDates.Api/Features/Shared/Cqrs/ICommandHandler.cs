using System.Threading.Tasks;

namespace DeliveryDates.Api.Features.Shared.Cqrs
{
    public interface ICommandHandler<in TCommand>
    {
        void Handle(TCommand command);
    }
}