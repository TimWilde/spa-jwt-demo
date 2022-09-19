import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import Login from "./components/Login";

const AppRoutes = [
  {
    index: true,
    name: 'Home',
    path: '/',
    element: <Home />
  },
  {
    name: 'Counter',
    path: '/counter',
    element: <Counter />
  },
  {
    name: 'Server Data',
    path: '/fetch-data',
    element: <FetchData />
  },
  {
    name: 'Login',
    path: '/login',
    element: <Login />
  }
];

export default AppRoutes;
