import axios from 'axios';


async function GetMovies(context){
    try{
        let token=localStorage.getItem('user');
        axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
        var response=await axios.get('/api/movies')
        context.commit('GetMovies',response.data);
    }
    catch(err)
    {
        throw err
    }
}

async function PostPoster(context,{data})
{
    try{
        
        let token=localStorage.getItem('user');
        axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
        const formdata = new FormData();
        formdata.append("file", data);
        var response=await axios.post('/api/upload',formdata)
        response=response.data;
        return response;
    }
    catch(err)
    {
        throw err
    }
}
async function EditMovie(context,{data,id,movieResponse}){
    try{
        
        let token=localStorage.getItem('user');
        console.log('Token in EditMovie action:', token);
        axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
        var response=await axios.put(`/api/movies/${id}`,data)
        context.commit('ReplaceMovie',{data:movieResponse});
    }
    catch(err)
    {
        throw err
    }
}

async function PostMovie(context,{data,movieResponse}){
    try{
        
        let token=localStorage.getItem('user');
        axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
        var response=await axios.post('/api/movies/',data)
        movieResponse.id=response.data;
        context.commit('AddMovie',{data:movieResponse});
    }
    catch(err)
    {
        throw err
    }
}
async function DeleteMovie(context,{id}){
    try{
        
        let token=localStorage.getItem('user');
        axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
        var response=await axios.delete(`/api/movies/${id}`)
        context.commit('DeleteMovie',id)
    }
    catch(err)
    {
        throw err
    }
}

async function GetMoviesById(context,{id})
{
    try{
        
        let token=localStorage.getItem('user');
        axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
        var response=await axios.get(`/api/movies/${id}`);
        context.commit('ReplaceMovie',{data:response.data});
        return response.data;      
    }
    catch(err)
    {
        throw err
    }   
}

export {
    GetMovies,
    PostPoster,
    EditMovie,
    DeleteMovie,
    PostMovie,
    GetMoviesById,
}
