namespace Omg.Lol.Net.Clients.Abstract;

using System.Threading.Tasks;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models;
using Omg.Lol.Net.Models.Items;
using Omg.Lol.Net.Models.Purl;

public interface IPurlsClient : IApiInfoCarrier
{
    public Task<CommonResponse<SinglePurl>> RetrievePurlAsync(string address, string purlName);

    public Task<CommonResponse<MessageItem>> DeletePurlAsync(string address, string purlName);

    public Task<CommonResponse<MultiplePurls>> RetrievePurlsAsync(string address);

    // todo: this return value doesn't follow the API, to save an extra model.
    public Task<CommonResponse<MessageItem>> CreatePurlAsync(string address, PurlPost purl);
}
