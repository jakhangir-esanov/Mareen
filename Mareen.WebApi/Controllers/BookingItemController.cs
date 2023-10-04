using Mareen.Service.DTOs.BookingItems;
using Mareen.Service.Interfaces;
using Mareen.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mareen.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingItemController : ControllerBase
{
    private readonly IBookingItemService bookingItemService;

    public BookingItemController(IBookingItemService bookingItemService)
    {
        this.bookingItemService = bookingItemService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(BookingItemCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.bookingItemService.AddAsync(dto)
        });

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(BookingItemUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.bookingItemService.ModifyAsync(dto)
        });

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.bookingItemService.RemoveAsync(id)
        });

    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.bookingItemService.RetrieveByIdAsync(id)
        });

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.bookingItemService.RetrieveAllAsync()
        });
}
