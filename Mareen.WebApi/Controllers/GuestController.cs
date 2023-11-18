using Mareen.Service.DTOs.Attachments;
using Mareen.Service.DTOs.Guests;
using Mareen.Service.Interfaces;
using Mareen.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mareen.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GuestController : ControllerBase
{
    private readonly IGuestService guestService;

    public GuestController(IGuestService guestService)
    {
        this.guestService = guestService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(GuestCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.guestService.AddAsync(dto)
        });

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(GuestUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.guestService.ModifyAsync(dto)
        });

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.guestService.RemoveAsync(id)
        });

    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.guestService.RetrieveByIdAsync(id)
        });

    [HttpPost("image-upload")]
    public async Task<IActionResult> ImageUploadAsync(long guestId, [FromForm] AttachmentCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.guestService.ImageUploadAsync(guestId, dto)
        });

    [HttpPost("update-image")]
    public async Task<IActionResult> UpdateImageAsync(long guestId, [FromForm] AttachmentCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.guestService.ModifyImageAsync(guestId, dto)
        });

    [HttpGet("get-all-payment-histories")]
    public async Task<IActionResult> GetAllPaymentHisotriesAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.guestService.RetrieveAllPaymentHistoriesAsync(id)
        });

    [HttpGet("get-all-bookings")]
    public async Task<IActionResult> GetAllBookingsAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.guestService.RetrieveAllBookingsAsync(id)
        });

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllGuestAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.guestService.RetrieveAllAsync()
        });
}
