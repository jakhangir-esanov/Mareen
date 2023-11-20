using Mareen.DAL.IRepositories;
using Mareen.DAL.Repositories;
using Mareen.Service.Interfaces;
using Mareen.Service.Mappers;
using Mareen.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Mareen.gRPC.Extentions;

public static class ServiceCollection
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddAutoMapper(typeof(MappingProfile));

        services.AddScoped<IAttachmentService, AttachmentService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IBookingItemService, BookingItemService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IGuestService, GuestService>();
        services.AddScoped<IHotelService, HotelService>();
        services.AddScoped<IPaymentHistoryService, PaymentHistoryService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<IUserService, UserService>();
    }

    public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            var Key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JWT:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Key)
            };
        });
    }
}
