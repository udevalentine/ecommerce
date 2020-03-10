import React from 'react';

import { Link } from "react-router-dom";

import ProductSlider from './productslider';
import Carouselroll from './Carouselroll';
import Subscribe from './subscribe';
import Featuredproduct from './featuredproduct';
import Footer from './footer';



export const CatalogView = () => {
  
  return (
    <div class="bg-white">
    <div className="clearfix"></div>
    <div className="col-md-12 p-5">
      <h3>CATEGORIES</h3></div>
      <div className="clearfix"></div>
      
      <div className="container-fluid mainProducts">
        <ProductSlider />
      </div>

      <div className="container-fluid">
        <Featuredproduct />
      </div>


      <Subscribe />



      <Footer />
    </div>
  )
}


