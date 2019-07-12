﻿using Numero3.EntityFramework.Demo.DomainModel.NetCore;
using System;
using System.Threading.Tasks;

namespace Numero3.EntityFramework.Demo.Repositories.NetCore
{
    public interface IUserRepository {
        User Get(Guid userId);
        Task<User> GetAsync(Guid userId);
        void Add(User user);
    }
}