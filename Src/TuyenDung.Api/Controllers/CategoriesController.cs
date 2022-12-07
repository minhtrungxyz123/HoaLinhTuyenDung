using Microsoft.AspNetCore.Mvc;
using TuyenDung.Common.XBaseModel;
using TuyenDung.Model;
using TuyenDung.Service;

namespace TuyenDung.Api.Controllers
{
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        #region List

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            var entity = await _categoryService.GetAll();
            return Ok(new XBaseResult
            {
                success = true,
                data = entity
            });
        }

        [Route("get")]
        [HttpGet]
        public IActionResult Get([FromQuery] CategorySearchModel searchModel)
        {
            var searchContext = new CategorySearchContext
            {
                Keywords = searchModel.Keywords,
                PageIndex = searchModel.PageIndex - 1,
                PageSize = searchModel.PageSize,
            };

            var entities = _categoryService.GetPaging(searchContext);
            return Ok(new XBaseResult
            {
                success = true,
                data = entities,
                totalCount = entities.TotalCount
            });
        }

        [Route("create")]
        [HttpGet]
        public IActionResult Create()
        {
            var model = new CategoryModel();

            return Ok(new XBaseResult
            {
                data = model
            });
        }

        [Route("get-by-id")]
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var role = await _categoryService.GetById(id);
            if (role == null)
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
                data = role
            });
        }

        #endregion List

        #region Method

        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryModel entity)
        {
            var result = await _categoryService.InsertAsync(entity);
            if (result > 0)
            {
                return Ok(new XBaseResult
                {
                    success = true,
                    data = result,
                    message = string.Format(
                    "Thêm thành công")
                });
            }
            else
            {
                return BadRequest(new XBaseResult
                {
                    success = false,
                    message = string.Format("Thêm không thành công")
                });
            }
        }

        [Route("edit")]
        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] CategoryModel model)
        {
            var item = await _categoryService.GetById(model.Id);
            if (item == null)
                return NotFound(new XBaseResult
                {
                    success = false,
                    message = string.Format("Dữ liệu không tồn tại")
                });

            var result = await _categoryService.UpdateAsync(model);

            if (result > 0)
            {
                return Ok(new XBaseResult
                {
                    success = true,
                    message = string.Format("Sửa thành công"),
                    data = result
                });
            }
            else
            {
                return BadRequest(new XBaseResult
                {
                    success = false,
                    message = string.Format("Sửa thất bại")
                });
            }
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

            await _categoryService.DeletesAsync(ids);

            return Ok(new XBaseResult
            {
                success = true,
                message = string.Format("Xóa thành công!")
            });
        }

        #endregion Method
    }
}