using ARC.Application.Features.Countries.Queries.GetAll;

namespace ARC.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/countries")]
    public class CountriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CountriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets all countries (localized name).
        /// </summary>
        /// <returns>List of countries with id, name, iso3, and code.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<CountryDto>), 200)]
        [EndpointDescription("Gets all countries (localized name)")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllCountriesQuery(), cancellationToken);
            return result.ToActionResult();
        }
    }
}