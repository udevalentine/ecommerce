import React, { Component } from 'react';
import { render } from 'react-dom';
import { BrowserRouter, Route } from "react-router-dom";


import { HomeView } from './views/Home';
import { CatalogView } from './views/Catalog';
import { NestedView } from './views/Nested';
import { NavBar } from './shared/NavBar';
import '../node_modules/slick-carousel/slick/slick.css';

class App extends Component {
  render() {
    return (
      <BrowserRouter>
        <div>
          <NavBar />
          <Route exact path="/" component={HomeView} />
          <Route path="/another" component={() => <div>yo!</div>} />
          <Route path="/catalog" component={CatalogView} />
          <Route path="/nested" component={NestedView} />
        </div>
      </BrowserRouter>
    );
  }
}

render(<App />, document.getElementById('root'));

/**
 * React Router 4.1.2 example
 * This snippet includes following examples:
 * 
 * - Basic multi-view application 
 * - Route Parameters (views/Catalog)
 * - Nested Routes with sub-viws (views/Nested)
 */