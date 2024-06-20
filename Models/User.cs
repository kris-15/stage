using System.ComponentModel;
using System.Text.Json.Serialization;

namespace AtmEquityProject.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public string LastName { get; set; }
        public required string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
        [DefaultValue(true)]
        public bool isActive { get; set; }
    }

}
