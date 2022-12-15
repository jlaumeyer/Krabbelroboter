import struct

class Action:
    def __init__(self,executable,dir):
        self.reward = 0
        self.executable = executable
        self.explored = False
        self.dir = dir
    def encode(self):
        return struct.pack('h??',self.reward,self.executable,self.explored)
    def decode(self,buffer,offset):
        self.reward = struct.unpack('h',buffer[offset:offset+2])[0]
        self.explored = True
        return offset + 2
    def update(self,reward):
        if self.explored == False:
            self.reward = reward
            self.explored = True