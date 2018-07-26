using System;
using Microsoft.AspNetCore.Mvc;
using PhoneShopAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhoneShopAPI.Helpers;

namespace PhoneShopAPI.Controllers
{
    [Route("api/[controller]")]
    public class PhonesController : Controller
    {
        private readonly PhoneContext _context;

        public PhonesController(PhoneContext context)
        {
            _context = context;

            if (_context.PhoneItems.Count() == 0)
            {
                _context.PhoneItems.AddRangeAsync(
                    new Phone() { Name = "iPhone X", Description = "Access your work directory, email or calendar with iPhone X by Apple." },
                    new Phone() { Name = "Galaxy S9+", Description = "Access your work directory, email or calendar with Galaxy S9+ by Samsung." },
                    new Phone() { Name = "iPhone 8 Plus", Description = "Access your work directory, email or calendar with iPhone 8 Plus by Apple." },
                    new Phone() { Name = "Galaxy Note8", Description = "Access your work directory, email or calendar with Galaxy Note8 by Samsung." });
                _context.SaveChangesAsync();
            }
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<List<Phone>> GetAll()
        {
            try
            {
                return Ok(_context.PhoneItems.ToList());
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
                var phone = await _context.PhoneItems.FindAsync(id);
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
                await _context.PhoneItems.AddAsync(phone);
                await _context.SaveChangesAsync();

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
                var phone = await _context.PhoneItems.FindAsync(id);
                if (phone == null)
                {
                    return NotFound();
                }

                phone.Name = item.Name;
                phone.Description = item.Description;

                _context.PhoneItems.Update(phone);
                await _context.SaveChangesAsync();

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
                var phone = await _context.PhoneItems.FindAsync(id);
                if (phone == null)
                {
                    return NotFound();
                }
                _context.PhoneItems.Remove(phone);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest(ErrorCode.CouldNotDeleteEntity);
            }
        }
    }
}