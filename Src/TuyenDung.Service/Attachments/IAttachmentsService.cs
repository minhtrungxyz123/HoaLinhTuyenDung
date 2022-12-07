using TuyenDung.Model;

namespace TuyenDung.Service
{
    public interface IAttachmentsService
    {
        Task<AttachmentModel> GetById(int id);

        Task<int> DeletesAsync(IEnumerable<int> ids);
    }
}