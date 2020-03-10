import React, { Component } from 'react';
// import { Card, CardImg, CardTitle, CardText, CardDeck,
//     CardSubtitle, CardBody, CardFooter, Progress
// } from 'reactstrap';
import { Button  } from 'reactstrap';
import './carousel.css';



class Featuredproduct extends Component {

    
   

    render() {

    

     
        return(
            <div className= "row  bg-lighti p-4" >
                <div className="col-md-12 text-center f2em"><span className="span300">Featured</span> <b>Package</b>
                    <div className="separator separator-primary"></div></div>
              
                <div className="col-md-7  p-4 h-200">
                    <div className="product-details">


                        <div className="col-md-12"><h3 class="f2em span300">Abatte Flower  <span className=" badge badge-success">Free Shipping</span></h3></div>
                        
<div className="col-md-12">
                        <div className="row text-warning">
                            <div className="col-md-2 pt-1 text-right"><h5 className=" text-right"><strike>Was #300,000</strike></h5></div>
                            <div className="col-md-6"><h3> Now only #30,000</h3> </div>
                        </div>
</div>

                        <p className="information">"Especially good for container gardening, the Angelonia will keep blooming
                 all summer even if old flowers are removed. Once tall enough to cut, bring them inside and you'll
                 notice a light scent that some say is reminiscent of apples. "</p>
                        <div className="clearfix"></div>


                     

                        <hr />
                        <Button className="btn btn-neutral">
                            <span className="price">49 $</span>
                            <span className="shopping-cart"><i className="fa fa-shopping-cart" aria-hidden="true"></i></span>
                            <span className="buy">Buy Now</span>
                        </Button>

                    </div>
                </div>
                <div className="col-md-5 p-4 h-200 ">
                    <div className="product-image">

                        <img src="https://sc01.alicdn.com/kf/HTB1Cic9HFXXXXbZXpXXq6xXFXXX3/200006212/HTB1Cic9HFXXXXbZXpXXq6xXFXXX3.jpg" alt="Omar Dsoky" height="400" />


                        <div className="info">
                            <h3>The Description</h3>
                            <ul>
                                <li><strong>Sun Needs: </strong>Full Sun</li>
                                <li><strong>Soil Needs: </strong>Damp</li>
                                <li><strong>Zones: </strong>9 - 11</li>
                                <li><strong>Height: </strong>2 - 3 feet</li>
                                <li><strong>Blooms in: </strong>Mid‑Summer - Mid‑Fall</li>
                                <li><strong>Features: </strong>Tolerates heat</li>
                            </ul>
                        </div>
                    </div>
                </div>
</div>
        );
}
}
export default Featuredproduct;