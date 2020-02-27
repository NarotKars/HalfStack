import React from 'react';
import manager from './manager.png'; 
import '../App.css';
const axios = require('axios').default;

class ManagerInfo extends React.Component {
  constructor(props){
    super(props);
    this.state={
      Name:'',
      Email:'',
      Address:'',
    }
  }

  componentDidMount() {
    axios.get('http://localhost:44390/getbyid/7',)
     .then((response) => {
       this.setState({
        Name:response.data.Name,
        Email:response.data.Email,
        Address:response.data.Address

    })
     });
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
                    <li>Name:{this.state.Name}</li>
                    <li>Email:{this.state.Email}</li>
                    <li>Address:{this.state.Address}</li>
                </div>  
            </div>
        </div>
      );
  }
}
export default ManagerInfo;