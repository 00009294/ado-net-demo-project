using AdoNetDemoProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetDemoProject.Interfaces.Repositories
{
    public interface IPersonRepository
    {
        public Task<IEnumerable<Person>> GetAllAsync(int skip, int take);
        public Task<Person> GetByIdAsync(int id);
        public Task<bool> CreateAsync(Person person);
        public Task<bool> UpdateAsync(int id , Person person);
        public Task<bool> DeleteAsync(int id);
    }
}
