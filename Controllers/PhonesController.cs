using System;
using Microsoft.AspNetCore.Mvc;
using PhoneShopAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhoneShopAPI.Helpers;
using PhoneShopAPI.Data.Access.DAL;
using PhoneShopAPI.Services.Interfaces;
using PhoneShopAPI.Services;

namespace PhoneShopAPI.Controllers
{
    [Route("api/[controller]")]
    public class PhonesController : Controller
    {
        private readonly IPhoneService _service;

        public PhonesController(IPhoneRepository repository)
        {
            _service = new PhoneService(this.ModelState, repository);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<List<Phone>> GetAll()
        {
            try
            {
                return Ok(_service.ListPhones());
            }
            catch (Exception)
            {
                return BadRequest(ErrorCode.RecordsNotFound);
            }
        }

        [HttpGet("{id}", Name = "GetPhone")]
        [ProducesResponseType(typeof(Phone), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Phone>> GetById(int id)
        {
            try
            {
                var phone = await _service.GetPhoneAsync(id);
                if (phone == null)
                {
                    return NotFound();
                }
                return Ok(phone);
            }
            catch (Exception)
            {
                return BadRequest(ErrorCode.RecordNotFound);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Phone phone)
        {
            try
            {
                if (await _service.CreatePhoneItemAsync(phone))
                {
                    return CreatedAtRoute("GetPhone", new { id = phone.Id }, phone);
                }
                return BadRequest(this.ModelState);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ErrorCode.CouldNotCreateEntity);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, [FromBody] Phone item)
        {
            try
            {
                var phone = await _service.GetPhoneAsync(id);
                if (phone == null)
                {
                    return NotFound();
                }

                if (await _service.UpdatePhoneItem(phone, item))
                {
                    return NoContent();
                }
                return BadRequest(this.ModelState);
            }
            catch (Exception)
            {
                return BadRequest(ErrorCode.CouldNotUpdateEntity);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var phone = await _service.GetPhoneAsync(id);
                if (phone == null)
                {
                    return NotFound();
                }

                await _service.DeletePhoneItem(phone);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest(ErrorCode.CouldNotDeleteEntity);
            }
        }
    }
}