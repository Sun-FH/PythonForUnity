import socket
import json
import struct
import cv2
from cvzone.HandTrackingModule import HandDetector

sock = socket.socket(socket.AF_INET,socket.SOCK_STREAM)
host = '127.0.0.1'
port = 11111
sock.bind((host,port))
sock.listen(10)

cap = cv2.VideoCapture(0)
cap.set(3,1280)
cap.set(4,720)
decetor = HandDetector(detectionCon=0.8)
while True:
    client, add = sock.accept()
    print(client, "------", add)
    data = client.recv(1024)
    print(data.decode())

    while True:
        success, img = cap.read()
        img = cv2.flip(img, -1)
        allHands, img = decetor.findHands(img)
        if len(allHands) != 0:
            js = json.dumps(allHands[0])
            try:
                msg_len = len(js)
                hander = struct.pack('i',msg_len)
                client.send(hander)
                client.send(js.encode('utf-8'))
            except ConnectionError as e:
                print('close connection !')
                client.close()
                break

        cv2.imshow('image',img)
        cv2.waitKey(1)

    client.close()




