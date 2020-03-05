import React from 'react';
import { Redirect,Route,Switch } from 'react-router-dom';
import './App.css';
import Customer from './Components/App';
import Manager from './Manager/ManagerPage';
import Delivery from './Delivery/App';

class App extends React.Component {
  render()
  {
    return (
      <div>
        <Customer/>
        {/* <Manager/>
        <Delivery/> */}
      </div>
    );
  }
}

export default App;


