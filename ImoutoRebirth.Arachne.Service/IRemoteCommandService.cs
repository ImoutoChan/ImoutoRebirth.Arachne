using System.Threading;
using System.Threading.Tasks;

namespace ImoutoRebirth.Arachne.Service
{
    public interface IRemoteCommandService
    {
        Task SendCommand<T>(object command, CancellationToken cancellationToken = default);
    }
}