using AutoMapper;
using MediatR;
using NhaThuoc.Application.Request.Product;
using NhaThuoc.Domain.Abtractions.IRepositories;

namespace NhaThuoc.Application.Handlers.Product
{
    public class GetByNameProductRequestHandler : IRequestHandler<GetByNameProductRequest, Domain.Entities.Product>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public GetByNameProductRequestHandler(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }
        public async Task<Domain.Entities.Product> Handle(GetByNameProductRequest request, CancellationToken cancellationToken)
        {
            var product = await productRepository.FindByIdAsync(request.Id);
            return mapper.Map<Domain.Entities.Product>(product);
        }
    }
}
