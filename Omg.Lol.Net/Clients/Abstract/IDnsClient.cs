namespace Omg.Lol.Net.Clients.Abstract;

using System.Threading.Tasks;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models;
using Omg.Lol.Net.Models.Items;

public interface IDnsClient : IApiInfoCarrier
{
    public Task<CommonResponse<MultipleDnsRecords>> RetrieveDnsRecordsAsync(string address);

    public Task<CommonResponse<DnsModified>> CreateDnsRecordAsync(string address, DnsRecordPost record);

    public Task<CommonResponse<DnsModified>> UpdateDnsRecordAsync(string address, string recordId, DnsRecordPost record);

    public Task<CommonResponse<MessageItem>> DeleteDnsRecordAsync(string address, string recordId);
}
