import React from 'react';
import deliveryPerson from './delivery-person.png';
class Header extends React.Component {
    constructor(props){
        super(props);
        this.state={personalInfo:[]}
    }
    componentDidMount()
    {
        const that = this;
        fetch("https://localhost:5001/api/DeliveryPerson/7")
        .then(function(response) {
            return response.json();
        })
        .then(function(jsonStr) {
            that.setState({ personalInfo: jsonStr });
            console.log(that.state.personalInfo)
        }).catch((err)=>{console.log(err);})
    }
    render() {
        return(
            <div id="header">
                <div>
                    <br/>
                    <img src={deliveryPerson} alt="deliveryPerson"/>
                </div>
                <div className="name">
                    <li>{this.state.personalInfo.name}</li>
                </div>
            </div>
        );
    }
}

export default Header;