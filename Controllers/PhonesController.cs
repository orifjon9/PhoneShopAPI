using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhoneShopAPI.Helpers;
using PhoneShopAPI.Data.Access.DAL;
using PhoneShopAPI.Services.Interfaces;
using PhoneShopAPI.Services;
using PhoneShopAPI.ViewModels;
using PhoneShopAPI.Filters;
using Microsoft.AspNetCore.Authorization;

namespace PhoneShopAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class PhonesController : ControllerBase
    {
        private readonly IPhoneService _service;

        public PhonesController(IPhoneService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<List<PhoneViewModel>> GetAll()
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
        [ProducesResponseType(typeof(PhoneViewModel), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<PhoneViewModel>> GetById(int id)
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
        [ValidateViewModel]
        public async Task<IActionResult> Create([FromBody]PhoneViewModel phoneViewModel)
        {
            try
            {
                if (await _service.CreatePhoneItemAsync(phoneViewModel))
                {
                    return CreatedAtRoute("GetPhone", new { id = phoneViewModel.Id }, phoneViewModel);
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
        [ProducesResponseType(typeof(PhoneViewModel), 204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, [FromBody] PhoneViewModel item)
        {
            try
            {
                var phone = await _service.GetPhoneAsync(id);
                if (phone == null)
                {
                    return NotFound();
                }

                if (await _service.UpdatePhoneItem(phone.Id, item))
                {
                    return NoContent();
                }
                return BadRequest(this.ModelState);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
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