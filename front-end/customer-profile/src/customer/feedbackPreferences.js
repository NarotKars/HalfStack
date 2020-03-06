import React from 'react';
import edit from './edit.png';
import del from './del.png';
import save from './save.png';
var i=1;
class Feedback extends React.Component{
    constructor(props){
        super(props);
        this.SetFeedback=this.SetProperty.bind(this);
        this.SetPreference=this.SetPreference.bind(this);
        this.addPref=this.addPref.bind(this);
        this.updatePreference=this.updatePreference.bind(this);
        this.editPref=this.editPref.bind(this);
        this.state={
            feedback: '',
            preference: '',
            editPreference: '',
            delete: false,
            preferences: [],
            update: []
        }
    }
    componentDidMount() {
        var toBeUpdated=[];
        const that = this;
    fetch("https://localhost:5001/customer/preferences/1/")
        .then(function(response) {
            return response.json();
        })
        .then(function(jsonStr) {
            that.setState({ preferences: jsonStr });
            for(var j=0; j<jsonStr.length;j++)
            {
                toBeUpdated.push(false);
            }
            that.setState({ update: toBeUpdated});
            console.log(that.state.preferences)
        }).catch((err)=>{console.log(err);})
    }
   componentDidUpdate(prevProps, prevState)
    {
        if(prevState.delete!==this.state.delete)
        {
            console.log(prevState.delete, this.state.delete);
            const that=this;
            fetch("https://localhost:5001/customer/preferences/1/")
        .then(function(response) {
            return response.json();
        })
        .then(function(jsonStr) {
            that.setState({ preferences: jsonStr,
                            delete: false });
            console.log(that.state.preferences)
        }).catch((err)=>{console.log(err);})
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
    updatePreference(e)
    {
        this.setState({
            [e.target.name]: e.target.value
        })
    }
    addPref()
    {
        const newPref={
            customerId: 1,
            text: this.state.preference
        }
        var toBeUpdated=[...this.state.update];
        toBeUpdated.push(false);
        fetch('https://localhost:5001/customer/preferences/1/', {
            method: 'POST',
            body: JSON.stringify(newPref),
            headers: { 'Content-Type' : 'application/json'} })
            .catch(error => console.error('Error:', error))
            this.setState({
                preference: "",
                preferences:[...this.state.preferences,newPref],
                update: toBeUpdated
            })
    }
    editPref = (id) =>
    {
        var updated=[...this.state.preferences];
        var toBeUpdated=[...this.state.update];
        const index=this.state.preferences.map(item => item.preferenceId).indexOf(id);
        console.log(this.state.preferences[index].text)
        if(toBeUpdated[index]===false) 
        {
            console.log(this.state.preference);
            toBeUpdated[index]=true;
            this.setState({update: toBeUpdated,
                           editPreference: updated[index].text})
        }
        else{
            updated[index].text=this.state.editPreference;
            toBeUpdated[index]=false;
            const updatedPref={
                preferenceId: id,
                text: updated[index].text
            }
            fetch('https://localhost:5001/customer/preferences/1', {
                method: 'PUT',
                body: JSON.stringify(updatedPref),
                headers: {
                    "Content-type" : "application/json"
                }
            })
            this.setState({update:toBeUpdated,
                preferences: updated,
              editPreference: ''});
        }
        
    }
    deletePref = (id) =>
    {
        this.setState({delete: true});
        var url='https://localhost:5001/customer/preferences/'+id;
          fetch(url ,{
            method: 'DELETE'
          }).catch((err)=>{console.log(err);})
          var deleted=[...this.state.preferences];
          const index=this.state.preferences.map(item => item.preferenceId).indexOf(id);
          deleted.splice(index,1);
          this.setState({
          preferences:[...deleted],
    })
  }
  postFeedback = () => {
   const newFeedback={
       User_Id: 1,
       Text: this.state.feedback
   }
    fetch('https://localhost:5001/feedback/1', { 
        method: 'POST',
        body: JSON.stringify(newFeedback), 
        headers:{ 'Content-Type': 'application/json' } })
        .catch(error => console.error('Error:', error))
        this.setState({feedback:''});
  }
    render() {
        i=1;
        return (
            <div>
                <div id="CRM">
                    <span>Your comfort is our top priority:)))))</span>
                </div>
                <div className="prefWrapper">
                    <input onChange={this.SetPreference} name="preference" value={this.state.preference} className="prefInput"></input>
                    <button className="addPref" onClick={this.addPref}>Add</button>
                    {
                        this.state.preferences.map(item => {
                            if(this.state.update[i-1])
                            {
                            return (<div key={i} className="prefs">
                                        <span className="pref">{i++}</span>
                                        <input type="text" onChange={this.updatePreference} name="editPreference" value={this.state.editPreference} className="editInput"></input>
                                        <img src={save} alt="edit" className="eimg" onClick={()=>this.editPref(item.preferenceId)}/> 
                                        <img src={del} alt="edit" className="dimg" onClick={()=>this.deletePref(item.preferenceId)}/>
                                    </div>)
                            }
                            else
                            {
                                return (<div key={i} className="prefs">
                                        <span className="pref">{i++}</span>
                                        <span className="pref">{item.text}</span>
                                        <img src={edit} alt="edit" className="eimg" onClick={()=>this.editPref(item.preferenceId)}/> 
                                        <img src={del} alt="edit" className="dimg" onClick={()=>this.deletePref(item.preferenceId)}/>
                                    </div>)
                            }
                        })
                    }
                </div>
                <div className="feed">
                    <textarea onChange={this.SetFeedback} name="feedback" value={this.state.feedback} cols="80" rows="7"></textarea>
                    <button onClick={()=>this.postFeedback()} className="okBtn2">OK</button>
                </div>
            </div>
        )
    }
}

export default Feedback;