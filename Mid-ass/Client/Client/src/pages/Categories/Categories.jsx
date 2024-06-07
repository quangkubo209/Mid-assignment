import React, { useState, useEffect } from 'react';
import { Table, Button, Input, message, Modal } from 'antd';
import { useNavigate } from 'react-router-dom';
import categoryApi from '../../api/categoryApi';

const { Search } = Input;

const Categories = () => {
    const fakeData  =  [
        {name : 'category 1 '}, 
        {name: 'category 2'},
    ]
    const [categories, setCategories] = useState([]);
    const [loading, setLoading] = useState(true);
    const [deleteConfirmVisible, setDeleteConfirmVisible] = useState(false);
    const [deletingCategoryId, setDeletingCategoryId] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchCategories = async () => {
            try {
                const response = await categoryApi.getListCategory();
                setCategories(response.data.content);
                setLoading(false);
            } catch (error) {
                message.error('Failed to fetch categories');
            }
        };

        fetchCategories();
    }, []);

    const handleSearch = async (value) => {
        try {
            const response = await categoryApi.searchCategories(value);
            setCategories(response.data);
        } catch (error) {
            message.error('Failed to search categories');
        }
    };

    const handleDelete = async (id) => {
        try {
            setDeletingCategoryId(id);
            setDeleteConfirmVisible(true);
        } catch (error) {
            message.error('Failed to delete category');
        }
    };

    const handleDeleteConfirm = async () => {
        try {
            await categoryApi.deleteCategory(deletingCategoryId);
            setCategories(categories.filter(category => category.id !== deletingCategoryId));
            message.success('Category deleted successfully');
            setDeleteConfirmVisible(false);
        } catch (error) {
            message.error('Failed to delete category');
        }
    };

    const handleDeleteCancel = () => {
        setDeleteConfirmVisible(false);
    };

    const columns = [
        {
            title: 'ID',
            dataIndex: 'id',
            key: 'id',
            sorter: (a, b) => a.id - b.id,
        },
        {
            title: 'Name',
            dataIndex: 'name',
            key: 'name',
            sorter: (a, b) => a.name.localeCompare(b.name),
        },
        {
            title: 'Action',
            key: 'action',
            render: (text, record) => (
                <span>
                    <Button type="link" onClick={() => navigate(`/categories/${record.id}`)}>Detail</Button>
                    <Button type="link" onClick={() => navigate(`/categories/edit/${record.id}`)}>Edit</Button>
                    <Button type="link" onClick={() => handleDelete(record.id)}>Delete</Button>
                </span>
            ),
        },
    ];

    return (
        <div style={{ margin: "20px 50px" }}>
            <div style={{ display: "flex", justifyContent: "center" }}>
                <Search
                    placeholder="Search by name"
                    enterButton
                    onSearch={handleSearch}
                    style={{ marginBottom: '16px', width: '25%', marginLeft: "16px" }}
                />
            </div>
            <div style={{ display: "flex", flexDirection: "row", justifyContent: "start" }}>
                <Button onClick={() => navigate("/categories/create")} type="primary" style={{ marginBottom: '16px' }}>
                    New Category
                </Button>
            </div>
            <Table columns={columns} dataSource={categories} rowKey="id" loading={loading} />
            <Modal title="Confirm Delete" visible={deleteConfirmVisible} onOk={handleDeleteConfirm} onCancel={handleDeleteCancel} okText="Yes" cancelText="No" >
                <p>Are you sure you want to delete this category?</p>
            </Modal>
        </div>
    );
};

export default Categories;
