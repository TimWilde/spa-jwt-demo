import {atom} from "recoil";

const jwtTokenState = atom({
   key: 'userJwtToken',
   default: null
})

export default jwtTokenState