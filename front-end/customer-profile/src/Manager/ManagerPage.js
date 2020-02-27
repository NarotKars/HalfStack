import React from 'react';
import Status from './Status';
import ManagerInfo from './ManagerInfo';
import OrdersList from './OdersList'
import '../App.css'

class Manager extends React.Component{
  constructor(props){
    super(props);
    this.state={
      num: 1
    }

    this.handleStateChange = this.handleStateChange.bind(this);
  }

  handleStateChange(id){
    this.setState({
      num: id
    })
  }
  
  render(){
    return (
      <div className='App'>
        <ManagerInfo/>
        <Status handleStateChange={this.handleStateChange} />
           <OrdersList status={this.state.num} />  
      </div>
    );
  }
}

export default Manager;