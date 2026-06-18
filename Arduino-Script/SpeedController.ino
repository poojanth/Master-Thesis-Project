//VERSION 1.0: inital Version
//VERSION 1.1: LED pin added 
//VERSION 1.3: Comments added

uint8_t statusledpin = 3;
uint8_t pedaltrainerpin = 5;
float scalfactor = 30000;  // corresponds to complete steps per minute

char serialCommand;
byte speed;
bool dval;
bool isHigh;
float dt;
unsigned long lastOnset, lastOffset, timestamp;
unsigned long events[3] = { 0, 0, 0 };
// 3 events in buffer means 2 event differnces considered for speed calculation (one back and forth movement of onme foot)
// minimum is 3 to be able to estimate stop condition
const byte bufsize = 3; 
unsigned long curEvents[bufsize];
unsigned int evtCount, bufCount, curCount, curBufSize;
float curspeed;
unsigned long flankCount[2];

void reset() {
  // pinMode(pedaltrainerpin, INPUT_PULLUP); // use this for button between GND and pin
  pinMode(pedaltrainerpin, INPUT);
  pinMode(statusledpin, OUTPUT);
  isHigh = digitalRead(pedaltrainerpin) == HIGH;
  digitalWrite(statusledpin,isHigh);
  for (int k = 0; k < bufsize; k++) {
    events[k] = 0;
  }
  speed = 0;
  bufCount = bufsize - 1;
  evtCount = 0;
  flankCount[0]=0;
  flankCount[1]=0;
  lastOnset = millis();
  lastOffset = millis();
}

void setup() {
  Serial.begin(9600);
  reset();
}

void sendDebugInfo() {
  Serial.println("Pinval");
  Serial.println(digitalRead(pedaltrainerpin) == HIGH);
  Serial.println("isHigh");
  Serial.println(isHigh);
  Serial.println("speed");
  Serial.println(speed);
  Serial.println("events1");
  Serial.println(events[0]);
  Serial.println("events2");
  Serial.println(events[1]);
  Serial.println("events3");
  Serial.println(events[2]);
  Serial.println("lastOnset");
  Serial.println(lastOnset);
  Serial.println("lastOffset");
  Serial.println(lastOffset);
  Serial.println("bufCount");
  Serial.println(bufCount);
  Serial.println("flankCountOn");
  Serial.println(flankCount[0]);
  Serial.println("flankCountOff");
  Serial.println(flankCount[1]);
  }

void loop() {
  // check for serialport input
  while (Serial.available()) {
    serialCommand = Serial.read();
    switch (serialCommand) {
      case 'R':  // reset
        reset();
        break;
      case 'X':  // send debug information
        sendDebugInfo();
        break;
      case 'S':  // S requests the current speed
        Serial.write(speed);
        break;
    }
  }
  timestamp = millis();

  dval = digitalRead(pedaltrainerpin) == HIGH;
  digitalWrite(statusledpin,dval);

  // LIGHTBARRIER LOGIC: TTL high means beam interrupted
  if (isHigh && !dval) {  // falling flank
    isHigh = dval;
    flankCount[0]++;
    if (timestamp > lastOffset + 50) {
      lastOnset = timestamp;
      delay(50);
    }


  } else if (!isHigh && dval) {  // rising flank
    isHigh = dval;
    flankCount[1]++;
    lastOffset = timestamp; // we assume rising flank is the offset
    // bufCount is the counter for the ring buffer
    bufCount = bufCount == (bufsize - 1) ? 0 : bufCount + 1;
    // events contain the center timestamp between falling and rising flank
    events[bufCount] = lastOnset + (lastOffset - lastOnset) / 2;
    evtCount++;
    delay(50);
  }
  if (evtCount<2) return; // we need at least 2 events to calculate speed

  curCount = bufCount;
  //curBufSize = bufsize;
  if (speed == 0 || evtCount<bufsize) curBufSize = 2; // if we start from zero we do not smooth
  else curBufSize = bufsize;

  for (int k = 0; k < bufsize; k++) {
    // here we sort the events from latest to oldest
    curEvents[k] = events[curCount];
    curCount = curCount == 0 ? bufsize - 1 : curCount - 1;
  }
  dt = (float)(curEvents[0] - curEvents[curBufSize-1]);
  curspeed = (scalfactor / dt) * (float)(curBufSize - 1); // smooth across events
    
  if (curspeed > 255) curspeed = 255; // limit the maximum possible speed
  speed = round(curspeed); // accept only integer values

  // curspeed is the speed estimated from the last event to now  
  // if user stops, no further event would occur, so we estimate it every time we come here
  curspeed = scalfactor / (float)(timestamp - events[bufCount]); 
  // we calculate it to estimate whether user intends to stop
  if (curspeed < speed) speed = curspeed; // user seems to get slower, i.e. needs mor time than previous event based estimate. This slows the speed continuously down
  if (evtCount>2){
    // if the current speed is slower than 25% of the last event-based estimate, assume the user stopped
    if (curspeed < 0.25*(scalfactor / (curEvents[0] - curEvents[1]))){
      speed = 0; // if speed gets much smaller than last buffer event, we assume a stop
    }
  }
  //  Serial.println(speed); This was necessary in Poojan's Master project but destroyed the handshake approach    
}
