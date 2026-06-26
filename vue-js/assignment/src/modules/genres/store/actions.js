import axios from 'axios';
async function GetGenres(context){
    try{
        var response=await axios.get('/api/genres')
        context.commit('GetGenres',response.data);
    }
    catch(err){
        throw err
    }
}
async function PostGenre(context,{data}){
    try{
        var response=await axios.post('/api/genres',data);
        data["id"]=response.data;
        context.commit('AddGenre',data);
        return response.data;
    }
    catch(err)
    {
        throw err
    }
}
export {
    GetGenres,
    PostGenre
}
