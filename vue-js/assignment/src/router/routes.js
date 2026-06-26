import MovieForm from '../views/MovieForm.vue';
import AllMovies from '../views/AllMovies.vue';
import Login from '../views/Login.vue';

export const Routes=[
    {
    path:'/',
    component:AllMovies
    },
    {
        path:'/movies/add',
        component:MovieForm
    },
    {
        path:'/movies/:id(\\d+)/edit',
        component:MovieForm
    },
    {
        path:'/login',
        component:Login
    }
]