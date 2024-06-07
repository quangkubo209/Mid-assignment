import axios from "axios";

const instance = axios.create({
    baseURL: "https://localhost:7034/api/",
    headers: {
        "Content-Type": "application/json",
    },
});

instance.interceptors.request.use((config) => {
    config.headers.Authorization = `Bearer ${localStorage.getItem("TOKEN")}`;
    return config;
});


// instance.interceptors.response.use(
//     (res) => {
//         return res?.data;
//         // return {data: res?.data, status: res.status};
//     },
//     async (error) => {
//         if (error.response.status === 401) {
//             window.location.href = "/login";
//             return Promise.reject(error.response.data);
//         }
//         return Promise.reject(error);
//     }
// );

export const httpClient = instance;

