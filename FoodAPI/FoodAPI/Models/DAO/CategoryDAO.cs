using FoodAPI.Models.DTO;
using FoodAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FoodAPI.Models.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;

        public static CategoryDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CategoryDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        FoodAppDbEntities1 db = new FoodAppDbEntities1();

        public async Task<List<CategoryDTO>> GetAllCategory()
        {
            var CategoryList = (await db.Categories
                        .ToListAsync())
                        .Select(category => new CategoryDTO(category))
                        .ToList();
            return CategoryList;
        }

        public async Task<int> AddCategory(CategoryDTO categoryDTO)
        {
            var category = new Category()
            {
                Name = categoryDTO.Name,
                ImageUrl = categoryDTO.ImageUrl,
                //IsDeleted = false
            };

            db.Categories.Add(category);
            await db.SaveChangesAsync();

            return category.Id;
        }

        public async Task<bool> UpdateCategory(CategoryDTO category)
        {
            var result = db.Categories.SingleOrDefault(c => c.Id == category.Id);

            try
            {
                result.Name = category.Name;
                result.ImageUrl = category.ImageUrl;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

        public async Task<bool> DeleteCategory(int ID)
        {
            var result = db.Categories.SingleOrDefault(c => c.Id == ID);

            try
            {
                db.Categories.Remove(result);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

        //public async Task<bool> RestoreAllCategory()
        //{
        //    var CategoryList = (await db.Categories
        //                .ToListAsync())
        //                .Select(category => new CategoryDTO(category))
        //                .ToList();
        //    try
        //    {
        //        var DeletedList = db.Categories.Where(c => c.IsDeleted == true).ToList();
        //        DeletedList.ForEach(c => c.IsDeleted = false);
        //        db.SaveChanges();


        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //        throw e;
        //    }
        //}
    }
}