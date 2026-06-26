<template>
    <v-container fluid class="auth-page">
    <section class="auth-visual">
        <div class="auth-brand">
            <div class="auth-mark">MV</div>
            <h1>MovieVerse</h1>
            <p>A polished home for managing the films, people, and genres that shape your catalog.</p>
        </div>
    </section>
    <v-sheet elevation="0" width="440" class="auth-card">
        <div class="brand-kicker">Account access</div>
        <h2>Welcome to MovieVerse</h2>
        <v-tabs v-model="tab" class="auth-tabs">
            <v-tab>Login</v-tab>
            <v-tab>Register</v-tab>
        </v-tabs>
        <v-tabs-items v-model="tab">
            <v-tab-item>
                <Login></Login>
            </v-tab-item>
            <v-tab-item>
                <Register></Register>
            </v-tab-item>
        </v-tabs-items>
    </v-sheet>
    </v-container>
</template>

<script>
  import { ref } from 'vue'
  import Vuetify from 'vuetify'
  import Login from '../modules/login/components/login.vue';
  import Register from '../modules/login/components/register.vue';
  export default {
    components:{
        'Login':Login,
        'Register':Register,
    },
    vuetify: new Vuetify(),
    data () {
      return {
        tab: ref('one'),
      }
    },
  }
</script>

<style scoped>
.auth-page {
    align-items: center;
    display: grid;
    gap: 38px;
    grid-template-columns: minmax(0, 1fr) 440px;
    min-height: 100vh;
    padding: 44px;
}

.auth-visual {
    align-items: flex-end;
    background:
        linear-gradient(135deg, rgba(18, 19, 25, 0.92), rgba(47, 37, 35, 0.7)),
        url('https://images.unsplash.com/photo-1524985069026-dd778a71c7b4?auto=format&fit=crop&w=1400&q=80');
    background-position: center;
    background-size: cover;
    border-radius: 8px;
    display: flex;
    min-height: calc(100vh - 88px);
    overflow: hidden;
    padding: 54px;
}

.auth-brand {
    color: #fff;
    max-width: 560px;
}

.auth-mark {
    align-items: center;
    background: #c52a49;
    border-radius: 8px;
    display: flex;
    font-weight: 800;
    height: 48px;
    justify-content: center;
    margin-bottom: 24px;
    width: 48px;
}

.auth-brand h1 {
    font-size: 4rem;
    font-weight: 800;
    line-height: 0.98;
    margin: 0 0 18px;
}

.auth-brand p {
    color: rgba(255, 255, 255, 0.76);
    font-size: 1.05rem;
    font-weight: 500;
    line-height: 1.7;
    margin: 0;
}

.auth-card {
    background: rgba(255, 255, 255, 0.9) !important;
    border: 1px solid rgba(24, 25, 31, 0.09);
    border-radius: 8px !important;
    box-shadow: 0 24px 70px rgba(33, 30, 25, 0.12) !important;
    padding: 34px;
}

.auth-card h2 {
    color: #17181e;
    font-size: 1.7rem;
    font-weight: 800;
    margin: 6px 0 24px;
}

.auth-tabs {
    background: #f3eee6;
    border-radius: 8px;
    margin-bottom: 22px;
}

@media (max-width: 900px) {
    .auth-page {
        grid-template-columns: 1fr;
        padding: 20px;
    }

    .auth-visual {
        min-height: 310px;
        padding: 32px;
    }

    .auth-card {
        width: 100% !important;
    }

    .auth-brand h1 {
        font-size: 2.7rem;
    }
}
</style>
