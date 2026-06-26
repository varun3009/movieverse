import "regenerator-runtime/runtime";
import Vue from 'vue'
import App from './App.vue'
import  vuex from 'vuex'
import {Routes} from './router/routes'
import VueRouter from 'vue-router'
import vuetify from './plugins/vuetify'
import vClickOutside from 'v-click-outside'

Vue.use(VueRouter)
Vue.use(vuex);
Vue.use(vClickOutside);
const router=new VueRouter(
  {
    routes: Routes,
    mode: 'history'
  }
)

router.beforeEach((to, from, next) => {
  const isLoggedIn = !!localStorage.getItem('user');

  if (!isLoggedIn && to.path !== '/login') {
    next('/login');
  } else if (to.path === '/login' && isLoggedIn) {
    next('/');
  } else {
    next();
  }
});

new Vue({
  el: '#app',
  vuetify,
  render: h => h(App),
  router:router
})
