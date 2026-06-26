<template>
    <v-container class="movie-form-page">
        <v-snackbar
        v-model="error"
        :timeout="timeout"
        top
        color="#151821"
        >
            {{ message }}
        </v-snackbar>
        <section class="form-hero">
            <div class="brand-kicker">MovieVerse studio</div>
            <h1>{{ $route.params.id != null ? 'Edit catalog title' : 'Add a new title' }}</h1>
            <p>Keep your movie metadata, creative team, genre tags, poster, and story details publication-ready.</p>
        </section>
        <v-form ref="movieForm" @submit.prevent class="brand-panel form-card">
            <v-row>
                <v-col cols="12">
                    <v-text-field
                        v-model="title"
                        label="Movie Title"
                        :rules="nameRules"
                        outlined
                        required
                    />
                </v-col>
                <v-col cols="12">
                    <v-text-field
                        v-model="yearOfRelease"
                        label="Movie Released Year"
                        :rules="yearRules"
                        type="number"
                        outlined
                        required
                    />
                </v-col>
                <v-col cols="12" md="9">
                    <v-select
                        return-object
                        v-model="selectedActors"
                        :items="actors"
                        item-text="name"
                        attach
                        chips
                        label="Select Actors"
                        multiple
                        outlined
                        :rules="selectionRules"
                    >
                    </v-select>
                </v-col>
                <v-col cols="12" md="3">
                    <AddPerson block :isActor="true" @selectActor="addActor" @errorRaised="handleError">
                        <h3 slot="title">Add Actor</h3>
                    </AddPerson>
                </v-col>
                <v-col cols="12" md="9" row>
                    <v-select
                        return-object
                        v-model="selectedProducer"
                        :items="producers"
                        item-text="name"
                        attach
                        chips
                        label="Select Producer"
                        outlined
                        :rules="[v=> !!v || 'select a producer']"
                    >
                    </v-select>
                </v-col>
                <v-col cols="12" md="3">
                    <AddPerson block :isActor="false" @selectProducer="addProducer" @errorRaised="handleError">
                        <h3 slot="title">Add Producer</h3>
                    </AddPerson>
                </v-col>
                <v-col cols="12" md="9">
                    <v-select
                        return-object
                        v-model="selectedGenres"
                        :items="genres"
                        item-text="name"
                        attach
                        chips
                        label="Select Genres"
                        multiple
                        outlined
                        :rules="selectionRules"
                    >
                    </v-select>
                </v-col>
                <v-col cols="12" md="3">
                    <AddGenre block @selectGenre="addGenre" @errorRaised="handleError">
                    </AddGenre>
                </v-col>
                <v-col cols="12">
                    <v-textarea
                        solo
                        outlined
                        v-model="plot"
                        name="input-7-4"
                        label="Plot"
                        :rules="plotRules"
                    >
                    </v-textarea>
                </v-col>
                <v-col cols="12" v-if="show">
                    <v-file-input
                        accept="image/*"
                        label="File input"
                        v-model="file"
                        outlined
                        :rules="[v => !!v || 'Select a file']"
                        required
                    >
                    </v-file-input>
                </v-col>
                <v-col cols="6" v-if="!show">
                    <v-img class="poster-preview" :src="file" ></v-img>
                </v-col>
                <v-col cols="6" class="d-flex justify-center align-center" v-if="!show">
                    <v-btn block depressed class="secondary-btn" @click="show=true;file=null">Change poster</v-btn>
                </v-col>
                <v-col cols="12">
                    <v-btn type="submit" @click="submit" block depressed class="submit-btn">
                        {{ $route.params.id != null ? 'Save movie' : 'Add movie' }}
                    </v-btn>
                </v-col>
            </v-row>
        </v-form>
    </v-container>
</template>

