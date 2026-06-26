import axios from 'axios';
async function Login(context,{data}){
    try{
        var response=await axios.post('/api/login',data);
        localStorage.setItem('user',response.data.token);
        context.commit('Login',response.data.token);
    }
    catch(err){
        throw err
    }
}
async function Logout(context){
    try{
        localStorage.removeItem('user');
        context.commit('Logout');
    }
    catch(err){
        throw err
    }
}
async function Register(context,{data}){
    try{
        console.log(data);
        var response=await axios.post('/api/register',data);
        console.log(response.data.token);
        localStorage.setItem('user',response.data.token);
        context.commit('Login',response.data.token);
    }
    catch(err){
        throw err
    }
}
export {
    Login,
    Logout,
    Register
}
