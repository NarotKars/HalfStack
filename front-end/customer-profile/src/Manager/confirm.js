import React from 'react';
var i=0;
    
class OrdersList extends React.Component{
  constructor(props) {
    super(props);
    this.state={
        orders: [],
        status:this.props.status,
        details:[],
        showDetails:[]
    }

    this.seeDetails=this.seeDetails.bind()
  }

  seeDetails = (id) => {

    const that=this;
    const j=that.state.orders.map(item => item.orderId).indexOf(id);
    var showPrevDetails=[...that.state.showDetails];

    if(showPrevDetails[j]===true)
    {
        
        showPrevDetails[j]=false;
        that.setState({showDetails: showPrevDetails
                       });
    }
    else
    {
    var showdetails=[];
    for(var k=0;k<that.state.showDetails.length;k++)
    {
        showdetails.push(false);
    }
    showdetails[j]= true;
    

    fetch("https://localhost:5001/manager/order/details/"+id)
        .then(function(response) {
            return response.json();
        })
        .then(function(jsonStr) {
            that.setState({showDetails:showdetails,   
              details: jsonStr});
              console.log(jsonStr);
        }).catch((err)=>{console.log(err);})
    }

  }

  componentDidMount()
  {
    const that = this;
    var showdetails=[];
    fetch("https://localhost:5001/manager/orders/" + this.state.status)
        .then(function(response) {
            return response.json();
        })
        .then(function(jsonStr) {
          {
            var r=jsonStr
            for(var j=0; j<r.length;j++)
            {
                showdetails.push(false);
            }
            that.setState({orders: r,
                showDetails: showdetails
            });
            console.log(r);
        }}).catch((err)=>{console.log(err);})
  }

  render() {
    return(
       
        <div className="tableWrapper">
                 <div className="fakeTableHeader">
                      <div className="fakeTableColMan">#</div>
                      <div className="fakeTableColMan">Date</div>
                      <div className="fakeTableColMan">Address</div>
                      <div className="fakeTableColMan">Details</div>
                  </div>

              {i=0,
              this.state.orders.map(item => {
                  i++;
                        return (
                        <div className="fakeTableWrapper">
                          <div key={item.orderId} className="fakeTableRow">
                             <div className="fakeTableColMan">{i}</div>
                             <div className="fakeTableColMan">{item.orderDate}</div>
                             <div className="fakeTableColMan">{item.address}</div>
                             {this.state.showDetails[i-1] ?
                             <div className="fakeTableColMan"><button onClick={() => this.seeDetails(item.orderId)} className="seeDetailsBtn">hide details</button></div>:
                             <div className="fakeTableColMan"><button onClick={() => this.seeDetails(item.orderId)} className="seeDetailsBtn">see details</button></div>}
                          </div>


                        {this.state.showDetails[i-1] ? <div className="fakeDetailTableHeader">
                                                        <div className="fakeTableColMan"></div>
                                                        <div className="fakeTableColMan">Product Name</div>
                                                        <div className="fakeTableColMan">Quantity</div>
                                                        <div className="fakeTableColMan">Price</div>
                                                    </div> : ' '}
                        {this.state.showDetails[i-1]?this.state.details.map(it =>{return (<div className="fakeDetailsTableRow" key={it.barcode}>
                                                                                                <div className="fakeDetailsTableColMan"></div>
                                                                                                <div className="fakeDetailsTableColMan">{it.product}</div>
                                                                                                <div className="fakeDetailsTableColMan">{it.quantity}</div> 
                                                                                                <div className="fakeDetailsTableColMan">{it.price * it.quantity}</div>                                                                                              
                                                                                            </div>)})
                                                                                             : ''}

                       {this.state.showDetails[i-1] ? <div className="fakeDetailsTableRow">
                                                        <br/> 
                                                        <div className="fakeDetailsTableColMan"></div>
                                                        <div className="fakeDetailsTableColMan">Customer</div>
                                                        <div className="fakeDetailsTableColMan">Delvery date</div>
                                                        <div className="fakeDetailsTableColMan"></div>  
                                                        </div> : ''}
                    {this.state.showDetails[i-1] ? <div className="fakeDetailsTableRow">
                                                        <div className="fakeDetailsTableColMan"></div>
                                                        <div className="fakeDetailsTableColMan">{this.state.details[0].customer + this.state.details[0].phonenumber}</div>
                                                        <div className="fakeDetailsTableColMan">{this.state.details[0].deliverydate}</div>
                                                        <div className="fakeDetailsTableColMan"></div>
                                                        
                                                        <br/><br/><br/><br/><br/>
                                                    </div> : ''}
              


                           </div>  ) 
                          })}
                           </div>);}
                           
                           }                                                                        
                                                

export default OrdersList;
