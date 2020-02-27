import React from 'react';
const axios = require('axios').default;
var i=0;
    
class OrdersList extends React.Component{
  constructor(props) {
    super(props);
    this.state={
        orders: [],
        productList1: [{barcode: 111, name: 'milk'},
                       {barcode: 222, name: 'bread'},
                       {barcode: 333, name: 'tomato'}],
        productList2: [{barcode: 444, name: 'sugar'},
                       {barcode: 555, name: 'apple'},
                       {barcode: 666, name: 'pencil'}],
    }
  }

  componentDidMount()
  {
        axios.get('http://localhost:44390/api/Orders/{0}',this.props.status)
        .then((response) => {
          this.setState({oders: response.data})
        });
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
                            <tr key={item.order_Id} onClick={() => this.seeDetails(item.order_Id)}>
                             <td>{i}</td>
                             <td>{item.order_Date}</td>
                             <td>{item.city}</td>
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
