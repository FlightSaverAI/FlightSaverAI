using FlightSaverApi.Models;
using FlightSaverApi.Queries.Statistics;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightSaverApi.Controllers;

[Route("/statistics")]
[Authorize(Policy = "RequireUserRole")]
public class StatisticsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StatisticsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    //GET: /statistics
    [HttpGet]
    public async Task<ActionResult<FlightStatistics>> GetStatistics(int userId, CancellationToken cancellationToken)
    {
        var query = new GetStatisticsQuery(userId);
        
        var statistics = await _mediator.Send(query, cancellationToken);
        
        return Ok(statistics);
    }
}