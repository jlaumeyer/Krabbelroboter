import random, struct

class State:
    def __init__(self):
        self.value = 0
        self.actions = []
    def encode(self):
        buffer = struct.pack('f',self.value)
        for action in self.actions:
            buffer += action.encode()
        return buffer
    def decode(self,stream,offset):
        self.value = struct.unpack('f',stream[offset:offset+4])[0]
        offset += 4
        for action in self.actions:
            offset = action.decode(stream,offset)
        return offset
    def get_random_action(self):
        options = list(filter(
            lambda action: action.executable and not action.explored,
            self.actions
        ))
        if len(options) == 0:
            options = list(filter(
                lambda action: action.executable,
                self.actions
            ))
        rn = random.randint(0,len(options)-1)
        return options[rn]