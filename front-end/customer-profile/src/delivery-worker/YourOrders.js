import React from "react";
var id=2;
var a='';
class ToBeAccepted extends React.Component{
    constructor(props){
        super(props);
        this.acceptOrder=this.acceptOrder.bind(this);
        this.state={
            orderCustomerIds: [],
            preferences:[],
            accepted:[],
            personalInfo:[],
            orderPath:[],
            isOneOfTheOrdersAccepted:localStorage.getItem('busy'),
            currentCustomerId: localStorage.getItem('customerId')
        }
    }

    componentDidMount()
    {
        const that=this;
        fetch("https://localhost:5001/delivery/tobeaccepted/" + id)
        .then(function(response) {
            return response.json();
        })
        .then(function(jsonStr) {
        that.setState({orderCustomerIds: jsonStr});
        }).catch((err)=>{console.log(err);})
        if(localStorage.getItem('busy')!=='0')
        {
            console.log(localStorage.getItem('customerId'));
            fetch("https://localhost:5001/customer/preferences/" + localStorage.getItem('customerId'))
            .then(function(response) {
                return response.json();
            })
            .then(function(jsonStr) {
                console.log(jsonStr);
            that.setState({preferences: jsonStr})
            }).catch((err)=>{console.log(err);})

            
            fetch("https://localhost:5001/customer/getbyid/" + localStorage.getItem('customerId'))
            .then(function(response) {
                return response.json();
            })
            .then(function(jsonStr) {
                that.setState({ personalInfo: jsonStr });
                console.log(that.state.personalInfo)
            }).catch((err)=>{console.log(err);})


            fetch("https://localhost:5001/customer/orders/details/path/" + localStorage.getItem('busy'))
            .then(function(response) {
                return response.json();
            })
            .then(function(jsonStr) {
                that.setState({ orderPath: jsonStr });
            }).catch((err)=>{console.log(err);})
            
        }
    }
    acceptOrder = (orderId, customerId) => {
        const deliverStatusChanger={
            deliveryId: id,
            status: 'busy'
        }
        fetch('https://localhost:5001/deliveryworker/status/' + id, {
                method: 'PUT',
                body: JSON.stringify(deliverStatusChanger),
                headers: {
                    "Content-type" : "application/json"
                }
            })
        localStorage.setItem('busy',orderId);
        localStorage.setItem('customerId',customerId);

        const deliveryAccepted={
            orderid: orderId,
            deliveryId: 2
        }
        fetch('https://localhost:5001/deliveryworker/accept/12' +orderId, {
                method: 'PUT',
                body: JSON.stringify(deliveryAccepted),
                headers: {
                    "Content-type" : "application/json"
                }
            })


        this.setState({isOneOfTheOrdersAccepted: orderId});
        const that=this;
        fetch("https://localhost:5001/customer/preferences/" + customerId)
        .then(function(response) {
            return response.json();
        })
        .then(function(jsonStr) {
        that.setState({preferences: jsonStr})
        }).catch((err)=>{console.log(err);})


        fetch("https://localhost:5001/customer/getbyid/" + localStorage.getItem('customerId'))
            .then(function(response) {
                return response.json();
            })
            .then(function(jsonStr) {
                that.setState({ personalInfo: jsonStr });
                console.log(that.state.personalInfo)
            }).catch((err)=>{console.log(err);})


        fetch("https://localhost:5001/customer/orders/details/path/" + localStorage.getItem('busy'))
            .then(function(response) {
                return response.json();
            })
            .then(function(jsonStr) {
                that.setState({ orderPath: jsonStr });
            }).catch((err)=>{console.log(err);})
    }
    finishOrder=()=> {

        const deliverStatusChanger={
            deliveryId: id,
            status: 'free'
        }
        fetch('https://localhost:5001/deliveryworker/status/' + id, {
                method: 'PUT',
                body: JSON.stringify(deliverStatusChanger),
                headers: {
                    "Content-type" : "application/json"
                }
            })


        const orderStatusChanger={
            orderId: parseInt(localStorage.getItem('busy')),
            status: 'delivered'
        }
        console.log(parseInt(localStorage.getItem('busy')));
        fetch('https://localhost:5001/api/orderstatus/update/' + localStorage.getItem('busy'), {
                method: 'PUT',
                body: JSON.stringify(orderStatusChanger),
                headers: {
                    "Content-type" : "application/json"
                }
            })
        console.log(localStorage.getItem('busy'));
        localStorage.setItem('busy',0);
        this.setState({isOneOfTheOrdersAccepted: false});
        var deleted=[...this.state.orderCustomerIds];
        const index=this.state.orderCustomerIds.map(item => item.orderId).indexOf(parseInt(localStorage.getItem('busy')));
        deleted.splice(index,1);
        this.setState({
            orderCustomerIds:[...deleted],
        })
    }
    render(){
        return(
            localStorage.getItem('busy')!=='0' ? <div className="orderProductsWrapper">
                <span className="CPIH">Personal Information</span>
                <div className="customerInfoWrapperr">
                    <div className="col2">
                        {this.state.personalInfo.name!=='unknown' ? <li>Name:</li> : ''}
                        <li>Address:</li>
                        <li>Phone:</li>
                    </div>
                    <div className="col2">
                    {this.state.personalInfo.name!=='unknown' ?  <li>{this.state.personalInfo.name}</li> : ''}
                        <li>{this.state.personalInfo.address}</li>
                        <li>{this.state.personalInfo.phone_Number}</li>
                    </div>
                </div>

                <span className="CPIH">Products</span>
                <div>
                <div className="tableWrapperr">
                    <div className="fakeTableHeaderr">
                        <div className="fakeTableColl">Branch Name</div>
                        <div className="fakeTableColl">Product Name</div>
                        <div className="fakeTableColl">Quantity</div>
                    </div>
                    <div className="fakeTableWrapperr">
                    {this.state.orderPath.reverse().map(item => {
                        return (
                            <div className="fakeTableRoww">
                             <div className="fakeTableColl">{item.branchName}</div>
                             <div className="fakeTableColl">{item.barcode}</div>
                             <div className="fakeTableColl">{item.quantity}</div>
                             </div>
                             )})}
                </div></div></div>
                <span className="CPIH">Preferences</span>
                <div className="customerInfoWrapperr">
                    <div className="col1">
                {this.state.preferences.length!==0 ? this.state.preferences.map(item => {
                    return(<li className="preferenceList">{item.text}</li>)
                }): <span>No Preferences!!!</span>}
                    </div>
                    </div>
                <button onClick={()=>this.finishOrder()}>Finish</button>
            </div> : this.state.orderCustomerIds.map(item => { 
                return (
                <div key={item.orderId} className="newOrder" onClick={()=>this.acceptOrder(item.orderId,item.customerId)}><span className="acceptMe">Accept me!!!</span></div>)})
        );
    }
}

export default ToBeAccepted;