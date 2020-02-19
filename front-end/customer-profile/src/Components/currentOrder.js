import React from "react";
var i=0;
class CurrentOrder extends React.Component {
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
    i=0;
    const that = this;
    fetch("https://localhost:5001/customer/1/")
        .then(function(response) {
            return response.json();
        })
        .then(function(jsonStr) {
            var r=jsonStr.filter(item => item.status==='in time')
            that.setState({ orders: r });
            console.log(that.state.orders)
        }).catch((err)=>{console.log(err);})
  }

  seeDetails = () => {
      
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

export default CurrentOrder;