
/*
Extracts command signatures from a message buffer into an array
"play,1,Star Wars;setup,4"
["play,1,Star Wars", "setup,4"]
*/
/*
void extract_command_signatures(char *input, char **output, int *count) {
  char *token = strtok(input, ";");
  int i = 0;
  while (token != NULL) {
    output[i] = strdup(token);
    token = strtok(NULL, ";");
    i++;
  }
  *count = i;
}
*/


// void tcp_callback() {
//   char *token = strtok(messageBuffer, ",");

//   if (token == NULL) {
//     wifi_command_TCP_transmit("No such command\n", 17);
//     return;
//   }
//   char command[16];
//   strncpy(command, token, sizeof(command));
//   command[sizeof(command) - 1] = "\0";

//   if (strcmp(command, "play") == 0) {
//     char *second_part = strtok(NULL, ",");
//     if (second_part == NULL) {
//       wifi_command_TCP_transmit("Missing parameter\n", 19);
//       return;
//     }
    
//     int param = atoi(second_part);
//     if (param == 1) {
//       wifi_command_TCP_transmit("Playing: Star Wars Theme\n", 26);
//       tone_play_starwars();
//     } else if (param == 2) {
//       wifi_command_TCP_transmit("Playing: Super Mario Theme\n", 28);
//       tone_play_mario();
//     } else {
//       wifi_command_TCP_transmit("Could not find track\n", 22);
//       tone_play(200, 1000);
//     }
//   }

//   return;
// }