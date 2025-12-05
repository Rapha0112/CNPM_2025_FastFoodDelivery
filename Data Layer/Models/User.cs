using Microsoft.AspNetCore.Identity;
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
    // Lưu ý: IdentityUser mặc định có Id là kiểu String.
    // MongoDB dùng ObjectId (dạng chuỗi ký tự) nên nó tương thích tốt, không cần sửa Id.
    public class User : IdentityUser
    {
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? Status { get; set; }

        // 2. XỬ LÝ LIÊN KẾT (BẮT BUỘC PHẢI CÓ)
        
        // Trong SQL, User chứa danh sách Orders để dùng Navigation Property.
        // Trong MongoDB, tuyệt đối KHÔNG lưu danh sách đơn hàng vào User.
        // Lý do: Nếu user mua 10.000 đơn, document User sẽ quá tải và login bị chậm.
        // Ta dùng [BsonIgnore] để ngắt bỏ việc lưu các danh sách này vào DB.

        [BsonIgnore]
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        [BsonIgnore]
        public virtual ICollection<FeedBack> FeedBacks { get; set; } = new List<FeedBack>();

        [BsonIgnore]
        public virtual ICollection<OrderStatus> OrderStatuses { get; set; } = new List<OrderStatus>();

        [BsonIgnore]
        public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
    }
}