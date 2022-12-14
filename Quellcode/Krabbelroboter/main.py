#!/usr/bin/env python3
import sys,socket,time,struct
from CrawlingRobot import CrawlingRobot

#sys.stdout = sys.stderr
robot = CrawlingRobot(m1rom=90,m2rom=180)
########################################################
sock = socket.socket()
sock.bind(('',0))
ip_address = socket.gethostbyname(socket.gethostname())
port = sock.getsockname()[1]
print("IP-Adresse: {}".format(ip_address))
print("Port: {}".format(port))
########################################################
sock.listen(1)
conn,addr = sock.accept()
########################################################
while True:
    buffer = conn.recv(512)
    while len(buffer) < 512:
        buffer += conn.recv(512)
    code = buffer[0:1].decode()
    if code == "0":
        robot.move(0)
    if code == "1":
        robot.move(1)
    if code == "2":
        robot.move(2)
    if code == "3":
        robot.move(3)
    if code == "4":
        robot.explore()
    if code == "5":
        robot.learn(buffer[1])
    if code == '6':
        robot.exploit()
    if code == '7':
        robot.reset()
    if code == '8':
        robot.mdp.decode(buffer,1)
    conn.sendall(robot.mdp.encode().ljust(512,b'\0'))