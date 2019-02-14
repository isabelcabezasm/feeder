// motor B in the L298N
// L298N Dual H Bridge  --> specs: https://www.sparkfun.com/datasheets/Robotics/L298_H_Bridge.pdf
// IN 4  --> GPIO 12 (PWM)


const Gpio = require('onoff').Gpio; //include onoff to interact with the GPIO
const raspi = require('raspi');
const pwm = require('raspi-pwm');



var LED5 = new Gpio(5, 'out'); //use GPIO  5, and specify that it is output
var LED12 = new pwm.PWM('P1-12'); // GPIO 12 is the one that supports PWM


var blinkInterval = setInterval(blinkLED, 3000); //run the blinkLED function every 250ms

function blinkLED() { //function to start blinking

  if (LED5.readSync() === 0) { //check the pin state, if the state is 0 (or off)
    //here the movement is faster
    LED5.writeSync(1);
    LED12.write(0);

  } else {
    //Here the movement is slower
    LED5.writeSync(0); 
    LED12.write(0.5);
  }

}

function endBlink() { //function to stop blinking
  clearInterval(blinkInterval); // Stop blink intervals
  LED5.writeSync(0); // Turn LED off
  LED5.unexport(); // Unexport GPIO to free resources

  LED12.write(0); // Turn LED off
}

setTimeout(endBlink, 10000); //stop blinking after 5 seconds 