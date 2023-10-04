using Mareen.Service.DTOs.Bookings;
using Mareen.Service.Interfaces;
using Mareen.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mareen.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly IBookingService bookingService;

    public BookingController(IBookingService bookingService)
    {
        this.bookingService = bookingService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync(BookingCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.bookingService.AddAsync(dto)
        });

    [HttpPut("update")]
    public async Task<IActionResult> PutAsync(BookingUpdateDto dto)
        => Ok(new Response 
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.bookingService.ModifyAsync(dto)
        });

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.bookingService.RemoveAsync(id)
        });

    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.bookingService.RetrieveByIdAsync(id)
        });

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.bookingService.RetrieveAllAsync()
        });

    [HttpGet("get-all-booking-items")]
    public async Task<IActionResult> GetAllBookingItemsAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.bookingService.RetrieveAllBookingItemsAsync(id)
        });
}
