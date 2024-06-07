import { httpClient } from '../axios/httpClient';
import axios from 'axios';

const PREFIX = "Auth";

const authApi = {
    signin: (data) => {
        // const url = `${PREFIX}/login`;
        return axios.post("https://localhost:7034/api/Auth/login", data);
    },
};

export default authApi;
