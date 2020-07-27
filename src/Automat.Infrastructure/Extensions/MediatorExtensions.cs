using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading;
using Automat.Infrastructure.Common.Contracts;
using MediatR;

namespace Automat.Infrastructure.Extensions
{
    public static class MediatorExtensions
    {
        public static IObservable<dynamic> SendStream<TRequest, TResponse>(this IMediator mediator,
            TRequest request, Func<TResponse, dynamic> mapTo = null,
            CancellationToken token = default)
            where TRequest : CommandBase, IRequest<TResponse>
            where TResponse : CommandResultBase
        {
            return mediator.Send(request, token)
                .ToObservable()
                .Select(x => x.PresentFor(mapTo));
        }
    }
}
