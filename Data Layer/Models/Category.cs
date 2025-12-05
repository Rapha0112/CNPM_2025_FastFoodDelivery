using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// 1. Thêm thư viện MongoDB
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data_Layer.Models
{
    public class Category
    {
        // 2. Định nghĩa Khóa chính
        [BsonId] // Báo hiệu đây là _id của MongoDB
        public Guid CategoryId { get; set; }

        public string CategoriesName { get; set; }
        public string? CategoriesStatus { get; set; }

        // 3. Xử lý quan hệ 1-Nhiều (1 Danh mục - Nhiều Món ăn)
        
        // Trong MongoDB, ta thường KHÔNG lưu danh sách món ăn vào trong Category
        // (để tránh document bị quá nặng nếu danh mục có quá nhiều món).
        // Ta dùng [BsonIgnore] để MongoDB không lưu trường này vào Database.
        
        // Khi muốn lấy danh sách món ăn của danh mục này, ở tầng Service bạn sẽ viết hàm:
        // _menuFoodItemCollection.Find(x => x.CategoryId == this.CategoryId).ToList();
        
        [BsonIgnore]
        public virtual ICollection<MenuFoodItem> MenuFoodItems { get; set; } = new List<MenuFoodItem>();
    }
}