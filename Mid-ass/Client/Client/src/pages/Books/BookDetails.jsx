import { useEffect, useState } from "react";
import { useNavigate, useParams } from 'react-router-dom';
import { Card, Button, message, Table } from 'antd';
import bookApi from "../../api/bookApi";

const BookDetails = () => {
    const { id } = useParams();
    const [book, setBook] = useState();
    const navigate = useNavigate();

    useEffect(() => {
        const fetchBook = async () => {
            try {
                const response = await bookApi.getBookById(id);
                setBook(response.data.content);
            } catch (error) {
                console.error(error);
                message.error('Failed to fetch book details');
            }
        };
        fetchBook();
    }, [id]);

    const columns = [
        {
            title: 'Field',
            dataIndex: 'field',
            key: 'field',
            className: 'font-semibold',
        },
        {
            title: 'Details',
            dataIndex: 'details',
            key: 'details',
        },
    ];

    const data = [
        { key: '1', field: 'ID', details: book?.id },
        { key: '2', field: 'Author', details: book?.author },
        { key: '3', field: 'Title', details: book?.title },
        { key: '4', field: 'Publish Year', details: book?.publicationYear },
        { key: '5', field: 'Instock amount', details: book?.instockAmount },
    ];

    return (
        <div className="pt-10 flex flex-col items-center bg-gray-200 rounded">
            <Card className="w-3/4 shadow-lg p-6 rounded-md">
                <h2 className="text-2xl font-bold mb-6">Book Details</h2>
                <Table
                    columns={columns}
                    dataSource={data}
                    pagination={false}
                    showHeader={false}
                    rowClassName="text-lg"
                />
                <div className="flex justify-end mt-6">
                    <Button onClick={() => navigate('/books')} className="bg-red-500 text-white hover:bg-red-600">Back</Button>
                </div>
            </Card>
        </div>
    );
};

export default BookDetails;
