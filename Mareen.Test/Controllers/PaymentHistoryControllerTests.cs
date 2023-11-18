using Mareen.Service.DTOs.PaymentHistories;
using Microsoft.AspNetCore.Mvc;

namespace Mareen.Test.Controllers;

public class PaymentHistoryControllerTests
{
    private readonly IPaymentHistoryService paymentHistoryService;
    private readonly PaymentHistoryController paymentHistoryController;
    public PaymentHistoryControllerTests()
    {
        this.paymentHistoryService = A.Fake<IPaymentHistoryService>();
        this.paymentHistoryController = new PaymentHistoryController(this.paymentHistoryService);
    }

    [Fact]
    public async void ShouldCreatePaymentHistorySuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        var paymentHisotry = new PaymentHistoryCreationDto()
        {
            Amount = 100,
            BookingId = 1,
            GuestId = 1,
            PaymentId = 1,
            PaymentStatus = Domain.Enums.PaymentStatus.InProgress,
            PaymentType = Domain.Enums.PaymentType.cash
        };

        //Act
        var result = await paymentHistoryController.CreateAsync(paymentHisotry);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldUpdatePaymentHistorySuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        var paymentHistory = new PaymentHistoryUpdateDto()
        {
            Amount = 100,
            BookingId = 2,
            GuestId = 2,
            PaymentId = 2,
            PaymentStatus = Domain.Enums.PaymentStatus.Success,
            PaymentType = Domain.Enums.PaymentType.card
        };

        //Act
        var result = await paymentHistoryController.UpdateAsync(paymentHistory);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldDeletePaymentHisotrySuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        long id = 1;

        //Act
        var result = await paymentHistoryController.DeleteAsync(id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldGetByIdPaymentHistorySuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        long id = 1;

        //Act
        var result = await paymentHistoryController.GetByIdAsync(id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldGetAllPaymentHistorySuccessfullyReturnStatusCodeOk()
    {
        //Arrange

        //Act
        var result = await paymentHistoryController.GetAllAsync();

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }
}

