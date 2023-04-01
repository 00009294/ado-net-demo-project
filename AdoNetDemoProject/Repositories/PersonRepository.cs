
using AdoNetDemoProject.DbConstants;
using AdoNetDemoProject.Interfaces.Repositories;
using AdoNetDemoProject.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetDemoProject.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        NpgsqlConnection _connection = new NpgsqlConnection(AppDbContext._connectionString);
        public async Task<IEnumerable<Person>> GetAllAsync(int skip, int take)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"SELECT * FROM person OFFSET {skip} LIMIT {take}";
                NpgsqlCommand command = new NpgsqlCommand(query, _connection);
                List<Person> people = new List<Person>();
                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Person person = new Person()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Address = reader.GetString(2),
                        PhoneNumber = reader.GetString(3)
                    };
                    people.Add(person);
                }
                return people;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                await _connection.CloseAsync();
            }

        }
        public async Task<Person> GetByIdAsync(int id)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"SELECT * FROM person WHERE id = {id}";
                NpgsqlCommand command = new NpgsqlCommand(query, _connection);
                var reader = await command.ExecuteReaderAsync();
                while(await reader.ReadAsync())
                {
                    Person person = new Person()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Address = reader.GetString(2),
                        PhoneNumber = reader.GetString(3)
                    };
                    return person;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            } finally
            {
                await _connection.CloseAsync();
            }
        }
        public async Task<bool> CreateAsync(Person person)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "INSERT INTO public.person( name, address, phone_number)" +
                    $" VALUES ( '{person.Name}', '{person.Address}', '{person.PhoneNumber}');";
                NpgsqlCommand command = new NpgsqlCommand(query, _connection);
                var reader = await command.ExecuteNonQueryAsync();
                return reader>0;
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<bool> UpdateAsync(int id, Person person)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "UPDATE public.person " +
                    $" SET name='{person.Name}', address='{person.Address}', phone_number='{person.PhoneNumber}' " +
                    $" WHERE id = {id}";
                NpgsqlCommand command = new NpgsqlCommand(query, _connection);
                var reader = await command.ExecuteNonQueryAsync();
                return reader>0;
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "DELETE FROM public.person " +
                    $" WHERE id = {id}";
                NpgsqlCommand command = new NpgsqlCommand(query, _connection);
                var reader = await command.ExecuteNonQueryAsync();
                return reader > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }
    }
}
