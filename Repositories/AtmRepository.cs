using AtmEquityProject.Data;
using AtmEquityProject.Interfaces;
using AtmEquityProject.Models;

namespace AtmEquityProject.Repositories
{
    public class AtmRepository : IAtm
    {
        private readonly DataContext _context;
        public AtmRepository(DataContext context) 
        {
            _context = context;
        }
        public ICollection<Atm> GetAtms()
        {
            return _context.Atms.ToList();
        }
        public Atm GetAtm(int accountNum)
        {
            return _context.Atms.Where(a => a.NumCompte == accountNum).FirstOrDefault();
        }
        public Atm GetAtmById(int id)
        {
            return _context.Atms.Where(a => a.Id == id).FirstOrDefault();
        }
        public bool AtmExists(int accountNum)
        {
            return _context.Atms.Any(a => a.NumCompte == accountNum);
        }public bool AtmExistsWithId(int id)
        {
            return _context.Atms.Any(a => a.Id == id);
        }
        public bool CheckAtmById(int id)
        {
            return _context.Atms.Any(a => a.Id == id);
        }
        public bool CreateAtm(Atm atm)
        {
            _context.Add(atm);
            return Save();
        } 
        public bool DeleteAtm(Atm atm)
        {
            _context.Remove(atm);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateAtm(Atm atm)
        {
            _context.Update(atm);
            return Save();

        }
    }
}
