using HRFlow.Entities.Enums;

namespace HRFlow.Entities
{
    public class Contact
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string PersonalEmail { get; set; }

        public string CompanyEmail { get; set; }

        public string PhoneNumber { get; set; }   

        public Gender Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public string ExtraInfo { get; set; }
    }
}