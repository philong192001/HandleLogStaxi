using ManageVoyage.Models;

namespace ManageVoyage.Repositories;

public interface ICaroBookRepository
{
    public void SaveRange(VoyageCaroRes res);
}
