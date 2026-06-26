const GetMovies=state=> state.movies;
const GetMovieById= (state)=>(id)=>{var movie=state.movies.find(movie=>movie.id==id); return movie;};

export {
    GetMovieById,
    GetMovies
}