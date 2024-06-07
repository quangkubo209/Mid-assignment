import React, { useState, useEffect } from 'react';
import { Table, Tag } from 'antd';
import { message } from 'antd';

const BorrowRequest = () => {
    const [requests, setRequests] = useState([]);

    useEffect(() => {
        // Tạo một mảng chứa các yêu cầu mượn sách (fake data)
        const fakeRequests = [
            {
                id: '1',
                usernameRequestor: 'user1',
                dateRequested: '2024-06-06',
                status: 'Waiting'
            },
            {
                id: '2',
                usernameRequestor: 'user2',
                dateRequested: '2024-06-07',
                status: 'Approved'
            },
            {
                id: '3',
                usernameRequestor: 'user3',
                dateRequested: '2024-06-08',
                status: 'Rejected'
            }
        ];

        // Gán fakeRequests vào state
        setRequests(fakeRequests);
    }, []);

    const columns = [
        {
            title: 'Request ID',
            dataIndex: 'id',
            key: 'id',
        },
        {
            title: 'Requestor',
            dataIndex: 'usernameRequestor',
            key: 'usernameRequestor',
        },
        {
            title: 'Amount',
            dataIndex: 'Amount',
            key: 'requestDetails.Amount',
        },
        {
            title: 'Date Requested',
            dataIndex: 'dateRequested',
            key: 'dateRequested',
        },
        {
            title: 'Status',
            dataIndex: 'status',
            key: 'status',
            render: status => (
                <Tag color={status === 'Waiting' ? 'orange' : status === 'Approved' ? 'green' : 'red'}>
                    {status}
                </Tag>
            ),
        },
        // Các cột khác (Approver ID, Request Details) có thể được thêm vào đây
    ];

    return (
        <div className="container mx-auto mt-8">
            <h1 className="text-3xl font-semibold mb-6">Borrowing Requests</h1>
            <Table dataSource={requests} columns={columns} rowKey="id" />
        </div>
    );
};

export default BorrowRequest;
