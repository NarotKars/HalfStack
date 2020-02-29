import React from "react";

class PersonalInfo extends React.Component{
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
            <div className="listWrapper">
                <div className="list">
                    <li>Name:</li>
                    <li>Email:</li>
                    <li>Address:</li>
                </div>
                <div className="list">
                    <li>{this.state.personalInfo.name}</li>
                    <li>{this.state.personalInfo.email}</li>
                    <li>{this.state.personalInfo.address}</li>
                </div>
            </div>
        );
    }
}

export default PersonalInfo;
