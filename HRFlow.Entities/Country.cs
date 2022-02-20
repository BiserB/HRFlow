using System.ComponentModel.DataAnnotations;

namespace HRFlow.Entities
{
    public class Country
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ISOCode { get; set; }

        public int RegionId { get; set; }

        public Region Region { get; set; }

        public List<Municipality> Municipalities { get; set; }
    }
}
