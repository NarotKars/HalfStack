import React from "react";

class Menu extends React.Component{
    constructor(props){
        super(props);
        this.state={
            buttons: [{id:1, clicked:true}, 
                      {id:2, clicked:false},
                      {id:3, clicked:false}]
        }
    }
    changeTheCategory = (id) =>
    {
        var changed=[{id:1, clicked:false}, 
            {id:2, clicked:false},
            {id:3, clicked:false}]
        changed[id-1].clicked=true;
        this.setState({
            buttons:[...changed]
        })
        this.props.handleStateChange(id);
    }
    render(){
        return(
            <div className="menu">
                <button className={this.state.buttons[0].clicked ? "menuButtonChanged" : "menuButton"} onClick={()=>this.changeTheCategory(this.state.buttons[0].id)}>Current Order List</button>
                <button className={this.state.buttons[1].clicked ? "menuButtonChanged" : "menuButton"} onClick={()=>this.changeTheCategory(this.state.buttons[1].id)}>Order History</button>
                <button className={this.state.buttons[2].clicked ? "menuButtonChanged" : "menuButton"} onClick={()=>this.changeTheCategory(this.state.buttons[2].id)}>Preferences/Feedback</button>
            </div>
        )
    }
}

export default Menu;