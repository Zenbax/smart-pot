#include "tone.h"
#include "includes.h"

#define BUZ_BIT PA7
#define BUZ_DDR DDRA
#define BUZ_PORT PORTA

void tone_init(){
BUZ_DDR|=(1<<BUZ_BIT);
}


void tone_play(uint16_t frequency, uint16_t duration) {


    // Calculate the half-period delay in microseconds
    uint16_t delay_us = 500000 / frequency;

    // Calculate the number of cycles needed for the specified duration
    uint16_t loop = (uint16_t) ((uint32_t) duration * 1000 / (2 * delay_us));



    // Initialize Timer2 in normal mode
    TCCR2A = 0;
    TCCR2B = 0;

    uint8_t prescaler_bits = 0;
    uint16_t prescaler_value = 0;

    // Choose prescaler based on delay
    if (delay_us > 4000) {
        prescaler_bits = (1 << CS22) | (1 << CS21) | (1 << CS20); // 1024
        prescaler_value = 1024;
    } else if (delay_us > 2000) {
        prescaler_bits = (1 << CS22) | (1 << CS21); // 256
        prescaler_value = 256;
    } else if (delay_us > 1000) {
        prescaler_bits = (1 << CS22) | (1 << CS20); // 128
        prescaler_value = 128;
    } else if (delay_us >500) {
        prescaler_bits = (1 << CS22); // 64
        prescaler_value = 64;
    } else if (delay_us >125) {
        prescaler_bits = (1 << CS21)| (1 << CS20); //32
        prescaler_value = 32;
    }
    else
     {
       prescaler_bits = (1 << CS21) ; // 8
        prescaler_value = 8;
     }

    // Set the prescaler
    TCCR2B = prescaler_bits;

    // Calculate the number of timer ticks needed for the specified delay
    uint8_t num_ticks = (F_CPU / 1000000UL) * delay_us / prescaler_value;




    // Generate the tone
    for (uint16_t i = 0; i < loop; i++) {
        // Set PA1 high
        BUZ_PORT |= (1 << BUZ_BIT);
            // Reset the timer counter
    TCNT2 = 0;

    // Wait until the timer counter reaches the required ticks
    while (TCNT2 < num_ticks) {
        // Busy-wait
    }

        // Set PA1 low
        BUZ_PORT &= ~(1 << BUZ_BIT);
            // Reset the timer counter
    TCNT2 = 0;

    // Wait until the timer counter reaches the required ticks
    while (TCNT2 < num_ticks) {
        // Busy-wait
    }
    }

    TCCR2B = 0;
}


void tone_play_starwars()
{

    tone_play(392, 500);  // G4 for 500 ms
    tone_play(392, 500);  // G4 for 500 ms
    tone_play(392, 500);  // G4 for 500 ms
    tone_play(311, 350);  // E♭4 for 350 ms
    tone_play(466, 150);  // B4 for 150 ms
    tone_play(392, 500);  // G4 for 500 ms
    tone_play(311, 350);  // E♭4 for 350 ms
    tone_play(466, 150);  // B4 for 150 ms
    tone_play(392, 1000); // G4 for 1000 ms

    tone_play(587, 500);  // D5 for 500 ms
    tone_play(587, 500);  // D5 for 500 ms
    tone_play(587, 500);  // D5 for 500 ms
    tone_play(622, 350);  // D#5 for 350 ms
    tone_play(466, 150);  // B4 for 150 ms
    tone_play(370, 500);  // F#4 for 500 ms
    tone_play(311, 350);  // Eb4 for 350 ms
    tone_play(466, 150);  // B4 for 150 ms
    tone_play(392, 1000); // G4 for 1000 ms
}



