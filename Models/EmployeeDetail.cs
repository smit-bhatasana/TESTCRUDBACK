namespace APIProject.Models
{
    public class EmployeeDetail
    {
        public int EmployeeDetailId { get; set; }

        public int EmployeeId { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public DateTime JoiningDate { get; set; }
    }
}
