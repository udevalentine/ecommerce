import React from 'react';
import { Link, Route } from "react-router-dom";

export const NestedView = ({ match }) => {
  return (
    <div>


      <div className="container pt-5">
        <h3>CART</h3>
        <hr/>

        <div className="card no-hover">


        <table className="table table-striped">
       
            <tr>
              <th>Item</th>
              <th>Quantity</th>
              <th>Unit Price</th>
              <th>Total</th>
            </tr>
            <tr className="pt-2">
              <td>
                	
<sub>ANDROID PHONES</sub>
<div>
LEAGOO KIICAA POWER - 5.0" 3G Android 7.0 2GB/16GB 4000mAh L-Sensor Fingerprint EU - Champagne Gold
</div>

<div><sub>Variation: Gold</sub></div>
<div><sub>Seller: Globalomerce</sub></div>
<button className="btn btn-link">Remove</button>
              </td>
              <td> <select className="form-control"><option>1</option>  </select>  </td>
              <td><div>₦ 23,990</div>
                <div>₦ 33,647</div>
                <div className="text-success">Savings: ₦ 9,657</div>
</td>
              <td>₦ 23,990</td>
            </tr>

            <tr>
              <th></th>
              <th></th>
              <th></th>
              <th>SubTotal: ₦ 33,647
          
              </th>
            </tr>

            <tr>
              <th></th>
              <th></th>
              <th></th>
              <th>Total: ₦ 23,990
                <hr />
              </th>
            </tr>

          </table>

          <div className="row mb-4 p-2">
            <div className="col-md-4"><h4>Total: <b>₦ 23,990</b></h4></div>
            <div className="col-md-4"> <h4>Savings: <span className="text-success">₦ 9,657</span></h4></div>
            <div className="col-md-4"><button className="btn btn-warning btn-block ">PROCEED TO CHECKOUT</button></div>
          </div>
     

</div>
      </div>
 
    </div>
  )
};

