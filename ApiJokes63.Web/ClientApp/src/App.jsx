import { Route, Routes } from 'react-router-dom';
import Home from './Home';
import Signup from './Signup';
import Login from './Login';
import { ContextComponent } from './Context';
import Layout from './Layout';
import './App.css'
import 'bootstrap/dist/css/bootstrap.min.css';
import ViewAll from './ViewAll';
import Logout from './Logout';


function App() {

    return (
        <ContextComponent>
            <Layout>
                <Routes>
                    <Route exact path='/' element={<Home />} />
                    <Route exact path='/signup' element={<Signup />} />
                    <Route exact path='/login' element={<Login />} /> 
                    <Route exact path='/viewall' element={<ViewAll />} />  
                    <Route exact path='/logout' element={<Logout />} /> 
               
                </Routes>
            </Layout>
        </ContextComponent>
    )
}

export default App