import React from 'react';
import customer from './customer.png';
class Header extends React.Component {
    render() {
        return(
            <div id="header">
                <div>
                    <br/>
                    <img src={customer} alt="customer"/>
                </div>
                <div className="listWrapper">
                    <div className="list">
                        <li>Name:</li>
                        <li>Phone:</li>
                        <li>Email:</li>
                        <li>Address:</li>
                    </div>
                    <div className="list">
                        <li>Narot Kars Karapetian</li>
                        <li>+374 11111111</li>
                        <li>garabediangars@gmail.com</li>
                        <li>Aleq Manukyan 1/1</li>
                    </div>
                </div>
            </div>
        );
    }
}

export default Header;