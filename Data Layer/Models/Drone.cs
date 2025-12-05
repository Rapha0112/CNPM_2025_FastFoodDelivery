using System;
// 1. Giữ lại DataAnnotations để validate dữ liệu đầu vào (Required, MaxLength...)
using System.ComponentModel.DataAnnotations;
// 2. Thêm thư viện MongoDB
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data_Layer.Models
{
    // Bỏ dòng [Table("Drone")] đi, MongoDB tự tạo collection tên "Drone"
    public class Drone
    {
        // 3. Định nghĩa Khóa chính
        [BsonId]
        public int DroneId { get; set; } 
        // LƯU Ý QUAN TRỌNG: 
        // Trong SQL, int thường là tự tăng (1, 2, 3...). 
        // Trong MongoDB, nó KHÔNG tự tăng. Khi tạo Drone mới, bạn phải tự gán số ID (ví dụ: 1, 2, 3) 
        // hoặc đổi sang dùng string/Guid để Mongo tự sinh.

        [Required]
        public string Name { get; set; } // "Drone #1"

        public string? Model { get; set; } // "DJI"

        public int BatteryLevel { get; set; } = 100;

        public string Status { get; set; } = "Ready"; 

        // Tọa độ
        public double CurrentLat { get; set; } 
        public double CurrentLng { get; set; }

        // 4. Khóa ngoại (Foreign Key)
        // Vẫn giữ lại ID để biết Drone đang cầm đơn hàng nào
        public Guid? CurrentOrderId { get; set; }

        // 5. Ngắt quan hệ (Navigation Property)
        // Trong MongoDB, ta KHÔNG lưu nguyên cục Order vào trong Drone.
        // Ta dùng [BsonIgnore] để khi lưu Drone, nó không kéo theo cả Order vào DB.
        [BsonIgnore]
        public virtual Order? CurrentOrder { get; set; }
    }
}