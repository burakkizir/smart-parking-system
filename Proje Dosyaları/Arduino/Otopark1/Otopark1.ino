  #include <Servo.h>        // SERVO MOTORUN KÜTÜPHANESİ DAHİL EDİLİYOR
Servo motor1;             // SERVO MOTORA İSİM VERİLDİ 

#define limit 10     // ARAÇ ALGILAMA MESAFESİ
#define kapihizi 15   // GARAÇ AÇMA-KAPAMA HIZI TERS ORANTILI
#define ldrlimit 100
#define s1 A0     //LDR 1 BAĞLANTI PİNİ
#define s2 A1     //LDR 2 BAĞLANTI PİNİ
#define s3 A2     //LDR 3 BAĞLANTI PİNİ
#define s4 A3     //LDR 4 BAĞLANTI PİNİ
#define s5 A4     //LDR 5 BAĞLANTI PİNİ
#define s6 A5     //LDR 6 BAĞLANTI PİNİ
#define yled 2    // LEDİN YEŞİL BACAĞININ BAĞLANTI PİNİ
#define kled 3    // LEDİN KIRMIZI BACAĞININ BAĞLANTI PİNİ
#define mled 5    // LEDİN MAVİ BACAĞININ BAĞLANTI PİNİ

int d1 = 0;       // LDR 1 DEN OKUNAN DEĞERİ ALACAK DEĞİŞKEN
int d2 = 0;       // LDR 2 DEN OKUNAN DEĞERİ ALACAK DEĞİŞKEN
int d3 = 0;       // LDR 3 DEN OKUNAN DEĞERİ ALACAK DEĞİŞKEN
int d4 = 0;       // LDR 4 DEN OKUNAN DEĞERİ ALACAK DEĞİŞKEN
int d5 = 0;       // LDR 5 DEN OKUNAN DEĞERİ ALACAK DEĞİŞKEN
int d6 = 0;       // LDR 6 DEN OKUNAN DEĞERİ ALACAK DEĞİŞKEN

int tr1 = 6;      // MESAFE SENSÖRÜ TRIGGER BAĞLANTI PİNİ
int ec1 = 7;      // MESAFE SENSÖRÜ ECHO BAĞLANTI PİNİ

int pos = 0;      // MOTORUN BAŞLANGIÇ AÇI DEĞERİ

long sure1;       // MESAFE ÖLÇÜMÜNDE KULLANILAN SÜRE DEĞİŞKENİ
long mesafe1;     // MESAFE ÖLÇÜMÜNDE KULLANILAN MESAFE DEĞİŞKENİ

bool acik1 = true;    // KAPININ DURUMUNU ALAN DEĞİŞKEN
bool dolu = false;    // PARK YERİNİN DURUMUNU ALAN DEĞİŞKEN
bool acil = false;    // ACİL DURUMU ALAN DEĞİŞKEN

void setup() {

  Serial.begin(9600);       // PC BAĞLANTISI BAŞLATILIYOR
  pinMode(tr1, OUTPUT);     // MESAFE SENSÖRÜ TRIGGER PİNİ ÇIKIŞ OLARAK AYARLANDI
  pinMode(ec1, INPUT);      // MESAFE SENSÖRÜ ECHO PİNİ GİRİŞ OLARAK AYARLANDI
  pinMode(yled, OUTPUT);    // LEDİN YEŞİL BACAK PİNİ ÇIKIŞ OLARAK AYARLANDI
  pinMode(kled, OUTPUT);    // LEDİN KIRMIZI BACAK PİNİ ÇIKIŞ OLARAK AYARLANDI
  pinMode(mled, OUTPUT);    // LEDİN MAVİ BACAK PİNİ ÇIKIŞ OLARAK AYARLANDI

  motor1.attach(9);         // MOTORUN BAĞLANDIĞI PİN BELİRLENDİ
}

