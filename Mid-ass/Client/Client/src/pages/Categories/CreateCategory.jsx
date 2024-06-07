import React from 'react';
import { Form, Input, Button, message } from 'antd';
import categoryApi from '../../api/categoryApi';

import { useNavigate } from'react-router-dom';
const CreateCategory = () => {
    const [form] = Form.useForm();
    const navigate = useNavigate();

    const onFinish = async (values) => {
        try {
            await categoryApi.createCategory(values);
            message.success('Category created successfully');
            navigate('/category');
            form.resetFields();
        } catch (error) {
            message.error('Failed to create category');
        }
    };

    return (
        <Form style={{ margin: "20px 50px" }} form={form} onFinish={onFinish} layout="vertical">
            <Form.Item
                name="name"
                label="Name"
                rules={[{ required: true, message: 'Name is required' }]}
            >
                <Input />
            </Form.Item>
            <Form.Item>
                <Button type="primary" htmlType="submit">
                    Save
                </Button>
            </Form.Item>
        </Form>
    );
};

export default CreateCategory;
