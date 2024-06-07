import { createContext, useContext, useEffect, useState } from "react"
import userAPi from "../api/userApi";

const AuthContext = createContext({
    isAuthenticated: false,
    setIsAuthenticated: () => {},
    user: {
        username: '',
        id: '', 
        role: ''
    },
    setUser: () => {}
});

export const useAuthContext = () => useContext(AuthContext);

const AuthProvider = (props) => {
    const token = localStorage.getItem('token');
    const [isAuthenticated, setIsAuthenticated] = useState(!!token);
    const [user, setUser] = useState({
        username: '',
        id: '',
        role: null,
    });

    // useEffect(() => {
    //     if (localStorage.getItem("TOKEN")) {
    //         const fetch = async () => {
    //             try {
    //                 console.log("get to token ");
    //                 const response = await userAPi.getUserByToken();
    //                 //if success 
    //                 if (response.data.success) {
    //                     setIsAuthenticated(true);
    //                     setUser(response.data.content.User);
    //                 }
    //             } catch (err) {
    //                 setUser({});
    //                 localStorage.removeItem("TOKEN");
    //                 // localStorage.removeItem("REFRESH_TOKEN");
    //                 console.log(err);
    //             }
    //         };

    //         fetch();
    //     }
    // }, []);

    return (
        <AuthContext.Provider value={{isAuthenticated, setIsAuthenticated, user, setUser}}>
            {props.children}
        </AuthContext.Provider>
    )
};

export default AuthProvider;