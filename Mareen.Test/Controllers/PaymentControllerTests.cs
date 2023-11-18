using Mareen.Service.DTOs.Payments;
using Microsoft.AspNetCore.Mvc;

namespace Mareen.Test.Controllers;

public class PaymentControllerTests
{
    private readonly IPaymentService paymentService;
    private readonly PaymentController paymentController;
    public PaymentControllerTests()
    {
        this.paymentService = A.Fake<IPaymentService>();
        this.paymentController = new PaymentController(this.paymentService);
    }

    [Fact]
    public async void ShouldCreatePaymentSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        var payment = new PaymentCreationDto()
        {
            Amount = 1,
            BookingId = 1,
            GuestId = 1,
            PaymentStatus = Domain.Enums.PaymentStatus.InProgress,
            PaymentType = Domain.Enums.PaymentType.cash
        };

        //Act
        var result = await paymentController.CreateAsync(payment);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldUpdatePaymentSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        var payment = new PaymentUpdateDto()
        {
            Amount = 2,
            BookingId = 2,
            GuestId = 2,
            PaymentStatus = Domain.Enums.PaymentStatus.Success,
            PaymentType = Domain.Enums.PaymentType.card
        };

        //Act
        var result = await paymentController.UpdateAsync(payment);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldDeletePaymentSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        long id = 1;

        //Act
        var result = await paymentController.DeleteAsync(id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldGetByIdPaymentSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        long id = 1;

        //Act
        var result = await paymentController.GetByIdAsync(id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldGetAllPaymentsSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        
        //Act
        var result = await paymentController.GetAllAsync();

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }
}
