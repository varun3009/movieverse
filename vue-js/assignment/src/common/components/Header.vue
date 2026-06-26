<template>
    <v-app-bar flat app height="76" class="movieverse-header">
        <div class="brand-lockup" @click="$router.push({ path: '/' })">
            <div class="brand-mark">MV</div>
            <div>
                <div class="brand-name">MovieVerse</div>
                <div class="brand-line">Curated cinema library</div>
            </div>
        </div>
        <v-spacer></v-spacer>
        <div class="nav-actions">
            <v-btn :to="'/'" text class="nav-link">Home</v-btn>
            <v-btn :to='"/movies/add"' depressed class="primary-action">Add movie</v-btn>
        </div>
        <div class="user-pill">
            <span>Welcome, {{ userName }}</span>
        </div>
        <v-btn @click="logout" text class="logout-btn">Logout</v-btn>
    </v-app-bar>
</template>

<script>
import {mapGetters, mapMutations} from 'vuex';
export default{
    methods:{
        parseJwt(token) {
            try {
                const base64Url = token.split('.')[1];
                const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
                const jsonPayload = decodeURIComponent(atob(base64).split('').map(function(c) {
                    return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
                }).join(''));
                return JSON.parse(jsonPayload);
            } catch (e) {
                console.error('Invalid token:', e);
                return null;
            }
        },
        ...mapMutations(['Logout']),
        logout() {
            this.Logout();
            this.$router.push({ path: '/login' });
        }
    },
    computed:{
        ...mapGetters(['GetToken']),
        userName() {
            const token = this.GetToken;
            console.log('Token in Header:', token);
            if (token) {
                const decoded = this.parseJwt(token);
                if (decoded) {
                    return decoded.name ||
                        decoded.unique_name ||
                        decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
                }
            }
            return '';
        },
    }
}
</script>

<style scoped>
.movieverse-header {
    backdrop-filter: blur(18px);
    background: rgba(252, 250, 246, 0.88) !important;
    border-bottom: 1px solid rgba(24, 25, 31, 0.08) !important;
    padding: 0 42px;
}

.brand-lockup {
    align-items: center;
    cursor: pointer;
    display: flex;
    gap: 14px;
    min-width: 236px;
}

.brand-mark {
    align-items: center;
    background: #16171d;
    border-radius: 8px;
    color: #f7efe2;
    display: flex;
    font-size: 0.88rem;
    font-weight: 800;
    height: 42px;
    justify-content: center;
    width: 42px;
}

.brand-name {
    color: #15161c;
    font-size: 1.18rem;
    font-weight: 800;
    line-height: 1;
}

.brand-line {
    color: #777169;
    font-size: 0.74rem;
    font-weight: 600;
    margin-top: 4px;
}

.nav-actions {
    align-items: center;
    display: flex;
    gap: 8px;
}

.nav-link {
    color: #383838 !important;
    font-weight: 700;
}

.primary-action {
    background: #c52a49 !important;
    border-radius: 6px;
    color: #fff !important;
    font-weight: 800;
    min-width: 118px;
}

.user-pill {
    border-left: 1px solid rgba(24, 25, 31, 0.1);
    color: #5f5a54;
    font-size: 0.84rem;
    font-weight: 700;
    margin-left: 20px;
    padding-left: 20px;
    white-space: nowrap;
}

.logout-btn {
    color: #6e675e !important;
    font-weight: 700;
    margin-left: 10px;
}

@media (max-width: 760px) {
    .movieverse-header {
        height: auto !important;
        padding: 10px 14px;
    }

    .brand-line,
    .user-pill {
        display: none;
    }

    .brand-lockup {
        min-width: auto;
    }

    .brand-name {
        font-size: 1rem;
    }

    .nav-actions {
        gap: 2px;
    }

    .primary-action {
        min-width: 94px;
        padding: 0 10px !important;
    }
}
</style>
