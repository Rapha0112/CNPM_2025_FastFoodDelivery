using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// 1. Thêm 2 dòng này để nhận diện kiểu dữ liệu Mongo
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data_Layer.Models
{
    public class AdminDashboard
    {
        // 2. Cấu hình cho biến Decimal
        // MongoDB cần dùng chuẩn Decimal128 để đảm bảo tính tiền không bị sai số lẻ
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal TotalSalesByMonth { get; set; }
    }
}