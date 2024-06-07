import { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { Form, Input, Button, message } from 'antd';
import bookApi from '../../api/bookApi';

const EditBook = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [loading, setLoading] = useState(false);
    const [form] = Form.useForm();

    useEffect(() => {
        const fetchBookDetails = async () => {
            try {
                const response = await bookApi.getBookById(id);
                form.setFieldsValue(response.data.content);
            } catch (error) {
                message.error('Failed to fetch book details');
            }
        };

        fetchBookDetails();
    }, [form, id]);

    const onFinish = async (values) => {
        setLoading(true);
        try {
            await bookApi.updateBook(id, values);
            message.success('Book updated successfully');
            navigate('/books');
        } catch (error) {
            message.error('Failed to update book');
        }
        setLoading(false);
    };

    return (
        <div className="container mx-auto mt-8">
            <h1 className="text-3xl font-semibold mb-6">Edit Book</h1>
            <Form
                form={form}
                layout="vertical"
                onFinish={onFinish}
            >
                <Form.Item
                    name="title"
                    label="Title"
                    rules={[{ required: true, message: 'Please enter the title' }]}
                >
                    <Input />
                </Form.Item>
                <Form.Item
                    name="author"
                    label="Author"
                    rules={[{ required: true, message: 'Please enter the author' }]}
                >
                    <Input />
                </Form.Item>
                <Form.Item
                    name="categoryId"
                    label="Category ID"
                    rules={[{ required: true, message: 'Please enter the category ID' }]}
                >
                    <Input />
                </Form.Item>
                <Form.Item
                    name="publicationYear"
                    label="Publication Year"
                    rules={[{ required: true, message: 'Please enter the publication year' }]}
                >
                    <Input />
                </Form.Item>
                <Form.Item
                    name="description"
                    label="Description"
                >
                    <Input.TextArea />
                </Form.Item>
                <Form.Item
                    name="averageRating"
                    label="Average Rating"
                >
                    <Input />
                </Form.Item>
                <Form.Item
                    name="instockAmount"
                    label="In-stock Amount"
                    rules={[{ required: true, message: 'Please enter the in-stock amount' }]}
                >
                    <Input />
                </Form.Item>
                <Form.Item>
                    <Button type="primary" htmlType="submit" loading={loading}>
                        Update
                    </Button>
                </Form.Item>
            </Form>
        </div>
    );
};

export default EditBook;
