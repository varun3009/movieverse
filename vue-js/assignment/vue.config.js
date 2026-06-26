module.exports = {
    devServer: {
      proxy: {
        '^/users': {
          target: 'http://localhost:59919/',
          ws: true,
          changeOrigin: true
        },
      }
    }
  }
