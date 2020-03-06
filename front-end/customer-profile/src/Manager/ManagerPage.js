import React from 'react';
import Status from './Status';
import ManagerInfo from './ManagerInfo';
import OrdersList from './OdersList';
import Confirm from './confirm';
import NotConfirm from './notconfirmed';
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
        
        {this.state.num===2?<Confirm status={'confirmed'}/> : this.state.num===3?<NotConfirm status={'not confirmed'}/>:
           <OrdersList status={ 'new'}/>}
      </div>
    );
  }
}

export default Manager;