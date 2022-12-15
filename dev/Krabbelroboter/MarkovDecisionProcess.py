from State import State
from Action import Action
import random, struct

class MarkovDecisionProcess:
    def __init__(self,row_count,col_count):
        self.matrix = []
        for i in range(row_count):
            self.matrix.append([])
            for j in range(col_count):
                state = State()
                state.actions.append(Action(i != 0,[-1,0]))
                state.actions.append(Action(j != col_count-1,[0,1]))
                state.actions.append(Action(i != row_count-1,[1,0]))
                state.actions.append(Action(j != 0,[0,-1]))
                self.matrix[i].append(state)
        self.row,self.col = 0,0
        self.weight = .9
    def encode(self):
        buffer = struct.pack('bb',self.row,self.col)
        for i in range(len(self.matrix)):
            for j in range(len(self.matrix[i])):
                buffer += self.matrix[i][j].encode()
        #return struct.pack('h',len(buffer)) + buffer
        return buffer
    def decode(self,stream,offset):
        for i in range(len(self.matrix)):
            for j in range(len(self.matrix[i])):
                offset = self.matrix[i][j].decode(stream,offset)
    def reset(self):
        for i in range(len(self.matrix)):
            for j in range(len(self.matrix[i])):
                self.matrix[i][j].value = 0
    def get_current_state(self):
        return self.matrix[self.row][self.col]
    def execute(self,action):
        self.row += action.dir[0]
        self.col += action.dir[1]
    def delta(self,i,j,action):
        return self.matrix[i+action.dir[0]][j+action.dir[1]]
    def learn(self):
        for i in range(len(self.matrix)):
            for j in range(len(self.matrix[i])):
                state = self.matrix[i][j]
                max = -999999
                options = list(filter(lambda action: action.executable,state.actions))
                for action in options:
                    next = self.delta(i,j,action)
                    if action.reward + self.weight * next.value > max:
                        max = action.reward + self.weight * next.value
                state.value = max
    def get_best_action(self):
        i,j = self.row,self.col
        state = self.matrix[i][j]
        options = list(filter(lambda action: action.executable,state.actions))
        best = options[0]
        for action in options:
            next = self.delta(i,j,action)
            if action.reward + self.weight * next.value > best.reward + self.weight * self.delta(i,j,best).value:
                best = action
        return best