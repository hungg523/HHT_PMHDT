using AutoMapper;
using MediatR;
using NhaThuoc.Application.DTOs;
using NhaThuoc.Application.Request.Product;
using NhaThuoc.Domain.Abtractions.IRepositories;

namespace NhaThuoc.Application.Handlers.Product
{
    public class GetAllProductsRequestHandler : IRequestHandler<GetAllProductsRequest, List<ProductDTO>>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public GetAllProductsRequestHandler(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public Task<List<ProductDTO>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
        {
            var products = productRepository.FindAll().ToList();
            var productDtos = mapper.Map<List<ProductDTO>>(products);
            return Task.FromResult(productDtos);
        }
    }
}