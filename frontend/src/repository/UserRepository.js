import {USER_LOGIN_URL} from "./Base";

export const Login = async (email, password) => {
   try {
      return await fetch(USER_LOGIN_URL, {
           method: 'POST',
           body: JSON.stringify({
               email: email,
               password: password
           }),
           headers: {
               "Content-Type": "application/json",
               "Accept": "application/json",
           }
       }).then(res => res.json())

   } catch (e) {
       console.log(e);
   }

}