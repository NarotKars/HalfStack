import React from 'react';
import '../App.css';
import Header from './Header';
import Menu from './Menu';
import YourOrders from './YourOrders';
import PersonalInfo from './PersonalInfo';
class App extends React.Component {
  constructor(props){
    super(props);
    this.handleStateChange = this.handleStateChange.bind(this);
    this.state={
      num: 2,
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
          
           {this.state.num===1 ? <PersonalInfo /> : <YourOrders />}    
      </div>
    );
  }
}

export default App;


