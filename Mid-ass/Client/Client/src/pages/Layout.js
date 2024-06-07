import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Nav from "../components/Nav";
import Home from "../pages/Home";
import BookDetails from '../pages/Books/BookDetails';
import Books from "../pages/Books/Books";
import CreateBook from '../pages/Books/Create';
import EditBook from '../pages/Books/EditBook';
import Categories from '../pages/Categories/Categories';
import CategoryDetails from '../pages/Categories/CategoryDetails';
import CreateCategory from '../pages/Categories/CreateCategory';
import EditCategory from '../pages/Categories/EditCategory';
import BorrowingRequests from './Borrows/BorrowRequest';
import Login from './Login';
import BorrowRequestAdmin from './Borrows/BorrowRequetsAdmin';

const Layout = () => {
  return (
    <div>
      <Nav />
      <div  style={{ marginTop: '100px'  }}>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/books" element={<Books />} />
          <Route path="/books/create" element={<CreateBook />} />
          <Route path="/books/edit/:id" element={<EditBook />} />
          <Route path="/books/:id" element={<BookDetails />} />
          <Route path="/categories" element={<Categories />} />
          <Route path="/categories/create" element={<CreateCategory />} />
          <Route path="/categories/edit/:id" element={<EditCategory />} />
          <Route path="/categories/:id" element={<CategoryDetails />} />
          <Route path="/borrowing-requests" element={<BorrowingRequests />} />
          <Route path="/borrowing-requests-admin" element={<BorrowRequestAdmin />} />

          <Route path="/login" element={<Login />} />
        </Routes>
      </div>
    </div>
  )
};

export default Layout;
