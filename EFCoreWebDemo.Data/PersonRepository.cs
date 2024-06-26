﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreWebDemo.Data
{
    public class PersonRepository
    {
        private readonly string _connectionString;

        public PersonRepository(string connectionString)
        {
            _connectionString = connectionString;    
        }

        public List<Person> GetAll()
        {
            using var context = new PeopleDataContext(_connectionString);
            return context.People.ToList();
        }

        public void Add(Person person)
        {
            using var context = new PeopleDataContext(_connectionString);
            context.People.Add(person);
            context.SaveChanges();
        }

        public Person GetById(int id)
        {
            using var context = new PeopleDataContext(_connectionString);
            return context.People.FirstOrDefault(p => p.Id == id);
        }

        public void Update(Person person)
        {
            using var context = new PeopleDataContext(_connectionString);
            context.Entry(person).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            using var context = new PeopleDataContext(_connectionString);
            context.Database.ExecuteSqlInterpolated($"DELETE FROM People WHERE Id = {id}");

            //var person = new Person { Id = id };
            //context.People.Remove(person);
            //context.SaveChanges();

        }
    }
}
