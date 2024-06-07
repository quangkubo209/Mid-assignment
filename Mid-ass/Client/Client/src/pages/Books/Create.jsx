import React, { useState, useEffect } from 'react';
import { Form, Input, Button, message, Select } from 'antd';
import 'tailwindcss/tailwind.css'; 
import postApi from '../../api/bookApi';
import categoryApi from '../../api/categoryApi';
import { useNavigate } from'react-router-dom';

const { Option } = Select;

const CreatePage = () => {
  const [form] = Form.useForm();
  const [categories, setCategories] = useState([]);
  const [selectedCategory, setSelectedCategory] = useState('');

  const navigate = useNavigate();


  //fetch list categories
  useEffect(() => {
    const fetchCategories = async () => {
      try {
        const response = await categoryApi.getListCategory();
        setCategories(response.data.content );
      } catch (error) {
        console.error('Failed to fetch categories:', error);
      }
    };

    fetchCategories(); 
  }, []); 


  const onFinish = async (values) => {
    try {
      await postApi.createBook(values);
      message.success('Book created successfully');
      form.resetFields();
      navigate('/books');
      
      // Navigate back to the book page after successful creation
    } catch (error) {
      message.error('Book created failed');
      navigate('/books');
      message.error('Failed to create book');
    }
  };

  //rating from 1 to 5
  const validateAverageRating = (_, value) => {
    if (value < 1 || value > 5) {
      return Promise.reject('Average Rating must be between 1 and 5');
    }
    return Promise.resolve();
  };

  return (
    <div className="flex justify-center items-center h-screen">
      <Form style={{ width: '50%' }} form={form} onFinish={onFinish} layout="vertical" className="p-8 border border-gray-200 rounded-lg shadow-md">
        <Form.Item
          name="title"
          label="Title"
          rules={[{ required: true, message: 'Title is required' }]}
        >
          <Input className="w-full" />
        </Form.Item>
        <Form.Item
          name="author"
          label="Author"
          rules={[{ required: true, message: 'Author is required' }]}
        >
          <Input className="w-full" />
        </Form.Item>
        <Form.Item
          name="categoryId"
          label="Category"
          rules={[{ required: true, message: 'Category is required' }]}
        >
          <Select defaultValue={selectedCategory} onChange={(value) => setSelectedCategory(value)} className="w-full">
            {categories.map(category => (
              <Option key={category.id} value={category.id}>{category.name}</Option>
            ))}
          </Select>
        </Form.Item>
        <Form.Item
          name="PublicationYear"
          label="Publication Year"
          rules={[{ required: true, message: 'Publication Year is required' }]}
        >
          <Input type="number" className="w-full" />
        </Form.Item>
        <Form.Item
          name="description"
          label="Description"
          rules={[{ required: true, message: 'Description is required' }]}
        >
          <Input.TextArea className="w-full" />
        </Form.Item>
        <Form.Item
        name="averageRating"
        label="Average Rating"
        rules={[
          { 
            required: true, 
            message: 'Average Rating is required' 
          },
          {
            validator: validateAverageRating, // Custom validator function
          }
        ]}
      >
          <Input type="number" className="w-full" />
        </Form.Item>
        <Form.Item
          name="instockAmount"
          label="In Stock Amount"
          rules={[{ required: true, message: 'In Stock Amount is required' }]}
        >
          <Input type="number" className="w-full" />
        </Form.Item>
        <Form.Item className='flex justify-center'>
          <Button type="primary" htmlType="submit" className="w-full">
            Save
          </Button>
        </Form.Item>
      </Form>
    </div>
  );
};

export default CreatePage;