using MediatR;

namespace D.DomainBase.Common
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {

    }
}
