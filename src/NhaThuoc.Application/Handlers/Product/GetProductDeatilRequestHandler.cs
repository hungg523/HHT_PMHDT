using MediatR;
using NhaThuoc.Application.Request.Product;
using NhaThuoc.Application.Validators.Product;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Share.DependencyInjection.Extensions;
using NhaThuoc.Share.Exceptions;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Handlers.Product
{
    public class GetProductDeatilRequestHandler : IRequestHandler<GetProductDeatilRequest, Entities.Product>
    {
        private readonly IProductRepository productRepository;

        public GetProductDeatilRequestHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<Entities.Product> Handle(GetProductDeatilRequest request, CancellationToken cancellationToken)
        {
            var validator = new GetProductDeatilRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            validationResult.ThrowIfInvalid();

            var product = await productRepository.FindByIdAsync(request.Id);
            if (product is null) product.ThrowNotFound();
            return product;
        }
    }
}