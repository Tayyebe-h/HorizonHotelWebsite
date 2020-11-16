﻿using System;
using System.Collections.Generic;
using System.Linq;
using HorizonHotelWebsite.Data;
using HorizonHotelWebsite.Models.Entities.user;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HorizonHotelWebsite.Models.Repositories
{
    public class AdminUserRepository : IAdminUserRepository
    {
        private readonly DataBaseContext _dataBaseContext;

        public AdminUserRepository(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        public IEnumerable<User> AllUsers
        {
            get
            {
                return _dataBaseContext.Userss;
            }
        }
        public User GetById(int? id)
        {
            return _dataBaseContext.Userss.Include(u => u.Bookings).ThenInclude(b => b.Room).FirstOrDefault(u => u.Id == id);
        }

        public User CreateUser(User user)
        {
            if (user != null)
            {
                _dataBaseContext.Userss.Add(user);
                _dataBaseContext.SaveChanges();
                return user;
            }

            return user;
        }

        public User DeleteUser(User user)
        {

            if (user != null)
            {
                _dataBaseContext.Userss.Remove(user);
                _dataBaseContext.SaveChanges();
                return user;
            }

            return user;
        }

        public User Update(int? id)
        {
            var user = GetById(id);

            if (user != null)
            {
                _dataBaseContext.Update(user);
                _dataBaseContext.SaveChanges();
                return user;
            }

            return user;
        }

        public User UpdateWithUser(User user)
        {
            if (user != null)
            {
                _dataBaseContext.Update(user);
                _dataBaseContext.SaveChanges();
                return user;
            }

            return user;
        }
    }
}