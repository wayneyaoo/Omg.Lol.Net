﻿namespace Omg.Lol.Net.Clients.Abstract;

using System.Threading.Tasks;
using Omg.Lol.Net.Infrastructure;
using Omg.Lol.Net.Models;
using Omg.Lol.Net.Models.Items;
using Omg.Lol.Net.Models.Paste;

public interface IPastebinClient : IApiInfoCarrier
{
    public Task<CommonResponse<SinglePaste>> RetrieveASpecificPasteAsync(string address, string pasteTitle);

    public Task<CommonResponse<MultiplePastes>> RetrievePublicAndPrivatePastebinAsync(string address);

    public Task<CommonResponse<MultiplePastes>> RetrievePublicPastebinAsync(string address);

    public Task<CommonResponse<PasteModified>> CreateOrUpdatePasteAsync(string address, PastePost paste);

    public Task<CommonResponse<MessageItem>> DeletePasteAsync(string address, string pasteTitle);
}
