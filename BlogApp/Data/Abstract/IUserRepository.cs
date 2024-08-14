using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Entity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BlogApp.Data.Abstract
{
    public interface IUserRepository
    {
        IQueryable<User> Users{ get ;}
        void CreateUser (User user);
    }
}