import React from 'react';
import { Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';
import {
    Form, FormGroup, Input, Button
} from 'reactstrap';

class Login extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      modal: false
    };

    this.toggle = this.toggle.bind(this);
  }

  toggle() {
    this.setState({
      modal: !this.state.modal
    });
  }

  render() {
    return (
      <div>
        <Button color="btn btn-neutral btn-block" onClick={this.toggle}>Login</Button>
        <Modal isOpen={this.state.modal} toggle={this.toggle} className={this.props.className}>
          <ModalHeader toggle={this.toggle}>LOGIN TO YOUR ACCOUNT</ModalHeader><hr/>
          <ModalBody>
                    <Form>
                        <FormGroup className="col-md-12 col-sm-12">
                            {/* <Label for="exampleEmail" hidden>Email</Label> */}
                            <Input type="text" className="form-control" name="text" id="Searchtext1" placeholder="Email Address" />
                        </FormGroup>
                        {' '}
                        <FormGroup className="col-md-12 col-sm-12">
                            {/* <Label for="exampleEmail" hidden>Email</Label> */}
                            <Input type="text" className="form-control" name="text" id="Searchtext1" placeholder="password" />
                        </FormGroup>
                        {' '}
                        <FormGroup className="col-md-12 col-sm-12 ">
                            <Button className="btn btn-info btn-block">Login</Button>
                            <div className="col-md-12 text-center">OR</div>
                            <Button color="primary btn-block" onClick={this.toggle}>Facebook</Button>
                        </FormGroup>
                    </Form>
          </ModalBody>
          <ModalFooter>
           {' '}
          Don't have an Account <Button className="btn btn-link">Create Account</Button>
            
          </ModalFooter>
        </Modal>
      </div>
    );
  }
}

export default Login;