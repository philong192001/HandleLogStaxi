using EFCore.BulkExtensions;
using ManageVoyage.Data;
using ManageVoyage.Models;
using System.Collections.Concurrent;

namespace ManageVoyage.Repositories;

public class CaroBookRepository : ICaroBookRepository
{
    private readonly CaroBookingContext _context;
    private ConcurrentBag<CaroBooking> caroBookings;
    private ConcurrentBag<CaroBookingProcess> caroBookingProcesses;

    public CaroBookRepository(CaroBookingContext context)
    {
        _context = context;
        caroBookingProcesses = new ConcurrentBag<CaroBookingProcess>();
        caroBookings = new ConcurrentBag<CaroBooking>();
    }

    public string SaveRange(VoyageCaroRes res)
    {
        // Chuyển đổi danh sách res.Data thành danh sách CaroBooking
        var carBooks = res.Data.Select(l => new CaroBooking
        {
            CarType = l.Car_Type,
            CreatedAt = l.Created_At,
            NameCustomer = l.Customer.Name,
            PhoneCustomer = l.Customer.Phone,
            Partner = l.Partner_Uid,
            TotalCash = l.Total_Price,
            SourceAddress = l.Source.Address,
            ReturnAddress = l.Destinations.Any() ? l.Destinations[0].Address : null,
            SourceLat = l.Source.Latitude,
            SourceLong = l.Source.Longitude,
            SourceApp = l.Customer_Source_Display,
            Status = l.Status
        }).ToList();

        foreach (var item in carBooks)
        {
            //Kiểm tra xem carBook đã tồn tại trong danh sách _context.CaroBookings chưa
            if (!_context.CaroBookings.Any(existingCarBook => existingCarBook.NameCustomer == item.NameCustomer && existingCarBook.PhoneCustomer == item.PhoneCustomer
            && existingCarBook.CreatedAt == item.CreatedAt && existingCarBook.SourceAddress == item.SourceAddress && existingCarBook.SourceLat == item.SourceLat
            && existingCarBook.SourceLong == item.SourceLong && existingCarBook.Status == item.Status && existingCarBook.CarType == item.CarType
            && existingCarBook.SourceApp == item.SourceApp && existingCarBook.TotalCash == item.TotalCash && existingCarBook.Partner == item.Partner && existingCarBook.ReturnAddress == item.ReturnAddress))
            {
                caroBookings.Add(item);
            }

            //Kiểm tra xem carBook đã tồn tại trong danh sách _context.CaroBookings chưa  và kiểm tra check nếu trùng trong list thì không add vào list - PhoneNumber unique , PK
            if (!_context.CaroBookingProcesses.Any(existingCarBookProcess => existingCarBookProcess.PhoneCustomer == item.PhoneCustomer) && !caroBookingProcesses.Any(x => x.PhoneCustomer == item.PhoneCustomer))
            {
                caroBookingProcesses.Add(new CaroBookingProcess()
                {
                    NameCustomer = item.NameCustomer,
                    PhoneCustomer = item.PhoneCustomer,
                });
            }
        }

        // Bulk insert danh sách carBooks vào cơ sở dữ liệu
        _context.BulkInsert(caroBookings);
        _context.BulkInsert(caroBookingProcesses);

        return $"{caroBookings.Count()}:{caroBookingProcesses.Count()}";
    }
}