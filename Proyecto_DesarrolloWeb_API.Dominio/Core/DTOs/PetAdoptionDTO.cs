using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_DesarrolloWeb_API.Dominio.Core.DTOs
{
    public class PetAdoptionDTO
    {
        public int? IdUser { get; set; }
        public int? IdPet { get; set; }
        public string Description { get; set; }
    }
}
