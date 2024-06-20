using System.Text.Json.Serialization;

namespace AtmEquityProject.Dto
{
    public class UserDto
    {
        public required string FirstName { get; set; }
        public string LastName { get; set; }
        public required string Username { get; set; }
        public string Password { get; set; }
    }
}
