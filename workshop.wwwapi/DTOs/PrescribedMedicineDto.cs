using workshop.wwwapi.Models;

namespace workshop.wwwapi.DTOs
{
    public class PrescribedMedicineDto
    {
        public int PrescriptionId { get; set; }
        public int MedicineId { get; set; }
        public int Quantity { get; set; }
        public string Notes { get; set; }
    }
}
