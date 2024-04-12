#include "wifi.h"
#include "tone.h"
#include "buttons.h"
#include "light.h"
#include "display.h"
#include <util/delay.h>
#include <stdlib.h>
#include <string.h>
#include <stdio.h>
#include <Arduino.h>
#include "moisture.h"

char messageBuffer[256];

typedef struct {
  char name[16];
  char params[240];
  int is_valid;
} tcp_command_t;

tcp_command_t pot_extract_command() {
  tcp_command_t command;
  command.is_valid = 0;

  char *comma_position = strchr(messageBuffer, ',');
  if (comma_position == NULL) {
    comma_position = 0;
  }
  
  size_t name_length = comma_position - messageBuffer;
  if (name_length >= sizeof(command.name)) {
    return command;
  }

  strncpy(command.name, messageBuffer, name_length);
  command.name[name_length] = '\0';

  size_t params_length = strlen(comma_position + 1);
  if (! (params_length >= sizeof(command.params))) {
    strcpy(command.params, comma_position + 1);
  }
  
  command.is_valid = 1;
  return command;
}
void pot_print_command(tcp_command_t *command) {
  printf("Name: %s\n", command->name);
  printf("Params: %s\n", command->params);
  printf("Valid: %d\n", command->is_valid);
}

void tcp_play_command(int song, int repeat, char *album) {
  if (song == 1 && strcmp(album, "Star Wars") == 0) {
    wifi_command_TCP_transmit("PLAYING: Star Wars Theme\n", 25);
    tone_play_starwars();
  } else if (song == 1) {
    wifi_command_TCP_transmit("PLAYING: Super Mario Theme\n", 27);
    tone_play_mario();
  } else {
    tone_play(200, 1000);
  }
}

void tcp_callback() {
  tcp_command_t command = pot_extract_command();
  if (command.is_valid == 0) {
    wifi_command_TCP_transmit("INVALID COMMAND\n", 16);
    return;
  }

  if (strcmp(command.name, "play") == 0) {
    wifi_command_TCP_transmit("EXECUTING: play\n", 16);

    char *songToken = strtok(command.params, ",");
    char *repeatToken = strtok(NULL, ",");
    char *album = strtok(NULL, ",");
    if (songToken == NULL || repeatToken == NULL || album == NULL) {
      wifi_command_TCP_transmit("Missing parameters\n", 20);
      return;
    }
    int song = atoi(songToken);
    int repeat = atoi(repeatToken);
    wifi_command_TCP_transmit("PARAMETERS: Ok\n", 16);

    tcp_play_command(song, repeat, album);
  } else if (strcmp(command.name, "moisture") == 0) {
    uint16_t *value = moisture_read();
    char buffer[10];
    sprintf(buffer, "%d", value);

    wifi_command_TCP_transmit(buffer, strlen(buffer));
  } else {
    wifi_command_TCP_transmit("NO SUCH COMMAND\n", 16);
  }
}


int main() {
  // strcpy(messageBuffer, "play,1,0,Star Wars");
  // tcp_command_t command = tcp_get_command(messageBuffer);
  // tcp_print_command(&command);
  // tcp_callback();

  // return 0;
  moisture_init();
  tone_init();
  wifi_init();
  display_init();
  buttons_init();

  wifi_command_join_AP("JOIIIN IOT", "bxww1482");
  wifi_command_create_TCP_connection("192.168.43.221", 23, &tcp_callback, &messageBuffer);

  while(1) {
    if (buttons_1_pressed()) {
      wifi_command_TCP_transmit("Hello from Arduino over LAN\n", 28);
      _delay_ms(2000);
    }
    if (buttons_2_pressed()) {
      tone_play_mario();
    }
    if (buttons_3_pressed()) {
      uint16_t value = moisture_read();
      char buffer[18];
      sprintf(buffer, "%d", value);

      wifi_command_TCP_transmit(buffer, strlen(buffer));
    }
  }
  return 0;
}