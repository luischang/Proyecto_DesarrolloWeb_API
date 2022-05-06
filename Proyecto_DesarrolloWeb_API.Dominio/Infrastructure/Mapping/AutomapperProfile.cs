using AutoMapper;
using Proyecto_DesarrolloWeb_API.Dominio.Core.DTOs;
using Proyecto_DesarrolloWeb_API.Dominio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_DesarrolloWeb_API.Dominio.Infrastructure.Mapping
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Pet, PetAllDTO>();
            CreateMap<PetPostDTO, Pet>();

            CreateMap<PetTypeDTO, PetType>();
            CreateMap<PetType, PetTypeDTO>();

            CreateMap<PetAge, PetAgeDTO>();
            CreateMap<PetAgeDTO, PetAge>();

            CreateMap<PetBreed, PetBreedDTO>();
            CreateMap<PetBreedDTO, PetBreed>();

            CreateMap<PetSize, PetSizeDTO>();
            CreateMap<PetSizeDTO, PetSize>();

            CreateMap<PetImage, PetImageDTO>();
            CreateMap<PetImageDTO, PetImage>();

            CreateMap<LostPet, LostPetDTO>();
            CreateMap<LostPetDTO, LostPet>();

            CreateMap<PetAdoption, PetAdoptionDTO>();
            CreateMap<PetAdoptionDTO, PetAdoption>();
            
            CreateMap<LostPet, LostPetDTORegistration>();
            CreateMap<LostPetDTORegistration, LostPet>();

            CreateMap<PetAdoptionDTO, PetAdoptionDTO>();
            CreateMap<PetAdoptionDTO, PetAdoptionDTO>();

            CreateMap<LostPet, LostPetDTORegistrationFound>();
            CreateMap<LostPetDTORegistrationFound, LostPet>();

        }
    }
}
