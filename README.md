# Krabbelroboter

![Krabbelroboter](https://github.com/jlaumeyer/Krabbelroboter/blob/main/material/Medien/rendered_2.png)

Der Krabbelroboter ist ein Roboter, der sich durch die Bewegung seines Arms fortbewegen möchte. Zwei Gelenke ermöglichen es ihm den Arm zu heben und zu senken, zu strecken und zu beugen. Ein zielführendes Bewegungsmuster könnte sein, den Arm zunächst zu heben, dann zu strecken, ihn anschließend zu senken und schließlich zu beugen. Voraussetzung ist, dass der Arm in der Beugung genügend Reibung mit dem Untergrund aufbauen kann, um den Rumpf nach vorne zu ziehen. Am Beispiel des Roboters kann maschinelles Lernen in der Schule unterrichtet werden. Durch die Implementierung des Q-Learning Algorithmus, zuzuordnen dem Paradigma des verstärkenden Lernens, lernt der Roboter das Laufen aus Erfahrungen.

Folgende Schritte leiten zum Aufbau und Betrieb des Roboters an:
+ Aufbau des Roboters entsprechend der Anleitung in der Datei *Anleitung.pdf*. Darüber hinaus muss der Ultraschallsensor mit **Port 1**, der Motor auf dem Rücken des Roboters mit **Port C** und der andere Motor mit **Port D** verbunden werden. Um den Motor auf dem Rücken des Roboters zu stabilisieren, macht es Sinn, ihn mit doppelseitigem Klebeband am Baustein zu befestigen.
+ Bespielen einer Micro SD-Karte mit dem Betriebssystem [ev3dev](https://www.ev3dev.org/). Die SD-Karte kann anschließend in den EV3 Baustein eingesteckt und der Baustein gestartet werden.
+ Herstellen einer Internetverbindung durch das Einstecken und Einrichten eines W-LAN-Sticks.
+ Übertragen des Programmcodes aus dem Ordner [dev/Krabbelroboter](dev/Krabbelroboter) auf den EV3 Baustein. Das ist zum Beispiel mit der Entwicklungsumgebung [Visual Studio Code](https://code.visualstudio.com/) und der Erweiterung [ev3dev-browser](https://marketplace.visualstudio.com/items?itemName=ev3dev.ev3dev-browser) komfortabel möglich.

Der Roboter öffnet nach Programmstart einen Webserver und wartet auf eingehende Verbindungen. IP-Adresse und Port werden auf der Konsole des Bausteins ausgegeben. Um mit dem Roboter zu interagieren, ihn lernen und laufen zu lassen, wird ein zweites Programm benötigt. Dieses ist im Ordner [dev/Gui](dev/Gui) abgelegt. Über die grafische Benutzeroberfläche kann man sich mit dem Roboter verbinden, interne Zustände beobachten, manipulieren und Bewebungssequenzen ausführen.

![gui](https://user-images.githubusercontent.com/120554609/207822779-ba066f08-5ffd-491d-9099-99302e4dd32a.png)
