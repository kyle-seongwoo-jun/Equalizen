var mongoose = require('mongoose');
var userSchema = new mongoose.Schema({
    name : String,
    id : String,
    password : String
});

module.exports = mongoose.model('User',userSchema);