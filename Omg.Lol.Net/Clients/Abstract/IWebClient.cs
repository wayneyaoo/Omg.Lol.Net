namespace Omg.Lol.Net.Clients.Abstract;

using System.Threading;
using System.Threading.Tasks;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models;
using Omg.Lol.Net.Models.Items;
using Omg.Lol.Net.Models.Web;

public interface IWebClient : IApiInfoCarrier
{
    public Task<CommonResponse<WebPageContent>> RetrieveWebPageContentAsync(
        string address,
        CancellationToken cancellationToken = default);

    public Task<CommonResponse<MessageItem>> UpdateWebPageContentAsync(
        string address,
        WebPageUpdate webPageUpdate,
        CancellationToken cancellationToken = default);
}
