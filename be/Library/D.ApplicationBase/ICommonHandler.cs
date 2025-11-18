using D.DomainBase.Common;
using MediatR;

namespace D.ApplicationBase
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
        where TCommand : IRequest
    {
    }

    public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
    }
}
