import React from 'react';
import '../App.css'

class Status extends React.Component{
  constructor(props){
      super(props);
      this.state={
          buttons: [{id:1, clicked:true,status:'new'}, 
                    {id:2, clicked:false,status:'confirmed'},
                    {id:3, clicked:false,status:'not confirmed'},
                    {id:4, clicked:false,status:'rejected'},
                    {id:5, clicked:false,status:'completed'}]
      }
  }
  changeTheCategory = (id) =>
  {
      var changed=[{id:1, clicked:false}, 
          {id:2, clicked:false},
          {id:3, clicked:false},
          {id:4, clicked:false},
          {id:5, clicked:false}]
      changed[id-1].clicked=true;
      this.setState({
          buttons:[...changed]
      })
      this.props.handleStateChange(id);
  }
  render(){
      return(
          <div className="menu">
              <button className={this.state.buttons[0].clicked ? "menuButtonChanged" : "menuButton"} onClick={()=>this.changeTheCategory(this.state.buttons[0].id)}>New</button>
              <button className={this.state.buttons[1].clicked ? "menuButtonChanged" : "menuButton"} onClick={()=>this.changeTheCategory(this.state.buttons[1].id)}>Confirmed</button>
               <button className={this.state.buttons[2].clicked ? "menuButtonChanged" : "menuButton"} onClick={()=>this.changeTheCategory(this.state.buttons[2].id)}>Not Confirmed</button>
              <button className={this.state.buttons[3].clicked ? "menuButtonChanged" : "menuButton"} onClick={()=>this.changeTheCategory(this.state.buttons[3].id)}>Later</button>
              {/* <button className={this.state.buttons[4].clicked ? "menuButtonChanged" : "menuButton"} onClick={()=>this.changeTheCategory(this.state.buttons[4].id)}>Completed</button> */} 
          </div>
      )
  }
}

export default Status;