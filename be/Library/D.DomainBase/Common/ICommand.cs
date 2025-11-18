using MediatR;

namespace D.DomainBase.Common
{
    public interface ICommand : IRequest
    {
    }

    public interface ICommand<out TResult> : IRequest<TResult>
    {

    }
}
