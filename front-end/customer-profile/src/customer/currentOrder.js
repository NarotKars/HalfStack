import React from "react";
var o=0;
class CurrentOrder extends React.Component {
    constructor(props) {
        super(props);
        this.seeDetails=this.seeDetails.bind(this);
        this.OrderFeedback=this.OrderFeedback.bind(this);
        this.saveFeedback=this.saveFeedback.bind(this);
        this.state={
            orders: [],
            products: [],
            showDetails: [],
            orderDetails: [],
            orderFeedback: [] 
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
            var r=jsonStr.filter(item => item.status==='confirmed')
            for(var j=0; j<r.length;j++)
                showdetails.push(false);
            that.setState({orders: r,
                showDetails: showdetails});
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

  OrderFeedback(e,id)
    {
        var feed=[...this.state.orderFeedback];
        const index=this.state.orders.map(item => item.orderId).indexOf(id);
        console.log(index);
        feed[index]=e.target.value;
        this.setState({orderFeedback: feed});
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
                    orderFeedback:'',
                   orderDetails: orderdetails})
    }
    o=0;
  } 
  saveFeedback=(id) => {
    const index=this.state.orders.map(item => item.orderId).indexOf(id);
    const fb={
        Id: id,
        feedback: this.state.orderFeedback[index]
    }
    fetch('https://localhost:5001/api/orders/feedback', {
        method: 'PUT',
        body: JSON.stringify(fb),
        headers: {
            "Content-type" : "application/json"
        }
    })
    var f=[...this.state.orderFeedback];
    f[index]='';
    this.setState({orderFeed:'',
                  orderFeedback: f});
  }
    render() {
        return(
            <div className="tableWrapper">
                    <div className="fakeTableHeader">
                        <div className="fakeTableCol">#</div>
                        <div className="fakeTableCol">Date</div>
                        <div className="fakeTableCol">Address</div>
                        <div className="fakeTableCol">Status</div>
                        <div className="fakeTableCol">Feedback</div>
                        <div className="fakeTableCol">Details</div>
                    </div>
                {o=0,
                this.state.orders.map(item => {
                    o++;
                        return (
                            <div className="fakeTableWrapper" key={o}>
                            <div key={item.orderId} className="fakeTableRow">
                             <div className="fakeTableCol">{o}</div>
                             <div className="fakeTableCol">{item.orderDate}</div>
                             <div className="fakeTableCol">{item.address}</div>
                             <div className="fakeTableCol">{item.status}</div>
                             <div className="fakeTableCol">
                                <input type="text" onChange={(e) => this.OrderFeedback(e,item.orderId)} name="orderFeed" value={this.state.orderFeedback[o-1]} className="orderFeed"></input>
                                <button onClick={() => this.saveFeedback(item.orderId)} className="okBtn">OK</button>
                             </div>
                             {this.state.showDetails[o-1] ? 
                             <div className="fakeTableCol"><button onClick={() => this.seeDetails(item.orderId)} className="seeDetailsBtn">hide details</button></div>:
                             <div className="fakeTableCol"><button onClick={() => this.seeDetails(item.orderId)} className="seeDetailsBtn">see details</button></div>}
                            </div>
                        {this.state.showDetails[o-1] ? <div className="fakeDetailTableHeader">
                                                            <div className="fakeTableCol"></div>
                                                            <div className="fakeTableCol">Category Name</div>
                                                            <div className="fakeTableCol">Product Name</div>
                                                            <div className="fakeTableCol">Quantity</div>
                                                            <div className="fakeTableCol">Price</div>
                                                            <div className="fakeTableCol"></div>
                                                        </div> : ''}
                        {this.state.showDetails[o-1] ? this.state.orderDetails.map(it =>{return(<div className="fakeDetailsTableRow" key={it.id}>
                                                                                                    <div className="fakeDetailsTableCol"></div>
                                                                                                    <div className="fakeDetailsTableCol">{it.categoryName}</div>
                                                                                                    <div className="fakeDetailsTableCol">{it.product}</div>
                                                                                                    <div className="fakeDetailsTableCol">{it.quantity}</div>
                                                                                                    <div className="fakeDetailsTableCol">{it.quantity*it.price}</div>
                                                                                                </div>)}) : ''}
                        {this.state.showDetails[o-1] ? <div className="fakeDetailsTableRow">
                                                        <div className="fakeDetailsTableCol"></div>
                                                        <div className="fakeDetailsTableCol"></div>
                                                        <div className="fakeDetailsTableCol"></div>
                                                        <div className="fakeDetailsTableCol"></div>
                                                        <div className="fakeDetailsTableCol">{'total: '}{this.state.orders[o-1].amount}</div>
                                                    </div> : ''}
                            </div>
                    )})
                }
            </div>
        )}
}

export default CurrentOrder;