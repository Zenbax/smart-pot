#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "cJSON.h" 


cJSON* rawDataToJSON(const char* key, const char* value) {
    // Create a cJSON object
    cJSON* json = cJSON_CreateObject();
    // Add key-value pair to JSON
    cJSON_AddStringToObject(json, key, value);

    return json;
}