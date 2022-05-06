using Proyecto_DesarrolloWeb_API.Dominio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_DesarrolloWeb_API.Dominio.Core.DTOs
{
    public class PetAllDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? IdPetType { get; set; }
        public int? IdPetBreed { get; set; }
        public string Sex { get; set; }
        public int? IdPetSize { get; set; }
        public int? IdPetAge { get; set; }
        public string Temperament { get; set; }
        public string Video { get; set; }
        public int? IdUser { get; set; }

    }

    public class PetPostDTO
    {
        public string Name { get; set; }
        public int? IdPetType { get; set; }
        public int? IdPetBreed { get; set; }
        public string Sex { get; set; }
        public int? IdPetSize { get; set; }
        public int? IdPetAge { get; set; }
        public string Temperament { get; set; }
        public string Video { get; set; }
        public int? IdUser { get; set; }
        public ICollection<PetImageDTO> PetImage { get; set; }

    }

    public class PetTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PetAgeDTO
    {
        public int Id { get; set; }
        public string AgeRange { get; set; }
    }

    public class PetBreedDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PetSizeDTO
    {
        public int Id { get; set; }
        public string Size { get; set; }
    }

    public class PetImageDTO
    {
        public int Id { get; set; }
        public byte[] Photo { get; set; }
        public int? IdPet { get; set; }
    }
}
