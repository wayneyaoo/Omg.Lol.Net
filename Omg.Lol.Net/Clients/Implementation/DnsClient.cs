namespace Omg.Lol.Net.Clients.Implementation;

using System.Threading.Tasks;
using Newtonsoft.Json;
using Omg.Lol.Net.Clients.Abstract;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models;
using Omg.Lol.Net.Models.Items;

public class DnsClient : IDnsClient
{
    public string Token { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    private const string RetrieveDnsRecordsEndpoint = "/address/{0}/dns";

    private const string CreateaDnsRecordEndpoint = RetrieveDnsRecordsEndpoint;

    private const string UpdateExistingDnsRecordEnpoint = RetrieveDnsRecordsEndpoint;

    private const string DeleteDnsRecordEnpoint = "/address/{0}/dns/{1}";

    private readonly IApiServerCommunicationHandler apiServerCommunicationHandler;

    public DnsClient(IApiServerCommunicationHandler apiServerCommunicationHandler)
    {
        this.apiServerCommunicationHandler = apiServerCommunicationHandler;
    }

    public async Task<CommonResponse<MultipleDnsRecords>> RetrieveDnsRecordsAsync(string address)
        => await this.apiServerCommunicationHandler.GetAsync<CommonResponse<MultipleDnsRecords>>(
                this.Url + string.Format(RetrieveDnsRecordsEndpoint, address),
                this.Token)
            .ConfigureAwait(false);

    public async Task<CommonResponse<DnsModified>> CreateDnsRecordAsync(string address, DnsRecordPost record)
        => await this.apiServerCommunicationHandler.PostAsync<CommonResponse<DnsModified>>(
                this.Url + string.Format(CreateaDnsRecordEndpoint, address),
                JsonConvert.SerializeObject(record),
                this.Token)
            .ConfigureAwait(false);

    // TODO: Come back when this https://github.com/neatnik/omg.lol/issues/532 is resolved.
    public async Task<CommonResponse<DnsModified>> UpdateDnsRecordAsync(string address, DnsRecordUpdate record)
        => await this.apiServerCommunicationHandler.PutAsync<CommonResponse<DnsModified>>(
                this.Url + string.Format(UpdateExistingDnsRecordEnpoint, address),
                JsonConvert.SerializeObject(record),
                this.Token)
            .ConfigureAwait(false);

    public async Task<CommonResponse<MessageItem>> DeleteDnsRecordAsync(string address, string recordId)
        => await this.apiServerCommunicationHandler.DeleteAsync<CommonResponse<MessageItem>>(
                this.Url + string.Format(DeleteDnsRecordEnpoint, address, recordId),
                this.Token)
            .ConfigureAwait(false);
}
