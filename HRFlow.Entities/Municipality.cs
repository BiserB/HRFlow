using System.ComponentModel.DataAnnotations;

namespace HRFlow.Entities
{
    public class Municipality
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ISOCode { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }

        public List<Contact> Contacts { get; set; }
    }
}