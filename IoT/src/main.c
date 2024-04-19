#include "wifi.h"
#include "tone.h"
#include "buttons.h"
#include "light.h"
#include "display.h"
#include "moisture.h"
#include "hc_sr04.h"
#include <util/delay.h>
#include <stdlib.h>
#include <string.h>
#include <stdio.h>
#include <Arduino.h>
#include "tcp_command_receiver.h"

int main() {
  moisture_init();
  hc_sr04_init();
  display_init();
  buttons_init();
  tone_init();
  wifi_init();

  wifi_command_join_AP("JOIIIN IOT", "bxww1482");
  tcp_listen("192.168.43.221", 23); // Listen for incoming messages

  while(1) {
    if (buttons_1_pressed()) {
      uint8_t message = "Hello from Arduino over LAN\n";
      wifi_command_TCP_transmit(message, strlen(message));
      _delay_ms(2000);
    }
    if (buttons_2_pressed()) {
      tone_play_mario();
    }
    if (buttons_3_pressed()) {
      uint16_t distance_value = hc_sr04_takeMeasurement();
      uint8_t moisture_value = moisture_read();
      char buffer[30];
      sprintf(buffer, "moisture: %d, distance: %d\n", moisture_value, distance_value);
      wifi_command_TCP_transmit(buffer, strlen(buffer));
    }
  }
  return 0;
}