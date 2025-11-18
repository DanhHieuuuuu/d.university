using D.DomainBase.Common;
using MediatR;

namespace D.ApplicationBase
{
    public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {

    }
}
