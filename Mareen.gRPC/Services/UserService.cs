using Grpc.Core;
using Google.Protobuf.WellKnownTypes;
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
            Id = result.Id,
            FirstName = result.FirstName,
            LastName = result.LastName,
            Email = result.Email,
            PhoneNumber = result.PhoneNumber,
            DateOfBirth = result.DateOfBirth.ToUniversalTime().ToTimestamp(),
            HotelId = result.Hotel.Id,
            Password = result.Password,
            UserRole = (int)result.UserRole
        });
    }

    public override async Task<UserUpdateResponse> UpdateAsync(UserUpdateRequest request, ServerCallContext context)
    {
        var user = new UserUpdateDto()
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Password = request.Password,
            DateOfBirth = request.DateOfBirth.ToDateTime(),
            HotelId = request.HotelId,
            UserRole = (Domain.Enums.Role)request.UserRole
        };

        var result = await userService.ModifyAsync(user);

        return await Task.FromResult(new UserUpdateResponse
        {
            Id = result.Id,
            FirstName = result.FirstName,
            LastName = result.LastName,
            Email = result.Email,
            PhoneNumber = result.PhoneNumber,
            Password = result.Password,
            DateOfBirth = result.DateOfBirth.ToUniversalTime().ToTimestamp(),
            HotelId = result.Hotel.Id,
            UserRole = (int)result.UserRole
        });
    }

    public override async Task<UserDeleteResponse> DeleteAsync(UserDeleteRequest request, ServerCallContext context)
    {
        long id = request.Id;

        var result = await userService.RemoveAsync(id);

        return await Task.FromResult(new UserDeleteResponse
        {
            IsDelete = result
        });
    }

    public override async Task<UserGetResponse> GetByIdAsync(UserGetRequest request, ServerCallContext context)
    {
        long id = request.Id;

        var result = await userService.RetrieveByIdAsync(id);

        return await Task.FromResult(new UserGetResponse
        {
            Id = result.Id,
            FirstName = result.FirstName,
            LastName = result.LastName,
            Email = result.Email,
            PhoneNumber = result.PhoneNumber,
            DateOfBirth = result.DateOfBirth.ToUniversalTime().ToTimestamp(),
            HotelId = result.Hotel.Id,
            Password = result.Password,
            UserRole = (int)result.UserRole
        });
    }

    public override async Task<UserGetAllResponse> GetAllAsync(UserGetAllRequest request, ServerCallContext context)
    {
        var result = await userService.RetrieveAllAsync();

        var response = new UserGetAllResponse();

        foreach (var item in result)
        {
            var user = new UserGetResponse
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Email = item.Email,
                PhoneNumber = item.PhoneNumber,
                Password = item.Password,
                DateOfBirth = item.DateOfBirth.ToUniversalTime().ToTimestamp(),
                HotelId = item.Hotel.Id,
                UserRole = (int)item.UserRole
            };

            response.Sth.Add(user);
        }

        return response;
    }
}
