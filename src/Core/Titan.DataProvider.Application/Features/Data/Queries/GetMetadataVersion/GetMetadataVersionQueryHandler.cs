using System.Threading;
using System.Threading.Tasks;
using Titan.DataProvider.Application.Abstractions.Infrastructure;
using Titan.DataProvider.Application.Abstractions.Application.Messaging;
using Titan.DataProvider.Domain.Shared;
using Titan.DataProvider.Application.Models.GalaxyOfHeroes.Metadata;
using Newtonsoft.Json;
using Titan.DataProvider.Application.Errors;

namespace Titan.DataProvider.Application.Features.Data.Queries.GetMetadataVersion
{
    public sealed class GetMetadataVersionQueryHandler : IQueryHandler<GetMetadataVersionQuery, MetadataResponse>
    {
        private readonly IGalaxyOfHeroesService _api;

        public GetMetadataVersionQueryHandler(IGalaxyOfHeroesService api)
            => _api = api;

        public async Task<Result<MetadataResponse>> Handle(GetMetadataVersionQuery request, CancellationToken cancellationToken)
        {
            var response = await _api.GetMetadata(cancellationToken: cancellationToken);
            if (!response.IsSuccessStatusCode)
                return Result.Failure<MetadataResponse>(ApplicationErrors.HttpClient.RequestNotSuccessful);
            return JsonConvert.DeserializeObject<MetadataResponse>(
                    await response.Content.ReadAsStringAsync(cancellationToken));
        }
    }
}