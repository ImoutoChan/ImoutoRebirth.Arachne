using System;
using System.Linq;
using System.Threading.Tasks;
using ImoutoRebirth.Arachne.Core;
using ImoutoRebirth.Arachne.Core.Models;
using ImoutoRebirth.Arachne.MessageContracts;
using ImoutoRebirth.Arachne.Service.Commands;
using ImoutoRebirth.Lilin.MessageContracts;
using MassTransit;

namespace ImoutoRebirth.Arachne.Service
{
    public class SearchMetadataCommandHandler : ISearchMetadataCommandHandler
    {
        private readonly IArachneSearchService _arachneSearchService;
        private readonly IRemoteCommandService _remoteCommandService;

        public SearchMetadataCommandHandler(IArachneSearchService arachneSearchService, IRemoteCommandService remoteCommandService)
        {
            _arachneSearchService = arachneSearchService;
            _remoteCommandService = remoteCommandService;
        }

        public async Task Search(ConsumeContext<ISearchMetadataCommand> context, SearchEngineType where)
        {
            var md5 = context.Message.Md5;

            var searchResults = await _arachneSearchService.Search(new Image(md5), where);

            await _remoteCommandService.SendCommand<IUpdateMetadataCommand>(ConvertToCommand(searchResults));

            // todo debug only
            if (searchResults is Metadata searchResult)
                Console.Out.WriteLine(searchResult.Source + " | " + searchResult.IsFound);
        }

        private object ConvertToCommand(SearchResult searchResults)
        {
            if (searchResults is Metadata searchResult)
            {
                if (searchResult.IsFound)
                {
                    var tags = searchResult.Tags.Select(CreateFileTag).ToArray();
                    var notes = searchResult.Notes.Select(CreateFileNotes).ToArray();

                    return new UpdateMetadataCommand
                    {
                        FileId = searchResult.
                    }
                }
            }
        }
    }
}