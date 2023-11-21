using Mareen.Service.DTOs.Attachments;
using Mareen.Service.DTOs.Rooms;
using Mareen.Service.Interfaces;
using Mareen.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mareen.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoomController : ControllerBase
{
    private readonly IRoomService roomService;

    public RoomController(IRoomService roomService)
    {
        this.roomService = roomService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(RoomCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.roomService.AddAsync(dto)
        });

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(RoomUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.roomService.ModifyAsync(dto)
        });

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.roomService.RemoveAsync(id)
        });

    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.roomService.RetrieveByIdAsync(id)
        });

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.roomService.RetrieveAllAsync()
        });

    [HttpGet("get-all-available-rooms")]
    public async Task<IActionResult> GetAllAvailableRoomsAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.roomService.RetrieveAllAvailableAsync()
        });
}
