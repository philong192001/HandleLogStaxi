using ManageVoyage.Data;
using ManageVoyage.Models;

namespace ManageVoyage.Repositories;

public class CaroBookRepository : ICaroBookRepository
{
    private readonly CaroBookingContext _context;

    public CaroBookRepository(CaroBookingContext context)
    {
        _context = context;
    }

    public void SaveRange(VoyageCaroRes res)
    {
        foreach (var l in res.Data)
        {
            var carBook = new CaroBooking()
            {
                CarType = l.Car_Type,
                CreatedAt = l.Created_At,
                NameCustomer = l.Customer.Name,
                PhoneCustomer = l.Customer.Phone,
                Partner = l.Partner_Uid,
                TotalCash = l.Total_Price,
                SourceAddress = l.Source.Address,
                ReturnAddress = l.Destinations[0].Address,
                SourceLat = l.Source.Latitude,
                SourceLong = l.Source.Longitude,
                SourceApp = l.Customer_Source_Display,
                Status = l.Status
            };

            // Kiểm tra xem carBook đã tồn tại trong danh sách _context.CaroBookings chưa
            if (!_context.CaroBookings.Any(existingCarBook => existingCarBook.NameCustomer == carBook.NameCustomer && existingCarBook.PhoneCustomer == carBook.PhoneCustomer
            && existingCarBook.CreatedAt == carBook.CreatedAt && existingCarBook.SourceAddress == carBook.SourceAddress && existingCarBook.SourceLat == carBook.SourceLat
            && existingCarBook.SourceLong == carBook.SourceLong && existingCarBook.Status == carBook.Status && existingCarBook.CarType == carBook.CarType
            && existingCarBook.SourceApp == carBook.SourceApp && existingCarBook.TotalCash == carBook.TotalCash && existingCarBook.Partner == carBook.Partner
            ))
            {
                _context.CaroBookings.Add(carBook);
            }


            var carBookProcess = new CaroBookingProcess()
            {
                NameCustomer = l.Customer.Name,
                PhoneCustomer = l.Customer.Phone,
            };

            // Kiểm tra xem carBook đã tồn tại trong danh sách _context.CaroBookingsProcess chưa
            if (!_context.CaroBookingProcesses.Any(existingCarBookProcess => existingCarBookProcess.NameCustomer == carBookProcess.NameCustomer && existingCarBookProcess.PhoneCustomer == carBookProcess.PhoneCustomer))
            {
                _context.CaroBookingProcesses.Add(carBookProcess);
            }
            _context.SaveChanges();
        }
    }
}
