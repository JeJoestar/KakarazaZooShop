using KakarazaZoohop.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KakarazaZooShop.WebApi.Dtos
{
    public class PatientDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string AnimalName { get; set; }

        [Required]
        public string AnimalType { get; set; }

        public int OwnerId { get; set; }

        public string OwnerName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Diagnosis { get; set; }

        public string FutureVisit { get; set; }
    }
}
