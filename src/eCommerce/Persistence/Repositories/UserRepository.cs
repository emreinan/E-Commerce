﻿using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class UserRepository(AppDbContext context) : EfRepositoryBase<User, Guid, AppDbContext>(context), IUserRepository
{
}
