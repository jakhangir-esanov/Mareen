using Mareen.Service.Interfaces;

namespace Mareen.gRPC.Services;

public class GuestService : guest.guestBase
{
    private readonly IGuestService guestService;

    public GuestService(IGuestService guestService)
    {
        this.guestService = guestService;
    }


}
