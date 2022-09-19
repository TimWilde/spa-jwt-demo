import React, { Component } from 'react';
import './NavMenu.css';
import {Link} from "wouter";
import AppRoutes from "../AppRoutes";

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true
    };
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  render() {
    return (
        <header>
          <h1>FrontEnd.SPA</h1>
          <ul>
            {AppRoutes.map((route, index)=>(
               <li key={index}>
                 <Link to={route.path}>{route.name}</Link>
               </li>
            ))}
          </ul>
          
          <p>Or, view the <a href="//localhost:7172/swagger/index.html" target="swagger">Swagger UI</a></p>
        </header>
    );
  }
}
