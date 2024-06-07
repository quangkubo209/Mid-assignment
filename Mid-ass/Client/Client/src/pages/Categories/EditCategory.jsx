import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Form, Button, message, Card } from "antd";
import categoryApi from "../../api/categoryApi";
import InputField from "../../components/InputField";

const EditCategory = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [form] = Form.useForm();

    const [loading, setLoading] = useState(false);
    const [formData, setFormData] = useState({ id: '', name: '' });

    useEffect(() => {
        const fetchCategoryDetails = async () => {
            try {
                const response = await categoryApi.getCategoryDetails(id);
                console.log(response.data.content);
                setFormData({id: response.data.content.id, name: response.data.content.name});
            } catch (error) {
                console.log(error);
            }
        };

        fetchCategoryDetails();
    }, [id]);

    const onFinish = async (values) => {
        setLoading(true);
        try {
            await categoryApi.updateCategory(id, values);
            message.success('Category updated successfully');
            navigate("/category");
        } catch (error) {
            message.error('Failed to update category');
        }
        setLoading(false);
    };

    return (
        <div className="flex justify-center items-center h-screen bg-gray-200">
            <Card className="w-[80%] max-w-lg p-8">
                <h1 className="text-3xl font-semibold mb-6">Edit Category</h1>
                <Form
                    form={form}
                    layout="vertical"
                    initialValues={formData}
                    onFinish={onFinish}
                >
                    <Form.Item
                        name="name"
                        label="Name"
                        rules={[{ required: true, message: 'Please enter the category name' }]}
                    >
                        <InputField />
                    </Form.Item>
                    <Form.Item>
                        <Button type="primary" htmlType="submit" loading={loading}>
                            Update
                        </Button>
                    </Form.Item>
                </Form>
            </Card>
        </div>
    );
};

export default EditCategory;
