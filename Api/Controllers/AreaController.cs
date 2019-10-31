using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Contracts;
using ServiceLayer.DTO;

namespace Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        IAreaService areaService;

        public AreaController(IAreaService _areaService)
        {
            areaService = _areaService;

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var areas = areaService.GetAll();
            return Ok(areas);
        }

        [HttpGet]
        [Route("{Id}")]
        public IActionResult GetById([FromRoute]int Id)
        {
            var areas = areaService.GetAreaById(Id);
            return Ok(areas);
        }

        [HttpPost]

        public IActionResult AddArea([FromBody] AreaCreateRequestDTO areaDTO)
        {
            if (areaDTO == null)
                return BadRequest("Request is null!");

            if (!ModelState.IsValid)
                return BadRequest("Data validation errors!");

            var area = new Area()
            {
                Name = areaDTO.Name,
            };

            var response = areaService.CreateNewArea(area);

            return Ok(response);
        }
    }
}