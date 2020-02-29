import React from 'react';
const axios = require('axios').default;
var i=0;
    
class OrdersList extends React.Component{
  constructor(props) {
    super(props);
    this.state={
        orders: [],
    }
  }

  componentDidMount()
  {
    const that = this;
    fetch("https://localhost:5001/customer/orders/1/")
        .then(function(response) {
            return response.json();
        })
        .then(function(jsonStr) {
            that.setState({orders: jsonStr});
            console.log(that.state.showDetails)
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
                    </tr>
                </thead>
            <tbody>
            {
                this.state.orders.map(item => {
                    i++;
                        return (
                            <tr key={item.orderId} onClick={() => this.seeDetails(item.order_Id)}>
                             <td>{i}</td>
                             <td>{item.orderDate}</td>
                             <td>{item.address}</td>
                             <td>{item.status}</td>
                            </tr>
                    )})
            }
            </tbody>
            </table>
            </div>
        </div>       
    )}
  
}

export default OrdersList;
