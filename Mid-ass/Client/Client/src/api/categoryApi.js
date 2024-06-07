import axios from 'axios';
import { httpClient } from '../axios/httpClient';

const PREFIX = "https://localhost:7034/api/Category";

const categoryApi = {
    getListCategory: () => {
        const url = `${PREFIX}`;
        return axios.get(url);
    },

    getCategoryDetails: (id) => {
        const url = `${PREFIX}/${id}`;
        return axios.get(url);
    },

    createCategory: (data) => {
        const url = `${PREFIX}`;
        return httpClient.post(url, data);
    },

    updateCategory: (id, data) => {
        const url = `${PREFIX}/${id}`;
        return axios.put(url, data);
    },

    deleteCategory: (id) => {
        const url = `${PREFIX}/${id}`;
        return httpClient.delete(url);
    },

    searchCategory: async (query) => {
        try {
            const response = await axios.get(`${PREFIX}?q=${query}`);
            return response.data;
        } catch (error) {
            console.error(`Error searching categories with query ${query}:`, error);
            throw error;
        }
    },
};

export default categoryApi;
