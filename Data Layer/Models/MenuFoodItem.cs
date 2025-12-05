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
    public class MenuFoodItem
    {
        // 2. Định nghĩa Khóa chính
        [BsonId]
        public Guid FoodId { get; set; }

        public Guid? CategoryId { get; set; }
        public string FoodName { get; set; }
        public string? FoodDescription { get; set; }
        public string? Image { get; set; }

        // 3. Cấu hình Tiền tệ (Rất quan trọng)
        // Dùng Decimal128 để đảm bảo giá tiền chính xác, không bị lỗi làm tròn
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal? UnitPrice { get; set; }

        public string? FoodStatus { get; set; }

        // 4. Xử lý các mối quan hệ (QUAN TRỌNG)
        
        // A. Category: Ta chỉ lưu CategoryId ở trên. 
        // Biến này để hiển thị code, không lưu vào DB để tránh lặp dữ liệu.
        [BsonIgnore]
        public virtual Category? Category { get; set; }

        // B. OrderDetails và Carts: 
        // Đây là lỗi phổ biến nhất khi chuyển từ SQL -> NoSQL.
        // Trong SQL, bạn để List ở đây để truy vấn ngược "Món này đã bán trong những đơn nào?".
        // Trong MongoDB, TUYỆT ĐỐI KHÔNG LƯU danh sách này vào trong món ăn.
        // Nếu món này bán được 1 triệu lần, cái List này sẽ chứa 1 triệu dòng -> Treo Database ngay lập tức.
        
        [BsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        [BsonIgnore]
        public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
    }
}