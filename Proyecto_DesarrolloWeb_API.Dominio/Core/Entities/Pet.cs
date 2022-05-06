using System;
using System.Collections.Generic;

#nullable disable

namespace Proyecto_DesarrolloWeb_API.Dominio.Core.Entities
{
    public partial class Pet
    {
        public Pet()
        {
            PetImage = new HashSet<PetImage>();
        }

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
        public DateTime? RegistrationDate { get; set; }

        public virtual PetAge IdPetAgeNavigation { get; set; }
        public virtual PetBreed IdPetBreedNavigation { get; set; }
        public virtual PetSize IdPetSizeNavigation { get; set; }
        public virtual PetType IdPetTypeNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
        public virtual ICollection<PetImage> PetImage { get; set; }
    }
}
