﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<MeterReadItem> MeterReadItems { get; }

    DbSet<AccountItem> AccountItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
