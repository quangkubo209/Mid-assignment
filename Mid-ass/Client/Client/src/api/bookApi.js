import axios from 'axios';
import { httpClient } from '../axios/httpClient';

const bookApi = {
    getListBooks: () => {
        return httpClient.get("Book");
    },

    getBookById: (id) => {
        return httpClient.get(`Book/${id}`);
    },

    createBook: (data) => {
        return httpClient.post("Book", data);
    },

    updateBook: (id, data) => {
        const url = `Book/${id}`;
        return httpClient.put(url, data);
    },

    deleteBook: (id) => {
        const url = `Book/${id}`;
        return httpClient.delete(url);
    },

    // searchBook: async (query) => {
    //     try {
    //       const response = await httpClient.get(`${PREFIX}?q=${query}`);
    //       return response.data;
    //     } catch (error) {
    //       console.error(`Error searching posts with query ${query}:`, error);
    //       throw error;
    //     }
    // },
};

export default bookApi;
