const GetActors=state=> state.actors;
const GetActorById=(state)=>(id)=>state.actors.filter(actor=>actor.id==id);
const GetProducers=state=> state.producers;
const GetProducerById=(state)=>(id)=>state.producers.filter(producer=>producer.id==id);

export {
    GetActors,
    GetActorById,
    GetProducers,
    GetProducerById,
 
}