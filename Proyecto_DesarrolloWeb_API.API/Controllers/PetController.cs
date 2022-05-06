using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_DesarrolloWeb_API.API.Models;
using Proyecto_DesarrolloWeb_API.Dominio.Core.DTOs;
using Proyecto_DesarrolloWeb_API.Dominio.Core.Entities;
using Proyecto_DesarrolloWeb_API.Dominio.Core.Interfaces;
using Proyecto_DesarrolloWeb_API.Dominio.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_DesarrolloWeb_API.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IPetRepository _petRepository;
        private readonly ILostPetRepository _lostPetRepository;
        private readonly IPetAdoptionRepository _petAdoptionRepository;
        private readonly IMapper _mapper;


        public PetController(IPetRepository petRepository, ILostPetRepository lostPetRepository, IPetAdoptionRepository petAdoptionRepository, IMapper mapper)
        {
            _petRepository = petRepository;
            _lostPetRepository = lostPetRepository;
            _petAdoptionRepository = petAdoptionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetPets")]
        public async Task<IActionResult> GetPets()
        {
            var pets = await _petRepository.GetPets();
            var petsList = _mapper.Map<IEnumerable<PetAllDTO>>(pets);

            return Ok(petsList);
        }

        [HttpGet]
        [Route("GetUserPets")]
        public async Task<IActionResult> GetUserPets(int userId)
        {
            var pets = await _petRepository.GetUserPets(userId);
            var petsList = _mapper.Map<IEnumerable<PetAllDTO>>(pets);

            return Ok(petsList);
        }

        [HttpGet]
        [Route("GetLostPets")]
        public async Task<IActionResult> GetLostPets(int userId)
        {
            var pets = await _lostPetRepository.GetLostPets(userId);
            var petsList = _mapper.Map<IEnumerable<LostPetJoinDTO>>(pets);

            return Ok(petsList);
        }

        [HttpGet]
        [Route("GetPetsType")]
        public async Task<IActionResult> GetPetsType()
        {
            var petsType = await _petRepository.GetPetsType();

            var petsTypeList = _mapper.Map<IEnumerable<PetTypeDTO>>(petsType);

            return Ok(petsTypeList);
        }

        [HttpGet]
        [Route("GetPetsAge")]
        public async Task<IActionResult> GetPetsAge()
        {
            var petsAge = await _petRepository.GetPetsAge();

            var petsAgeList = _mapper.Map<IEnumerable<PetAgeDTO>>(petsAge);

            return Ok(petsAgeList);
        }

        [HttpGet]
        [Route("GetPetsBreed")]
        public async Task<IActionResult> GetPetsBreed()
        {
            var petsBreed = await _petRepository.GetPetsBreed();

            var petsBreedList = _mapper.Map<IEnumerable<PetBreedDTO>>(petsBreed);

            return Ok(petsBreedList);
        }

        [HttpGet]
        [Route("GetPetsImage")]
        public async Task<IActionResult> GetPetsImage()
        {
            var petsImage = await _petRepository.GetPetsImage();

            var petsImageList = _mapper.Map<IEnumerable<PetImageDTO>>(petsImage);

            return Ok(petsImageList);
        }

        [HttpGet]
        [Route("GetPetsSize")]
        public async Task<IActionResult> GetPetsSize()
        {
            var petsSize = await _petRepository.GetPetsSize();

            var petsSizeList = _mapper.Map<IEnumerable<PetSizeDTO>>(petsSize);

            return Ok(petsSizeList);
        }

        [HttpPost]
        [Route("PostPet")]
        public async Task<IActionResult> PostPet(PetPostDTO petPostDTO)
        {
            var pet = _mapper.Map<Pet>(petPostDTO);
            var result = await _petRepository.Insert(pet);

            return Ok(result ? pet.Id : 0);

        }

        [HttpPost]
        [Route("PostRegisterLostPet")]
        public async Task<IActionResult> PostRegisterLostPet(LostPetDTORegistration lostPetDTORegistration)
        {
            var lostPet = _mapper.Map<LostPet>(lostPetDTORegistration);
            var result = await _lostPetRepository.Insert(lostPet);

            return Ok(result ? lostPet.Id : 0);

        }

        [HttpPost]
        [Route("PostRegisterFoundPet")]
        public async Task<IActionResult> PostRegisterFoundPet(LostPetDTORegistrationFound lostPetDTORegistrationFound)
        {
            var lostPet = _mapper.Map<LostPet>(lostPetDTORegistrationFound);
            var result = await _lostPetRepository.RegisterPetFound(lostPet);

            return Ok(result ? 1 : 0);

        }

        [HttpPost]
        [Route("PostRegisterPetAdoption")]
        public async Task<IActionResult> PostRegisterPetAdoption(PetAdoptionDTO petAdoptionDTO)
        {
            var pet = _mapper.Map<PetAdoption>(petAdoptionDTO);
            var result = await _petAdoptionRepository.RegisterPetAdoption(pet);

            return Ok(result ? pet.Id : 0);

        }

        [HttpPost]
        [Route("GetMascotasByStatus")]
        public async Task<IActionResult> GetMascotasByStatus([FromBody] GetMascotas getMascotas)
        {
            //Found
            //Lost

            //Adopted
            //Registered
            string status = getMascotas.Status;
            using var _context = new PetRescueDBContext();
            ResponseCustom response = null;
            var query = from pet in _context.Pet
                        join type in _context.PetType on pet.IdPetType equals type.Id
                        //join image in _context.PetImage on pet.Id equals image.IdPet
                        join age in _context.PetAge on pet.IdPetAge equals age.Id
                        join lost in _context.LostPet on pet.Id equals lost.IdPet into petLost
                        from x in petLost.DefaultIfEmpty()
                        join adoption in _context.PetAdoption on pet.Id equals adoption.IdPet into petAdoption
                        from y in petAdoption.DefaultIfEmpty()
                        join user in _context.User on pet.IdUser equals user.Id

                        where(
                        (x.Status.Contains(status == null ? "" : status) || y.Status.Contains(status == null ? "" : status))
                        //&& (user.Id == getMascotas.IdUser)
                        && ( (getMascotas.IdUser != null)?(user.Id == getMascotas.IdUser) :(true) )
                        )
                        select new ReportPetWithImageDTO
                        {
                            Id = pet.Id,
                            IdUser = user.Id,
                            Name = pet.Name,
                            Owner = user.Name + " "+ user.LastName,
                            RegistrationDate = pet.RegistrationDate,
                            Type = type.Name,
                            Age = age.AgeRange,
                            AdoptionStatus = y.Status==null ? "No registra" : y.Status,
                            LostStatus = x.Status == null ? "No registra" : x.Status,
                            PetImage = pet.PetImage
                        };

            //return await query.ToListAsync();
            var listadoMascotas = await query.ToListAsync();
            response = new ResponseCustom()
            {
                Code = 200,
                Msg = "",
                Data = listadoMascotas
            };
            return Ok(response);

        }


        [HttpPost]
        [Route("GetDetailMascota")]
        public async Task<IActionResult> GetDetailMascota([FromBody] Pet petGet)
        {
            using var _context = new PetRescueDBContext();
            ResponseCustomGetDetalleMascota response = null;
            var query = from pet in _context.Pet
                        join type in _context.PetType on pet.IdPetType equals type.Id
                        join age in _context.PetAge on pet.IdPetAge equals age.Id
                        join lost in _context.LostPet on pet.Id equals lost.IdPet into petLost
                        from x in petLost.DefaultIfEmpty()
                        join adoption in _context.PetAdoption on pet.Id equals adoption.IdPet into petAdoption
                        from y in petAdoption.DefaultIfEmpty()
                        join user in _context.User on pet.IdUser equals user.Id

                        where (
                        (pet.Id == petGet.Id)
                        )
                        select new DetailPet
                        {
                            Id = pet.Id,
                            IdUser = user.Id,
                            Email = user.Email,
                            Name = pet.Name,
                            Owner = user.Name + " "+ user.LastName,
                            RegistrationDate = pet.RegistrationDate,
                            Type = type.Name,
                            Age = age.AgeRange,
                            AdoptionStatus = y.Status==null ? "No registra" : y.Status,
                            LostStatus = x.Status == null ? "No registra" : x.Status,
                            PetImage = pet.PetImage
                        };
            var petFromDb = await query.FirstOrDefaultAsync();
            response = new ResponseCustomGetDetalleMascota()
            {
                Code = 200,
                Msg = "",
                Data = petFromDb
            };
            return Ok(response);
        }
    }
}
