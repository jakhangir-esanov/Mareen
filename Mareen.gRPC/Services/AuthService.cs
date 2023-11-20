using Grpc.Core;
using Mareen.Service.Interfaces;

namespace Mareen.gRPC.Services;

public class AuthService : auth.authBase
{
    private readonly IAuthService authService;

    public AuthService(IAuthService authService)
    {
        this.authService = authService;
    }

    public override async Task<AuthCreationForUserResponse> GenerateTokenForUsersAsync(AuthCreationForUserRequest request, ServerCallContext context)
    {
        var result = await authService.GenerateTokenForUserAsync(request.Email, request.Password);

        return await Task.FromResult(new AuthCreationForUserResponse
        {
            Token = result
        });
    }

    public override async Task<AuthCreationForGuestResponse> GenerateTokenForGuestAsync(AuthCreationForGuestRequest request, ServerCallContext context)
    {
        var result = await authService.GenerateTokenForGuestAsync(request.Email, request.Password);

        return await Task.FromResult(new AuthCreationForGuestResponse 
        {
            Token = result
        });
    }
}
