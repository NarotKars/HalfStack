import React from 'react';
import customer from './customer.png';
class Header extends React.Component {
    constructor(props){
        super(props);
        this.state={personalInfo:[]}
    }
    componentDidMount()
    {
        const that = this;
        fetch("https://localhost:5001/customer/getbyid/1")
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
                    <img src={customer} alt="customer"/>
                </div>
                <div className="listWrapper">
                    <div className="list">
                        <li>Name:</li>
                        <li>Phone:</li>
                        <li>Email:</li>
                        <li>Address:</li>
                    </div>
                    <div className="list">
                        <li>{this.state.personalInfo.name}</li>
                        <li>{this.state.personalInfo.phone_Number}</li>
                        <li>{this.state.personalInfo.email}</li>
                        <li>{this.state.personalInfo.address}</li>
                    </div>
                </div>
            </div>
        );
    }
}

export default Header;