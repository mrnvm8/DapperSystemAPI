using DapperSystemAPI.Mapping;
using Microsoft.AspNetCore.Mvc;
using People.Application.Models;
using People.Application.Repositories.PeopleRepository;
using People.Application.Services;
using People.Contracts.Requests;

namespace DapperSystemAPI.Controllers
{
    [ApiController]
    public class PeopleController : ControllerBase
    {
       
        private readonly IPersonService _personService;

        public PeopleController(IPersonService personService)
        {
           _personService = personService;
        }

        [HttpPost(ApiEndpoints.People.Create)] //=> /api/people 
        public async Task<IActionResult> Create([FromBody] CreatePersonRequest request,
            CancellationToken token)
        {

            //this for mapping person formbody to model person
            var person = request.MapToPerson();
            var result = await  _personService.CreateAsync(person, token);

            return CreatedAtAction(nameof(Get), new {id = person.Id}, person);
        }

        [HttpGet(ApiEndpoints.People.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken token)
        {
            var person = await  _personService.GetByIdAsync(id, token);
            if(person is null)
            {
                return NotFound();
            }
            var response = person.MapToResponse();
            return Ok(response);
        }

        [HttpGet(ApiEndpoints.People.GetAll)]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            var people = await  _personService.GetAllAsync(token);
            var peopleResponse = people.MapToResponse();
            return Ok(peopleResponse);
        }

        [HttpPut(ApiEndpoints.People.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid id, 
            [FromBody] UpadatePersonRequest request, CancellationToken token)
        {
            var person = request.MapToPerson(id);
            var updatedPerson =  await  _personService.UpdateAsync(person, token);
            if (updatedPerson is null)
            {
                return NotFound();
            }

            var response = person.MapToResponse();
            return Ok(response);
        }

        [HttpDelete(ApiEndpoints.People.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken token)
        {
            var deleted = await  _personService.DeleteByIdAsync(id, token);
            if (!deleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
