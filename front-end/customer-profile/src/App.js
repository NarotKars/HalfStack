import React from 'react';
import Customer from './customer/App';
import Manager from './manager/ManagerPage';
import Delivery from './delivery-worker/App.js';

class App extends React.Component {
  render()
  {
    return (
      <div>
        {/*<Manager/>*/}
       <Manager/>
        {/*<Delivery/>*/}
      </div>
    );
  }
}

export default App;