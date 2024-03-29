﻿namespace Omg.Lol.Net.Clients.Abstract;

using System.Threading;
using System.Threading.Tasks;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models;
using Omg.Lol.Net.Models.Items;
using Omg.Lol.Net.Models.Status;

public interface IStatuslogClient : IApiInfoCarrier
{
    /// <summary>
    /// Retrieve a single status belonging to an address with a status id.
    /// </summary>
    /// <returns>A single status.</returns>
    public Task<CommonResponse<SingleStatus>> RetrieveInvidualStatusAsync(
        string address,
        string statusId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a status belonging to a given account with a status id.
    /// </summary>
    /// <returns>A message confirming the deletion.</returns>
    // Todo: undocumented API
    public Task<CommonResponse<MessageItem>> DeleteStatusAsync(
        string address,
        string statusId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieve the latest status of a given account.
    /// </summary>
    /// <returns>The lastest (single) status.</returns>
    // todo: Undocumented api
    public Task<CommonResponse<SingleStatus>> RetrieveLatestStatusAsync(
        string address,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieve all statuses belonging to an address.
    /// </summary>
    /// <returns>All statuses this address posted.</returns>
    public Task<CommonResponse<MultipleStatuses>> RetrieveEntireStatusesAsync(
        string address,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieve all statuses in omg.lol service.
    /// </summary>
    /// <returns>All statuses in the omg.lol service.</returns>
    public Task<CommonResponse<MultipleStatuses>> RetrieveEntireStatusesAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieve statuses in omg.lol service in the past two days.
    /// </summary>
    /// <returns>All statuses in the omg.lol service in the past two days.</returns>
    public Task<CommonResponse<MultipleStatuses>> RetrieveEntireLatestStatusAsync(
        CancellationToken cancellationToken = default);

    public Task<CommonResponse<StatusModified>> CreateStatusAsync(
        string address,
        StatusPost status,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create status via a single line string.
    /// </summary>
    /// <returns></returns>
    public Task<CommonResponse<StatusModified>> CreateStatusAsync(
        string address,
        string status,
        CancellationToken cancellationToken = default);

    // Todo: inconsistency in doc and postman collection.
    public Task<CommonResponse<StatusModified>> UpdateStatusAsync(
        string address,
        StatusPatch status,
        CancellationToken cancellationToken = default);

    public Task<CommonResponse<StatusBio>> RetrieveStatusBioAsync(
        string address,
        CancellationToken cancellationToken = default);

    public Task<CommonResponse<MessageItem>> UpdateStatusBioAsync(
        string address,
        ContentItem content,
        CancellationToken cancellationToken = default);
}
