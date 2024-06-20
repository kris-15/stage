using System.ComponentModel;

namespace AtmEquityProject.Models
{
    public class AuthenticateRequest
    {
        [DefaultValue("Username")]
        public required string Username { get; set; }

        [DefaultValue("********")]
        public required string Password { get; set; }
    }
}
