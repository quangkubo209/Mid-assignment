// import { useRoutes } from "react-router-dom"
// import Books from "../pages/Books/Books"
// import Profile from "../pages/Profile"
// import Home from "../pages/Home"
// import RequiredAuth from "../components/RequiredAuth"
// import React, { Suspense } from "react"
// import BookDetails from "../pages/Books/BookDetails"
// import Create from "../pages/Books/Create"
// import EditBook from "../pages/Books/EditBook"

// const LoginLazy = React.lazy(
//     () => import("../pages/Login")
// );

// const AppRoutes = () => {
//     const elements = useRoutes(
//         [
//             {path: '/', element: <Home/>},
//             {
//                 path: '/books', 
//                 element: <Books/>
//             },
//             {
//                 path: '/books/:id',
//                 element: <BookDetails/>
//             },
//             {
//                 path: '/books/create',
//                 element: <Create/>
//             },
//             {
//                 path: '/books/edit/:id',
//                 element: <EditBook/>
//             },
//             {
//                 path: '/login', 
//                 element: 
//                     <Suspense>
//                         <LoginLazy/>
//                     </Suspense>
//             },
//             {
//                 path: '/profile', 
//                 element: 
//                     <RequiredAuth>
//                         <Profile/>
//                     </RequiredAuth>
//             }
//         ]
//     );
//     return elements;
// }

// export default AppRoutes;