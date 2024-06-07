import React, { useState, useEffect } from 'react';
import { Table, Button, Input, message, Modal } from 'antd';
import { useNavigate } from 'react-router-dom';
import bookApi from '../../api/bookApi';

const { Search } = Input;

const GetListPage = () => {
  const [posts, setPosts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [deleteConfirmVisible, setDeleteConfirmVisible] = useState(false); 
  const [deletingPostId, setDeletingPostId] = useState(null); 
  const navigate = useNavigate();

  useEffect(() => {
    const fetchPosts = async () => {
      try {
        console.log("log to fetch");
        const response = await bookApi.getListBooks();
        console.log("response1: " + response.data.content);
        setPosts(response.data.content);
        setLoading(false);
      } catch (error) {
        message.error(error.message);

      }
    };

    fetchPosts();
  }, []);

  const handleSearch = async (value) => {
    try {
      const response = await bookApi.searchPosts(value); 
    } catch (error) {
      message.error('Failed to search posts');
    }
  };

  const handleDelete = async (id) => {
    try {
      setDeletingPostId(id);
      setDeleteConfirmVisible(true);
    } catch (error) {
      message.error('Failed to delete post');
    }
  };

  const handleDeleteConfirm = async () => {
    try {
      await bookApi.deleteBook(deletingPostId); // API call to delete the post
      setPosts(posts.filter(post => post.id !== deletingPostId));
      message.success('Post deleted successfully');
      setDeleteConfirmVisible(false);
    } catch (error) {
      message.error('Failed to delete post');
    }
  };

  const handleDeleteCancel = () => {
    setDeleteConfirmVisible(false);
  };

  const columns = [
    {
      title: 'Title',
      dataIndex: 'title',
      key: 'title',
      sorter: (a, b) => a.title.localeCompare(b.title),
    },
    {
      title: 'Author',
      dataIndex: 'author',
      key: 'author',
      sorter: (a, b) => a.author.localeCompare(b.author),
    },
    {
      title: 'Category ',
      dataIndex: 'categoryName',
      key: 'categoryName',
    },
    {
      title: "Publication year",
      dataIndex: 'publicationYear',
      key: 'publicationYear',
      sorter: (a, b) => a.publicationYear - b.publicationYear,
    },
    {
      title: 'Average Rating',
      dataIndex: 'averageRating',
      key: 'averageRating',
      sorter: (a, b) => a.averageRating - b.averageRating,
    },
    {
      title: 'Action',
      key: 'action',
      render: (text, record) => (
        <span>
          <Button type="link" onClick={() => navigate(`/books/${record.id}`)}>Detail</Button>
          <Button type="link" onClick={() => navigate(`/books/edit/${record.id}`)}>Edit</Button>
          <Button type="link" onClick={() => handleDelete(record.id)}>Delete</Button>
        </span>
      ),
    },
  ];

  return (
    <div style={{ margin: "20px 50px" }}>
      <div style={{ display: "flex", justifyContent: "center" }}>
        <Search
          placeholder="Search by title"
          enterButton
          onSearch={handleSearch}
          style={{ marginBottom: '16px', width: '25%', marginLeft: "16px" }}
        />
      </div>
      <div style={{ display: "flex", flexDirection: "row", justifyContent: "start" }}>
        <Button onClick={() => navigate("/books/create")} type="primary" style={{ marginBottom: '16px' }}>
          New Book
        </Button>
      </div>
      <Table
        columns={columns}
        dataSource={posts}
        rowKey="id"
        loading={loading}
      />
      {/* Dialog xác nhận xóa */}
      <Modal
        title="Confirm Delete"
        visible={deleteConfirmVisible}
        onOk={handleDeleteConfirm}
        onCancel={handleDeleteCancel}
        okText="Yes"
        cancelText="No"
      >
        <p>Are you sure you want to delete this post?</p>
      </Modal>
    </div>
  );
};

export default GetListPage;
