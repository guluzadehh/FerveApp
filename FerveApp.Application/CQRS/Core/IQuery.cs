using MediatR;
using SharedKernel;

namespace FerveApp.Application;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{

}
