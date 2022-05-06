using Proyecto_DesarrolloWeb_API.Dominio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_DesarrolloWeb_API.Dominio.Core.DTOs
{
    public class ReportDTO
    {
    }

    public class ReportUserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public int petsCount { get; set; }
    }

    public class ReportPetDTO
    {
        public int IdPet { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string LostStatus { get; set; }
        public string AdoptionStatus { get; set; }
        public string Type { get; set; }
        public string Age { get; set; }
        public ICollection<byte[]> Photo { get; set; }
    }
    public class ReportPetWithImageDTO
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string LostStatus { get; set; }
        public string AdoptionStatus { get; set; }
        public string Type { get; set; }
        public string Age { get; set; }
        public ICollection<PetImage> PetImage { get; set; }
    }
}
