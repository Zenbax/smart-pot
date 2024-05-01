#include "includes.h"
#include "moisture.h"
#include <stdint.h>
#include "display.h"

// MOISTURE_ANALOG_PIN A9

void moisture_init() {
  // Set 100001 for choosing ADC9
  ADMUX = (1 << REFS0) | (1 << MUX0);
  ADCSRB = (1 << MUX5);

  DIDR2 = (1 << ADC9D);
  
  ADCSRA = (1 << ADEN);

  /*
  SOURCE REFERENCE: 
    https://content.arduino.cc/assets/ATmega640-1280-1281-2560-2561-Datasheet-DS40002211A.pdf

  ADMUX
    REFS0 - Select voltage reference for ADC 
    MUX0 - Set least significant bit to 1 

  ADCSRB
    MUX5 - Set most significant bit to 1

  WHEN ADMUX AND ADCSRB IS RUN IT WILL SET THE MUX VALUE TO 
  100001 (MUX5 and MUX0) such that the MUX points at ADC9 PK1
  on the arduino board

  DIDR2
    ADC9D - Disable something

  ADCSRA
    ADEN - Enables ADC

  */

}

uint8_t moisture_read() {
  // Start the ADC conversion
  ADCSRA |= (1 << ADSC);
  
  // Wait for the conversion to complete
  // ADSC WILL READ AS ONE WHILE CONVERSION IS IN PROGRESS
  while (ADCSRA & (1 << ADSC)); // Wait until ADSC is cleared
  
  // Read the ADC result (16-bit value)
  // ADD THE LOW BYTE AND HIGH BYTE (SHIFT POSITION BY 8 TO LEFT)
  uint16_t adc_value = ADCL;
  adc_value |= (ADCH << 8);

  uint8_t moisture_percentage = (1 - ((float)adc_value / 1023.0)) * 100;

  return moisture_percentage;
}