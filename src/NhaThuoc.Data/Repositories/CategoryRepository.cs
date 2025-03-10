﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NhaThuoc.Data.Repositories.Base;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Domain.Entities;
using NhaThuoc.Share.Exceptions;

namespace NhaThuoc.Data.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext context;

        public CategoryRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper) 
        {
            this.context = context;
        }

        public async Task<(List<int>? existingIds, List<int>? missingIds)> CheckIdsExistAsync(List<int>? ids)
        {
            ids = ids.Distinct().ToList() ?? new List<int>();
            var existingIds = await context.Set<Category>()
                                   .Where(category => category.Id.HasValue && ids.Contains(category.Id.Value))
                                   .Select(category => category.Id.Value)
                                   .ToListAsync();
            var missingIds = ids.Except(existingIds).ToList();
            if (missingIds.Any())
            {
                throw new NhaThuocException(StatusCodes.Status404NotFound, new List<string> { $"Id {string.Join(", ", missingIds)} is not found in Category" });
            }
            return (existingIds, missingIds);
        }
    }
}