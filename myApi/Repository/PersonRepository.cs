using AutoMapper;
using myApi.Models;
using myData.Context;
using myApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myApi.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PersonDbContext dbContext;
        private readonly ILogger<PersonRepository> logger;
        private readonly IMapper mapper;

        public PersonRepository(PersonDbContext dbContext, ILogger<PersonRepository> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
        }
        private void SeedData()
        {
            if (!dbContext.Persons.Any())
            {
                dbContext.Persons.Add(new myData.DTO.Person() { Id = 1, Name = "John Doe", Email = "doei@test.com" });
                dbContext.Persons.Add(new myData.DTO.Person() { Id = 2, Name = "Jane Doe", Email = "doeia@test.com" });
                dbContext.Persons.Add(new myData.DTO.Person() { Id = 3, Name = "Tom Smith", Email = "tom.smith@test.com" });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Person> Persons, string ErrorMessage)> GetPersonsAsync()
        {
            try
            {
                logger?.LogInformation("Querying persons");
                var persons = await dbContext.Persons.ToListAsync();
                if (persons != null && persons.Any())
                {
                    logger?.LogInformation($"{persons.Count} person(s) found");
                    var result = mapper.Map<IEnumerable<myData.DTO.Person>, IEnumerable<Models.Person>>(persons);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
        public async Task<(bool IsSuccess, Person Person, string ErrorMessage)> GetPersonAsync(int id)
        {
            try
            {
                logger?.LogInformation("Querying person");
                var person = await dbContext.Persons.FirstOrDefaultAsync(c => c.Id == id);
                if (person != null)
                {
                    logger?.LogInformation("Person found");
                    var result = mapper.Map<myData.DTO.Person, Models.Person>(person);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        //simillar to add
        public async Task<(bool IsSuccess, string ErrorMessage)> SavePersonAsync(Person person)
        {
            try
            {
                logger?.LogInformation("Querying person");

                if(!string.IsNullOrEmpty(person.Name) && !string.IsNullOrEmpty(person.Email))
                {
                    logger?.LogInformation("Person is being stored");
                    //improve id lookup, and storage
                    var persons = await dbContext.Persons.ToListAsync();
                    dbContext.Persons.Add(new myData.DTO.Person() { Id = persons.Count + 1, Name = person.Name, Email = person.Email });
                    dbContext.SaveChanges();
                    return (true, null);
                }
                //error, data incomplete to store
                return (false, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, ex.Message);
            }
        }
    }
}
