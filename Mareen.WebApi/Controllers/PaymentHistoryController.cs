using Mareen.Service.DTOs.PaymentHistories;
using Mareen.Service.Interfaces;
using Mareen.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mareen.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentHistoryController : ControllerBase
{
    private readonly IPaymentHistoryService historyService;

    public PaymentHistoryController(IPaymentHistoryService historyService)
    {
        this.historyService = historyService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(PaymentHistoryCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.historyService.AddAsync(dto)
        });

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(PaymentHistoryUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.historyService.ModifyAsync(dto)
        });

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.historyService.RetrieveByIdAsync(id)
        });

    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.historyService.RetrieveByIdAsync(id)
        });

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
        => Ok(new Response
        {
            StatusCode = 200, 
            Message = "Success",
            Data = await this.historyService.RetrieveAllAsync()
        });
}
