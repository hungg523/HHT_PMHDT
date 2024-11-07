using AutoMapper;
using MediatR;
using NhaThuoc.Application.Request.Customers.Customer;
using NhaThuoc.Application.Request.Product;
using NhaThuoc.Domain.Abtractions.IRepositories;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Handlers.Product
{
    public class GetAllProductRequestHandler : IRequestHandler<GetAllProductRequest, List<Entities.Product>>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public GetAllProductRequestHandler(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }
        public async Task<List<Entities.Product>> Handle(GetAllProductRequest request, CancellationToken cancellationToken)
        {
            var product = productRepository.FindAll();

            return mapper.Map<List<Entities.Product>>(product);
        }
    }
}
