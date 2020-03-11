import React from "react";

class Menu extends React.Component{
    constructor(props){
        super(props);
        this.state={
            buttons: [{id:1, clicked:false}, 
                      {id:2, clicked:true},
                      {id:3, clicked:false}]
        }
    }
    changeTheCategory = (id) =>
    {
        var changed=[{id:1, clicked:false}, 
            {id:2, clicked:false},
            {id:3, clicked:false}]
        changed[id-1].clicked=true;
        this.setState({buttons:[...changed]});
        this.props.handleStateChange(id);
    }
    render(){
        return(
            <div className="menu">
                <button className={this.state.buttons[0].clicked ? "menuButtonChanged" : "menuButton"} onClick={()=>this.changeTheCategory(this.state.buttons[0].id)}>Personal Infromation</button>
                <button className={this.state.buttons[1].clicked ? "menuButtonChanged" : "menuButton"} onClick={()=>this.changeTheCategory(this.state.buttons[1].id)}>Your Orders</button>
            </div>
        )
    }
}

export default Menu;