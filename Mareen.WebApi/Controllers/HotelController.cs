using Mareen.Service.DTOs.Hotels;
using Mareen.Service.Interfaces;
using Mareen.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mareen.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelController : ControllerBase
{
    private readonly IHotelService hotelService;

    public HotelController(IHotelService hotelService)
    {
        this.hotelService = hotelService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(HotelCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.hotelService.AddAsync(dto)
        });

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(HotelUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.hotelService.ModifyAsync(dto)
        });

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.hotelService.RemoveAsync(id)
        });

    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.hotelService.RetrieveByIdAsync(id)
        });

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.hotelService.RetrieveAllAsync()
        });

    [HttpGet("get-all-employees")]
    public async Task<IActionResult> GetAllEmployeesAsync(long id)
        => Ok(new Response 
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.hotelService.RetrieveAllEmployeesAsync(id)
        });

    [HttpGet("get-all-rooms")]
    public async Task<IActionResult> GetAllRoomsAsync(long id)
        => Ok(new Response 
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.hotelService.RetrieveAllRoomsAsync(id)
        });
}
