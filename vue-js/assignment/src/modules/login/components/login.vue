<template>
    <v-container class="auth-form">
    <v-snackbar
    v-model="error"
    :timeout="timeout"
    top
    >
        {{ message }}
    </v-snackbar>
    <v-form ref="form" v-model="valid" lazy-validation>
    <v-text-field
      v-model="email"
      :rules="emailRules"
      label="E-mail"
      outlined
      required/>
    <v-text-field
      v-model="password"
        :rules="passwordRules"
        label="Password"
        type="password"
        outlined
        required/>
        <v-row class="d-flex justify-center pb-2">
    <v-btn
      :disabled="!valid"
      depressed
      block
      class="submit-btn"
      @click="submit()">Submit</v-btn>
      </v-row>
    </v-form>
    </v-container>
</template>
<script>
import { ref } from 'vue'
import Vuetify from 'vuetify'
import { mapActions } from 'vuex';
export default {
    data()
    {
        return {
            valid: false,
            email: '',
            emailRules: [
                v => !!v || 'E-mail is required',
                v => /.+@.+\..+/.test(v) || 'E-mail must be valid',
            ],
            password: '',
            passwordRules: [
                v => !!v || 'Password is required',
                v => v.length >= 4 || 'Password must be at least 4 characters',
            ],
            message:'',
            error:false,
            timeout:3000
        }
    },
    methods: {
        ...mapActions({
            login:'Login'
        }),
        async submit () {
            try{
                if (this.$refs.form.validate()) {
                    await this.login({data:{Email:this.email,Password:this.password}});
                    this.$router.push({ path: '/' })
                }
            }
            catch(err)
            {
                console.log(err);
                this.error=true;
                this.message='unable to login, check your credentials';
            }
        },
    }
}
</script>

<style scoped>
.auth-form {
    padding: 0 !important;
}

.submit-btn {
    background: #c52a49 !important;
    border-radius: 6px;
    color: #fff !important;
    font-weight: 800;
    height: 46px !important;
}
</style>
