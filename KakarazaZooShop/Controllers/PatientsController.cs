using KakarazaZoohop.Model;
using KakarazaZooShop.WebApi.Dtos;
using KakarazaZooShop.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KakarazaZooShop.WebApi.Controllers
{
    [ApiController]
    [Route("/patients")]
    public class PatientsController : ControllerBase
    {
        private readonly IZooShopContext _zooShopContext;

        public PatientsController(IZooShopContext zooShopContext)
        {
            _zooShopContext = zooShopContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetPatients()
        {
            var patients = await _zooShopContext.Patients.Include(p => p.Owner).Include(p => p.VisitRecord).ToListAsync();
            return Ok(patients.Select(p => p.ToDto()).ToList());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var patient = await _zooShopContext.Patients.Include(p => p.Owner).Include(p => p.VisitRecord).FirstOrDefaultAsync(p => p.Id == id);
            if (patient is null)
            {
                return BadRequest("Invalid id");
            }

            return Ok(patient.ToDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreatePatient(CreatePatientDto dto)
        {
            var owner = await _zooShopContext.Owners.FirstOrDefaultAsync(o => o.Fullname.ToLower() == dto.OwnerName.ToLower());
            if (owner is null)
            {
                owner = new Owner
                {
                    Fullname = dto.OwnerName,
                };


                await _zooShopContext.Owners.AddAsync(owner);
                await _zooShopContext.SaveChangesAsync();
            }

            var newPatient = new Patient
            {
                AnimalName = dto.AnimalName,
                AnimalType = dto.AnimalType,
                DateOfBirth = dto.DateOfBirth,
                Diagnosis = dto.Diagnosis,
                OwnerId = owner.Id,
            };

            await _zooShopContext.Patients.AddAsync(newPatient);
            await _zooShopContext.SaveChangesAsync();

            if (dto.FutureVisit is not null && dto.FutureVisit.HasValue)
            {
                var newVisitRecord = new VisitRecord
                {
                    DateOfVisit = dto.FutureVisit.Value,
                };

                await _zooShopContext.VisitRecords.AddAsync(newVisitRecord);
                newPatient.VisitRecord = newVisitRecord;
                _zooShopContext.Patients.Update(newPatient);
                await _zooShopContext.SaveChangesAsync();
            }



            return Ok(newPatient.ToDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient([FromRoute]int id, UpdatePatientDto dto)
        {
            var patientToUpdate = await _zooShopContext.Patients.FindAsync(id);

            if (patientToUpdate is null)
            {
                return BadRequest("Invalid id");
            }

            patientToUpdate.AnimalName = dto.AnimalName;
            patientToUpdate.AnimalType = dto.AnimalType;
            patientToUpdate.DateOfBirth = dto.DateOfBirth;
            patientToUpdate.Diagnosis = dto.Diagnosis;

            _zooShopContext.Patients.Update(patientToUpdate);
            await _zooShopContext.SaveChangesAsync();

            return Ok(patientToUpdate.ToDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> UpdatePatient([FromRoute] int id)
        {
            var patientToRemove = await _zooShopContext.Patients.FindAsync(id);

            if (patientToRemove is null)
            {
                return BadRequest("Invalid id");
            }

            _zooShopContext.Patients.Remove(patientToRemove);
            await _zooShopContext.SaveChangesAsync();

            return Ok();
        }
    }
}