void loop() {

  if (Serial.available() > 0) {
    String gelen = Serial.readString();
    if (gelen.indexOf("acil") > -1) {
      if (acil == false) {
        digitalWrite(yled, HIGH);
        digitalWrite(kled, HIGH);
        digitalWrite(mled, LOW);  // low rgbd
        acil = true;
        motor1.write(80);
      } else {
        acil = false;
        motor1.write(0);
        digitalWrite(mled, HIGH);  // hıgh rgb
      }
      gelen = "";
    }
  }


  d1 = analogRead(s1);      // LDR 1 DEN DEĞER OKUNDU
  d2 = analogRead(s2);      // LDR 2 DEN DEĞER OKUNDU
  d3 = analogRead(s3);      // LDR 3 DEN DEĞER OKUNDU
  d4 = analogRead(s4);      // LDR 4 DEN DEĞER OKUNDU
  d5 = analogRead(s5);      // LDR 5 DEN DEĞER OKUNDU
  d6 = analogRead(s6);      // LDR 6 DAN DEĞER OKUNDU
  /*
    Serial.print(d1);Serial.print(" - ");
    Serial.print(d2);Serial.print(" - ");
    Serial.print(d3);Serial.print(" - ");
    Serial.print(d4);Serial.print(" - ");
    Serial.print(d5);Serial.print(" - ");
    Serial.println(d6);
  */

  if (d1 > ldrlimit) {     // EĞER OKUNAN DEĞER 400 DEN BÜYÜKSE
    d1 = 0;           // OKUNAN DEĞERİ 1 YAP
  } else {            // EĞER OKUNAN DEĞER 400 DEN BÜYÜK DEĞİLSE
    d1 = 1;           // OKUNAN DEĞERİ 0 YAP
  }

  if (d2 > ldrlimit) {
    d2 = 0;
  } else {
    d2 = 1;
  }

  if (d3 > ldrlimit) {
    d3 = 0;
  } else {
    d3 = 1;
  }

  if (d4 > ldrlimit) {
    d4 = 0;
  } else {
    d4 = 1;
  }

  if (d5 > ldrlimit) {
    d5 = 0;
  } else {
    d5 = 1;
  }

  if (d6 > ldrlimit) {
    d6 = 0;
  } else {
    d6 = 1;
  }

  if ((d1 + d2 + d3 + d4 + d5 + d6) > 5) {  // TÜM OKUNAN DEĞERLERİN TOPLAMI 5 TEN BÜYÜKSE...
    dolu = true;                            // DOLU DEĞİŞKENİNİ DOĞRU YAP
    digitalWrite(kled, 0);                  // KIRMIZI LED PİNİNİ 0V YAP
    digitalWrite(yled, 1);                  // YEŞİL LED PİNİNİ 5V YAP
  } else {                                  // TÜM OKUNAN DEĞERLERİN TOPLAMI 5 TEN BÜYÜK DEĞİLSE ...
    dolu = false;                           // DOLU DEĞİŞKENİNİ YANLIŞ YAP
    digitalWrite(kled, 1);                  // KIRMIZI LED PİNİNİ 5V YAP
    digitalWrite(yled, 0);                  // YEŞİL LED PİNİNİ 0V YAP
  }

  // OKUNAN DEĞERLER ARASINDA ":" OLARAK TEK SATIR HALİNDE PC YE GÖNDERİLİYOR...
  Serial.print(d1);
  Serial.print(":");
  Serial.print(d2);
  Serial.print(":");
  Serial.print(d3);
  Serial.print(":");
  Serial.print(d4);
  Serial.print(":");
  Serial.print(d5);
  Serial.print(":");
  Serial.println(d6);

  if (acil == false) {
    digitalWrite(tr1, LOW);       // TRIGGER PİNİ 0V YAPILDI
    delayMicroseconds(5);
    digitalWrite(tr1, HIGH);      // TRIGGER PİNİ 5V YAPILDI
    delayMicroseconds(10);
    digitalWrite(tr1, LOW);       // TRIGGER PİNİ 0V YAPILDI

    sure1 = pulseIn(ec1, HIGH);   // ARADA GEÇEN SÜRE İLE MESAFE HESAPLANIYOR...
    mesafe1 = (sure1 / 29.1) / 2;
    if (mesafe1 > 200) {          // MESAFE 200cm DEN BÜYÜKSE 200 OLARAK KABUL ET
      mesafe1 = 200;
    }
    if (mesafe1 < 1) {         // MESAFE 1cm DEN KÜÇÜKSE 200 OLARAK KABUL ET
      mesafe1 = 200;
    }

     
    if (dolu == false) {                    // OTOPARK DOLU DEĞİLSE...
      if (mesafe1 < limit) {                // MESAFE LİMİT DEĞERİNDEN KÜÇÜKSE...
        if (acik1 == false) {               // KAPI AÇIK DEĞİLSE...
          for (pos = 0; pos < 80; pos++) {  // KAPI AÇILIYOR...
            motor1.write(pos);
            delay(kapihizi);
          }
          acik1 = true;                     // KAPI DURUMU AÇIK OLARAK ATANDI
        }
      }
      //delay(5000);
      if (mesafe1 > limit) {                // MESAFE LİMİT DEĞERDEN BÜYÜKSE...
        if (acik1 == true) {                // KAPI AÇIK İSE...
          for (pos = 80; pos > 0; pos--) {  // KAPI KAPATILIYOR...
            motor1.write(pos);
            delay(kapihizi);
          }
          acik1 = false;                    // KAPI DURUMU KAPALI OLARAK ATANDI
        }
      }
    }
  }
  delay(500);                             // BAŞA DÖNMEK İÇİN YARIM SN BEKLE...
}
