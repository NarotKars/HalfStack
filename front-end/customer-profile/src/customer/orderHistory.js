import React from "react";
var m=0;
class OrderHistory extends React.Component {
    constructor(props) {
        super(props);
        this.OrderFeedback=this.OrderFeedback.bind(this);
        this.seeDetails=this.seeDetails.bind(this);
        this.saveFeedback=this.saveFeedback.bind(this);
        this.reorder=this.reorder.bind(this);
        this.changeAddress=this.changeAddress.bind(this);
        this.cancelChangingAddress=this.cancelChangingAddress.bind(this);
        this.state={
            orders: [],
            products: [],
            showDetails: [],
            orderDetails: [],
            orderFeedback:[],
            changeAddress:[],
            newAddress:[]
        }
    }

  componentDidMount()
  {
    m=0;
    var showdetails=[];
    var changeaddress=[];
    var newaddress=[];
    const that = this;
    fetch("https://localhost:5001/customer/orders/1/")
        .then(function(response) {
            return response.json();
        })
        .then(function(jsonStr) {
            var r=jsonStr.filter(item => item.status!=='in time' && item.status!=='new')
            for(var j=0; j<r.length;j++)
            {
                showdetails.push(false);
                changeaddress.push(false);
                newaddress.push('');
            }
            that.setState({orders: r,
                showDetails: showdetails,
                newAddress: newaddress,
                changeAddress:changeaddress});
            console.log(that.state.orders)
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
newAddress(e, id)
{
    var na=[...this.state.newAddress];
    const index=this.state.orders.map(item => item.orderId).indexOf(id);
    na[index]=e.target.value;
    this.setState({newAddress: na});
    console.log(this.state.newAddress);
}
seeDetails = (id) => {
    console.log(id);
    const j=this.state.orders.map(item => item.orderId).indexOf(id);
    var showPrevDetails=[...this.state.showDetails];

    if(showPrevDetails[j]===true)
    {
        const a=this.state.showDetails.map(item => item).indexOf(true);
        var changeaddress=[...this.state.changeAddress];
        changeaddress[a]=false;

        showPrevDetails[j]=false;
        this.setState({showDetails: showPrevDetails,
                       changeAddress: changeaddress});
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
    m=0;
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
  reorder = (id) => {
    var date=new Date().getDate();
    var month = new Date().getMonth() + 1;
    var year = new Date().getFullYear();
    var hours = new Date().getHours();
    var min = new Date().getMinutes();
    var sec = new Date().getSeconds();
    console.log(year+'-'+month+'-'+date+'T'+hours+':'+min+':'+sec);
    var d=year+'-';
    if(month<=9) d+='0';
    d+=month+'-';
    if(date<=9) d+='0';
    d+=date+'T';
    if(hours<=9) d+='0';
    d+=hours+':';
    if(min<=9) d+='0';
    d+=min+':';
    if(sec<=9) d+='0';
    d+=sec;
    const index=this.state.orders.map(item => item.orderId).indexOf(id);
    var city,street='',number,neworderaddress='';
    if(this.state.newAddress[index]!=='')
    {
        var words = this.state.newAddress[index].split(" ");
            var ind;
            while(words.indexOf("")!==-1)
            {
                ind=words.indexOf("");
                words.splice(ind,1);
            }
            city=words[0];
            number=words[words.length-1];
            for(var j=1;j<words.length-1;j++)
            {
                street+=words[j];
                street+=' ';
            }
            neworderaddress+=city + ' ' + street + number;
    }
    else
    {
        var words = this.state.orders[index].address.split(" ");
            var ind;
            while(words.indexOf("")!==-1)
            {
                ind=words.indexOf("");
                words.splice(ind,1);
            }
            city=words[0];
            number=words[words.length-1];
            for(var j=1;j<words.length-1;j++)
            {
                street+=words[j];
                street+=' ';
            }
            neworderaddress+=city + ' ' + street + number;
    }
    const newOrder ={
        orderId: id,
	    customerId: 1,
        orderDate: d,
        amount: this.state.orders[index].amount,
        Address: {"City":city, "Street": street, "Number": number}
      }
      fetch('https://localhost:5001/customer/reorder/1', { 
        method: 'POST',
        body: JSON.stringify(newOrder), 
        headers:{ 'Content-Type': 'application/json' } })
        .catch(error => console.error('Error:', error))
  }

  changeAddress = (id) => {
    const index=this.state.orders.map(item => item.orderId).indexOf(id);
    var changeaddress=[...this.state.changeAddress];
    changeaddress[index]=!changeaddress[index];
    this.setState({changeAddress:changeaddress});
  }
  cancelChangingAddress = (id) => {
    const index=this.state.orders.map(item => item.orderId).indexOf(id);
    var newaddress=[...this.state.newAddress];
    newaddress[index]='';
    this.setState({newAddress: newaddress});
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
            {m=0,
            this.state.orders.map(item => {
                m++;
                    return (
                        <div className="fakeTableWrapper">
                        <div key={item.orderId} className="fakeTableRow">
                         <div className="fakeTableCol">{m}</div>
                         <div className="fakeTableCol">{item.orderDate}</div>
                         <div className="fakeTableCol">{item.address}</div>
                         <div className="fakeTableCol">{item.status}</div>
                         <div className="fakeTableCol">
                            <input type="text" onChange={(e) => this.OrderFeedback(e,item.orderId)} name="orderFeed" value={this.state.orderFeedback[m-1]} className="orderFeed"></input>
                            <button onClick={() => this.saveFeedback(item.orderId)} className="okBtn">OK</button>
                         </div>
                         {this.state.showDetails[m-1] ?
                         <div className="fakeTableCol"><button onClick={() => this.seeDetails(item.orderId)} className="seeDetailsBtn">hide details</button></div>:
                         <div className="fakeTableCol"><button onClick={() => this.seeDetails(item.orderId)} className="seeDetailsBtn">see details</button></div>}
                        </div>
                    {this.state.showDetails[m-1] ? <div className="fakeDetailTableHeader">
                                                        <div className="fakeTableCol"></div>
                                                        <div className="fakeTableCol">Category Name</div>
                                                        <div className="fakeTableCol">Product Name</div>
                                                        <div className="fakeTableCol">Quantity</div>
                                                        <div className="fakeTableCol">Price</div>
                                                        <div className="fakeTableCol"></div>
                                                    </div> : ''}
                    {this.state.showDetails[m-1] ? this.state.orderDetails.map(it =>{return(<div className="fakeDetailsTableRow" key={it.id}>
                                                                                                <div className="fakeDetailsTableCol"></div>
                                                                                                <div className="fakeDetailsTableCol">{it.categoryName}</div>
                                                                                                <div className="fakeDetailsTableCol">{it.product}</div>
                                                                                                <div className="fakeDetailsTableCol">{it.quantity}</div>
                                                                                                <div className="fakeDetailsTableCol">{it.quantity*it.price}</div>
                                                                                            </div>)}) : ''}
                    {this.state.showDetails[m-1] ? <div className="fakeDetailsTableRow">
                                                        <div className="fakeDetailsTableCol"></div>
                                                        <div className="fakeDetailsTableCol"></div>
                                                        <div className="fakeDetailsTableCol"></div>
                                                        <div className="fakeDetailsTableCol"></div>
                                                        <div className="fakeDetailsTableCol">{'total: '}{this.state.orders[m-1].amount}</div>
                                                        <div className="fakeDetailsTableCol"><button onClick={() => this.reorder(item.orderId)} className="seeDetailsBtn">Reorder</button>
                                                                                             <button onClick={() => this.changeAddress(item.orderId)} className="seeDetailsBtn">Change Address</button></div>
                                                        </div> : ''}
                    {this.state.showDetails[m-1] && this.state.changeAddress[m-1] ? <div className="fakeDetailsTableRow">
                                                        <div className="fakeDetailsTableCol"></div>
                                                        <div className="fakeDetailsTableCol"></div>
                                                        <div className="fakeDetailsTableCol"></div>
                                                        <div className="fakeDetailsTableCol"></div>
                                                        <div className="reorderWrapper">{"New Address: "}
                                                                                        <input type="text" onChange={(e) => this.newAddress(e,item.orderId)} name="newAddress" value={this.state.newAddress[m-1]}></input>
                                                                                        <br/><button onClick={() => this.changeAddress(item.orderId)} className="seeDetailsBtn">Ok</button>
                                                                                        <button onClick={() => this.changeAddress(item.orderId)} className="seeDetailsBtn">Cancel</button>
                                                                                        </div>
                                                        <br/><br/><br/><br/><br/>
                                                    </div> : ''}
                        </div>
                )})
            }
        </div>
    )}
}

export default OrderHistory;