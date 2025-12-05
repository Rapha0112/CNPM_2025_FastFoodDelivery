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
    public class Cart
    {
        // 2. Định nghĩa khóa chính
        [BsonId]
        public Guid Id { get; set; }

        public string? UserID { get; set; }
        public Guid foodId { get; set; }
        public int Quantity { get; set; }
        
        // 3. XỬ LÝ LIÊN KẾT (QUAN TRỌNG)
        
        // Trong MongoDB, ta KHÔNG lưu nguyên cả đối tượng User vào giỏ hàng 
        // (để tránh dữ liệu bị lặp và lỗi vòng lặp vô tận).
        // Ta dùng [BsonIgnore] để Mongo bỏ qua, không lưu dòng này vào DB.
        [BsonIgnore]
        public virtual User User { get; set; }

        // Tương tự, ta chỉ lưu 'foodId' ở trên. 
        // Còn object 'Food' này dùng để hiển thị code, không lưu vào DB.
        // Khi lấy giỏ hàng ra, Service của bạn sẽ phải tìm thông tin món ăn dựa vào foodId để điền vào đây.
        [BsonIgnore]
        public virtual MenuFoodItem Food { get; set; }
    }
}