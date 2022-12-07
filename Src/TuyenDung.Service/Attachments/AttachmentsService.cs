using AutoMapper;
using TuyenDung.Data.EF;
using TuyenDung.Model;

namespace TuyenDung.Service
{
    public class AttachmentsService : IAttachmentsService
    {
        private readonly TuyenDungDbContext _context;
        private readonly IMapper _mapper;

        public AttachmentsService(TuyenDungDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AttachmentModel> GetById(int id)
        {
            var entity = await _context.Attachments.FindAsync(id);

            var entityModel = new AttachmentModel()
            {
                Id = entity.Id,
                CreateDate = entity.CreateDate,
                FileName = entity.FileName,
                FilePath = entity.FilePath,
                FileSize = entity.FileSize,
                FileType = entity.FileType,
                KnowledgeBaseId = entity.KnowledgeBaseId,
                LastModifiedDate = entity.LastModifiedDate
            };
            return entityModel;
        }

        public async Task<int> DeletesAsync(IEnumerable<int> ids)
        {
            foreach (var entity in ids)
            {
                var item = await _context.Attachments.FindAsync(entity);

                _context.Attachments.Remove(item);
            }
            var result = await _context.SaveChangesAsync();
            return result;
        }
    }
}