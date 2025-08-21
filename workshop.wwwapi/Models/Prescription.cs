
namespace workshop.wwwapi.Models
{
    public class Prescription
    {
        public int Id { get; set; }
        public ICollection<PrescribedMedicine> Medicines { get; set; } = new List<PrescribedMedicine>();
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
    }
}
