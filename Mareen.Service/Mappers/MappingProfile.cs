using AutoMapper;
using Mareen.Domain.Entities;
using Mareen.Service.DTOs.BookingItems;
using Mareen.Service.DTOs.Bookings;
using Mareen.Service.DTOs.Guests;
using Mareen.Service.DTOs.HotelAttachments;
using Mareen.Service.DTOs.Hotels;
using Mareen.Service.DTOs.PaymentHistories;
using Mareen.Service.DTOs.Payments;
using Mareen.Service.DTOs.RoomAttachments;
using Mareen.Service.DTOs.Rooms;
using Mareen.Service.DTOs.ServiceAttachments;
using Mareen.Service.DTOs.Services;
using Mareen.Service.DTOs.Users;

namespace Mareen.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Booking 
        CreateMap<Booking, BookingCreationDto>().ReverseMap();
        CreateMap<BookingUpdateDto, Booking>().ReverseMap();
        CreateMap<BookingResultDto, Booking>().ReverseMap();

        //BookingItem
        CreateMap<BookingItem, BookingCreationDto>().ReverseMap();
        CreateMap<BookingItemUpdateDto, BookingItem>().ReverseMap();
        CreateMap<BookingItemResultDto, BookingItem>().ReverseMap();

        //Guest
        CreateMap<Guest, GuestCreationDto>().ReverseMap();
        CreateMap<GuestUpdateDto, Guest>().ReverseMap();
        CreateMap<GuestResultDto, Guest>().ReverseMap();

        //Hotel
        CreateMap<Hotel, HotelCreationDto>().ReverseMap();
        CreateMap<HotelUpdateDto, Hotel>().ReverseMap();
        CreateMap<HotelResultDto, Hotel>().ReverseMap();

        //Payment
        CreateMap<Payment, PaymentCreationDto>().ReverseMap();
        CreateMap<PaymentUpdateDto, Payment>().ReverseMap();
        CreateMap<PaymentResultDto, Payment>().ReverseMap();

        //PaymentHistory
        CreateMap<PaymentHistory, PaymentHistoryCreationDto>().ReverseMap();
        CreateMap<PaymentHistoryUpdateDto, PaymentHistory>().ReverseMap();
        CreateMap<PaymentHistoryResultDto, PaymentHistory>().ReverseMap();

        //Room
        CreateMap<Room, RoomCreationDto>().ReverseMap();
        CreateMap<RoomUpdateDto, Room>().ReverseMap();
        CreateMap<RoomResultDto, Room>().ReverseMap();

        //Service
        CreateMap<Domain.Entities.Service, ServiceCreationDto>().ReverseMap();
        CreateMap<ServiceUpdateDto, Domain.Entities.Service>().ReverseMap();
        CreateMap<ServiceResultDto, Domain.Entities.Service>().ReverseMap();

        //User
        CreateMap<User, UserCreationDto>().ReverseMap();
        CreateMap<UserUpdateDto, User>().ReverseMap();
        CreateMap<UserResultDto, User>().ReverseMap();

        //HotelAttachment
        CreateMap<HotelAttachment, HotelAttachmentCreationDto>().ReverseMap();
        CreateMap<HotelAttachmentUpdateDto, HotelAttachment>().ReverseMap();
        CreateMap<HotelAttachmentResultDto, HotelAttachment>().ReverseMap();

        //RoomAttachment
        CreateMap<RoomAttachment, RoomAttachmentCreationDto>().ReverseMap();
        CreateMap<RoomAttachmentUpdateDto, RoomAttachment>().ReverseMap();
        CreateMap<RoomAttachmentResultDto, RoomAttachment>().ReverseMap();

        //ServiceAttachment
        CreateMap<ServiceAttachment, ServiceAttachmentCreationDto>().ReverseMap();
        CreateMap<ServiceAttachmentUpdateDto, ServiceAttachment>().ReverseMap();
        CreateMap<ServiceAttachmentResultDto, ServiceAttachment>().ReverseMap();
    }
}
