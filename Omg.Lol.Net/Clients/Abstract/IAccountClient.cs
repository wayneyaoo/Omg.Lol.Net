namespace Omg.Lol.Net.Clients.Abstract;

using System.Threading.Tasks;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models;
using Omg.Lol.Net.Models.Account;
using Omg.Lol.Net.Models.Items;

public interface IAccountClient : IApiInfoCarrier
{
    public Task<CommonResponse<AccountInformation>> RetrieveAccountInformationAsync(string email);

    public Task<CommonResponse<AccountAddress[]>> RetrieveAccountAddressesAsync(string email);

    public Task<CommonResponse<AccountName>> RetrieveAccountNameAsync(string email);

    public Task<CommonResponse<AccountSettings>> RetrieveAccountSettingsAsync(string email);

    public Task<CommonResponse<MessageItem>> SetAccountSettingsAsync(string email, AccountSettings settings);

    public Task<CommonResponse<AccountName>> SetAccountNameAsync(string email, string newName);

    public Task<CommonResponse<AccountSession[]>> RetrieveAccountSessionsAsync(string email);

    public Task<CommonResponse<MessageItem>> DeleteAccountSessionAsync(string email, string sessionId);
}
