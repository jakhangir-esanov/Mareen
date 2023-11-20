using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Mareen.Service.DTOs.Users;
using Mareen.Service.Interfaces;

namespace Mareen.gRPC.Services;

public class UserService : user.userBase
{
    private readonly IUserService userService;

    public UserService(IUserService userService)
    {
        this.userService = userService;
    }

    public override async Task<UserCreationResponse> CreateAsync(UserCreationRequest request, ServerCallContext context)
    {
        var user = new UserCreationDto()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            DateOfBirth = request.DateOfBirth.ToDateTime(),
            HotelId = request.HotelId,
            Password = request.Password,
            UserRole = (Domain.Enums.Role)request.UserRole
        };

        var result = await userService.AddAsync(user);

        return await Task.FromResult(new UserCreationResponse
        {
            FirstName = result.FirstName,
            LastName = result.LastName,
            Email = result.Email,
            PhoneNumber = result.PhoneNumber,
            DateOfBirth = result.DateOfBirth.ToTimestamp(),
            HotelId = result.Hotel.Id,
            Password = result.Password,
            UserRole = (int)result.UserRole
        });
    }
}
