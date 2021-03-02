const express = require("express");

const app = express();
//app.set("view engine", "ejs");
app.set("view engine","vash")
app.set("views", __dirname + "/views");
app.use(express.static('public'))

app.listen(3000, () => { 
  console.log("Server started (http://localhost:3000/) !");
});


app.get("/", (req, res) => { 
 // res.send ("Hello world...");
 var t= __dirname + "/views";
 console.log(t);
 res.render("index");
});
app.get("/vash", (req, res) => { 
  
  res.render("index",{title:'vash template',content:'vash content'});
 });