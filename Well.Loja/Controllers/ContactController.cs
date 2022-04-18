using Microsoft.AspNetCore.Mvc;
using Well.Loja.Dto;
using Well.Loja.Repository;


namespace Well.Loja.Controllers
{
    [ApiController]
    [Route("v1")]
    public class ContactController: ControllerBase
    {
        private readonly IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        [HttpGet("contact")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var contacts = await _contactRepository.GetAllAsync();
                return Ok(contacts);
            }catch(Exception e)
            {
                return BadRequest(e);
            }
            
        }
        [HttpGet("contact/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            try
            {
                var contact = await _contactRepository.GetByIdAsync(id);
                
                return Ok(contact);
            }catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPost("contact")]
        public async Task<IActionResult> PostAsync([FromBody] ContactForCreationDto newContact)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = await _contactRepository.PostAsync(newContact);

                return Ok(result);

            }catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("contact/{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] ContactForUpdateDto newContact)
        {
            try
            {
                
                if (!ModelState.IsValid)
                    return BadRequest();

                var contact = await _contactRepository.GetByIdAsync(id);
                if (contact == null) return NotFound();




                var result = await _contactRepository.UpdateAsync(id, newContact);

                return Ok(result);

            }catch(Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpDelete("contact/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            try
            {
                await _contactRepository.DeleteAsync(id);

                return Ok();
            }catch(Exception e)
            {
                return BadRequest(e);
            }
        }

    }
    
}
