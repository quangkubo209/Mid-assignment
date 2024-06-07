// src/api/borrowingApi.js
import axios from 'axios';
import { httpClient } from '../axios/httpClient';

const PREFIX = "https://localhost:7034/api/Borrow";

const borrowingApi = {
    getList: () => {
        const url = `${PREFIX}`;
        return axios.get(url);
    },

     approveOrRejectRequest : (requestId, approverId, status) => {
        const url = `${PREFIX}/change-status`;
        return axios.post(url, {
            requestId,
            approverId,
            status
        });
     },
     borrowBook: (borrowRequest) => {
        const url = `${PREFIX}/borrow-book`;
        return axios.post(url, borrowRequest);
    }
    
};

export default borrowingApi;