#define NOTE_B0  31
#define NOTE_C1  33
#define NOTE_CS1 35
#define NOTE_D1  37
#define NOTE_DS1 39
#define NOTE_E1  41
#define NOTE_F1  44
#define NOTE_FS1 46
#define NOTE_G1  49
#define NOTE_GS1 52
#define NOTE_A1  55
#define NOTE_AS1 58
#define NOTE_B1  62
#define NOTE_C2  65
#define NOTE_CS2 69
#define NOTE_D2  73
#define NOTE_DS2 78
#define NOTE_E2  82
#define NOTE_F2  87
#define NOTE_FS2 93
#define NOTE_G2  98
#define NOTE_GS2 104
#define NOTE_A2  110
#define NOTE_AS2 117
#define NOTE_B2  123
#define NOTE_C3  131
#define NOTE_CS3 139
#define NOTE_D3  147
#define NOTE_DS3 156
#define NOTE_E3  165
#define NOTE_F3  175
#define NOTE_FS3 185
#define NOTE_G3  196
#define NOTE_GS3 208
#define NOTE_A3  220
#define NOTE_AS3 233
#define NOTE_B3  247
#define NOTE_C4  262
#define NOTE_CS4 277
#define NOTE_D4  294
#define NOTE_DS4 311
#define NOTE_E4  330
#define NOTE_F4  349
#define NOTE_FS4 370
#define NOTE_G4  392
#define NOTE_GS4 415
#define NOTE_A4  440
#define NOTE_AS4 466
#define NOTE_B4  494
#define NOTE_C5  523
#define NOTE_CS5 554
#define NOTE_D5  587
#define NOTE_DS5 622
#define NOTE_E5  659
#define NOTE_F5  698
#define NOTE_FS5 740
#define NOTE_G5  784
#define NOTE_GS5 831
#define NOTE_A5  880
#define NOTE_AS5 932
#define NOTE_B5  988
#define NOTE_C6  1047
#define NOTE_CS6 1109
#define NOTE_D6  1175
#define NOTE_DS6 1245
#define NOTE_E6  1319
#define NOTE_F6  1397
#define NOTE_FS6 1480
#define NOTE_G6  1568
#define NOTE_GS6 1661
#define NOTE_A6  1760
#define NOTE_AS6 1865
#define NOTE_B6  1976
#define NOTE_C7  2093
#define NOTE_CS7 2217
#define NOTE_D7  2349
#define NOTE_DS7 2489
#define NOTE_E7  2637
#define NOTE_F7  2794
#define NOTE_FS7 2960
#define NOTE_G7  3136
#define NOTE_GS7 3322
#define NOTE_A7  3520
#define NOTE_AS7 3729
#define NOTE_B7  3951
#define NOTE_C8  4186
#define NOTE_CS8 4435
#define NOTE_D8  4699
#define NOTE_DS8 4978

int melody[] = {
  NOTE_E7, NOTE_E7, 0, NOTE_E7,
  0, NOTE_C7, NOTE_E7, 0,
  NOTE_G7, 0, 0,  0,
  NOTE_G6, 0, 0, 0,

  NOTE_C7, 0, 0, NOTE_G6,
  0, 0, NOTE_E6, 0,
  0, NOTE_A6, 0, NOTE_B6,
  0, NOTE_AS6, NOTE_A6, 0,

  NOTE_G6, NOTE_E7, NOTE_G7,
  NOTE_A7, 0, NOTE_F7, NOTE_G7,
  0, NOTE_E7, 0, NOTE_C7,
  NOTE_D7, NOTE_B6, 0, 0,

  NOTE_C7, 0, 0, NOTE_G6,
  0, 0, NOTE_E6, 0,
  0, NOTE_A6, 0, NOTE_B6,
  0, NOTE_AS6, NOTE_A6, 0,

  NOTE_G6, NOTE_E7, NOTE_G7,
  NOTE_A7, 0, NOTE_F7, NOTE_G7,
  0, NOTE_E7, 0, NOTE_C7,
  NOTE_D7, NOTE_B6, 0, 0
};
//Mario main them tempo
int tempo[] = {
  12, 12, 12, 12,
  12, 12, 12, 12,
  12, 12, 12, 12,
  12, 12, 12, 12,

  12, 12, 12, 12,
  12, 12, 12, 12,
  12, 12, 12, 12,
  12, 12, 12, 12,

  9, 9, 9,
  12, 12, 12, 12,
  12, 12, 12, 12,
  12, 12, 12, 12,

  12, 12, 12, 12,
  12, 12, 12, 12,
  12, 12, 12, 12,
  12, 12, 12, 12,

  9, 9, 9,
  12, 12, 12, 12,
  12, 12, 12, 12,
  12, 12, 12, 12,
};


void tone_play_mario() {
  // tone_play(NOTE_E4, 200);
  // tone_play(NOTE_E4, 200);
  // tone_play(NOTE_E4, 200);
  // tone_play(NOTE_C4, 200);
  // tone_play(NOTE_E4, 200);
  // tone_play(NOTE_G4, 200);
  // tone_play(NOTE_G3, 200);

  tone_play(NOTE_C4, 200);
  tone_play(NOTE_G3, 200);
  tone_play(NOTE_E3, 200);
  tone_play(NOTE_A3, 200);
  tone_play(NOTE_B3, 200);
  tone_play(NOTE_AS3, 200);
  tone_play(NOTE_A3, 200);
  tone_play(NOTE_G3, 200);
  tone_play(NOTE_E4, 200);
  tone_play(NOTE_G4, 200);
  tone_play(NOTE_A4, 200);
  tone_play(NOTE_F4, 200);
  tone_play(NOTE_G4, 200);
  tone_play(NOTE_E4, 200);
  tone_play(NOTE_C4, 200);
  tone_play(NOTE_D4, 200);
  tone_play(NOTE_B3, 200);
}