<script>
import AddPerson from '../modules/persons/components/AddPerson.vue';
import { mapGetters } from 'vuex';
import { mapActions } from 'vuex';
import AddGenre from '../modules/genres/components/AddGenre.vue';

 export default{
    
    components:{
        'AddPerson':AddPerson,
        'AddGenre':AddGenre
    },
    data()
    {
        return {
            error:false,
            show:true,
            message:'',
            timeout:3000,
            movie:null,
            movieResponse:{},
            producerDialog: false,
            actorDialog:false,
            title:'',
            file:null,
            plot:'',
            selectedActors: [],
            selectedGenres: [],
            selectedProducer:null,
            yearOfRelease: '',
            nameRules:[
                value=>{
                    if (value && value.match(/^[0-9A-Za-z.,\s]+$/)) return true
                    return 'Title is required.'
                },
                value=>
                {
                    if(value && value.trim().length>0)
                        return true;
                    return 'enter valid Title'
                }
            ],
            yearRules:[
                value=>{
                    if(value)
                    return true;
                return 'year is required'
                 },
                value=>{
                    if(value>=1800)
                        return true;
                    return 'year should not be less than 1800'
                }
            ],
            selectionRules:[
                value =>{
                    if(value && value.length==0) return 'Select at least one value'
                    return true;
                }
            ],
            plotRules:[
                value => {
                    if (value && value.match(/[A-Za-z]+/)) return true
                    return 'Bio is required.'
                }
            ]
        }
    },
    methods:{
        ...mapActions(['PostPoster','PostMovie','EditMovie','GetMoviesById']),
        ...mapActions({getActorsAction:'GetActors',getGenresAction:'GetGenres',getProducersAction:'GetProducers',getMoviesAction:'GetMovies'}),
        async submit()
        {
            if(this.$refs.movieForm.validate())
            {
                var selectedActorsId=this.selectedActors.map(actor=>actor.id);
                var  actorsId=selectedActorsId.join(' ');
                var selectedGenresId=this.selectedGenres.map(genre=>genre.id);
                var genresId=selectedGenresId.join(' ');
                this.movieResponse.name=this.title;
                this.movieResponse.actors=actorsId;
                this.movieResponse.genres=genresId;
                this.movieResponse.yearOfRelease=this.yearOfRelease;
                this.movieResponse.producerId=Number(this.selectedProducer.id);   
                this.movieResponse.plot=this.plot;
                var movieResponseForState={plot:this.plot,name:this.title,yearOfRelease:this.yearOfRelease,actors:this.selectedActors,genres:this.selectedGenres,producer:this.selectedProducer}
                try{
                    if(typeof this.file==='string')
                    {
                        this.movieResponse.poster=this.file;
                        movieResponseForState.poster=this.file;
                    }
                    else
                    {
                        var link=await this.PostPoster({data:this.file});
                        this.movieResponse.poster=link;
                        movieResponseForState.poster=link;
                    }
                    var id=this.$route.params.id
                    if(this.GetMovies==null || this.GetMovies.length==0)
                    {
                        await this.getMoviesAction();
                    }
                    if(id!=null)
                    {
                        movieResponseForState.id=Number(id);
                        await this.EditMovie({data:this.movieResponse,id:id,movieResponse:movieResponseForState});
                    }
                    else                   
                        await this.PostMovie({data:this.movieResponse,movieResponse:movieResponseForState});
                    this.$router.push({ path: '/' })
                }
                catch(err)
                {
                    this.handleError(err);
                }
            }
        },
        handleError(err)
        {
            this.error=true;
            this.message = err;
        },
        openProducerDialog()
        {
            this.producerDialog=true;
        },
        openActorDialog()
        {
            this.actorDialog=true;
        },
        addProducer(producerid)
        {
            var producer=this.producers.find(p=>p.id==producerid);
            this.selectedProducer=producer;
        },
        addActor(actorid)
        {
            var actor=this.actors.find(a=>a.id==actorid);
            this.selectedActors=[...this.selectedActors,actor];
        },
        addGenre(genreid)
        {
            var genre=this.genres.find(g=>g.id==genreid);
            this.selectedGenres=[...this.selectedGenres,genre];
        }
    },
    computed:
    {
        ...mapGetters(['GetProducers','GetActors','GetGenres','GetMovieById','GetMovies']),
        producers()
        {
            var p=this.GetProducers;
            return p;
        },
        actors()
        {
            return this.GetActors;
        },
        genres()
        {
            var p=this.GetGenres;
            return p;
        }
    },
    async created()
    {
        try{
            if(this.producers==null || this.producers.length==0)
            {
                await this.getActorsAction();
                await this.getGenresAction();
                await this.getProducersAction();
            }
            if(this.$route.params.id!=null)
            {
                this.show=false;
                this.movie={...await this.GetMoviesById({id:this.$route.params.id})};
                this.title=this.movie.name;
                this.file=this.movie.poster;
                this.selectedActors=this.movie.actors;
                this.selectedGenres=this.movie.genres;
                this.yearOfRelease=this.movie.yearOfRelease;
                this.selectedProducer=this.movie.producer;   
                this.plot=this.movie.plot;
            }
        }
        catch(err)
        {
            this.handleError(err);
        }
    }
 }
</script>

<style scoped>
.movie-form-page {
    max-width: 980px;
    padding-bottom: 58px;
    padding-top: 38px;
}

.form-hero {
    margin-bottom: 24px;
}

.form-hero h1 {
    color: #17181e;
    font-size: 2.45rem;
    font-weight: 800;
    line-height: 1.08;
    margin: 8px 0 10px;
}

.form-hero p {
    color: #706a62;
    font-size: 1rem;
    font-weight: 500;
    line-height: 1.7;
    margin: 0;
    max-width: 620px;
}

.form-card {
    padding: 30px;
}

.poster-preview {
    border-radius: 8px;
    max-height: 320px;
}

.submit-btn {
    background: #c52a49 !important;
    border-radius: 6px;
    color: #fff !important;
    font-weight: 800;
    height: 48px !important;
}

.secondary-btn {
    background: #15161c !important;
    border-radius: 6px;
    color: #fff !important;
    font-weight: 800;
}

@media (max-width: 760px) {
    .movie-form-page {
        padding-top: 22px;
    }

    .form-hero h1 {
        font-size: 2rem;
    }

    .form-card {
        padding: 20px;
    }
}
</style>
