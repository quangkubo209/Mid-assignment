import React, { useState, useEffect } from 'react';
import { Button, List, InputNumber, message } from 'antd';
import bookApi from '../api/bookApi';
import { useAuthContext } from '../contexts/AuthContext';
import borrowingApi from '../api/borrowApi';

const Home = () => {
    const [books, setBooks] = useState([]);
    const{user,setUser }= useAuthContext();
    const [borrowingRequests, setBorrowingRequests] = useState([]);

    useEffect(() => {
        const fetchPosts = async () => {
            try {
                console.log("log to fetch");
                const response = await bookApi.getListBooks();
                console.log("response1: ", response.data.content);
                setBooks(Array.isArray(response.data.content) ? response.data.content : []); 
            } catch (error) {
                console.error('Error fetching books:', error);
                message.error('Failed to fetch books');
            }
        };

        fetchPosts();
    }, []);

    const handleBorrowRequest = (bookId, field, value) => {
        const requestExists = borrowingRequests.some(request => request.bookId === bookId);

        if (requestExists) {
            setBorrowingRequests(borrowingRequests.map(request => {
                if (request.bookId === bookId) {
                    return { ...request, [field]: value };
                }
                return request;
            }));
        } else {
            setBorrowingRequests([...borrowingRequests, { bookId, [field]: value }]);
        }
    };

    const handleSendRequest = async () => {
        if (borrowingRequests.length === 0) {
            message.error('No borrowing requests to send');
            return;
        }

        const requestPayload = {
            requestorId: user.id, 
            dateRequested: new Date().toISOString(),
            status: 'Waiting',
            bookBorrowingRequestDetails: borrowingRequests.map(request => ({
                bookId: request.bookId,
                borrowingPeriod: request.borrowingPeriod || 7,
                amount: request.quantity
            }))
        };

        try {
            console.log('Sending borrowing request: ', requestPayload);
            await borrowingApi.borrowBook(requestPayload);
            message.success('Borrowing request sent successfully');
            setBorrowingRequests([]);
        } catch (error) {
            console.error('Error sending borrowing request:', error);
            message.error('Failed to send borrowing request');
        }
    };

    return (
        <div className="container mx-auto p-4">
            <h1 className="text-3xl font-semibold mb-4">Available Books</h1>
            <List
                itemLayout="horizontal"
                dataSource={Array.isArray(books) ? books : []} 
                renderItem={book => (
                    <List.Item className="flex justify-between items-center">
                        <div>
                            <p className="text-xl font-semibold">{book.title}</p>
                            <p className="text-gray-600">Quantity available: {book.instockAmount}</p>
                        </div>
                        <InputNumber
                            className="ml-4 w-20"
                            min={1}
                            max={book.quantity}
                            defaultValue={1}
                            onChange={(value) => handleBorrowRequest(book.id, 'quantity', value)}
                        />
                        <InputNumber
                            className="ml-4 w-20"
                            min={1}
                            max={30} 
                            defaultValue={14} 
                            onChange={(value) => handleBorrowRequest(book.id, 'borrowingPeriod', value)}
                        />
                    </List.Item>
                )}
            />
            <div className="mt-4">
                <Button
                    type="primary"
                    onClick={handleSendRequest}
                    disabled={borrowingRequests.length === 0}
                >
                    Send 
                </Button>
            </div>
        </div>
    );
};

export default Home;
