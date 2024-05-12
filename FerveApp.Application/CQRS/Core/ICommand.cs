using MediatR;
using SharedKernel;

namespace FerveApp.Application;

public interface ICommand : IRequest<Result>
{

}

public interface ICommand<TResponse>
    : IRequest<Result<TResponse>>
{

}
