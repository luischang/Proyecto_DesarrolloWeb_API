using System;
using System.Collections.Generic;

#nullable disable

namespace Proyecto_DesarrolloWeb_API.Dominio.Core.Entities
{
    public partial class PetAdoption
    {
        public int Id { get; set; }
        public int? IdPet { get; set; }
        public int? IdUser { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime? RegistrationDate { get; set; }
    }
}
