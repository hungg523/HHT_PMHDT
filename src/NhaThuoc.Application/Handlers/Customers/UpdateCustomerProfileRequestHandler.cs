using AssetServer.Enumerations;
using AutoMapper;
using MediatR;
using NhaThuoc.Application.Request.Customers.Customer;
using NhaThuoc.Application.Validators.Customer;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Domain.Entities;
using NhaThuoc.Share.DependencyInjection.Extensions;
using NhaThuoc.Share.Exceptions;
using NhaThuoc.Share.Service;

namespace NhaThuoc.Application.Handlers.Customers
{
    public class UpdateCustomerProfileRequestHandler : IRequestHandler<UpdateProifleCustomerRequest, ApiResponse>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;
        private readonly IFileService fileService;

        public UpdateCustomerProfileRequestHandler(ICustomerRepository customerRepository, IMapper mapper, IFileService fileService)
        {
            this.customerRepository = customerRepository;
            this.mapper = mapper;
            this.fileService = fileService;
        }

        public async Task<ApiResponse> Handle(UpdateProifleCustomerRequest request, CancellationToken cancellationToken)
        {
            await using (var transaction = customerRepository.BeginTransaction())
            {
                try
                {
                    var validator = new CustomerProfileValidator();
                    var validationResult = await validator.ValidateAsync(request, cancellationToken);
                    validationResult.ThrowIfInvalid();

                    var customer = await customerRepository.FindByIdAsync(request.Id!);
                    if (customer is null) customer.ThrowNotFound();
                    customer.FirstName = request.FirstName ?? customer.FirstName;
                    customer.LastName = request.LastName ?? customer.LastName;
                    customer.PhoneNumber = request.PhoneNumber ?? customer.PhoneNumber;
                    if (request.ImageData is not null)
                    {
                        string fileName = (Path.GetFileName(customer.AvatarImagePath) is { } name &&
                        Path.GetExtension(name)?.ToLowerInvariant() == fileService.GetFileExtensionFromBase64(request.ImageData)?.ToLowerInvariant()) ? name : $"{customer.Id}{fileService.GetFileExtensionFromBase64(request.ImageData)}";
                        customer.AvatarImagePath = await fileService.UploadFile(fileName, request.ImageData, AssetType.CAT_IMG);
                    }

                    customerRepository.Update(customer);
                    await customerRepository.SaveChangesAsync();
                    await transaction.CommitAsync(cancellationToken);
                    return ApiResponse.Success();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            }
        }
    }
}
