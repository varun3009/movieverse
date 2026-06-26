<template >
    <v-dialog v-model="dialog">
        <template v-slot:activator="{ on, attrs }">
            <v-btn
             v-bind="attrs"
             v-on="on"
             block
             depressed
             class="support-btn"
            >
                Add {{ isActor?' Actor':' Producer' }} 
            </v-btn>
        </template>
        <v-card class="support-dialog">
            <v-form ref="form" @submit.prevent>
                <v-container>
                    <v-row>
                        <v-col cols="12" class="dialog-title">
                            <slot name="title"></slot>
                        </v-col>
                        <v-col cols="12">
                            <v-text-field
                             v-model="username"
                             label="Username"
                             :rules="nameRules"
                             outlined
                             required    
                            />
                        </v-col>
                        <v-col cols="12">
                            <v-menu >
                                <template v-slot:activator="{on}">
                                    <v-text-field
                                     v-model="DOB"
                                     v-on="on"
                                     label="DOB"
                                     outlined
                                     :rules="[v => !!v || 'DOB is Required']"
                                    ></v-text-field>
                                </template>
                                <v-date-picker v-model="DOB" :max="getMaxDate"></v-date-picker>
                            </v-menu>
                        </v-col>
                        <v-col cols="12">
                            <v-radio-group
                             v-model="gender"
                             row
                             class="d-sm-flex"
                             mandatory
                            >
                                <template v-slot:label>
                                    <div>Gender</div>
                                </template>
                                <v-radio v-for="(value,index) in availableGenders" :key="index" :label="value" :value="value">

                                </v-radio>
                            </v-radio-group>
                        </v-col>
                        <v-col cols="12">
                            <v-textarea
                             solo
                             outlined
                             v-model="bio"
                             name="input-7-4"
                             label="Bio"
                             :rules="bioRules"
                            ></v-textarea>
                        </v-col>
                        <v-col cols="6">
                            <v-btn type="submit"  class="submit-btn" @click="validate" depressed block>Add</v-btn>
                        </v-col>
                        <v-col cols="6">
                            <v-btn  @click="dialog=false" class="secondary-btn" depressed block>Close</v-btn>
                        </v-col>
                    </v-row>
                </v-container>
            </v-form>
        </v-card>
    </v-dialog>
</template>
<script>
import { mapActions,mapGetters } from 'vuex';
    export default {
        props:
        {
            isActor:{
                type:Boolean
            }
        },
        data()
        {
            return{
                availableGenders:['male','female','others'],
                dialog:false,
                username:'',
                date:'',
                bio:'',
                lastname:'',
                DOB:'',
                gender:'',
                person:{},
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
                dateRules:[
                value => {
                    if(value) return true;
                    return 'Date is required.'
                }],
                bioRules:[
                value => {
                    if (value) return true
                    return 'Bio is required.'
                },
                value => {
                    if(value.match(/[A-Za-z.,]+/))
                        return true;
                    return 'enter valid BIO'
                }]
            }
        },
        computed:
        {
            ...mapGetters(['GetActors','GetProducers']),
            getMaxDate()
            {
                var maxdate=new Date();
                var month=maxdate.getMonth()+1;
                var day=maxdate.getDate();
                var year=maxdate.getFullYear();
                if(month<10)
                month='0'+month;
                if(day<10)
                day='0'+day;
                return year+'-'+month+'-'+day;
            }
        },
        methods:
        {
            ...mapActions(['PostActor','PostProducer']),
            async validate()
            {
                if(this.$refs.form.validate())
                {
                    try{
                        this.person['name']=this.username;
                        this.person['dob']=this.DOB;
                        this.person['bio']=this.bio;
                        this.person['sex']=this.gender;
                        if(this.isActor)
                        {
                            var actorid=await this.PostActor({data:{...this.person}});
                            var temp=this.GetActors;
                            this.$emit('selectActor',actorid);
                        }
                        else
                        {
                            var producerid=await this.PostProducer({data:{...this.person}});
                            var temp=this.GetProducers;         
                            this.$emit('selectProducer',producerid);
                        }
                        this.$refs.form.reset();
                        this.dialog = false;
                    }
                    catch(error)
                    {
                        this.dialog = false;
                        this.$emit('errorRaised',error);
                    }
                }
            }
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

.dialog-title {
    color: #17181e;
    justify-content: flex-start !important;
}

.dialog-title h3 {
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
