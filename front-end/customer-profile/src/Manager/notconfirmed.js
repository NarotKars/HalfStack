import React from 'react';
let i=0;
    
class NotConfirm extends React.Component{
  constructor(props) {
    super(props);
    this.state={
        orders: [],
        status:this.props.status,
        details:[],
        showDetails:false
    }

    this.seeDetails=this.seeDetails.bind()
  }

  seeDetails = (id) => {
    const that = this;
    that.state.showDetails=true;
    fetch("https://localhost:44390/manager/order/details/6/")
        .then(function(response) {
            return response.json();
        })
        .then(function(jsonStr) {
            that.setState({details: jsonStr});
            console.log(that.state.details)
        }).catch((err)=>{console.log(err);})

  }

  handleConfirm=(id)=>{
    const someData = {
      orderid:id,
      status:'confirmed'
     }

    const putMethod = {
      method: 'PUT',
      headers: {
       'Content-type': 'application/json' 
      },
      body: JSON.stringify(someData)
     }
     
     fetch("https://localhost:44390/manager/order/update", putMethod)
     .then(response => response.json())
  }

  // componentDidUpdate(prevProps, prevState)
  // {
  //     if(prevState.status!==this.state.status)
  //     {
  //         console.log(prevState.status , this.state.status);
  //         const that = this;
  //         fetch("https://localhost:44390/manager/orders/" + this.state.status)
  //             .then(function(response) {
  //                 return response.json();
  //             })
  //             .then(function(jsonStr) {
  //                 that.setState({orders: jsonStr});
  //             }).catch((err)=>{console.log(err);})
  //     }
  // }

  componentDidMount()
  {

    console.log(this.state.status,"confirm");
    const that = this;
    fetch("https://localhost:44390/manager/orders/" + this.state.status)
        .then(function(response) {
            return response.json();
        })
        .then(function(jsonStr) {
            that.setState({orders: jsonStr});
        }).catch((err)=>{console.log(err);})
  }

  render() {
    return(
        <div>
            <div className="tableWrapper">
            <table id="c_order">
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Date</th>
                        <th>Address</th>
                        <th>Status</th>
                        <th></th>
                        <th></th>
                    </tr>
                </thead>
            <tbody>
            {
                this.state.orders.map(item => {
                        return (
                            <tr key={item.orderId} >
                             <td>{i++}</td>
                             <td>{item.orderDate}</td>
                             <td>{item.address}</td>
                             <td>{item.status}</td>
                             <button onClick={() => this.seeDetails(item.orderId)}>Details</button> 
                             {this.state.showDetails?
                             <button onClick={() => this.handleConfirm(item.orderId)}>Confirm</button> :null}
                            </tr>
                              
                    )})
            }
            {this.state.details?this.state.details.map(it =>
                          {return(<div className="productDetails" key={it.product}>
                            <span className="td">{it.product}</span>
                            <span className="td">{it.quantity}</span>
                            <button onClick={() => this.handleDelete(it.product,it.orderId)}>Delete</button>                  
                            </div>)}
                            ): ''}
            </tbody>
            </table>
            </div>
        </div>       
    )}
  
}

export default NotConfirm;
