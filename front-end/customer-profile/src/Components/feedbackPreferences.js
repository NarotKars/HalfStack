import React from 'react';
class Feedback extends React.Component{
    constructor(props){
        super(props);
        this.SetFeedback=this.SetProperty.bind(this);
        this.SetPreference=this.SetPreference.bind(this);
        this.state={
            feedback: '',
            preference: ''
        }
    }
    SetProperty(e)
    {
        this.setState({
        [e.target.name]: e.target.value
        })
    }
    SetPreference(e)
    {
        this.setState({
            [e.target.name]: e.target.value
        })
    }
    render() {
        return (
            <div>
                <div className="pref">
                    <span>Your comfort is our top priority:)))))</span>
                </div>
                <div className="pref">
                    <input onChange={this.SetPreference} name="preference" value={this.state.preference}></input>
                    <button className="btn1">Add a preference</button>
                </div>
                <div className="feed">
                    <textarea onChange={this.SetFeedback} name="feedback" value={this.state.feedback} cols="80" rows="7"></textarea>
                    <button className="btn2">OK</button>
                </div>
                {console.log(this.state.feedback)}
                {console.log(this.state.preference)}
            </div>
        )
    }
}

export default Feedback;