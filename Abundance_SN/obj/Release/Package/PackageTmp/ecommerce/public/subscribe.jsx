import React, { Component } from 'react';

import { Form, FormGroup, Input, Button } from 'reactstrap';

class Subscribe extends Component {
    // constructor(props) {
    // super(props);

    // }

    render() {
        return (

   <div className="container-fluid bg-warning  h50 p-4">
   <div className="container">
          <div className="row">
              <div className="col-md-4 col-sm-12 Twoem p-1">SUBSCRIBE TO OUR NEWSLETTER</div>
            <div className="col-md-6 col-sm-12 pr-0 pl-0">
            <Form>
            <FormGroup>
                <Input type="text" className="form-control" name="text" id="Searchtext1" placeholder="Email Address" />
              </FormGroup>
              </Form>
            </div>
            <div className="col-md-2 col-sm-12"><Button className="btn btn-neutral btn-block">subscribe</Button></div>
          </div>
        </div>

            </div>
        )
    }
}

export default Subscribe;