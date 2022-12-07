using Microsoft.AspNetCore.Mvc;
using TuyenDung.Common.XBaseModel;
using TuyenDung.Service;

namespace TuyenDung.Api.Controllers
{
    public class AttachmentsController : BaseController
    {
        private readonly IAttachmentsService _attachmentsService;

        public AttachmentsController(IAttachmentsService attachmentsService)
        {
            _attachmentsService = attachmentsService;
        }

        [Route("get-by-id")]
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _attachmentsService.GetById(id);
            if (entity == null)
            {
                return NotFound(new XBaseResult
                {
                    success = false,
                    message = string.Format("Dữ liệu không tồn tại")
                });
            }
            return Ok(new XBaseResult
            {
                success = true,
                data = entity
            });
        }

        [Route("deletes")]
        [HttpPost]
        public async Task<IActionResult> Deletes(IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return Ok(new XBaseResult
                {
                    success = false,
                    message = string.Format("Xoá thất bại!")
                });
            }

            await _attachmentsService.DeletesAsync(ids);

            return Ok(new XBaseResult
            {
                success = true,
                message = string.Format("Xóa thành công!")
            });
        }
    }
}