import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import { notification } from "antd"; // Dùng notification của Antd cho đẹp

// Vẫn dùng API cũ hoặc API User/register tùy backend của bạn
const apiEndpoint = "http://localhost:5213/api/Shipper/register"; 

function AddRestaurant() {
  const navigate = useNavigate();
  const [account, setAccount] = useState({
    email: "",
    username: "",
    fullName: "",
    address: "",
    phoneNumber: "",
    password: "",
    confirmPassword: "",
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setAccount((prevState) => ({ ...prevState, [name]: value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (account.password !== account.confirmPassword) {
      notification.error({ message: "Lỗi", description: "Mật khẩu xác nhận không khớp!" });
      return;
    }

    try {
      // Payload gửi đi
      const payload = {
        email: account.email,
        username: account.username,
        fullName: account.fullName,
        address: account.address,
        phoneNumber: account.phoneNumber,
        password: account.password,
        confirmPassword: account.confirmPassword,
        // Nếu backend cần role, hãy thêm vào đây, ví dụ: role: "Restaurant"
      };

      const response = await axios.post(apiEndpoint, payload);

      if (response.data.isSuccess || response.status === 200) {
        notification.success({ message: "Thành công", description: "Đã thêm nhà hàng mới!" });
        navigate("/dashboard/restaurant"); // Quay về trang danh sách
      } else {
        notification.error({ message: "Thất bại", description: response.data.message });
      }
    } catch (error) {
      console.error("Error adding new restaurant:", error);
      notification.error({ message: "Lỗi", description: error.message });
    }
  };

  return (
    <div className="container mt-4" style={{ maxWidth: "600px" }}>
      <h2 className="mb-4 text-primary">Add New Restaurant</h2>
      
      <form onSubmit={handleSubmit} className="card p-4 shadow-sm">
        
        {/* Nhóm thông tin cơ bản của Nhà hàng */}
        <h5 className="mb-3 text-secondary">Thông tin nhà hàng</h5>
        
        <div className="mb-3">
          <label className="form-label">Restaurant Name (Full Name)</label>
          <input
            type="text"
            className="form-control"
            name="fullName"
            value={account.fullName}
            onChange={handleChange}
            placeholder="Ví dụ: Gà Rán KFC"
            required
          />
        </div>

        <div className="mb-3">
          <label className="form-label">Email</label>
          <input
            type="email"
            className="form-control"
            name="email"
            value={account.email}
            onChange={handleChange}
            placeholder="contact@restaurant.com"
            required
          />
        </div>

        <div className="mb-3">
          <label className="form-label">Phone Number</label>
          <input
            type="text"
            className="form-control"
            name="phoneNumber"
            value={account.phoneNumber}
            onChange={handleChange}
            required
          />
        </div>

        <div className="mb-3">
          <label className="form-label">Address</label>
          <input
            type="text"
            className="form-control"
            name="address"
            value={account.address}
            onChange={handleChange}
            required
          />
        </div>

        <hr />

        {/* Nhóm thông tin đăng nhập (Bắt buộc phải có để tạo account) */}
        <h5 className="mb-3 text-secondary">Thông tin đăng nhập</h5>

        <div className="mb-3">
          <label className="form-label">Username</label>
          <input
            type="text"
            className="form-control"
            name="username"
            value={account.username}
            onChange={handleChange}
            required
          />
        </div>

        <div className="row">
          <div className="col-md-6 mb-3">
            <label className="form-label">Password</label>
            <input
              type="password"
              className="form-control"
              name="password"
              value={account.password}
              onChange={handleChange}
              required
            />
          </div>
          <div className="col-md-6 mb-3">
            <label className="form-label">Confirm Password</label>
            <input
              type="password"
              className="form-control"
              name="confirmPassword"
              value={account.confirmPassword}
              onChange={handleChange}
              required
            />
          </div>
        </div>

        <div className="d-flex justify-content-end mt-3">
            <button
                type="button"
                className="btn btn-secondary me-2"
                onClick={() => navigate("/dashboard/restaurant")}
            >
                Cancel
            </button>
            <button type="submit" className="btn btn-primary">
                Save Restaurant
            </button>
        </div>
      </form>
    </div>
  );
}

export default AddRestaurant;