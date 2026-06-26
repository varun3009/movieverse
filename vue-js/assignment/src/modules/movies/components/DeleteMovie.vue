<template>
  <v-dialog v-model="dialog" max-width="374px">
    <template v-slot:activator="{ on, attrs }">
        <v-btn
          icon
          class="delete-trigger"
          v-bind="attrs"
          v-on="on"
          title="Delete movie"
        >
          <span class="material-icons">delete</span>
        </v-btn>
    </template>
    <v-card class="confirm-card">
        <v-card-title class="confirm-title">
          Delete Movie
        </v-card-title>
          <v-card-text class="confirm-copy">
              Are you sure you want to delete this movie?
          </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
            color="Secondary"
            text
            @click="dialog = false"
          >
            Cancel
          </v-btn>
          <v-btn
            color="#c52a49"
            text
            @click="deleteMovie"
          >
            Delete
          </v-btn>
        </v-card-actions>
      </v-card>
  </v-dialog>
</template>

<script>
export default {
    data()
    {
        return{
            dialog: false,
        }
    },
    methods:{
        deleteMovie()
        {
            this.$emit('deleteMovie');
            this.dialog = false;
        }
    }
}
</script>

<style scoped>
.delete-trigger {
  color: #7b7369 !important;
}

.delete-trigger:hover {
  color: #c52a49 !important;
}

.confirm-card {
  border-radius: 8px !important;
}

.confirm-title {
  color: #17181e;
  font-weight: 800;
}

.confirm-copy {
  color: #5f5a54;
  font-weight: 500;
  padding-top: 18px !important;
}
</style>
