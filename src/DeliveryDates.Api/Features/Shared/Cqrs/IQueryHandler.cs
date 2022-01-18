namespace DeliveryDates.Api.Features.Shared.Cqrs
{
    public interface IQueryHandler<in TQuery, TResponse>
    {
        TResponse Handle(TQuery request);
    }
}