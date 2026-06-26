<template>
    <v-container class="catalog-page">
        <v-snackbar
        v-model="error"
        :timeout="timeout"
        top
        color="#151821"
        >
            {{ message }}
        </v-snackbar>
        <section class="catalog-hero">
            <div>
                <div class="brand-kicker">MovieVerse collection</div>
                <h1>Your studio-grade movie archive.</h1>
                <p>Browse, refine, and manage a premium catalog of stories, casts, producers, and genres from one polished workspace.</p>
            </div>
            <div class="hero-stat">
                <span>{{ movies ? movies.length : 0 }}</span>
                <small>titles curated</small>
            </div>
        </section>
        <div class="section-heading">
            <div>
                <span class="brand-kicker">Now in library</span>
                <h2>Featured titles</h2>
            </div>
        </div>
        <v-row v-if="loading" class="movie-grid">
            <v-col cols="12" sm="6" lg="4" xl="3" v-for="item in 8" :key="item">
                <v-skeleton-loader
                    class="movie-skeleton"
                    type="image, article, actions"
                ></v-skeleton-loader>
            </v-col>
        </v-row>
        <v-row v-else class="movie-grid">
            <v-col cols="12" sm="6" lg="4" xl="3" v-for="(movie,index) in movies" :key="index">
                <MovieCard :movie="movie" @errorlistener="showError" @openDialog="openDialog"></MovieCard>
            </v-col>
        </v-row>
        <div v-if="!loading && (!movies || movies.length==0)" class="empty-state brand-panel">
            <span class="material-icons">movie_filter</span>
            <h2>No movies available</h2>
            <p>Add your first MovieVerse title to start building the catalog.</p>
        </div>
        <v-dialog v-model="dialog"  max-width="720px">
            <MovieDialog :movie="movieExplore" @closeDialog="closeDialog"/>
        </v-dialog>
    </v-container>
</template>

<script>
    import MovieCard from '../modules/movies/components/MovieCard.vue'
    import MovieDialog from '../modules/movies/components/MovieDialog.vue'
    import { mapGetters,mapActions } from 'vuex';
    export default{
        components:
        {
            'MovieCard':MovieCard,
            'MovieDialog':MovieDialog
        },
        data()
        {
            return{
                dialog: false,
                error:false,
                message:'',
                timeout:3000,
                loading:true,
                movieExplore:{}
            }
        },       
        methods:
        {
            ...mapActions({
                getMovies:'GetMovies'}),
            showError(value){
                this.error=true;
                this.message=value;
            },
            openDialog(movie){
                this.dialog=true;
                this.movieExplore=movie;
            },
            closeDialog(){
                this.dialog=false;
            }
        },
        computed:
        {
            ...mapGetters(['GetMovies']),
            movies()
            {
                return this.GetMovies;
            }
        },
        async created()
        {
            try{
                await this.getMovies();
            }
            catch(err)
            {
                this.showError(err);
            }
            finally
            {
                this.loading=false;
            }
        }
    }
</script>

<style scoped>
.catalog-page {
    max-width: 1220px;
    padding-bottom: 58px;
    padding-top: 36px;
}

.catalog-hero {
    align-items: flex-end;
    background:
        linear-gradient(115deg, rgba(21, 22, 28, 0.95), rgba(42, 39, 36, 0.88)),
        url('https://images.unsplash.com/photo-1489599849927-2ee91cede3ba?auto=format&fit=crop&w=1600&q=80');
    background-position: center;
    background-size: cover;
    border-radius: 8px;
    color: #fff;
    display: flex;
    justify-content: space-between;
    min-height: 310px;
    overflow: hidden;
    padding: 52px;
}

.catalog-hero h1 {
    font-size: 3.15rem;
    font-weight: 800;
    line-height: 1.02;
    margin: 12px 0 16px;
    max-width: 650px;
}

.catalog-hero p {
    color: rgba(255, 255, 255, 0.78);
    font-size: 1rem;
    font-weight: 500;
    line-height: 1.7;
    margin: 0;
    max-width: 570px;
}

.hero-stat {
    background: rgba(255, 255, 255, 0.1);
    border: 1px solid rgba(255, 255, 255, 0.2);
    border-radius: 8px;
    min-width: 150px;
    padding: 18px;
    text-align: center;
}

.hero-stat span {
    display: block;
    font-size: 2.4rem;
    font-weight: 800;
    line-height: 1;
}

.hero-stat small {
    color: rgba(255, 255, 255, 0.72);
    font-weight: 700;
}

.section-heading {
    align-items: flex-end;
    display: flex;
    justify-content: space-between;
    margin: 36px 0 18px;
}

.section-heading h2 {
    color: #17181e;
    font-size: 1.6rem;
    font-weight: 800;
    margin: 4px 0 0;
}

.movie-grid {
    margin-top: 0;
}

.movie-skeleton {
    background: #fffdf9 !important;
    border: 1px solid rgba(23, 24, 30, 0.08);
    border-radius: 8px !important;
    box-shadow: 0 18px 48px rgba(34, 31, 27, 0.1) !important;
    overflow: hidden;
}

.empty-state {
    align-items: center;
    display: flex;
    flex-direction: column;
    margin-top: 26px;
    padding: 50px 20px;
    text-align: center;
}

.empty-state .material-icons {
    color: #c52a49;
    font-size: 46px;
}

.empty-state h2 {
    font-size: 1.45rem;
    font-weight: 800;
    margin: 12px 0 6px;
}

.empty-state p {
    color: #777169;
    margin: 0;
}

@media (max-width: 760px) {
    .catalog-page {
        padding-top: 22px;
    }

    .catalog-hero {
        align-items: flex-start;
        flex-direction: column;
        gap: 28px;
        min-height: 360px;
        padding: 30px;
    }

    .catalog-hero h1 {
        font-size: 2.25rem;
    }
}
</style>
