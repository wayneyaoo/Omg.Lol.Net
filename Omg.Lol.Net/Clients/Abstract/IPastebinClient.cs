namespace Omg.Lol.Net.Clients.Abstract;

using System.Threading;
using System.Threading.Tasks;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models;
using Omg.Lol.Net.Models.Items;
using Omg.Lol.Net.Models.Paste;

public interface IPastebinClient : IApiInfoCarrier
{
    public Task<CommonResponse<SinglePaste>> RetrieveASpecificPasteAsync(
        string address,
        string pasteTitle,
        CancellationToken cancellationToken = default);

    public Task<CommonResponse<MultiplePastes>> RetrievePublicAndPrivatePastebinAsync(
        string address,
        CancellationToken cancellationToken = default);

    public Task<CommonResponse<MultiplePastes>> RetrievePublicPastebinAsync(
        string address,
        CancellationToken cancellationToken = default);

    public Task<CommonResponse<PasteModified>> CreateOrUpdatePasteAsync(
        string address,
        PastePost paste,
        CancellationToken cancellationToken = default);

    public Task<CommonResponse<MessageItem>> DeletePasteAsync(
        string address,
        string pasteTitle,
        CancellationToken cancellationToken = default);
}
