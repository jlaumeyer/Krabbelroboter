# Krabbelroboter

![alt text](https://github.com/jlaumeyer/Krabbelroboter/blob/main/material/Medien/rendered_2.png)

Der Krabbelroboter ist ein Roboter, der sich durch die Bewegung seines Arms fortbewegen möchte. Zwei Gelenke ermöglichen es ihm den Arm zu heben und zu senken, zu strecken und zu beugen. Ein zielführendes Bewegungsmuster könnte sein, den Arm zunächst zu heben, dann zu strecken, ihn anschließend zu senken und schließlich zu beugen. Voraussetzung ist, dass der Arm in der Beugung genügend Reibung mit dem Untergrund aufbauen kann, um den Rumpf nach vorne zu ziehen. Am Beispiel des Roboters kann maschinelles Lernen in der Schule unterrichtet werden. Durch die Implementierung des Q-Learning Algorithmus, zuzuordnen dem Paradigma des verstärkenden Lernens, lernt der Roboter das Laufen aus Erfahrungen.

Folgende Schritte sind zu befolgen, um den Roboter für die maschinelle Lernumgebung vorzubereiten:
+ Aufbau des Roboters entsprechend der Anleitung in der Datei *Anleitung.pdf*. Darüber muss der Ultraschallsensor mit *PORT 
+ Bespielen einer Micro SD-Karte mit dem Betriebssystem [ev3dev](https://www.ev3dev.org/). Die SD-Karte kann anschließend in den EV3 Baustein eingesteckt werden. Beim Starten wird automatisch das Betriebssytem von der SD-Karte gebooted.
+ Herstellen einer Internetverbindung durch das Einstecken und Einrichten eines W-LAN-Sticks.
+ Übertragen des Programmcodes aus dem Ordner *./dev/Krabbelroboter* auf den EV3 Baustein. Das ist zum Beispiel mit der Entwicklungsumgebung [Visual Studio Code](https://code.visualstudio.com/) und der Erweiterung [ev3dev-browser](https://marketplace.visualstudio.com/items?itemName=ev3dev.ev3dev-browser) komfortabel möglich.
