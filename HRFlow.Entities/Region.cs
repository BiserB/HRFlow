using System.ComponentModel.DataAnnotations;

namespace HRFlow.Entities
{
    public class Region
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Country> Countries { get; set; }
    }
}
