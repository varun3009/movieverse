const GetGenres=(state,data)=>{
    state.genres=data;
};
const AddGenre=(state,data)=>{
    state.genres=[...state.genres,data]
}
export{
    GetGenres,
    AddGenre
}