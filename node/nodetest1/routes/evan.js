var express = require('express');
var router = express.Router();

/* GET home page. */
router.get('/', function(req, res, next) {
  res.render('evan', { title: 'evan' });
});
router.get('/a', function(req, res, next) {
    res.send('respond with a resource');
  });
  router.get('/b', function(req, res, next) {
    res.render('b', { title: 'evan' });
  });

module.exports = router;
