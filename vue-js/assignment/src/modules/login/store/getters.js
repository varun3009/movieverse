function GetToken(state) {
    state.token = localStorage.getItem('user');
    return state.token;
}

export {
    GetToken
}