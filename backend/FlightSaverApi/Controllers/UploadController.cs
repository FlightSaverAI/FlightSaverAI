using AutoMapper;
using FlightSaverApi.Commands;
using FlightSaverApi.Data;
using FlightSaverApi.DTOs;
using FlightSaverApi.Interfaces.Services;
using FlightSaverApi.Models;
using FlightSaverApi.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightSaverApi.Controllers;

[Route("upload")]
[ApiController]
[Authorize(Policy = "RequireUserRole")]
public class UploadController : ControllerBase
{
    private readonly FlightSaverContext _dbContext;
    private readonly IMediator _mediator;

    public UploadController(IMediator mediator, FlightSaverContext dbContext)
    {
        _mediator = mediator;
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> UploadImage([FromForm] ImageUploadDTO imageUploadDto, CancellationToken cancellationToken)
    {
        if (imageUploadDto?.Image == null)
        {
            return BadRequest("No file uploaded.");
        }

        try
        {
            var command = new UploadImageCommand(imageUploadDto.Image);
            
            var url = await _mediator.Send(command, cancellationToken);

            return Ok(new { message = "Image uploaded successfully", url });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Error uploading image", error = ex.Message });
        }
    }
}