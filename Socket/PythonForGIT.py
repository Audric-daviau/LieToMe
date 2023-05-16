import socket
import time

host, port = "127.0.0.1", 25001
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
sock.connect((host, port))

startPos = [0, 0, 0]  # Vector3   x = 0, y = 0, z = 0
while True:
    time.sleep(0.5)  # sleep 0.5 sec
    startPos[0] += 1  # increase x by one
    # Converting Vector3 to a string, example "0,0,0"
    posString = ','.join(map(str, startPos))
    print(posString)

    # Converting string to Byte, and sending it to C#
    sock.sendall(posString.encode("UTF-8"))
    # receiveing data in Byte from C#, and converting it to String
    receivedData = sock.recv(1024).decode("UTF-8")
    print(receivedData)
