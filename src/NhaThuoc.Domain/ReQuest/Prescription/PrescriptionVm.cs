namespace NhaThuoc.Domain.ReQuest.Prescription
{
    public class PrescriptionVm
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string CustomerNote { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
