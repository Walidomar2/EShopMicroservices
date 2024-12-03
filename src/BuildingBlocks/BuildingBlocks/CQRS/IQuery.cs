using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface IQuery<out TRespone> : IRequest<TRespone> where TRespone : notnull
    {
    }
}
