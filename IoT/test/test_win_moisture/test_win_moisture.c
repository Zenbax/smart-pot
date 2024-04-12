#include "../fff.h"
#include "../unity.h"

#include "moisture.h"


#include <stdio.h>
#include <stdint.h>

// variables used in moisture.c
uint8_t ADMUX;
uint8_t ADCSRA;
uint8_t ADCSRB;
uint8_t ADCL;
uint8_t ADCH;
uint8_t DIDR2;
uint8_t PORTK;
uint8_t DDRK;

DEFINE_FFF_GLOBALS

void setUp(void)
{
  
}
void tearDown(void) {}


void test_correct_moisture_driver_initialization()
{
  moisture_init();

  TEST_ASSERT_EQUAL(65,ADMUX);

}






// Test that it sendst stuff nonBlocking. 

int main(void)
{
  UNITY_BEGIN();
  RUN_TEST(test_correct_moisture_driver_initialization);

  return UNITY_END();
}