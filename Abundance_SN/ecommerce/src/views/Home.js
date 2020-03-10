import React from 'react';

import ProductSlider from './productslider';
import Carouselroll from './Carouselroll';
import Subscribe from './subscribe';
import Featuredproduct from './featuredproduct';
import Footer from './footer';



export const HomeView = () => <div>







  <Carouselroll />



  <div className="container-fluid mainProducts">
    <ProductSlider />
  </div>

  <div className="container-fluid">
    <Featuredproduct />
  </div>

  <div className="clearfix"></div>

  <div className="container-fluid">
    <div className="row">
      <div className="col-md-6 col-sm-12 bgUrl5 h100"></div>
      <div className="col-md-6 col-sm-12 bgUrl4 h100"></div>
    </div>
  </div>


  <Subscribe />



  <Footer />


</div>




