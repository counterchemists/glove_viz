#include<Uduino.h>
Uduino uduino("glove"); // Declare and name your object

void setup()
{
  Serial.begin(9600);
  pinMode(A0, INPUT);
  pinMode(A1, INPUT);
}


void loop()
{
  uduino.update();       //!\ This part is mandatory

  if(uduino.isConnected()) {
    Serial.print("a ");
    Serial.println(analogRead(A0));
      delay(10);
    Serial.print("b ");
    Serial.println(analogRead(A1));
  }  
  
  delay(10);

}
