using Mareen.Service.DTOs.Services;
using Microsoft.AspNetCore.Mvc;

namespace Mareen.Test.Controllers;

public class ServiceControllerTests
{
    private readonly IServiceService serviceService;
    private readonly ServiceController serviceController;
    public ServiceControllerTests()
    {
        this.serviceService = A.Fake<IServiceService>();
        this.serviceController = new ServiceController(this.serviceService);
    }

    [Fact]
    public async void ShouldCreateServiceSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        var service = new ServiceCreationDto()
        {
            Description = "description",
            ServiceName = "name",
            Price = 1212
        };

        //Act
        var result = await serviceController.CreateAsync(service);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldUpdateServiceSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        var service = new ServiceUpdateDto()
        {
            Description = "description",
            ServiceName = "name",
            Price = 1515
        };

        //Act
        var result = await serviceController.UpdateAsync(service);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldDeleteServiceSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        long id = 1;

        //Act
        var result = await serviceController.DeleteAsync(id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldGetByIdServiceSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        long id = 1;

        //Act 
        var result = await serviceController.GetByIdAsync(id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldGetAllServicesSuccessfullyReturnStatusCodeOk()
    {
        //Arrange

        //Act
        var result = await serviceController.GetAllAsync();

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }
}
