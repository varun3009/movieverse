const GetMovies=(state,data)=>{
    state.movies=data;
};

const AddMovie=(state,{data})=>{
    state.movies=[...state.movies,{...data}];
}

const DeleteMovie=(state,id)=>{
    var tempState=state.movies.filter(movie=>movie.id!=id)
    state.movies=tempState;
}

const ReplaceMovie=(state,{data})=>{
    const target=state.movies.find(movie=>movie.id==data.id);
    if(target)
        Object.assign(target, {...data})
    else
        state.movies=[...state.movies,{...data}];
}

export {
    GetMovies,
    AddMovie,
    DeleteMovie,
    ReplaceMovie,
}