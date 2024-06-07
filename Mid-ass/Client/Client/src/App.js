import './App.css';
import Layout from "./pages/Layout";
import AuthProvider from './contexts/AuthContext';

function App() {
  return (
    <AuthProvider>
        <Layout/>
    </AuthProvider>
    
  );
}

export default App;
