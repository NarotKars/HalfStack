import React from "react";
var id=2;
class ToBeAccepted extends React.Component{
    constructor(props){
        super(props);
        this.acceptOrder=this.acceptOrder.bind(this);
        this.state={
            orderCustomerIds: [],
            preferences:[],
            accepted:[],
            personalInfo:[],
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
        }
    }
    acceptOrder = (orderId, customerId) => {
        localStorage.setItem('busy',orderId);
        localStorage.setItem('customerId',customerId);
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
    }
    finishOrder=()=> {
        console.log(localStorage.getItem('busy'));
        localStorage.setItem('busy',0);
        this.setState({isOneOfTheOrdersAccepted: false});
    }
    render(){
        return(
            localStorage.getItem('busy')!=='0' ? <div className="orderProductsWrapper">
                <span className="CPIH">Personal Information</span>
                <div className="customerInfoWrapper">
                    <div className="col">
                        {this.state.personalInfo.name!=='unknown' ? <li>Name:</li> : ''}
                        <li>Address:</li>
                        <li>Phone:</li>
                    </div>
                    <div className="col">
                    {this.state.personalInfo.name!=='unknown' ?  <li>{this.state.personalInfo.name}</li> : ''}
                        <li>{this.state.personalInfo.address}</li>
                        <li>{this.state.personalInfo.phone_Number}</li>
                    </div>
                </div>
                <span className="CPIH">Preferences</span>
                <div className="customerInfoWrapper">
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
            /*this.state.orderCustomerIds.map(item => { 
                return (
                <div key={item.orderId} className="newOrder" onClick={()=>this.acceptOrder(item.orderId,item.customerId)}><span className="acceptMe">Accept me!!!</span></div>
            )})*/
        );
    }
}

export default ToBeAccepted;