using AutoMapper;
using MediatR;
using NhaThuoc.Application.Request.Product;
using NhaThuoc.Domain.Abtractions.IRepositories;
using Entities = NhaThuoc.Domain.Entities;

namespace NhaThuoc.Application.Handlers.Product
{
    public class GetByNameProductRequestHandler : IRequestHandler<GetByNameProductRequest, List<Entities.Product>>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public GetByNameProductRequestHandler(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }
        public async Task<List<Entities.Product>> Handle(GetByNameProductRequest request, CancellationToken cancellationToken)
        {
            var product = productRepository.FindAll(x => x.ProductName == request.ProductName).ToList();
            return mapper.Map<List<Entities.Product>>(product);
        }
    }
}
