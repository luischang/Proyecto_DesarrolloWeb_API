using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_DesarrolloWeb_API.Dominio.Core.DTOs
{
    public class LostPetDTO
    {
    }

    public class LostPetDTORegistration
    {
        public int? IdUser { get; set; }
        public int? IdPet { get; set; }
        public DateTime? DateOfLoss { get; set; }
        public string DescriptionOfLoss { get; set; }
    }

    public class LostPetDTORegistrationFound
    {
        public int Id { get; set; }
        public int? IdUser { get; set; }
        public int? IdPet { get; set; }
        public DateTime? DateOfFound { get; set; }
        public string DescriptionOfFound { get; set; }
    }

    public class LostPetJoinDTO
    {
        public int? IdUser { get; set; }
        public int? IdPet { get; set; }
        public DateTime? DateOfLoss { get; set; }
        public string DescriptionOfLoss { get; set; }
        public string Name { get; set; }
    }
}
