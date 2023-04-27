using System.ComponentModel.DataAnnotations;

namespace BloodAPI.Models
{
    public class Admin : User
    {
        public int AdminID { get; set; }
    }
}
