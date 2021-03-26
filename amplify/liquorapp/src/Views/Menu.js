import React, { Component } from 'react';
import { HashRouter as Router, Route, Switch } from 'react-router-dom';
import { Button,FormControl,Form,Navbar, Nav, NavDropdown } from 'react-bootstrap';
import User from '../Modules/UserModule/User'
import Home from '../Pages/Home'
import Contact from '../Pages/Contact'
export class Menu extends Component {
    render() {
        return (
            <div>
                <Navbar sticky="top" bg="light" expand="lg">
                    <Navbar.Brand href="#/">Evan Dangol</Navbar.Brand>
                    <Navbar.Toggle aria-controls="basic-navbar-nav" />
                    <Navbar.Collapse id="basic-navbar-nav">
                        <Nav className="mr-auto">
                            <Nav.Link href="#/">Home</Nav.Link>
                            <Nav.Link href="#user">User</Nav.Link>
                            <Nav.Link href="#contact">Contact</Nav.Link>
                            <NavDropdown title="Dropdown" id="basic-nav-dropdown">
                                <NavDropdown.Item href="#action/3.1">Action</NavDropdown.Item>
                                <NavDropdown.Item href="#action/3.2">Another action</NavDropdown.Item>
                                <NavDropdown.Item href="#action/3.3">Something</NavDropdown.Item>
                                <NavDropdown.Divider />
                                <NavDropdown.Item href="#action/3.4">Separated link</NavDropdown.Item>
                            </NavDropdown>
                        </Nav>
                        <Form inline>
                            <FormControl type="text" placeholder="Search" className="mr-sm-2" />
                            <Button variant="outline-success">Search</Button>
                        </Form>
                    </Navbar.Collapse>
                </Navbar>
                <Router>
                    <Switch>
                        <Route exact path="/">
                            <Home />
                        </Route>
                        <Route path="/user">
                            <User />
                        </Route>
                        <Route path="/contact">
                            <Contact />
                        </Route>
                    </Switch>
                </Router>
            </div>

        )
    }
}

export default Menu
