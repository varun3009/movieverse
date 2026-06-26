import * as actions from './actions'
import * as mutations from './mutations'
import * as getters from './getters'

export default{
    state:{
        token:null,
        isAuthenticated:false,
    },
    actions,
    mutations,
    getters,
}