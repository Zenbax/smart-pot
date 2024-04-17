#include "tcp_command_receiver.h"
#include "wifi.h"
#include <stdio.h>
#include <string.h>
#include <stdint.h>

char messageBuffer[256];

tcp_command_t tcp_extract_command() {
  
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
  tcp_command_t command = tcp_extract_command();
  if (command.is_valid == 0) {
    uint8_t *message = "INVALID COMMAND\n";
    wifi_command_TCP_transmit(message, strlen(message));
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
int tcp_listen(char *ip, int port) {
  WIFI_ERROR_MESSAGE_t error = wifi_command_create_TCP_connection(ip, port, &tcp_callback, &messageBuffer);

  return error == WIFI_OK;
}