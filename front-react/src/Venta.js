import React,{Component} from "react";
import { variables } from './Variables.js'

export class Venta extends Component{
    constructor (props){
        super(props);

        this.state={
            ventas:[],
            modalTitle:"",
            idVenta:0,
            idFactura:0,
            idProducto:0,
            cantidad:0
        }
    }

    //Buscar mostrar los clientes existentes
    refreshList(){
       fetch(variables.API_URL +'Venta')
        .then(response=>response.json())
        .then(data =>{
            this.setState({ventas:data});
        });
    }

    componentDidMount(){
        this.refreshList();
    }

    //Peticiones
    VentaID =(e)=>{
        this.setState({idVenta:e.target.value});
    }

    FacturaID =(e)=>{
        this.setState({idFactura:e.target.value});
    }

    idProducto =(e)=>{
        this.setState({idProducto:e.target.value});
    }

    cantidadVen =(e)=>{
        this.setState({cantidad:e.target.value});
    }

    addClick(){
        this.setState({
            modalTitle:"Registrar Venta",
            idVenta:0,
            idFactura:0,
            idProducto:0,
            cantidad:0
        });
    }

    editClick(ven){
        this.setState({
            modalTitle:"Actualizar Registro",
            idVenta:ven.idVenta,
            idFactura:ven.idFactura,
            idProducto:ven.idProducto,
            cantidad:ven.cantidad
        });
    }

    //Create
    createClick(){
        fetch(variables.API_URL+'Venta', {
            method:'POST',
            headers:{
                'Accept':'application/json',
                'Content-Type':'application/json',
            },
            body: JSON.stringify({
                idFactura:this.state.idFactura,
                idProducto:this.state.idProducto,
                cantidad:this.state.cantidad
            })
        })
        .then(response=>response.json())
        .then((result)=>{
            alert('Factura registrada :)!');
            this.refreshList();
        }, (error)=>{
            alert('No se pudo registrar :(');
        })
    }
    //Update
    updateClick(id){
        const{
            idVenta            
        }=this.state

        id=idVenta;
        fetch(variables.API_URL+'Venta/'+idVenta,{
            method:'PUT',
            headers:{
                'Accept':'application/json',
                'Content-Type':'application/json',
            },
            body: JSON.stringify({
                idVenta:this.state.idVenta,
                idFactura:this.state.idFactura,
                idProducto:this.state.idProducto,
                cantidad:this.state.cantidad
            })
        })
        .then(res=>res.json())
        .then((result)=>{
            this.refreshList();
            alert('Registro actualizado :)!');
            
        }, (error)=>{
            this.refreshList();
            alert('Registro actualizado :)!');
        })
    }
    
    //Delete
    deleteClick(id){
        if(window.confirm('Seguro que desea eliminar este registro?')){ 
            fetch(variables.API_URL+'Venta/'+id.idVenta, {
                method:'DELETE',
                headers:{
                    'Accept':'application/json',
                    'Content-Type':'application/json',
                }
            })
            .then(response=>response.json())
            .then((result)=>{
                this.refreshList();
                alert('Registro Eliminado :)!');
                
            }, (error)=>{
                this.refreshList();
                alert('Registro Eliminado :)!');
            })
        }
    }

