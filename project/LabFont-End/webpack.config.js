const path = require('path');

module.exports = {
  entry: {
     app: './src/index.js',
     animation:"./src/animation.js"
  },
  output: {
    filename: '[name].min.js',
    path: path.resolve(__dirname, 'dist/js')
  }
};
