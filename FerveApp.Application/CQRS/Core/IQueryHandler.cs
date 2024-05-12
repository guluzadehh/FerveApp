using MediatR;
using SharedKernel;

namespace FerveApp.Application;


public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{

}
