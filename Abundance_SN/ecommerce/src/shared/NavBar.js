import React from 'react';
// import { Link } from "react-router-dom";
import {
  Form, FormGroup, Input, Button
} from 'reactstrap';
import {
  Collapse, Navbar, NavbarToggler, NavbarBrand, Nav, NavItem, NavLink, UncontrolledDropdown, DropdownToggle,
  DropdownMenu, DropdownItem
} from 'reactstrap';
import Login from '../views/loginModal';




  this.state = { isOpen: false };




export const NavBar = () => {
  return (
    <div>
    

      <div className="container-fluid bg-warning p-2">
        <div className="container">
          <div className="row">
           
            <div className="col-md-3 offset-md-3 offset-sm-0 hidden-md-down">Address:</div>
            <div className="col-md-3 col-sm-12">Email: info@ecommerce.com</div>
            <div className="col-md-2 col-sm-12">Phone: 09037879282</div>
          </div>
        </div>
      </div>

      <div>
     


        <div>
          <Navbar className="m-0 pl-5 pr-5" color="faded" light expand="lg">
            <NavbarBrand className="mr-5" href="/">ECOMMERCE</NavbarBrand>
            <NavbarToggler onClick={this.toggle} />
            <Collapse isOpen={this.state.isOpen} navbar>
              <Nav className="ml-auto" navbar>
                <NavItem>
                  <NavLink href="/Catalog">Components</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink href="/Nested">Components</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink href="/catalog">Components</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink href="https://github.com/reactstrap/reactstrap">Github</NavLink>
                </NavItem>
                <UncontrolledDropdown nav inNavbar>
                  <DropdownToggle nav caret>
                    Categories
                </DropdownToggle>
                  <DropdownMenu >
                    <DropdownItem>
                      shoes
                  </DropdownItem>
                    <DropdownItem>
                      bags
                  </DropdownItem>
                    <DropdownItem divider />
                    <DropdownItem>
                      slippers
                  </DropdownItem>
                  </DropdownMenu>
                </UncontrolledDropdown>

                <UncontrolledDropdown className="ml-5 mr-5" nav inNavbar>
                  <DropdownToggle nav caret>
                   <i className="fa fa-user"></i> Hello Tunde
                </DropdownToggle>
                  <DropdownMenu >
                    <DropdownItem>
                      My Account
                  </DropdownItem>
                    <DropdownItem>
                      My Orders
                  </DropdownItem>
                    <DropdownItem divider />
                    <DropdownItem>
                      <Login/>
                  </DropdownItem>
                  </DropdownMenu>
                </UncontrolledDropdown>

                <NavItem>
                  <NavLink href="/Nested"><i className="fa fa-cart"></i>Cart</NavLink>
                </NavItem>
              </Nav>
            </Collapse>
          </Navbar>
        </div>
      </div>

      <div className="container-fluid bg-lighti pl-5 pr-5 pt-3 pb-3">
        <div className="container">
          <Form>
            <FormGroup className="col-md-8 col-sm-12 pr-0 pl-0">
              {/* <Label for="exampleEmail" hidden>Email</Label> */}
              <Input type="text" className="form-control" name="text" id="Searchtext1" placeholder="search" />
            </FormGroup>
            {' '}
            <FormGroup className="col-md-2 col-sm-6 pl-0 pr-0">
              {/* <Label for="examplePassword" hidden>Categories</Label> */}
              <select className="form-control">
                <option>Categories</option>
                <option>Shoezs</option>
                <option>Categories</option>
              </select>
            </FormGroup>
            {' '}
            <FormGroup className="col-md-2 col-sm-12 pl-0 pr-0">
              <Button className="btn btn-info btn-block">SEARCH</Button>
            </FormGroup>
          </Form>


        </div>
      </div>



      
    </div>
  )
}