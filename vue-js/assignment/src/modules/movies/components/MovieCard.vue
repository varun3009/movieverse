<template>
    <v-card class="movie-card">
        <template v-if="movie.caption">
            <v-tooltip bottom>
                <template v-slot:activator="{ on, attrs }">
                    <v-img
                        class="movie-poster white--text align-end"
                        height="310px"
                        :src="movie.poster"
                        v-bind="attrs"
                        v-on="on"
                    >
                        <div class="poster-overlay">
                            <span>{{ movie.yearOfRelease }}</span>
                            <h3>{{ movie.name }}</h3>
                        </div>
                    </v-img>
                </template>
                <span>{{ movie.caption }}</span>
            </v-tooltip>
        </template>
        <template v-else>
            <v-img
                class="movie-poster white--text align-end"
                height="310px"
                :src="movie.poster"
            >
                <div class="poster-overlay">
                    <span>{{ movie.yearOfRelease }}</span>
                    <h3>{{ movie.name }}</h3>
                </div>
            </v-img>
        </template>
        <v-card-text class="card-body">
            <div class="producer-line">
                <span class="material-icons">local_movies</span>
                {{movie.producer.name}}
            </div>
            <div class="plot">{{ movie.plot }}</div>
        </v-card-text>
        <v-card-actions class="card-actions">
            <v-btn icon class="action-btn" @click="openDialog" title="View details">
                <span class="material-icons">open_in_new</span>
            </v-btn>
            <v-btn icon class="action-btn" :to="'movies/'+ movie.id +'/edit'" title="Edit movie">
                <span class="material-icons">edit</span>
            </v-btn>
            <DeleteMovie  @deleteMovie="deleteMovie"></DeleteMovie>
        </v-card-actions>
    </v-card>
</template>

<script>
import DeleteMovie from './DeleteMovie.vue';
import MovieDialog from './MovieDialog.vue';
import { mapActions } from 'vuex';
    export default {
        props:['movie'],
        components:{
            'DeleteMovie':DeleteMovie,
            'MovieDialog':MovieDialog,
        },
        methods:
        {
            ...mapActions(['DeleteMovie']),
            async deleteMovie(){
                try{
                    await this.DeleteMovie({'id':this.movie.id})
                }
                catch(err)
                {
                    this.$emit('errorlistener',err);
                }
            },
            openDialog(){
                this.$emit('openDialog',this.movie);
            }
        }
    }
</script>

<style scoped>
.movie-card {
    background: #fffdf9 !important;
    border: 1px solid rgba(23, 24, 30, 0.08);
    border-radius: 8px !important;
    box-shadow: 0 18px 48px rgba(34, 31, 27, 0.11) !important;
    height: 100%;
    overflow: hidden;
    transition: transform 180ms ease, box-shadow 180ms ease;
}

.movie-card:hover {
    box-shadow: 0 26px 64px rgba(34, 31, 27, 0.16) !important;
    transform: translateY(-4px);
}

.movie-poster {
    background: #15161c;
}

.poster-overlay {
    background: linear-gradient(180deg, transparent 0%, rgba(8, 9, 13, 0.86) 88%);
    padding: 72px 20px 20px;
    width: 100%;
}

.poster-overlay span {
    color: #f3c36a;
    font-size: 0.76rem;
    font-weight: 800;
}

.poster-overlay h3 {
    color: #fff;
    font-size: 1.28rem;
    font-weight: 800;
    line-height: 1.18;
    margin: 5px 0 0;
}

.card-body {
    min-height: 132px;
    padding: 18px 20px 10px !important;
}

.producer-line {
    align-items: center;
    color: #686159;
    display: flex;
    font-size: 0.85rem;
    font-weight: 800;
    gap: 7px;
    margin-bottom: 10px;
}

.producer-line .material-icons {
    color: #c52a49;
    font-size: 18px;
}

.plot{
    color: #3e3d3b;
    font-size: 0.94rem;
    line-height: 1.55;
    overflow: hidden;
    display: -webkit-box;
    -webkit-line-clamp: 2; 
            line-clamp: 2; 
    -webkit-box-orient: vertical;
 }

.card-actions {
    border-top: 1px solid rgba(23, 24, 30, 0.08);
    justify-content: flex-end;
    padding: 8px 14px 12px !important;
}

.action-btn {
    color: #4b4945 !important;
}

.action-btn:hover {
    color: #c52a49 !important;
}
</style>
