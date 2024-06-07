import { useEffect, useState } from "react";
import { Table, Button, message, Popconfirm } from 'antd';
import { CheckOutlined, CloseOutlined } from '@ant-design/icons';
import borrowApi from '../../api/borrowApi';
import { useAuthContext } from "../../contexts/AuthContext";

const BorrowRequestAdmin = () => {
    const [requests, setRequests] = useState([]);
    const {user} = useAuthContext();

    useEffect(() => {
        const fetchRequests = async () => {
            try {
                const response = await borrowApi.getList();
                setRequests(response.data.content);
            } catch (error) {
                console.error('Error fetching requests:', error);
            }
        };

        fetchRequests();
    }, []);

    const handleApprove = async (requestId) => {
        try {
            await borrowApi.approveOrRejectRequest(requestId, user.id, "approved");
            message.success('Request approved successfully');
            // Update UI after approving request
            setRequests(prevRequests => prevRequests.map(request => {
                if (request.id === requestId) {
                    return { ...request, status: "appproved" };
                }
                return request;
            }));
        } catch (error) {
            console.error('Error approving request:', error);
            message.error('Failed to approve request');
        }
    };

    const handleReject = async (requestId) => {
        try {
            await borrowApi.approveOrRejectRequest(requestId, user.id, "rejected");
            message.success('Request rejected successfully');
            // Update UI after rejecting request
            setRequests(prevRequests => prevRequests.map(request => {
                if (request.id === requestId) {
                    return { ...request, status: "rejected" };
                }
                return request;
            }));
        } catch (error) {
            console.error('Error rejecting request:', error);
            message.error('Failed to reject request');
        }
    };

    const columns = [
        {
            title: 'ID',
            dataIndex: 'id',
            key: 'id',
        },
        {
            title: 'Requestor',
            dataIndex: 'usernameRequestor',
            key: 'usernameRequestor',
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
            render: (status) => (
                <span style={{ color: status === 'Waiting' ? 'orange' : (status === 'approved' ? 'green' : 'red') }}>
                    {status}
                </span>
            ),
        },
        {
            title: 'Action',
            dataIndex: 'id',
            key: 'action',
            render: (id, record) => (
                record.status === 'Waiting' ? (
                    <span>
                        <Popconfirm
                            title="Are you sure to approve this request?"
                            onConfirm={() => handleApprove(id)}
                            okText="Yes"
                            cancelText="No"
                        >
                            <Button type="primary" icon={<CheckOutlined />} style={{ marginRight: 8 }}>Approve</Button>
                        </Popconfirm>
                        <Popconfirm
                            title="Are you sure to reject this request?"
                            onConfirm={() => handleReject(id)}
                            okText="Yes"
                            cancelText="No"
                        >
                            <Button type="danger" icon={<CloseOutlined />}>Reject</Button>
                        </Popconfirm>
                    </span>
                ) : null
            ),
        },
    ];

    return (
        <div className="flex justify-center items-center min-h-screen bg-gray-100">
            <div className="w-4/5 bg-white p-8 shadow-lg rounded-lg">
                <h1 className="text-3xl font-semibold mb-6 text-center">Admin Request Management</h1>
                <Table dataSource={requests} columns={columns} rowKey="id" />
            </div>
        </div>
    );
};

export default BorrowRequestAdmin;
