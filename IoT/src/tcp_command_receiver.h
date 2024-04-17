#pragma once

typedef struct {
  char name[16];
  char params[240];
  int is_valid;
} tcp_command_t;

tcp_command_t tcp_extract_command();
void tcp_play_command(int song, int repeat, char *album);
void tcp_callback();
int tcp_listen(char *ip, int port);