import React from 'react';

import Customer from './customer/App';
// import Manager from './manager/ManagerPage';
// import Delivery from './delivery/App.js';

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


