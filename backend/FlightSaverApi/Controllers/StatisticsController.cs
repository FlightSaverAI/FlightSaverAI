using FlightSaverApi.Helpers;
using FlightSaverApi.Models;
using FlightSaverApi.Queries.Statistics;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightSaverApi.Controllers;

[Route("/statistic")]
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
    public async Task<ActionResult<FlightStatistics>> GetStatistics(CancellationToken cancellationToken, int? userId = null)
    {
        try
        {
            userId ??= ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
            
            var query = new GetStatisticsQuery(userId.Value);
        
            var statistics = await _mediator.Send(query, cancellationToken);
        
            return Ok(statistics);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("bar")]
    public async Task<ActionResult<BarChartStatistics>> GetBarChartStatistics(CancellationToken cancellationToken,
        int? userId = null)
    {
        try
        {
            userId ??= ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
            
            var query = new GetBarChartStatisticsQuery(userId.Value);
        
            var statistics = await _mediator.Send(query, cancellationToken);
        
            return Ok(statistics);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet("circual")]
    public async Task<ActionResult<BarChartStatistics>> GetCircualChartStatistics(CancellationToken cancellationToken,
        int? userId = null)
    {
        try
        {
            userId ??= ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
            
            var query = new GetCircualChartStatisticsQuery(userId.Value);
        
            var statistics = await _mediator.Send(query, cancellationToken);
        
            return Ok(statistics);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet("line")]
    public async Task<ActionResult<BarChartStatistics>> GetLineChartStatistics(CancellationToken cancellationToken,
        int? userId = null)
    {
        try
        {
            userId ??= ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
            
            var query = new GetLineChartStatisticsQuery(userId.Value);
        
            var statistics = await _mediator.Send(query, cancellationToken);
        
            return Ok(statistics);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("basic")]
    public async Task<ActionResult<FlightStatistics>> GetBasicStatistics(CancellationToken cancellationToken,
        int? userId = null)
    {
        try
        {
            userId ??= ClaimsHelper.GetUserIdFromClaims(HttpContext.User);
            
            var query = new GetBasicStatisticsQuery(userId.Value);
            
            var statistics = await _mediator.Send(query, cancellationToken);
            
            return Ok(statistics);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}