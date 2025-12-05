using System;
using System.Collections.Generic;
// 1. Thêm 2 thư viện này của MongoDB
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data_Layer.Models
{
    public class Order
    {
        // 2. Định nghĩa Khóa chính (Primary Key)
        [BsonId] // Báo cho Mongo biết đây là _id
        public Guid OrderId { get; set; }

        public string? MemberId { get; set; }
        public int? DroneId { get; set; }
        public DateTime OrderDate { get; set; }

        public DateTime? ShippedDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public string? Address { get; set; }

        // 3. Xử lý kiểu số thập phân (Decimal)
        // MongoDB cần biết cách lưu Decimal, dùng Decimal128 là chuẩn nhất
        [BsonRepresentation(BsonType.Decimal128)]
        public Decimal? TotalPrice { get; set; }

        public string? StatusOrder { get; set; }
        public string? DeliveryStatus { get; set; }

        // --- CÁC TRƯỜNG MỚI BẠN THÊM ---
        public string Status { get; set; } = "PENDING";
        public double RestaurantLat { get; set; }
        public double RestaurantLng { get; set; }
        public double UserLat { get; set; }
        public double UserLng { get; set; }

        // --- 4. XỬ LÝ QUAN HỆ (QUAN TRỌNG) ---

        // A. OrderDetails: Trong NoSQL, chi tiết đơn hàng nên NẰM TRONG đơn hàng luôn (Embedding).
        // Bỏ chữ 'virtual' đi để tránh EF Core hiểu nhầm nếu còn sót lại code cũ.
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        // B. Các bảng liên kết khác (User, Drone, Feedback...): 
        // Trong MongoDB, ta KHÔNG lưu nguyên object User vào trong Order (sẽ bị lặp dữ liệu và lỗi vòng lặp).
        // Ta dùng [BsonIgnore] để khi lưu Order, nó không cố lưu cả User/Drone vào DB.
        // Khi cần lấy thông tin User, ta sẽ dùng MemberId để truy vấn riêng.

        [BsonIgnore]
        public virtual User? User { get; set; }

        [BsonIgnore]
        public virtual Drone? Drone { get; set; }

        [BsonIgnore]
        public virtual ICollection<TransactionBill> TransactionBills { get; set; } = new List<TransactionBill>();

        [BsonIgnore]
        public virtual ICollection<OrderStatus> OrderStatuses { get; set; } = new List<OrderStatus>();

        [BsonIgnore]
        public virtual ICollection<FeedBack> FeedBacks { get; set; } = new List<FeedBack>();
    }
}