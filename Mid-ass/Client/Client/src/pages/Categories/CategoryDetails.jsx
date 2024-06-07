import { useEffect, useState } from "react";
import { useParams, useNavigate } from 'react-router-dom';
import { Card, Button, message } from 'antd';
import categoryApi from "../../api/categoryApi";

const CategoryDetails = () => {
    const { id } = useParams();
    const [category, setCategory] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchCategories = async () => {
            try {
                const response = await categoryApi.getCategoryDetails(id);
                setCategory(response.data.content);
            } catch (error) {
                message.error('Failed to fetch categories');
            }
        };

        fetchCategories();
    }, [id]);

    return (
        <div className="pt-16 flex justify-center items-center flex-col">
            {category && (
                <Card title="Category Details" className="w-80 bg-gray-100 p-4 rounded-lg shadow-md">
                    <p className="font-semibold text-gray-800 mb-2">ID: {category.id}</p>
                    <p className="font-semibold text-gray-800 mb-2">Name: {category.name}</p>
                </Card>
            )}
            <Button className="mt-4" onClick={() => navigate('/categories')} type="primary">Back to Categories</Button>
        </div>
    );
};

export default CategoryDetails;
