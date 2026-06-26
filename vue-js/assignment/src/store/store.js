import Vue from 'vue';
import Vuex from 'vuex';
import VueResource from 'vue-resource';
import movie from '../modules/movies/store/index';
import person from '../modules/persons/store/index';
import genre from '../modules/genres/store/index';
import login from '../modules/login/store/index';

Vue.use(Vuex);
Vue.use(VueResource);
export const store=new Vuex.Store(
    {
        modules:{
            movie:movie,
            person:person,
            genre:genre,
            login:login
        }
    }
)