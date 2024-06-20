using AtmEquityProject.Models;

namespace AtmEquityProject.Interfaces
{
    public interface IAtm
    {
        ICollection<Atm> GetAtms();
        Atm GetAtm(int index);
        Atm GetAtmById(int index);

        bool CreateAtm(Atm atm);
        bool DeleteAtm(Atm atm);
        bool UpdateAtm(Atm atm);
        bool AtmExists(int accountNum);
        bool AtmExistsWithId(int id);
        bool Save();
        bool CheckAtmById(int atmId);
    }
}
