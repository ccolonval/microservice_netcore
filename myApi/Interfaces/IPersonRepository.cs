using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myApi.Interfaces
{
    public interface IPersonRepository
    {
        Task<(bool IsSuccess, IEnumerable<Models.Person> Persons, string ErrorMessage)> GetPersonsAsync();
        Task<(bool IsSuccess, Models.Person Person, string ErrorMessage)> GetPersonAsync(int id);
        Task<(bool IsSuccess, string ErrorMessage)> SavePersonAsync(Models.Person person);
    }
}
