import React, { useState } from "react";
import ShoppingCartItem from "./ShoppingCartItem";
import ShoppingCartSummary from "./ShoppingCartSummary";
import "./ShoppingCart.scss";
import { Link, useNavigate } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import {
  decreaseQuantity,
  increaseQuantity,
  removeFood,
} from "../../redux/features/fastfoodCart";
import { toast } from "react-toastify";

const ShoppingCart = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const initialItems = useSelector((state) => state.fastfoodcard);
  const userData = useSelector((store) => store.accountmanage);
  const [shipping, setShipping] = useState(30000);

  const totalPrice = initialItems?.reduce(
    (total, item) => total + item.price * item.quantity,
    0
  );

  const handleAdd = (id) => dispatch(increaseQuantity(id));
  const handleDecrease = (id) => dispatch(decreaseQuantity(id));
  const handleDelete = (id) => dispatch(removeFood(id));

  // Hàm chuyển hướng sang trang xác nhận
  const handleGoToConfirmation = () => {
    if (!initialItems || initialItems.length === 0) {
        toast.error("Giỏ hàng trống!");
        return;
    }
    if (!userData?.UserId) {
        toast.warning("Vui lòng đăng nhập để thanh toán!");
        navigate("/login");
        return;
    }
    navigate("/payment-confirmation", { 
        state: { 
            cartItems: initialItems, 
            totalPrice: totalPrice + shipping 
        } 
    });
  };

  return (
    <div className="shopping-cart">
      <div className="cart-items">
        <h2>Shopping Cart</h2>
        {initialItems?.length > 0 ? (
            initialItems.map((item) => (
            <ShoppingCartItem
                key={item.id}
                item={item}
                onAdd={handleAdd}
                onRemove={handleDecrease}
                onDelete={handleDelete}
            />
            ))
        ) : (
            <p style={{textAlign: 'center', margin: '20px'}}>Your cart is empty.</p>
        )}

        <div style={{display: 'flex', justifyContent: 'space-between', marginTop: '20px'}}>
            <Link to="/">
               <button className="back-to-shop">Back to Menu</button>
            </Link>
            {/* ĐÃ XÓA NÚT CHECKOUT Ở ĐÂY */}
        </div>
      </div>

      {/* Truyền hàm handleGoToConfirmation xuống Summary */}
      <ShoppingCartSummary
        items={initialItems}
        totalPrice={totalPrice}
        shipping={shipping}
        onCheckout={handleGoToConfirmation} 
      />
    </div>
  );
};

export default ShoppingCart;