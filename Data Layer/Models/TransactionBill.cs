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
    public class TransactionBill
    {
        // 2. Định nghĩa Khóa chính
        [BsonId]
        public Guid TractionId { get; set; } 
        // (Lưu ý: Có vẻ bạn gõ nhầm chữ "Transaction" thành "Traction", 
        // nhưng mình giữ nguyên theo code cũ để không bị lỗi ở các file khác).

        public Guid? OrderId { get; set; }

        // 3. Xử lý liên kết
        // Dùng [BsonIgnore] để MongoDB không lưu nguyên cả object Order vào trong hóa đơn.
        // Ta chỉ cần lưu OrderId là đủ để tra cứu.
        [BsonIgnore]
        public virtual Order Order { get; set; }
    }
}