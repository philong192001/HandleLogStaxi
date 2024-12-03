using System.ComponentModel.DataAnnotations.Schema;

namespace ManageVoyage.Models;

public class VoyageCaroRes
{
    public List<Data>? Data { get; set; }
    public int Total { get; set; }
}

public class Data
{
    [Column("Status")]
    public string? Status { get; set; }

    [Column("Customer")]
    public Customer Customer { get; set; }

    [Column("Source")]
    public AddressFromTo? Source { get; set; }

    [Column("Destinations")]
    public List<AddressFromTo>? Destinations { get; set; }

    [Column("Created_At")]
    public decimal? Created_At { get; set; }

    [Column("Car_Type")]
    public string? Car_Type { get; set; }

    [Column("Total_Price")]
    public decimal? Total_Price { get; set; }

    [Column("Partner_Uid")]
    public string? Partner_Uid { get; set; }

    [Column("Customer_Source_Display")]
    public string? Customer_Source_Display { get; set; }
}

public class Customer
{
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Name { get; set; }
}

public class AddressFromTo
{
    public string? Address { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
}