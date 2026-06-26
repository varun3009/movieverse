import axios from 'axios';
async function GetActors(context){
    try{
        var response=await axios.get('/api/actors')
        context.commit('GetActors',response.data);
    }
    catch(err)
    {
        throw err
    }
}
async function GetProducers(context){
    try{
        var response=await axios.get('/api/producers')
        context.commit('GetProducers',response.data);
    }
    catch(err)
    {
        throw err
    }
    
}

async function PostActor(context,{data}){
    try{
        var response=await axios.post('/api/actors',data)
        data["id"]=response.data;
        context.commit('AddActor',data)
        return response.data;
    }
    catch(err)
    {
        throw err
    }
}
async function PostProducer(context,{data}){
    try{
        var response=await axios.post('/api/producers',data);
        data["id"]=response.data;
        context.commit('AddProducer',data);
        return response.data;
    }
    catch(err)
    {
        throw err
    }
}


export{
    GetActors,
    GetProducers,
    PostActor,
    PostProducer,
}
