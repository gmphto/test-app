﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<ReadingItem> ReadingItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
