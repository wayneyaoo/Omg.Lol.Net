namespace Omg.Lol.Net.Clients.Abstract;

using System.Threading;
using System.Threading.Tasks;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models;
using Omg.Lol.Net.Models.Items;
using Omg.Lol.Net.Models.Now;

public interface INowClient : IApiInfoCarrier
{
    public Task<CommonResponse<Now>> RetrieveNowPageAsync(
        string address,
        CancellationToken cancellationToken = default);

    public Task<CommonResponse<NowGarden>> RetrieveNowGardenPagesAsync(CancellationToken cancellationToken = default);

    public Task<CommonResponse<MessageItem>> UpdateNowPageAsync(
        string address,
        NowContentPost updatedNowContent,
        CancellationToken cancellationToken = default);
}
