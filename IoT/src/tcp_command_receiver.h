#pragma once

typedef struct {
  char name[16];
  char params[240];
  int is_valid;
} tcp_command_t;

int tcp_listen(char *ip, int port);