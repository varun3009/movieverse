<template>
  <v-app class="movieverse-app">
    <v-snackbar
      v-model="error"
      :timeout="timeout"
      top
      color="#151821"
    >
      {{ message }}
    </v-snackbar>
    <Header v-if="user"></Header>
    <v-main>
      <div id="app" class="app-shell">
        <router-view @errorListener="showError" :key="$route.fullPath"></router-view>
      </div>
    </v-main>
  </v-app>
</template>

<script>
import AddPerson from './modules/persons/components/AddPerson.vue'
import Vuetify from 'vuetify'
import {store} from './store/store'
import {mapActions} from 'vuex'
import Header from './common/components/Header.vue';
import { mapGetters } from 'vuex/dist/vuex.common.js';
import axios from 'axios';
export default {
  name: 'app',
  store:store,
  vuetify: new Vuetify(),
  components: {'AddPerson': AddPerson,'Header': Header},
  data () {
    return {
      load:false,
      error:false,
      timeout:3000,
      message:'',
    }
  },
  methods:{
    ...mapActions(['GetMovies','GetActors','GetProducers','GetGenres']),
    showError(err)
    {
      this.error=true;
      this.message=err;
    }
  },
  computed:{
    ...mapGetters(['GetToken']),
    user()
    {
      if(this.GetToken)
      {
        let token=this.GetToken;
        axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
        return this.GetToken;
      }
      else
      {
         return null;
      }
  },
  }
}
</script>

<style>
@import url('https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700;800&display=swap');

html,
body {
  margin: 0;
  background: #f6f3ee;
  font-family: 'Inter', Arial, sans-serif;
}

.movieverse-app {
  min-height: 100vh;
  color: #16171d;
  background:
    radial-gradient(circle at top left, rgba(197, 42, 73, 0.14), transparent 28rem),
    linear-gradient(135deg, #fbfaf7 0%, #f2eee7 48%, #ece7de 100%);
  font-family: 'Inter', Arial, sans-serif;
}

.app-shell {
  min-height: 100vh;
}

.brand-kicker {
  color: #b82f49;
  font-size: 0.76rem;
  font-weight: 800;
  letter-spacing: 0.12em;
  text-transform: uppercase;
}

.brand-panel {
  background: rgba(255, 255, 255, 0.84) !important;
  border: 1px solid rgba(28, 27, 24, 0.08) !important;
  border-radius: 8px !important;
  box-shadow: 0 22px 70px rgba(32, 29, 24, 0.1) !important;
}

.v-btn {
  letter-spacing: 0 !important;
  text-transform: none !important;
}

.v-text-field--outlined fieldset,
.v-select--outlined fieldset,
.v-textarea--outlined fieldset,
.v-file-input--outlined fieldset {
  border-color: rgba(22, 23, 29, 0.16) !important;
}
</style>

