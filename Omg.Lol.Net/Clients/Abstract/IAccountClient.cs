namespace Omg.Lol.Net.Clients.Abstract;

using System.Threading;
using System.Threading.Tasks;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models;
using Omg.Lol.Net.Models.Account;
using Omg.Lol.Net.Models.Items;

public interface IAccountClient : IApiInfoCarrier
{
    public Task<CommonResponse<AccountInformation>> RetrieveAccountInformationAsync(string email, CancellationToken cancellationToken = default);

    public Task<CommonResponse<AccountAddress[]>> RetrieveAccountAddressesAsync(string email, CancellationToken cancellationToken = default);

    public Task<CommonResponse<AccountName>> RetrieveAccountNameAsync(string email, CancellationToken cancellationToken = default);

    public Task<CommonResponse<AccountSettings>> RetrieveAccountSettingsAsync(string email, CancellationToken cancellationToken = default);

    public Task<CommonResponse<MessageItem>> SetAccountSettingsAsync(string email, AccountSettings settings, CancellationToken cancellationToken = default);

    public Task<CommonResponse<AccountName>> SetAccountNameAsync(string email, string newName, CancellationToken cancellationToken = default);

    public Task<CommonResponse<AccountSession[]>> RetrieveAccountSessionsAsync(string email, CancellationToken cancellationToken = default);

    public Task<CommonResponse<MessageItem>> DeleteAccountSessionAsync(string email, string sessionId, CancellationToken cancellationToken = default);
}
