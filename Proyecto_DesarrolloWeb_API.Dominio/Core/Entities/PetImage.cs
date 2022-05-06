using System;
using System.Collections.Generic;

#nullable disable

namespace Proyecto_DesarrolloWeb_API.Dominio.Core.Entities
{
    public partial class PetImage
    {
        public int Id { get; set; }
        public byte[] Photo { get; set; }
        public int? IdPet { get; set; }

        public virtual Pet IdPetNavigation { get; set; }
    }
}
