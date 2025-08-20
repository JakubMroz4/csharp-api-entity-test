namespace workshop.wwwapi.DTOs
{
    public class AppointmentForPatientDto
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public DateTime Booking { get; set; }
    }
}
