using myApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace myApi.Controllers
{
    [ApiController]
    [Route("api/person")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository personRepository;

        public PersonController(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPersonAsync()
        {
            var result = await personRepository.GetPersonsAsync();
            if (result.IsSuccess)
            {
                return Ok(result.Persons);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonAsync(int id)
        {
            var result = await personRepository.GetPersonAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Person);
            }
            return NotFound();
        }

        [HttpPost()]
        public async Task<IActionResult> SavePersonAsync(Models.Person person)
        {
            var result = await personRepository.SavePersonAsync(person);
            if (result.IsSuccess)
            {
                return Ok();
            }
            return NotFound();
        }
    }

}
