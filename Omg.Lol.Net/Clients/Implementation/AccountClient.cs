namespace Omg.Lol.Net.Clients.Implementation;

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Omg.Lol.Net.Clients.Abstract;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models;
using Omg.Lol.Net.Models.Account;
using Omg.Lol.Net.Models.Items;

public sealed class AccountClient : IAccountClient
{
    public string Token { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    private const string RetrieveAccountInformationEndpoint = "/account/{0}/info";

    private const string RetrieveAccountAddressesEndpoint = "/account/{0}/addresses";

    private const string RetrieveAccountNameEndpoint = "/account/{0}/name";

    private const string SetAccountNameEndpoint = RetrieveAccountNameEndpoint;

    private const string RetrieveAccountSessionsEndpoint = "/account/{0}/sessions";

    private const string DeleteAccountSessionsEndpoint = RetrieveAccountSessionsEndpoint;

    private const string RetrieveAccountSettingsEndpoint = "/account/{0}/settings";

    private const string SetAccountsettingsEndpoint = RetrieveAccountSettingsEndpoint;

    private readonly IApiServerCommunicationHandler apiServerCommunicationHandler;

    public AccountClient(IApiServerCommunicationHandler apiServerCommunicationHandler)
    {
        this.apiServerCommunicationHandler = apiServerCommunicationHandler;
    }

    public async Task<CommonResponse<AccountInformation>> RetrieveAccountInformationAsync(string email)
        => await this.apiServerCommunicationHandler.GetAsync<CommonResponse<AccountInformation>>(
                this.Url + string.Format(RetrieveAccountInformationEndpoint, email), this.Token)
            .ConfigureAwait(false);

    public async Task<CommonResponse<AccountAddress[]>> RetrieveAccountAddressesAsync(string email)
        => await this.apiServerCommunicationHandler.GetAsync<CommonResponse<AccountAddress[]>>(
                this.Url + string.Format(RetrieveAccountAddressesEndpoint, email), this.Token)
            .ConfigureAwait(false);

    public async Task<CommonResponse<AccountName>> RetrieveAccountNameAsync(string email)
        => await this.apiServerCommunicationHandler.GetAsync<CommonResponse<AccountName>>(
                this.Url + string.Format(RetrieveAccountNameEndpoint, email), this.Token)
            .ConfigureAwait(false);

    public async Task<CommonResponse<AccountSettings>> RetrieveAccountSettingsAsync(string email)
        => await this.apiServerCommunicationHandler.GetAsync<CommonResponse<AccountSettings>>(
                this.Url + string.Format(RetrieveAccountSettingsEndpoint, email), this.Token)
            .ConfigureAwait(false);

    // Skip test due to test concurrency issue
    [ExcludeFromCodeCoverage]
    public async Task<CommonResponse<MessageItem>> SetAccountSettingsAsync(string email, AccountSettings settings)
        => await this.apiServerCommunicationHandler.PutAsync<CommonResponse<MessageItem>>(
                this.Url + string.Format(SetAccountsettingsEndpoint, email),
                JsonConvert.SerializeObject(settings),
                this.Token)
            .ConfigureAwait(false);

    // Skip test due to test concurrency issue
    [ExcludeFromCodeCoverage]
    public async Task<CommonResponse<AccountName>> SetAccountNameAsync(string email, string newName)
        => await this.apiServerCommunicationHandler.PutAsync<CommonResponse<AccountName>>(
                this.Url + string.Format(SetAccountNameEndpoint, email),
                JsonConvert.SerializeObject(new
                {
                    name = newName,
                }),
                this.Token)
            .ConfigureAwait(false);

    public async Task<CommonResponse<AccountSession[]>> RetrieveAccountSessionsAsync(string email)
        => await this.apiServerCommunicationHandler.GetAsync<CommonResponse<AccountSession[]>>(
                this.Url + string.Format(RetrieveAccountSessionsEndpoint, email), this.Token)
            .ConfigureAwait(false);

    //Skip test because we don't automatic way to create sessions
    [ExcludeFromCodeCoverage]
    public async Task<CommonResponse<MessageItem>> DeleteAccountSessionAsync(string email, string sessionId)
        => await this.apiServerCommunicationHandler.DeleteAsync<CommonResponse<MessageItem>>(
                this.Url + string.Format(DeleteAccountSessionsEndpoint, email), this.Token)
            .ConfigureAwait(false);
}
