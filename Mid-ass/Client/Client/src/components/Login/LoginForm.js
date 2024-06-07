import { useState } from "react";
import { Form, Input, Button, message } from "antd";
import { UserOutlined, LockOutlined } from "@ant-design/icons";
import { useAuthContext } from "../../contexts/AuthContext";
import { useNavigate } from "react-router-dom";
import userAPi from "../../api/userApi";
import authApi from "../../api/authApi";

const LoginForm = () => {
  const [loading, setLoading] = useState(false);
  const { setIsAuthenticated, setUser } = useAuthContext();
  const navigate = useNavigate();

  const onFinish = async (values) => {
    setLoading(true);
    try {
      const response = await authApi.signin(values);
      console.log("response: " + response);
      setUser({role: response.data.content.role, id: response.data.content.userId});
      setIsAuthenticated(true);
      localStorage.setItem("TOKEN", response.data.content.token);
      setLoading(false);
      message.success(response.data.message);
      return navigate("/books");
    }
    catch (error) {
      message.error(error.message);
      setLoading(false);
      navigate('/login');
    }
  };

  return (
    <Form
      name="loginForm"
      initialValues={{ remember: true }}
      onFinish={onFinish}
      className="bg-white w-1/2 rounded-md p-8"
    >
      <div className="text-3xl font-semibold mb-6">LOG IN</div>
      <Form.Item
        name="username"
        rules={[{ required: true, message: "Please input your username!" }]}
      >
        <Input
          prefix={<UserOutlined className="site-form-item-icon" />}
          placeholder="Username"
          className="mb-4"
        />
      </Form.Item>
      <Form.Item
        name="password"
        rules={[{ required: true, message: "Please input your password!" }]}
      >
        <Input.Password
          prefix={<LockOutlined className="site-form-item-icon" />}
          placeholder="Password"
          className="mb-6"
        />
      </Form.Item>
      <Form.Item className="flex justify-center">
        <Button
          type="primary"
          htmlType="submit"
          loading={loading}
          className="w-full"
        >
          Log in
        </Button>
      </Form.Item>
    </Form>
  );
};

export default LoginForm;
