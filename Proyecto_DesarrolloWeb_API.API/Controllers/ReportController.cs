using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Proyecto_DesarrolloWeb_API.Dominio.Core.DTOs;
using Proyecto_DesarrolloWeb_API.Dominio.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_DesarrolloWeb_API.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;

        public ReportController(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetUserReport")]
        public async Task<IActionResult> GetUserReport()
        {
            var userReport = await _reportRepository.GetUserReport();
            //var petsList = _mapper.Map<IEnumerable<ReportUserDTO>>(userReport);

            return Ok(userReport);
        }

        //GetUserReportDate
        [HttpGet]
        [Route("GetUserReportDate")]
        public async Task<IActionResult> GetUserReportDate(string startDate, string endDate)
        {
            var userReport = await _reportRepository.GetUserReportDate(Convert.ToDateTime(startDate), Convert.ToDateTime(endDate));
            //var petsList = _mapper.Map<IEnumerable<ReportUserDTO>>(userReport);

            return Ok(userReport);
        }

        [HttpGet]
        [Route("GetUserReportName")]
        public async Task<IActionResult> GetUserReportName(string text)
        {
            var userReport = await _reportRepository.GetUserReportName(text);
            //var petsList = _mapper.Map<IEnumerable<ReportUserDTO>>(userReport);

            return Ok(userReport);
        }

        [HttpGet]
        [Route("GetPetReport")]
        public async Task<IActionResult> GetPetReport(
           string startDate, string endDate, string owner, string petName, string status, int idPetType, int idPetAge, bool images)
        {

            if (startDate == null) startDate = "01/01/1900";
            if (endDate == null) endDate = DateTime.Now.Date.ToString(); 
            var userReport = await _reportRepository.GetPetReport(Convert.ToDateTime(startDate), Convert.ToDateTime(endDate), owner, petName, status, idPetType,idPetAge, images);
            //var petsList = _mapper.Map<IEnumerable<ReportUserDTO>>(userReport);

            return Ok(userReport);
        }

    }
}
