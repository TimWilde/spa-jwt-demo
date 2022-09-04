import React, { Component } from 'react';
import './NavMenu.css';
import {Link} from "wouter";

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
            <li>
              <Link to="/">Home</Link>
            </li>
            <li>
              <Link to="/counter">Counter</Link>
            </li>
            <li>
              <Link to="/fetch-data">Fetch data</Link>
            </li>
          </ul>
        </header>
    );
  }
}
