require("dotenv").config();
//Azure
var Protocol = require('azure-iot-device-mqtt').Mqtt;
var Client = require('azure-iot-device').Client;

//Hardware
const Gpio = require('onoff').Gpio; //include onoff to interact with the GPIO
const raspi = require('raspi');
const pwm = require('raspi-pwm');

//GPIO to control the motor
var LED5 = new Gpio(5, 'out'); //use GPIO  5, and specify that it is output
var LED12 = new pwm.PWM('P1-12'); // GPIO 12 is the one that supports PWM


// Azure connection
var connectionString = process.env.DEVICE_CONNECTION_STRING;
if (!connectionString) {
  console.log('Please set the DEVICE_CONNECTION_STRING environment variable.');
  process.exit(-1);
}

var client = Client.fromConnectionString(connectionString, Protocol);


client.open(function (err) {

    if (err) {
      console.error(err.toString());
      process.exit(-1);
    } else {
      console.log('client successfully connected');
      client.on('error', function (err) {
        console.error(err.toString());
        process.exit(-1);
      });
    }



        // register handler for 'spinmotor'
        client.onDeviceMethod('spinmotor', function (request, response) {
            console.log('received a request for spinmotor');

            /*** Let's spin the motor */
             MotorON();
             console.log('spinning motor');
             setTimeout(MotorOFF, 3000); //run the motorON function in three seconds


            console.log(JSON.stringify(request.payload, null, 2));
            var fakeResponsePayload = {
                key: 'value'
            };

            response.send(200, fakeResponsePayload, function (err) {
                if (err) {
                console.error('Unable to send method response: ' + err.toString());
                process.exit(-1);
                } else {
                console.log('response to spinmotor sent.');
                //process.exit(0);
                }
            });

        });
    

});  


function MotorON() { //function to spin
      LED5.writeSync(0);
      LED12.write(0.5);      
}

function MotorOFF(){
        LED5.writeSync(0); 
        LED12.write(0);
    
}