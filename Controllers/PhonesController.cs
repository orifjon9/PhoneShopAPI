using System;
using Microsoft.AspNetCore.Mvc;
using PhoneShopAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhoneShopAPI.Helpers;
using PhoneShopAPI.Data.Access.DAL;

namespace PhoneShopAPI.Controllers
{
    [Route("api/[controller]")]
    public class PhonesController : Controller
    {
        private readonly IPhoneRepository _repository;

        public PhonesController(IPhoneRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<List<Phone>> GetAll()
        {
            try
            {
                return Ok(_repository.GetAll());
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
                var phone = await _repository.GetByIdAsync(id);
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
                await _repository.AddAsync(phone);
                await _repository.CommitAsync();

                return CreatedAtRoute("GetPhone", new { id = phone.Id }, phone);
            }
            catch (Exception)
            {
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
                var phone = await _repository.GetByIdAsync(id);
                if (phone == null)
                {
                    return NotFound();
                }

                phone.Name = item.Name;
                phone.Description = item.Description;

                _repository.Update(phone);
                await _repository.CommitAsync();

                return NoContent();
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
                var phone = await _repository.GetByIdAsync(id);
                if (phone == null)
                {
                    return NotFound();
                }
                _repository.Delete(phone);
                await _repository.CommitAsync();

                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest(ErrorCode.CouldNotDeleteEntity);
            }
        }
    }
}