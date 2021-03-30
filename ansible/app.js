var express = require('express');
var app = express();
const util = require('util');
const exec = util.promisify(require('child_process').exec);

app.post('/', function(req, res){
    try {
        console.log('executing deployment...');
        exec("ansible-pull -U git@github.com:<edangolgithub>/ansible.git playbook.yml", (error, stdout, stderr) => {
                if (error) {
                console.log(`error: ${error.message}`);
                return;
        }
        if (stderr) {
                console.log(`stderr: ${stderr}`);
                return;
        }

        console.log(`stdout: ${stdout}`);

        });
    } catch (e) {
        console.log(e);
    }

    res.json({ received: true });
});

app.listen(8080, '127.0.0.1');
ssh-rsa
AAAAB3Nz3UKE32AymMFYJt1X79xBSoRpYUQmyUclX4acqBs1xfkHJBW0OOyA7haQmF487Kuc5fnGroCDxkzRg6TJH0INPFJOYtXNEWIffUg3eQwFPZbWU6TBBXL8haYUFLJ+I8FH8WcyW4EPEhofcWcv4ck+j348A8rnm+VBlaUDv6qWHQ/QRsfhHH3tGMo7iXsdL/JjcQNVMXbi/ijYsHEePhloCnxD0K7re+1Fd1B5IYuUkosQUSBrOIYL4lMrdoTukHhonjAHr57HSl4fZ8xp1VEcxRqy11UMb7YtX/Y2wCKZWXLh9UIAn4n6sJZH57Dmy8qT2w04GetrUCjcaeeosrDm804Ajt14HlVkOf0funwUWFjwCi45uKNuvNLa6xT4iLTJ7UbWGTtM5yI9dHFsBMn3tpPLey1bpWs/JcDDNtad78N6N3KYtgpIwUnqZbaXcaxulT3KtYVvCfn6ss00Ha9TwD0htSkZ13IJu2z+RV/6+9wstcQAYPaYPxbiH3DVmBUZ8yI0ekZMqwTRE9lOCidqGNb7B5Tax8QoifB3tPewWMVqal1chmzVzKbNKGAEkzAjDxSGM4PtZ6hTQUqKYJPwMSyeeM2EXq8w9g10pFRw8C2z+Et0+W++mzSpn+MdimyRmQlOmrtPNFB0NQQV2tvYeF95Qhjr2dULeLSU1Ot4NyhwY4ek85rcvJH0QqgRWZcuuWnmiJaQ== dangolevan@gmail.com