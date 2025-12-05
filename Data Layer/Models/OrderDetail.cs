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
    public class OrderDetail
    {
        // ID này trong MongoDB không quá quan trọng nếu bạn nhúng nó vào Order.
        // Bạn có thể giữ lại để code C# không bị lỗi, nhưng Mongo sẽ không tự tăng số này đâu nhé.
        public int OrderDetailId { get; set; }

        public Guid? OrderId { get; set; }
        public Guid? FoodId { get; set; }

        // 2. Cấu hình tiền tệ (Bắt buộc để không sai lệch giá)
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal? UnitPrice { get; set; }

        public int? Quantity { get; set; }

        // 3. XỬ LÝ LIÊN KẾT (CỰC KỲ QUAN TRỌNG)
        
        // A. MenuFoodItem: 
        // Ta dùng [BsonIgnore] để không lưu nguyên object món ăn vào đây.
        // (Tuy nhiên, Mẹo Pro: Trong thực tế NoSQL, người ta thường thêm field "FoodName" và "FoodImage" 
        // vào thẳng class này để lưu "chết" tên món tại thời điểm mua, phòng khi món gốc bị đổi tên/xóa).
        [BsonIgnore]
        public virtual MenuFoodItem MenuFoodItem { get; set; }

        // B. Order: 
        // BẮT BUỘC PHẢI IGNORE.
        // Nếu không, bạn sẽ bị lỗi "Circular Reference" (Vòng lặp vô tận):
        // Order chứa OrderDetail -> OrderDetail lại chứa Order -> Order lại chứa OrderDetail...
        // MongoDB sẽ báo lỗi "StackOverflow" ngay lập tức nếu thiếu dòng này.
        [BsonIgnore]
        public virtual Order Order { get; set; }
    }
}