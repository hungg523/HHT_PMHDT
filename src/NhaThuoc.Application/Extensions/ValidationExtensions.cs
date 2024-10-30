using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using NhaThuoc.Domain.Exceptions;

namespace NhaThuoc.Application.Extensions
{
    public static class ValidationExtensions
    {
        public static void ThrowIfInvalid(this ValidationResult validationResult)
        {
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                throw new NhaThuocException(StatusCodes.Status400BadRequest, errors);
            }
        }
    }

}