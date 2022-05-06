using System;
using System.Collections.Generic;

#nullable disable

namespace Proyecto_DesarrolloWeb_API.Dominio.Core.Entities
{
    public partial class PetAge
    {
        public PetAge()
        {
            Pet = new HashSet<Pet>();
        }

        public int Id { get; set; }
        public string AgeRange { get; set; }

        public virtual ICollection<Pet> Pet { get; set; }
    }
}
