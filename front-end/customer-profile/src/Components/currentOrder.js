import React from "react";
var o=0;
class CurrentOrder extends React.Component {
    constructor(props) {
        super(props);
        this.seeDetails=this.seeDetails.bind(this);
        this.state={
            orders: [],
            products: [],
            showDetails: [],
            orderDetails: []
        }
    }

  componentDidMount()
  {
      var showdetails=[];
      o=0;
    const that = this;
    fetch("https://localhost:5001/customer/orders/1/")
        .then(function(response) {
            return response.json();
        })
        .then(function(jsonStr) {
            var r=jsonStr.filter(item => item.status==='in time')
            for(var j=0; j<r.length;j++)
            {
                showdetails.push(false);
            }
            that.setState({orders: r,
                showDetails: showdetails});
            console.log(that.state.showDetails)
        }).catch((err)=>{console.log(err);})

        fetch("https://localhost:5001/customer/orders/details/1")
        .then(function(response) {
            return response.json();
        })
        .then(function(jsonStr) {
        that.setState({products: jsonStr})
        console.log(that.state.products);
        }).catch((err)=>{console.log(err);})
  }

  seeDetails = (id) => {
    const j=this.state.orders.map(item => item.orderId).indexOf(id);
    var showPrevDetails=[...this.state.showDetails];
    if(showPrevDetails[j]===true)
    {
        showPrevDetails[j]=false;
        this.setState({showDetails: showPrevDetails});
    }
    else
    {
    var showdetails=[];
    for(var k=0;k<this.state.showDetails.length;k++)
    {
        showdetails.push(false);
    }
    showdetails[j]= true;
    
    const orderdetails =this.state.products.filter(item => item.orderId===id);
    this.setState({showDetails: [...showdetails],
                   orderDetails: orderdetails})
    }
    o=0;
  } 
    render() {
        return(
            <div>
            <div className="tableWrapper">
                    <div className="fakeTableHeader">
                        <span className="td">#</span>
                        <span className="td">Date</span>
                        <span className="td">Address</span>
                        <span className="td">Status</span>
                    </div>
                {
                this.state.orders.map(item => {
                    o++;
                        return (
                            <div key={item.orderId} onClick={() => this.seeDetails(item.orderId)} className="fakeTable">
                             <span className="td">{o}</span>
                             <span className="td">{item.orderDate}</span>
                             <span className="td">{item.address}</span>
                             <span className="td">{item.status}</span>
                        {this.state.showDetails[o-1] ? this.state.orderDetails.map(it =>{return(<div className="productDetails" key={it.id}><span className="td">{it.product}</span><span className="td">{it.quantity}</span></div>)}) : ''}
                            </div>
                    )})
                }
            </div>
        </div>
        )}
}

export default CurrentOrder;