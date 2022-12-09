using KakarazaZoohop.Model;
using System.ComponentModel.DataAnnotations;

namespace KakarazaZooShop.WebApi.Dtos
{
    public class UpdatePatientDto
    {
        [Required]
        [MaxLength(50)]
        public string AnimalName { get; set; }

        [Required]
        public AnimalType AnimalType { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Diagnosis { get; set; }
    }
}
