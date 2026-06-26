function Login(state,data) {
    console.log('Login mutation called with token:', data);
    console.log('Current state before mutation:', state);
    state.token = data;
    state.isAuthenticated = true;
}

function Logout(state) {
    state.token = null;
    state.isAuthenticated = false;
    localStorage.removeItem('user');
}

export {
    Login,
    Logout
}