    //FRONT
    render(){
        const{
            ventas,
            modalTitle,
            idVenta,
            idFactura,
            idProducto,
            cantidad
        }=this.state

        return(
            <div className="container">
                <br/>
                <button type="button" className="btn btn-outline-warning m-2 float-end" 
                data-bs-toggle="modal" 
                data-bs-target="#exampleModal" 
                onClick={()=>this.addClick()}>Registrar Venta</button>

                <table className="table table-striped">
                    <thead>
                        <th>ID de Venta</th>
                        <th>ID de Factura</th>
                        <th>ID Producto</th>
                        <th>Cantidad de Productos</th>
                    </thead>
                    <tbody>
                        {ventas.map(ven =>
                            <tr key={ven.idVenta}>
                                <td>{ven.idVenta}</td>
                                <td>{ven.idFactura}</td>
                                <td>{ven.idProducto}</td>
                                <td>{ven.cantidad}</td>
                                <td>
                                <button type="button" className="btn btn-outline-warning"
                                data-bs-toggle="modal" 
                                data-bs-target="#exampleModal" 
                                onClick={()=>this.editClick(ven)}>

                                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-pen" viewBox="0 0 16 16">
                                    <path d="m13.498.795.149-.149a1.207 1.207 0 1 1 1.707 1.708l-.149.148a1.5 1.5 0 0 1-.059 2.059L4.854 14.854a.5.5 0 0 1-.233.131l-4 1a.5.5 0 0 1-.606-.606l1-4a.5.5 0 0 1 .131-.232l9.642-9.642a.5.5 0 0 0-.642.056L6.854 4.854a.5.5 0 1 1-.708-.708L9.44.854A1.5 1.5 0 0 1 11.5.796a1.5 1.5 0 0 1 1.998-.001zm-.644.766a.5.5 0 0 0-.707 0L1.95 11.756l-.764 3.057 3.057-.764L14.44 3.854a.5.5 0 0 0 0-.708l-1.585-1.585z"/>
                                    </svg>

                                </button>
                                <span> </span>
                                <button type="button" 
                                className="btn btn-outline-warning"
                                onClick={()=>this.deleteClick(ven)}>

                                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-x-square" viewBox="0 0 16 16">
                                    <path d="M14 1a1 1 0 0 1 1 1v12a1 1 0 0 1-1 1H2a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1h12zM2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2z"/>
                                    <path d="M4.646 4.646a.5.5 0 0 1 .708 0L8 7.293l2.646-2.647a.5.5 0 0 1 .708.708L8.707 8l2.647 2.646a.5.5 0 0 1-.708.708L8 8.707l-2.646 2.647a.5.5 0 0 1-.708-.708L7.293 8 4.646 5.354a.5.5 0 0 1 0-.708z"/>
                                    </svg>
                                    
                                </button>
                                </td>
                            </tr>
                            )}
                    </tbody>
                </table>
                <div className="modal fade" id="exampleModal" tabIndex="-1" aria-hidden="true">
                <div className="modal-dialog modal-lg modal-dialog-centered">
                <div className="modal-content">
                    <div className="modal-header">
                        <h5 className="modal-title">{modalTitle}</h5>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>

                    <div className="modal-body">
                        <div className="input-group mb-3">
                            <span className="btn btn-outline-secondary">ID de Venta</span>
                            <input type="text" className="form-control" value={idVenta} onChange={this.VentaID}/>
                        </div>

                        <div className="input-group mb-3">
                            <span className="btn btn-outline-secondary">ID de Factura</span>
                            <input type="text" className="form-control" value={idFactura} onChange={this.FacturaID}/>
                        </div>

                        <div className="input-group mb-3">
                            <span className="btn btn-outline-secondary">ID de Producto</span>
                            <input type="text" className="form-control" value={idProducto} onChange={this.idProducto}/>
                        </div>

                        <div className="input-group mb-3">
                            <span className="btn btn-outline-secondary">Cantidad de productos</span>
                            <input type="text" className="form-control" value={cantidad} onChange={this.cantidadVen}/>
                        </div>
                        <div>  

                        {idVenta==0?
                        <button type="button" 
                        className="btn btn-outline-warning float-start"
                        onClick={()=>this.createClick()}>Registrar</button>
                        :null}

                        {idVenta!==0?
                        <button type="button" 
                        className="btn btn-outline-warning float-start"
                        onClick={()=>this.updateClick()}>Guardar cambios</button>
                        :null}
                        </div>                        
                    </div>

                </div>
                </div>
                </div>
            </div>
        )
    }
}