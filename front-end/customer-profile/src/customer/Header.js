import React from 'react';
import customer from './customer.png';
class Header extends React.Component {
    constructor(props){
        super(props);
        this.editPersonalInformation=this.editPersonalInformation.bind(this);
        this.changePersonalInfo=this.changePersonalInfo.bind(this);
        this.state={personalInfo:[],
                    edit: false,
                    newName:'',
                    address:'',
        }
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
    changePersonalInfo = (e) => {
        this.setState({
        [e.target.name]: e.target.value
        });
    }
    editPersonalInformation = () => {   
        var city,street='',number,index,words;         
        if(this.state.edit===false)
        {
            words = this.state.personalInfo.address.split(" ");
            while(words.indexOf("")!==-1)
            {
                index=words.indexOf("");
                words.splice(index,1);
            }
            city=words[0];
            number=words[words.length-1];
            for(var j=1;j<words.length-1;j++)
            {
                street+=words[j];
                street+=' ';
            }
            this.setState({edit: true, 
                           newName: this.state.personalInfo.name, 
                           address: city+' '+street+number});
        }
        else
        {
            words = this.state.address.split(" ");
            while(words.indexOf("")!==-1)
            {
                index=words.indexOf("");
                words.splice(index,1);
            }
            city=words[0];
            number=words[words.length-1];
            for(j=1;j<words.length-1;j++)
            {
                street+=words[j];
                street+=' ';
            }
            const newPersonalInfo={
                Id: 1,
                name: this.state.newName,
                Address: {"City":city, "Street": street, "Number": number}
            }
            fetch('https://localhost:5001/customer/updatee/1', {
            method: 'PUT',
            body: JSON.stringify(newPersonalInfo),
            headers: {
            "Content-type" : "application/json"
            }
        })
            var info=this.state.personalInfo;
            info.name=newPersonalInfo.name;
            info.address=words[0] +' '+ street+number;
       
            console.log(newPersonalInfo);
            this.setState({edit: false,
                            personalInfo: info});
        }
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
                        <li>Address:</li>
                        <li>Phone:</li>
                        <li>Email:</li>
                        <li><span>&nbsp;</span></li>
                    </div>
                    {this.state.edit ? <div className="list">
                        <li><input type="text" onChange={(e) => this.changePersonalInfo(e)} value={this.state.newName} name="newName" className="orderFeed"></input></li>
                        <li><input type="text" onChange={(e) => this.changePersonalInfo(e)} value={this.state.address} name="address" className="orderFeed"></input></li>
                        <li>{this.state.personalInfo.phone_Number}</li>
                        <li>{this.state.personalInfo.email}</li>
                        <button onClick={() => this.editPersonalInformation()} className="saveEditBtn">save</button>
                    </div> : <div className="list">
                        <li>{this.state.personalInfo.name}</li>
                        <li>{this.state.personalInfo.address}</li>
                        <li>{this.state.personalInfo.phone_Number}</li>
                        <li>{this.state.personalInfo.email}</li>
                        <button onClick={() => this.editPersonalInformation()} className="saveEditBtn">edit</button>
                    </div>}
                </div>
                
            </div>
        );
    }
}

export default Header;