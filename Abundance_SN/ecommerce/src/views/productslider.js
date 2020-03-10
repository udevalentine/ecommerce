import React, { Component } from 'react';
// import { Card, CardImg, CardTitle, CardText, CardDeck,
//     CardSubtitle, div, CardFooter, Progress
// } from 'reactstrap';
import { Form, FormGroup, Input, Button,  Card, CardImg, 
    CardTitle, CardText, CardFooter, CardBlock, CardSubtitle, Progress  } from 'reactstrap';


import Slider from 'react-slick/lib/slider';
import './carousel.css';

function NextArrow(props) {
  const { className, style, onClick } = props
  return (
    <div
      className={className}
      style={{ ...style, display: 'block' }}
      onClick={onClick}
    ></div>
  );
}

function PrevArrow(props) {
  const { className, style, onClick } = props
  return (
    <div
      className={className}
      style={{ ...style, display: 'block'}}
      onClick={onClick}
    ></div>
  );
}


class ProductSlider extends Component {
 


render() {
  
    const settings = {
      arrows: true,
      dots: false,
      nextArrow: <NextArrow />,
      prevArrow: <PrevArrow />,
      infinite: true,
      speed: 500,
      slidesToShow: 3,
      slidesToScroll: 3

      
 
 
};
     
        return(
<div className="row">
            <div className="col-md-3 col-sm-12 bgUrl6 yoi p-0">
              <div className="col-md-12 mainProducts1 p-5">
              <div className="col-md-12 pl-0 f1em ">
FOOT <span className="NamelgText">WEARS</span>
            </div>
              <Form>
                <FormGroup>
                  <Input type="select" name="select" id="exampleSelect">
                    <option>Men</option>
                    <option>Women</option>
                    <option>Children</option>
                  </Input>
                </FormGroup>
                <FormGroup>
                  <Input type="password" name="password" id="examplePassword" placeholder="Sizes" />
                </FormGroup>
                <FormGroup>
                  <Input type="select" name="select" id="exampleSelect">
                    <option>Type</option>
                    <option>brown</option>
                    <option>green</option>
                    <option>4</option>
                    <option>5</option>
                  </Input>
                </FormGroup>

                <FormGroup className="mb-3">
                  <Input type="select" name="select" id="exampleSelect">
                    <option>Brand</option>
                    <option>Aba Shoes</option>
                    <option>Alaba Shoes</option>
                    <option>Kogi shoes</option>
                    <option>5</option>
                  </Input>
                </FormGroup>
                <Button className="btn btn-info btn-block mt-5">Submit</Button>
                </Form>

                {/*Form for sorting ends here*/}
                </div>
            </div>
            <div className="col-md-9 col-sm-12 mainProducts2">
         
              
  <div>
  
    <Slider {...settings}>
 
                  <div className="col-md-4 col-sm-12 p-3 pt-5">  
                    <Card className="col-sm-12 p-0 product">
                    <CardImg top width="100%" src="https://placeholdit.imgix.net/~text?txtsize=33&txt=256%C3%97180&w=256&h=180" alt="Card image cap" />
                      <CardBlock>
                        <CardTitle>rough Shoe</CardTitle>
                        <CardSubtitle>#35,000.00</CardSubtitle>
                        <CardText>This card has supporting text below as a natural lead-in to additional content.</CardText>
                      </CardBlock>
                      <div className="price-context">
                        
                          <Button className="btn btn-warning">Add to wishlist</Button>
                          <Button className="btn btn-warning">Buy</Button>
                          <Button className="btn btn-warning">Compare</Button>
                        
                      </div>
                    <CardFooter>
                      <div className="row">
                        <div className="col-md-5">200 Left</div>
                        <div className="col-md-7"><Progress value="25" /></div>
                      </div>
                    </CardFooter>
                      
                  </Card>

                   
                  </div>
      

                  <div className="col-md-4 col-sm-12 p-3 pt-5">
                    <Card className="col-sm-12 p-0 product">
                      <CardImg top width="100%" src="https://placeholdit.imgix.net/~text?txtsize=33&txt=256%C3%97180&w=256&h=180" alt="Card image cap" />
                      <CardBlock>
                        <CardTitle>dough Shoe</CardTitle>
                        <CardSubtitle>#35,000.00</CardSubtitle>
                        <CardText>This card has supporting text below as a natural lead-in to additional content.</CardText>
                      </CardBlock>
                      <div className="price-context">

                        <Button className="btn btn-warning">Add to wishlist</Button>
                        <Button className="btn btn-warning">Buy</Button>
                        <Button className="btn btn-warning">Compare</Button>

                      </div>
                      <CardFooter>
                        <div className="row">
                          <div className="col-md-5">200 Left</div>
                          <div className="col-md-7"><Progress value="25" /></div>
                        </div>
                      </CardFooter>

                    </Card>


                  </div>

                  <div className="col-md-4 col-sm-12 p-3 pt-5">
                    <Card className="col-sm-12 p-0 product">
                      <CardImg top width="100%" src="https://placeholdit.imgix.net/~text?txtsize=33&txt=256%C3%97180&w=256&h=180" alt="Card image cap" />
                      <CardBlock>
                        <CardTitle>slippers Shoe</CardTitle>
                        <CardSubtitle>#35,000.00</CardSubtitle>
                        <CardText>This card has supporting text below as a natural lead-in to additional content.</CardText>
                      </CardBlock>
                      <div className="price-context">

                        <Button className="btn btn-warning">Add to wishlist</Button>
                        <Button className="btn btn-warning">Buy</Button>
                        <Button className="btn btn-warning">Compare</Button>

                      </div>
                      <CardFooter>
                        <div className="row">
                          <div className="col-md-5">200 Left</div>
                          <div className="col-md-7"><Progress value="25" /></div>
                        </div>
                      </CardFooter>

                    </Card>


                  </div>

                  <div className="col-md-4 col-sm-12 p-3 pt-5">
                    <Card className="col-sm-12 p-0 product">
                      <CardImg top width="100%" src="https://placeholdit.imgix.net/~text?txtsize=33&txt=256%C3%97180&w=256&h=180" alt="Card image cap" />
                      <CardBlock>
                        <CardTitle>flat slippers</CardTitle>
                        <CardSubtitle>#35,000.00</CardSubtitle>
                        <CardText>This card has supporting text below as a natural lead-in to additional content.</CardText>
                      </CardBlock>
                      <div className="price-context">
                      <div className="col-md-12 text-white">
                      <h5>BRIEF DETAILS</h5>
                      Beautiful and nice product
                      <div className="clearfix"></div>
                      <button className="btn btn-link">View product</button>
                      </div>

                        <Button className="btn btn-warning">Add to wishlist</Button>
                        <Button className="btn btn-warning">Buy</Button>
                        <Button className="btn btn-warning">Compare</Button>

                      </div>
                      <CardFooter>
                        <div className="row">
                          <div className="col-md-5">200 Left</div>
                          <div className="col-md-7"><Progress value="25" /></div>
                        </div>
                      </CardFooter>

                    </Card>


                  </div>

              

               
    </Slider>
  </div>
           
             

</div>
</div>


        );
}
}
export default ProductSlider;