import React from 'react';
import './App.css';
import Header from './Components/Header';
import Menu from './Components/menu';
import FeedbackPreferences from './Components/feedbackPreferences';
import CurrentOrder from './Components/currentOrder';
import OrderHistory from './Components/orderHistory';
class App extends React.Component {
  constructor(props){
    super(props);
    this.handleStateChange = this.handleStateChange.bind(this);
    this.state={
      num: 1
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
          {this.state.num===1 ? <CurrentOrder /> : 
           this.state.num===2 ? <OrderHistory />  : <FeedbackPreferences/>}

      </div>
    );
  }
}

export default App;

