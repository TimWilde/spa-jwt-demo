import jwtTokenState from "./atoms/jwtTokenState";
import {useRecoilState} from "recoil";
import {useState} from "react";
import {Link, useLocation} from "wouter";

const Login = () => {
   const [jwtToken, setJwtToken] = useRecoilState(jwtTokenState)
   const [, setLocation] = useLocation()

   const [username, setUsername] = useState('')
   const [password, setPassword] = useState('')

   const doSignIn = async () => {
      const response = await fetch('api/authentication/login', {
         method: 'POST',
         cache: 'no-cache',
         mode: 'cors',
         headers: {
            'Content-Type': 'application/json'
         },
         body: JSON.stringify({username, password})
      });

      if (response.status === 200) {
         const data = await response.json()
         setJwtToken(data.token)
         setLocation('/')
      }
   }

   if (jwtToken === null) {
      return (
         <>
            <h1>Login</h1>
            <form>
               <div>
                  <label htmlFor="username">Username: </label>
                  <input id="username" type="text" value={username} onChange={e => setUsername(e.target.value)}/>
               </div>
               <div>
                  <label htmlFor="password">Password: </label>
                  <input id="password" type="password" value={password} onChange={e => setPassword(e.target.value)}/>
               </div>
               <div>
                  <input type="button" value="Submit" onClick={doSignIn}/>
               </div>
            </form>
         </>
      )
   } else {
      return (
         <>
            <p>You are already signed in</p>
            <Link to='/'>Home</Link>
         </>
      )
   }
}

export default Login
