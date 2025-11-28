import React from 'react';
import { Layout, Typography } from 'antd';
import { 
  DashboardOutlined, 
  UnorderedListOutlined, 
  CoffeeOutlined, 
  RocketOutlined, 
  SettingOutlined, 
  BankOutlined,
  UserOutlined
} from '@ant-design/icons';
import { useNavigate, useLocation, Outlet } from 'react-router-dom';

const { Sider, Content, Header } = Layout;
const { Title } = Typography;

const RestaurantLayout = () => {
  const navigate = useNavigate();
  const location = useLocation();

  const menuItems = [
    { key: '/restaurant/dashboard', icon: <DashboardOutlined />, label: 'Dashboard' },
    { key: '/restaurant/orders', icon: <UnorderedListOutlined />, label: 'Danh sách đơn hàng' },
    { key: '/restaurant/menu', icon: <CoffeeOutlined />, label: 'Quản lý món ăn' },
    { key: '/restaurant/drone', icon: <RocketOutlined />, label: 'Drone', danger: true }, // Màu đỏ
    { key: '/restaurant/settings', icon: <SettingOutlined />, label: 'Cài đặt quán' },
    { key: '/restaurant/withdraw', icon: <BankOutlined />, label: 'Rút tiền doanh thu' },
  ];

  return (
    <Layout style={{ minHeight: '100vh' }}>
      <Header style={{ background: '#fff', padding: '0 20px', display: 'flex', justifyContent: 'space-between', alignItems: 'center', borderBottom: '1px solid #f0f0f0' }}>
        <Title level={3} style={{ margin: 0 }}>FoodFast</Title>
        <div style={{ display: 'flex', alignItems: 'center', gap: '10px' }}>
          <UserOutlined style={{ fontSize: '20px', color: '#fa8c16' }} />
          <span style={{ fontWeight: 'bold' }}>Restaurant Admin</span>
        </div>
      </Header>
      <Layout>
        <Sider width={240} theme="light" style={{ background: '#f5f5f5', padding: '20px 0' }}>
            <div style={{ padding: '0 10px' }}>
                {menuItems.map(item => (
                    <div 
                        key={item.key}
                        onClick={() => navigate(item.key)}
                        style={{
                            padding: '12px 20px',
                            marginBottom: '10px',
                            borderRadius: '8px',
                            cursor: 'pointer',
                            // Logic màu sắc giống ảnh mẫu: Nút đang chọn sẽ đỏ hoặc xám đậm
                            backgroundColor: location.pathname === item.key ? (item.danger ? '#ff4d4f' : '#d9d9d9') : 'transparent',
                            color: location.pathname === item.key ? (item.danger ? '#fff' : '#000') : '#000',
                            fontWeight: location.pathname === item.key ? 'bold' : 'normal',
                            display: 'flex', alignItems: 'center', gap: '10px'
                        }}
                    >
                        {item.icon} {item.label}
                    </div>
                ))}
            </div>
        </Sider>
        <Content style={{ margin: '0', padding: '24px', background: '#fff' }}>
          <Outlet /> 
        </Content>
      </Layout>
    </Layout>
  );
};

export default RestaurantLayout;