import React from 'react';
import './custom.css';
import {Layout} from "./components/Layout";
import {Route, Switch} from "wouter";
import AppRoutes from "./AppRoutes";
import {RecoilRoot} from "recoil";

const App = () => (
   <React.StrictMode>
      <RecoilRoot>
         <Layout>
            <Switch>
               {AppRoutes.map((route, index) => (
                  <Route key={index} path={route.path}>{route.element}</Route>
               ))}
               <Route path="/:rest*">404!!</Route>
            </Switch>
         </Layout>
      </RecoilRoot>
   </React.StrictMode>
)

export default App;