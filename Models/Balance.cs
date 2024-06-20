namespace AtmEquityProject.Models
{
    public class Balance
    {
        public int Id { get; set; }
        public int Solde { get; set; }
        public Atm? IdAtm { get; set; }
    }
}
