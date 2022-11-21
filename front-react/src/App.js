import logo from './logo.svg';
import './App.css';
import { Cliente } from './Cliente';
import { Proveedor } from './Proveedor';
import { Producto } from './Producto';
import { Factura } from './Factura';
import { Venta } from './Venta';
import { BrowserRouter, Route, Switch, NavLink} from 'react-router-dom';

function App() {
  return (
    <BrowserRouter>
      <div className='App Container'>
        <h3 className='d-flex justify-content-center m-3'>
          Consumiendo API .NET Core con REACT :D
        </h3>
        

        <div className='container'>
          <nav className="navbar navbar-expand-sm bg-light navbar-dark">
            <ul className="navbar-nav">
              <li className="nav-item- m-1">
                <NavLink className="btn btn-outline-warning" to="/cliente">Clientes</NavLink>
              </li>
              <li className="nav-item- m-1">
                <NavLink className="btn btn-outline-warning" to="/proveedor">Proveedores</NavLink>
              </li>
              <li className="nav-item- m-1">
                <NavLink className="btn btn-outline-warning" to="/producto">Productos</NavLink>
              </li>
              <li className="nav-item- m-1">
                <NavLink className="btn btn-outline-warning" to="/factura">Facturas</NavLink>
              </li>
              <li className="nav-item- m-1">
                <NavLink className="btn btn-outline-warning" to="/venta">Ventas</NavLink>
              </li>
            </ul>
          </nav>
        </div>

        <Switch>
          <Route path='/cliente' component={Cliente}/>
          <Route path='/proveedor' component={Proveedor}/>
          <Route path='/producto' component={Producto}/>
          <Route path='/factura' component={Factura}/>
          <Route path='/venta' component={Venta}/>
        </Switch>

      </div>
    </BrowserRouter>
  );
}

export default App;
