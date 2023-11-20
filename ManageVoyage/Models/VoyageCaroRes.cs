namespace ManageVoyage.Models;

public class VoyageCaroRes
{
    public List<Data> Data { get; set; }
    public int Total { get; set; }
}

public class Data
{
    public string Status { get; set; }
    public Customer Customer { get; set; }
    public AddressFromTo Source { get; set; }
    public List<AddressFromTo> Destinations { get; set; }
    public decimal Created_At { get; set; }
    public string Car_Type { get; set; }
    public decimal Total_Price { get; set; }
    public string Partner_Uid { get; set; }
    public string Customer_Source_Display { get; set; }
}


public class Customer
{
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Name { get; set; }
}

public class AddressFromTo
{
    public string Address { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
}