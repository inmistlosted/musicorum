using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Musicorum.Data;
using Musicorum.Data.Entities;
using Musicorum.Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace Musicorum.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly MusicorumDbContext db;

        public UserService(MusicorumDbContext db)
        {
            this.db = db;
        }

        public bool UserExists(string userId) => this.db.Users.Any(u => u.Id == userId && u.IsDeleted == false);
        public object GetUserFullName(string id)
        {
            if (this.UserExists(id))
            {
                var user = this.db.Users.Find(id);
                return user.FirstName + " " + user.LastName;
            }
            return null;
        }

        public bool CheckIfDeleted(string userId)
        {
            throw new System.NotImplementedException();
        }

        public UserModel GetById(string id)
        {
            if (this.UserExists(id))
            {
                return Mapper.Map<UserModel>(this.db.Users.Find(id));
            }

            return null;
        }

        public void EditUser(string id, string firstName, string lastName, int age, string email, string username)
        {
            var user = this.db.Users.Find(id);

            user.FirstName = firstName;
            user.LastName = lastName;
            user.UserName = username;
            user.Age = age;
            user.Email = email;

            this.db.SaveChanges();
        }

        public void DeleteUser(string id)
        {
            var user = this.db.Users.Find(id);

            user.IsDeleted = true;

            this.db.SaveChanges();
        }

        public bool CheckIfDeletedByUserName(string username)
        {
            if (this.db.Users.Any(u => u.UserName == username))
            {
                return this.db.Users.FirstOrDefault(u => u.UserName == username).IsDeleted;
            }

            return true;
        }
    }
}