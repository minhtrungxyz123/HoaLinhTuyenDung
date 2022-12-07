using TuyenDung.Common.XBaseModel;
using TuyenDung.Model;

namespace TuyenDung.Service
{
    public interface ICategoryService
    {
        Task<int> InsertAsync(CategoryModel entity);

        Task<IList<CategoryModel>> GetAll();

        IPagedList<CategoryModel> GetPaging(CategorySearchContext ctx);

        Task<CategoryModel> GetById(int id);

        Task<int> UpdateAsync(CategoryModel entity);

        Task<int> DeletesAsync(IEnumerable<int> ids);
    }
}