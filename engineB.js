// motor B in the L298N
// L298N Dual H Bridge  --> specs: https://www.sparkfun.com/datasheets/Robotics/L298_H_Bridge.pdf
// GPIO 5 & 6 -> IN 3  & 4


var Gpio = require('onoff').Gpio; //include onoff to interact with the GPIO
var LED5 = new Gpio(5, 'out'); //use GPIO pin 4, and specify that it is output
var LED6 = new Gpio(6, 'out'); //use GPIO pin 4, and specify that it is output
var blinkInterval = setInterval(blinkLED, 250); //run the blinkLED function every 250ms

function blinkLED() { //function to start blinking
  if (LED5.readSync() === 0) { //check the pin state, if the state is 0 (or off)
    LED5.writeSync(1); 
    LED6.writeSync(0); 
  } else {
    LED5.writeSync(0); 
    LED6.writeSync(1); 
  }
}

function endBlink() { //function to stop blinking
  clearInterval(blinkInterval); // Stop blink intervals
  LED5.writeSync(0); // Turn LED off
  LED5.unexport(); // Unexport GPIO to free resources

  LED6.writeSync(0); // Turn LED off
  LED6.unexport(); // Unexport GPIO to free resources
}

setTimeout(endBlink, 5000); //stop blinking after 5 seconds 