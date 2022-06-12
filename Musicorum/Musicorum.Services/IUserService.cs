using Microsoft.AspNetCore.Http;
using Musicorum.Services.Models;
using System.Collections.Generic;

namespace Musicorum.Services
{
    public interface IUserService : IService
    {
        UserModel GetById(string id);

        bool UserExists(string userId);

        object GetUserFullName(string id);

        void EditUser(string id, string firstName, string lastName, int age, string email, string username);

        void DeleteUser(string id);
    }
}