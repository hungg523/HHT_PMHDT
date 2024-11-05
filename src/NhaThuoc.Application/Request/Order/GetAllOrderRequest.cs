using MediatR;
using Entities = NhaThuoc.Domain.Entities;


namespace NhaThuoc.Application.Request.Order
{
    public class GetAllOrderRequest : IRequest<List<Entities.Order>>
    {
    }
}
