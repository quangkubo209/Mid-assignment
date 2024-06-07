import axios from 'axios';
import { httpClient } from '../axios/httpClient';

const PREFIX =  "https://localhost:7034/api/User";

const userAPi = {
    getUserById: (id) => {
        const url = `${PREFIX}/${id}`;
        return httpClient.get(url);
    },
    getUserByToken: () => {
        const url = `${PREFIX}/token`;
        return httpClient.get(url);
    },
    query: (sort = 1, query = {}) => {
        const sortQuery = sort === 1 ? "asc,name" : "desc,name";
        const url = `${PREFIX}`;
        return axios.get(url, {
            params: {
                sort: sortQuery,
                ...query,
            },
        });
    },
    createUser: (data) => {
        const url = `${PREFIX}`;
        return httpClient.post(url, data, {
            headers: {
                "Content-Type": "multipart/form-data",
            },
        });
    },
    updateUserDetail: (data) => {
        const url = `${PREFIX}/update-user-detail`;
        return httpClient.patch(url, data);
    },
    updateUserDetailById: (id, data) => {
        const url = `${PREFIX}/${id}`;
        return httpClient.patch(url, data);
    },

    deleteUser: (id) => {
        const url = `${PREFIX}/${id}`;
        return httpClient.delete(url);
    },

    getUserByToken: () => {
        const url = `${PREFIX}/token`;
        return httpClient.get(url);
    },

};

export default userAPi;
