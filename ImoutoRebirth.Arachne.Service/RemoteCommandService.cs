using System.Threading;
using System.Threading.Tasks;
using MassTransit.RabbitMq.Extensions.Hosting.Contracts;

namespace ImoutoRebirth.Arachne.Service
{
    public class RemoteCommandService : IRemoteCommandService
    {
        private readonly IConfiguredSendEndpointProvider _configuredSendEndpointProvider;

        public RemoteCommandService(IConfiguredSendEndpointProvider configuredSendEndpointProvider)
        {
            _configuredSendEndpointProvider = configuredSendEndpointProvider;
        }

        public async Task SendCommand<T>(object command, CancellationToken cancellationToken = default)
        {
            var requestClient = await _configuredSendEndpointProvider.GetSendEndpoint<T>();
            await requestClient.Send(command, cancellationToken);
        }
    }
}