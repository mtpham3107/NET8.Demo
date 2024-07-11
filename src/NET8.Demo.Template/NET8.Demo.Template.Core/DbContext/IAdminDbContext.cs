﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NET8.Demo.Template.Entities;

namespace NET8.Demo.Template.Core.DbContext;

public interface IAdminDbContext : IDbContext
{
    public DbSet<User> Users { get; }
}
