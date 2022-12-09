using KakarazaZoohop.Model;
using KakarazaZooShop.WebApi.Dtos;

namespace KakarazaZooShop.WebApi.Extensions
{
    public static class DtoExtensions
    {
        public static OwnerDto ToDto(this Owner owner)
        {
            return new OwnerDto
            {
                Id = owner.Id,
                Fullname = owner.Fullname,
                Animals = owner.Animals.Select(a => a.ToDto()).ToList(),
            };
        }

        public static PatientDto ToDto(this Patient patient)
        {
            return new PatientDto
            {
                Id = patient.Id,
                AnimalName = patient.AnimalName,
                AnimalType = patient.AnimalType.ToString(),
                DateOfBirth = patient.DateOfBirth,
                Diagnosis = patient.Diagnosis,
                OwnerId = patient.OwnerId,
                OwnerName = patient.Owner?.Fullname,
                FutureVisit = patient.VisitRecord?.DateOfVisit.ToString() ?? string.Empty,
            };
        }
    }
}
