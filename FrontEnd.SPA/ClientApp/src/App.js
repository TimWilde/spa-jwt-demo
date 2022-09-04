import React from 'react';
import './custom.css';
import {Layout} from "./components/Layout";
import {Route} from "wouter";
import AppRoutes from "./AppRoutes";

const App = () => (
    <Layout>
        {AppRoutes.map((route, index) => (
            <Route key={index} path={route.path}>{route.element}</Route>
        ))}
    </Layout>
)

export default App;