namespace BloodAPI.Models
{
    public class DonationCenter
    {
        public int DonationCenterID { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public int OpensAt { get; set; }
        public int ClosesAt { get; set; }
        public int BloodBankID { get; set; }
        public int maxDayAppointments { get; set; }

        public virtual ICollection<Doctor>? Doctors { get; set; }
        public virtual ICollection<Appointment>? Appointments { get; set; }
        public virtual BloodBank? BloodBank { get; set; }
    }
}