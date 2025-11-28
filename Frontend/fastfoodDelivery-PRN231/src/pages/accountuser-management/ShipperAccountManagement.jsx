import React, { useState, useEffect } from "react";
import { Table, Button, Badge } from "react-bootstrap"; // Thêm Badge để hiển thị trạng thái đẹp hơn
import { useNavigate } from "react-router-dom";
import useShipperAccountManagement from "../../utils/useShipperAccountManagement";
import axios from "axios";
import { notification } from "antd";

function ShipperAccountManagement() {
  // Lấy data và hàm từ custom hook
  const { accounts, handleDisable, fetchAccounts } = useShipperAccountManagement();
  
  // Filter này giữ nguyên logic cũ (nếu DB vẫn lưu role là Shipper thì để Shipper, nếu đã đổi thành Restaurant thì sửa lại)
  const [roleFilter, setRoleFilter] = useState("Shipper"); 
  const navigate = useNavigate();

  useEffect(() => {
    fetchAccounts();
  }, [fetchAccounts]);

  // Lọc danh sách (nếu cần thiết)
  const filteredAccounts = roleFilter
    ? accounts.filter((account) => account.role === roleFilter || account.role === 'Restaurant') // Update lỏng điều kiện để hiện cả 2
    : accounts;

  // 1. HÀM MỞ KHÓA (UNLOCK)
  const handleUnlock = async (record) => {
    try {
      // Payload gửi đi (cần thiết cho API UpdateUser)
      const payload = {
        fullName: record.fullName,
        email: record.email,
        phoneNumber: record.phoneNumber,
        address: record.address,
        // Các trường khác giữ nguyên
      };

      // Gọi API UpdateUser để set lại status Active
      const response = await axios.put(
        `http://localhost:5213/api/User/UpdateUser?id=${record.id}`,
        payload
      );

      if (response.data.IsSuccess || response.status === 200) {
        notification.success({
          message: "Thành công",
          description: "Đã mở khóa tài khoản nhà hàng!",
        });
        fetchAccounts(); // Tải lại dữ liệu ngay lập tức
      } else {
        notification.error({
          message: "Lỗi",
          description: "Không thể mở khóa tài khoản.",
        });
      }
    } catch (error) {
      console.error(error);
      notification.error({
        message: "Lỗi",
        description: "Lỗi hệ thống khi mở khóa.",
      });
    }
  };

  return (
    <div className="container mt-3">
      {/* Tiêu đề đã đổi thành Restaurant */}
      <div className="d-flex justify-content-between align-items-center mb-3">
        <h2>Restaurant Account Management</h2>
        <Button variant="primary" onClick={() => navigate("/dashboard/addrestaurant")}>
          ADD NEW RESTAURANT
        </Button>
      </div>

      <Table striped bordered hover responsive>
        <thead className="table-dark">
          <tr>
            <th>Full Name</th>
            <th>Username</th>
            <th>Role</th>
            <th>Phone Number</th>
            <th>Address</th>
            <th>Status</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {filteredAccounts.length === 0 ? (
            <tr>
              <td colSpan="7" className="text-center">
                No accounts found
              </td>
            </tr>
          ) : (
            filteredAccounts.map((account) => (
              <tr key={account.id}>
                <td>{account.fullName}</td>
                <td>{account.email}</td>
                <td>
                    {/* Nếu role là Shipper thì hiện Restaurant, ngược lại hiện nguyên gốc */}
                    {account.role === 'Shipper' ? 'Restaurant' : account.role}
                </td>
                <td>{account.phoneNumber}</td>
                <td>{account.address}</td>
                <td>
                    {/* Hiển thị trạng thái có màu sắc */}
                    {account.status === 'IsDeleted' ? (
                        <Badge bg="danger">IsDeleted</Badge>
                    ) : (
                        <Badge bg="success">Active</Badge>
                    )}
                </td>
                <td>
                  {/* LOGIC CHUYỂN ĐỔI NÚT BẤM */}
                  {account.status === "IsDeleted" ? (
                    <Button
                      variant="success" // Màu xanh
                      size="sm"
                      onClick={() => handleUnlock(account)}
                    >
                      Unlock
                    </Button>
                  ) : (
                    <Button
                      variant="danger" // Màu đỏ
                      size="sm"
                      onClick={() => handleDisable(account.id)}
                    >
                      Disable
                    </Button>
                  )}
                </td>
              </tr>
            ))
          )}
        </tbody>
      </Table>
    </div>
  );
}

export default ShipperAccountManagement;