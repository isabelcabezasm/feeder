const raspi = require('raspi');
const pwm = require('raspi-pwm');

raspi.init(() => {

    
    const led3 = new pwm.PWM('P1-12');

    
   while(true)
   {
        var lum = 0;
        for ( i=0; i<100; i++){
            led3.write(lum);
            lum = lum+0.01;

            
        }
        lum = 1;
        for ( i=0; i<100; i++){
            led3.write(lum);
            lum = lum-0.01;
        }
    }
   
});