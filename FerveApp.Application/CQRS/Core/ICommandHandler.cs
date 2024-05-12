using MediatR;
using SharedKernel;

namespace FerveApp.Application;

public interface ICommandHandler<TCommand>
    : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{

}

public interface ICommandHandler<in TCommand, TResponse>
    : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{

}
