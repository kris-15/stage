namespace AtmEquityProject.Models
{
    public class Atm
    {
        public int Id { get; set; }
        public int Seuil { get; set; }
        public int NumCompte { get; set; }
        public string? Adresse { get; set; }
        public string? Devise { get; set; }
        public ICollection<Balance> Balances { get; set; } = new List<Balance>();
    }
}
