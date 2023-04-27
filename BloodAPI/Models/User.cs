using System.ComponentModel.DataAnnotations;

namespace BloodAPI.Models
{
    public class User
    {
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
