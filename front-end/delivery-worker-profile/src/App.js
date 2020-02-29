import React from 'react';
import './App.css';
import Header from './Components/Header';
import Menu from './Components/Menu';
import ToBeAccepted from './Components/ToBeAccepted';
import PersonalInfo from './Components/PersonalInfo';
import OrderHistory from './Components/OrderHistory';
class App extends React.Component {
  constructor(props){
    super(props);
    this.handleStateChange = this.handleStateChange.bind(this);
    this.state={
      num: 2
    }
  }
  handleStateChange(id){
    
    this.setState({
      num: id
    })
  }
  render()
  {
    return (
      <div className="App">
          <Header/>
          <Menu handleStateChange={this.handleStateChange} />
          
           {this.state.num===1 ? <PersonalInfo /> : 
           this.state.num===2 ? <ToBeAccepted />  : <OrderHistory/>}    
      </div>
    );
  }
}

export default App;


