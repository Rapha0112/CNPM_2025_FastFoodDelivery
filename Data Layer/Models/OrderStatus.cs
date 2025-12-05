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
    public class OrderStatus
    {
        // 2. Định nghĩa Khóa chính
        [BsonId]
        public Guid OrderStatusId { get; set; }

        // Các ID liên kết giữ nguyên để biết trạng thái này thuộc đơn nào, shipper nào cập nhật
        public string? ShipperId { get; set; }
        public Guid? OrderId { get; set; }
        
        public string? OrderStatusName { get; set; } // Ví dụ: "Đang giao", "Đã hủy"

        // 3. Xử lý liên kết (QUAN TRỌNG)
        
        // A. Order: 
        // Bắt buộc thêm [BsonIgnore].
        // Nếu không, khi lưu trạng thái "Đang giao", nó sẽ lưu kèm cả cái Đơn hàng to đùng vào trong dòng trạng thái này.
        // Điều đó gây lặp dữ liệu và tốn dung lượng vô ích.
        [BsonIgnore]
        public virtual Order Order { get; set; }

        // B. User (Shipper):
        // Bắt buộc thêm [BsonIgnore].
        // Ta chỉ cần lưu ShipperId là đủ. Khi nào cần hiện tên Shipper, ta sẽ dùng ID đó để tìm trong bảng User.
        [BsonIgnore]
        public virtual User User { get; set; }
    }
}