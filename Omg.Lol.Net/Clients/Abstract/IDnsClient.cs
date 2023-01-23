namespace Omg.Lol.Net.Clients.Abstract;

using System.Threading.Tasks;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models;
using Omg.Lol.Net.Models.Items;

public interface IDnsClient : IApiInfoCarrier
{
    public Task<CommonResponse<MultipleDnsRecords>> RetrieveDnsRecordsAsync(string address);

    public Task<CommonResponse<DnsModified>> CreateDnsRecordAsync(string address, DnsRecordPost record);

    /// <summary>
    /// Update an existing DNS record. Note the update performs a replacement of the whole data structure.
    /// </summary>
    /// <param name="address">The address the DNS record belongs to.</param>
    /// <param name="record">The content to update. This will completely replace the existing record.</param>
    /// <returns></returns>
    public Task<CommonResponse<DnsModified>> UpdateDnsRecordAsync(string address, DnsRecordUpdate record);

    public Task<CommonResponse<MessageItem>> DeleteDnsRecordAsync(string address, string recordId);
}
