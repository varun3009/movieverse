<template>
    <v-card class="movie-dialog">  
      <v-img
        class="dialog-poster"
        height="360"
        :src="movie.poster"
      >
        <div class="dialog-poster-copy">
          <span>{{ movie.yearOfRelease }}</span>
          <h2>{{movie.name}}</h2>
        </div>
      </v-img>
  
      <v-card-text class="dialog-copy">
        <div>{{movie.plot}}</div>
      </v-card-text>
    
      <v-divider class="mx-4"></v-divider>
    
      <v-card-title class="detail-heading">Actors</v-card-title>
    
      <v-card-text class="d-flex flex-row flex-wrap">
        <v-chip-group
          v-for="(actor,index) in movie.actors" :key="index"
        >
          <v-chip class="brand-chip">{{actor.name}}</v-chip>
        </v-chip-group>
      </v-card-text>
        
      <v-divider class="mx-4"></v-divider>
      
      <v-card-title class="detail-heading">Producer</v-card-title>
      <v-card-text>
        <v-chip class="brand-chip accent">{{movie.producer.name}}</v-chip>
      </v-card-text>
      <v-divider class="mx-4"></v-divider>
  
      <v-card-title class="detail-heading">Genres</v-card-title>
  
      <v-card-text class="d-flex flex-row flex-wrap">
        <v-chip-group
          v-for="(genre,index) in movie.genres" :key="index"
        >
        <v-chip class="brand-chip">{{genre.name}}</v-chip>
        </v-chip-group>
      </v-card-text>
  
      <v-card-actions class="dialog-actions">
        <v-spacer></v-spacer>
        <v-btn
          depressed
          class="close-btn"
          @click="close"
        >
          Close
        </v-btn>
      </v-card-actions>
    </v-card>
</template>

<script>
export default{
    props: {
        movie: {
            type: Object,
            required: true
        }
    },
    methods:
    {
        close() {
          this.$emit('closeDialog')
        },
        
    },
    
 }
</script>

<style scoped>
.movie-dialog {
  border-radius: 8px !important;
  overflow: hidden;
}

.dialog-poster {
  background: #15161c;
}

.dialog-poster-copy {
  align-items: flex-start;
  background: linear-gradient(180deg, transparent 0%, rgba(9, 10, 14, 0.9) 88%);
  display: flex;
  flex-direction: column;
  height: 100%;
  justify-content: flex-end;
  padding: 36px;
}

.dialog-poster-copy span {
  color: #f3c36a;
  font-size: 0.82rem;
  font-weight: 800;
}

.dialog-poster-copy h2 {
  color: #fff;
  font-size: 2.1rem;
  font-weight: 800;
  line-height: 1.08;
  margin: 6px 0 0;
}

.dialog-copy {
  color: #3e3d3b !important;
  font-size: 1rem;
  line-height: 1.7;
  padding: 26px 28px !important;
}

.detail-heading {
  color: #17181e;
  font-size: 1rem !important;
  font-weight: 800;
  padding-bottom: 6px !important;
}

.brand-chip {
  background: #f3eee6 !important;
  color: #312f2c !important;
  font-weight: 700;
}

.brand-chip.accent {
  background: #15161c !important;
  color: #fff !important;
}

.dialog-actions {
  padding: 10px 18px 18px !important;
}

.close-btn {
  background: #c52a49 !important;
  color: #fff !important;
  font-weight: 800;
}
</style>
