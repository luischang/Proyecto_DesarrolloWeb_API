using System;
using System.Collections.Generic;

#nullable disable

namespace Proyecto_DesarrolloWeb_API.Dominio.Core.Entities
{
    public partial class PetSize
    {
        public PetSize()
        {
            Pet = new HashSet<Pet>();
        }

        public int Id { get; set; }
        public string Size { get; set; }

        public virtual ICollection<Pet> Pet { get; set; }
    }
}
