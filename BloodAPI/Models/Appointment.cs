namespace BloodAPI.Models
{
    public enum Status
    {
        Pending,
        Confirmed
    }
    public class Appointment
    {
        public int Id { get; set; }
        public int DonorID { get; set; }
        public int DonationCenterID { get; set; }
        [DateValidation]
        public DateTime Date { get; set; }
        public Status StatusA { get; set; }
        public Donor? Donor { get; set; }
        public DonationCenter? DonationCenter { get; set; }
    }
}