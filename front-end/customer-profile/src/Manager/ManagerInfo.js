import React from 'react';
import manager from './manager.png'; 
import '../App.css';

class ManagerInfo extends React.Component {
  constructor(props){
    super(props);
    this.state={
      PersonalInfo:[]
    }
  }
  componentDidMount() {
    const that = this;
        fetch("https://localhost:5001/manager/getbyid/7")
        .then(function(response) {
            return response.json();
        })
        .then(function(jsonStr) {
            that.setState({ PersonalInfo: jsonStr });
            console.log(that.state.PersonalInfo)
        }).catch((err)=>{console.log(err);})
 }

  render() {
      return(
        <div id="header">
            <div>
                <br/>
                <img src={manager} alt="manager" />
            </div>
             <div div className="listWrapper"> 
                <div className="list">
                    <li>Name:{this.state.PersonalInfo.name}</li>
                    <li>Email:{this.state.PersonalInfo.email}</li>
                </div>  
            </div>
        </div>
      );
  }
}
export default ManagerInfo;