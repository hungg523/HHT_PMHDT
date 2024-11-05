using AutoMapper;
using NhaThuoc.Domain.Abtractions.IRepositories;
using NhaThuoc.Domain.Entities;

namespace NhaThuoc.Data.Repositories
{
    public class CustomerAddressRepository : GenericRepository<CustomerAddress>, ICustomerAddressRepository
    {
        public CustomerAddressRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
