using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KakarazaZoohop.Model
{
    public class Patient : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string AnimalName { get; set; }

        [Required]
        public AnimalType AnimalType { get; set; }

        public int OwnerId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public Owner Owner { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Diagnosis { get; set; }

        public int? VisitRecordId { get; set; }

        public VisitRecord VisitRecord { get; set; }
    }
}
