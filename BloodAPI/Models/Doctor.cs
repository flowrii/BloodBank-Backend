
using System.ComponentModel.DataAnnotations;

namespace BloodAPI.Models
{
    public class Doctor : User
    {
        public int DoctorID { get; set; }   
        public string FirstName { get; set; }       
        public string LastName { get; set; }       
        public string Email { get; set; }
        public string CNP { get; set; }
        public int DonationCenterID { get; set; }

        public DonationCenter? DonationCenter { get; set; }
    }
}
