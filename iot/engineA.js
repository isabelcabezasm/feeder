// engine A
// IN 1, 2
// GPIO 5 & 6

var Gpio = require('onoff').Gpio; //include onoff to interact with the GPIO
var LED27 = new Gpio(27, 'out'); //use GPIO pin 27, and specify that it is output
var LED22 = new Gpio(22, 'out'); //use GPIO pin 28, and specify that it is output
var blinkInterval = setInterval(blinkLED, 250); //run the blinkLED function every 250ms

function blinkLED() { //function to start blinking
  if (LED27.readSync() === 0) { //check the pin state, if the state is 0 (or off)
    LED27.writeSync(1); 
    LED22.writeSync(0); 
  } else {
    LED27.writeSync(0); 
    LED22.writeSync(1); 
  }
}

function endBlink() { //function to stop blinking
  clearInterval(blinkInterval); // Stop blink intervals
  LED27.writeSync(0); // Turn LED off
  LED27.unexport(); // Unexport GPIO to free resources

  LED22.writeSync(0); // Turn LED off
  LED22.unexport(); // Unexport GPIO to free resources
}

setTimeout(endBlink, 5000); //stop blinking after 5 seconds 