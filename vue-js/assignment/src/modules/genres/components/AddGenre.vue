<template>
    <v-dialog v-model="dialog">
        <template v-slot:activator="{ on, attrs }">
            <v-btn
             v-bind="attrs"
             v-on="on"
             block
             depressed
             class="support-btn"
            >
                Add Genre
            </v-btn>
        </template>
        <v-card class="support-dialog">
            <v-form ref="form" @submit.prevent>
                <v-container>
                    <v-row>
                        <v-col cols="12" class="dialog-title">
                            <h3>Add Genre</h3>
                        </v-col>
                        <v-col cols="12">
                            <v-text-field
                             v-model="name"
                             label="Name"
                             :rules="nameRules"
                             outlined
                             required    
                            />
                        </v-col>
                        <v-col cols="6">
                            <v-btn type="submit" @click="validate" class="submit-btn" depressed block>Add</v-btn>
                        </v-col>
                        <v-col cols="6">
                            <v-btn  @click="dialog=false" class='secondary-btn' depressed block>Close</v-btn>
                        </v-col>
                    </v-row>
                </v-container>
            </v-form>
        </v-card>
    </v-dialog>
</template>

<script>
import { mapActions,mapGetters } from 'vuex';

export default{
    data()
    {
        return{
            name:'',
            dialog:false,
            genre:{},
            nameRules:[
                value => {
                    if (value) return true
                    return 'Name is required.'
                },
                value=>
                {
                    if(value.match(/[A-Za-z.,]+/))
                        return true;
                    return 'enter valid name'
                }
                ],
        }
    },
    methods:
    {
        ...mapActions(['PostGenre']),
        async validate()
        {
            if(this.$refs.form.validate())
            {
                try{
                    this.genre.name=this.name;
                    var genreid=await this.PostGenre({data:{...this.genre}});
                    var temp=this.GetGenres;
                    this.$emit('selectGenre',genreid);

                }
                catch(err)
                {
                    this.dialog = false;
                    this.$emit('errorRaised',error);
                }
                this.$refs.form.reset();
                this.dialog = false;
            }
        }
    },
    computed:{
        ...mapGetters(['GetGenres'])
    }

}
</script>

<style scoped>
.support-btn {
    background: #15161c !important;
    border-radius: 6px;
    color: #fff !important;
    font-weight: 800;
    min-height: 42px;
}

.support-dialog {
    border-radius: 8px !important;
    padding: 12px;
}

.dialog-title h3 {
    color: #17181e;
    font-size: 1.2rem;
    font-weight: 800;
    margin: 0;
}

.submit-btn {
    background: #c52a49 !important;
    color: #fff !important;
    font-weight: 800;
}

.secondary-btn {
    background: #eee8df !important;
    color: #302d29 !important;
    font-weight: 800;
}
</style>
