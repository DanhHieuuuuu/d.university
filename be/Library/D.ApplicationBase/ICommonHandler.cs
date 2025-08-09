using D.DomainBase.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
