namespace Omg.Lol.Net.Clients.Abstract;

using System.Threading.Tasks;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models;
using Omg.Lol.Net.Models.Items;

public interface IStatuslogClient : IApiInfoCarrier
{
    /// <summary>
    /// Retrieve a single status belonging to an address with a status id.
    /// </summary>
    /// <param name="address">The address the status belongs to.</param>
    /// <param name="statusId">The status id.</param>
    /// <returns>A single status.</returns>
    public Task<CommonResponse<SingleStatus>> RetrieveInvidualStatusAsync(string address, string statusId);

    /// <summary>
    /// Delete a status belonging to a given account with a status id.
    /// </summary>
    /// <param name="address">The address the status belongs to.</param>
    /// <param name="statusId">The status id.</param>
    /// <returns>A message confirming the deletion.</returns>
    // Todo: undocumented API
    public Task<CommonResponse<MessageItem>> DeleteStatusAsync(string address, string statusId);

    /// <summary>
    /// Retrieve the latest status of a given account.
    /// </summary>
    /// <param name="address">The address.</param>
    /// <returns>The lastest (single) status.</returns>
    // todo: Undocumented api
    public Task<CommonResponse<SingleStatus>> RetrieveLatestStatusAsync(string address);

    /// <summary>
    /// Retrieve all statuses belonging to an address.
    /// </summary>
    /// <param name="address">The address.</param>
    /// <returns>All statuses this address posted.</returns>
    public Task<CommonResponse<MultipleStatuses>> RetrieveEntireStatusesAsync(string address);

    /// <summary>
    /// Retrieve all statuses in omg.lol service.
    /// </summary>
    /// <returns>All statuses in the omg.lol service.</returns>
    public Task<CommonResponse<MultipleStatuses>> RetrieveEntireStatusesAsync();

    /// <summary>
    /// Retrieve statuses in omg.lol service in the past two days.
    /// </summary>
    /// <returns>All statuses in the omg.lol service in the past two days.</returns>
    public Task<CommonResponse<MultipleStatuses>> RetrieveEntireLatestStatusAsync();

    public Task<CommonResponse<StatusModified>> CreateStatusAsync(string address, StatusPost status);

    // Todo: inconsistency in doc and postman collection.
    public Task<CommonResponse<StatusModified>> UpdateStatusAsync(string address, StatusPatch status);

    public Task<CommonResponse<StatusBio>> RetrieveStatusBioAsync(string address);

    public Task<CommonResponse<MessageItem>> UpdateStatusBioAsync(string address, ContentItem content);
}
