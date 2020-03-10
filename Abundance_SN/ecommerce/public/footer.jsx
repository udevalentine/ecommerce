import React, { Component } from 'react';
// import logo from './logo.svg';
import './App.css';
 import { Button} from 'reactstrap';




class Footer extends Component {
   

    render() {

    

    return(
        <div className="container-fluid bg-primary text-white h-100 ">
        <div className="row">
<div className="col-md-4 p-4 bg-dark-opacity">
<h3>Ecommerce</h3>
<hr/>
<h6>BRIEF US</h6>
<p className="fs9em">
    Lorem Ipsum is simply dummy text of the printing and typesetting industry. 
    Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, 
    when an unknown printer took a galley of type and scrambled it to make a type specimen book. 
    It has survived not only five centuries, but also the leap into electronic typesetting, 
    remaining essentially unchanged.
    </p>
</div>
                <div className="col-md-4 p-4 quickLinks">
                    <h3>Quick Links</h3>
                    <hr />
                    
                    <a href="./home" className="nav-item">Home</a>
            
                    <a href="./home" className="nav-item">Shop</a>
                    <a href="./home" className="nav-item">Categories</a>
                    <a href="./home" className="nav-item">About</a>
                    <a href="./home" className="nav-item">Other Links</a>

                    </div>
                <div className="col-md-4 p-4">
                    <h3>Contact US</h3>
                    <hr />
                    <address className="mt-2">
                        Head Office : 34, five centuries street,<br/>
                        Illupeju, Lagos
                    </address>
                    <div className="p-1">
                    Email:  </div>
                    <div className="p-1">Phone:</div>
                    <Button className="btn btn-warning btn-block mt-3"> Message Us</Button>
                </div>
        </div>
        <div className="clearfix"></div>
            <div className="container-fluid border-gray p-3">
<div className="row">
<div className="col-md-6 col-sm-12 p-2">Ecommerce &copy; 2018</div>
                    <div className="col-md-6 col-sm-12 text-right p-2">POWERED By &copy; Lloydant</div>
</div>
            </div>
            </div>
            
            
            

    );

}
}


       
export default Footer;