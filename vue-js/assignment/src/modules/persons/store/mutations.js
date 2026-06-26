const GetActors=(state,data)=>{
    state.actors=data;
   };
const GetProducers=(state,data)=>{
    state.producers=data;
   };
const AddProducer=(state,data)=>{
    state.producers=[...state.producers,data]
}
const AddActor=(state,data)=>{
    state.actors=[...state.actors,data]
}
export {
    GetActors,
    GetProducers,
    AddProducer,
    AddActor
 };