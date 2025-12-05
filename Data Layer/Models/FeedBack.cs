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
    public class FeedBack
    {
        // 2. Định nghĩa Khóa chính
        [BsonId] 
        public Guid FeedBackId { get; set; }

        // Các khóa ngoại (Foreign Keys) giữ nguyên để biết Feedback này của ai, đơn nào
        public string? UserId { get; set; }
        public Guid OrderId { get; set; }
        
        public string CommentMsg { get; set; }

        // 3. Xử lý liên kết (QUAN TRỌNG)
        // Trong MongoDB, bảng Feedback là bảng riêng.
        // Ta KHÔNG muốn khi lưu Feedback lại lưu kèm cả đống dữ liệu của Order và User vào trong đó.
        // Ta dùng [BsonIgnore] để MongoDB bỏ qua 2 biến này khi Lưu/Đọc từ DB.
        
        [BsonIgnore]
        public virtual Order Order { get; set; }

        [BsonIgnore]
        public virtual User User { get; set; }
    }
}