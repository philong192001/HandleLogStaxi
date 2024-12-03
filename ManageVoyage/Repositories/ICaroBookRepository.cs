using ManageVoyage.Models;

namespace ManageVoyage.Repositories;

public interface ICaroBookRepository
{
    public string SaveRange(VoyageCaroRes res);
}