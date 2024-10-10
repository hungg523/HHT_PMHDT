using NhaThuoc.Domain.ReQuest.Prescription;

namespace NhaThuocOnline.ApiIntergration
{
    public interface IPrescriptionApiClient
    {
        Task<bool> Create(PrescriptionCreateRequest request);
        Task<List<PrescriptionVm>> GetAll();
    }
}
