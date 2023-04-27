namespace BloodAPI.Models
{
    public class BloodBank
    {
        public int BloodBankID { get; set; }
        public string Area { get; set; }

        public virtual ICollection<DonationCenter>? donationCenters { get; set; }
    }
}
