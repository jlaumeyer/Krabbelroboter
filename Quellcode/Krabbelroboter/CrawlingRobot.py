from ev3dev2.motor import LargeMotor, OUTPUT_C, OUTPUT_D, SpeedDPS
from ev3dev2.sensor import INPUT_1
from Sensors import DistanceSensor
from MarkovDecisionProcess import MarkovDecisionProcess
import time

class CrawlingRobot:
    def __init__(self,*,m1rom,m2rom):
        self.m1steps = 4
        self.m2steps = 4
        self.mdp = MarkovDecisionProcess(self.m1steps,self.m2steps)
        self.m1step = m1rom / (self.m1steps-1)
        self.m2step = m2rom / (self.m2steps-1)
        self.dps = SpeedDPS(100)
        self.m1 = LargeMotor(OUTPUT_C)
        self.m2 = LargeMotor(OUTPUT_D)
        self.ds = DistanceSensor(INPUT_1)
    def reset(self):
        self.m1.on(speed=-20)
        self.m1.wait_until('stalled')
        self.m1.stop(stop_action='hold')
        self.m2.on(speed=-20)
        self.m2.wait_until('stalled')
        self.m2.stop(stop_action='hold')
        self.m2.on_for_degrees(self.dps,40)
        self.m1.on_for_degrees(self.dps,10)
        self.mdp = MarkovDecisionProcess(self.m1steps,self.m2steps)
    def move(self,action_index):
        state = self.mdp.get_current_state()
        action = state.actions[action_index]
        if action.executable:
            reward = self.execute(action)
            self.mdp.execute(action)
            action.update(reward)
    def explore(self):
        state = self.mdp.get_current_state()
        action = state.get_random_action()
        reward = self.execute(action)
        self.mdp.execute(action)
        action.update(reward)
    def learn(self,iterations):
        self.mdp.reset()
        for i in range(iterations):
            self.mdp.learn()
    def exploit(self):
        action = self.mdp.get_best_action()
        reward = self.execute(action)
        if not action.explored:
            action.update(reward)
            self.learn(50)
        self.mdp.execute(action)
    def execute(self,action):
        d1 = self.ds.distance()
        self.m1.on_for_degrees(self.dps,action.dir[0]*self.m1step)
        self.m2.on_for_degrees(self.dps,action.dir[1]*self.m2step)
        time.sleep(1.0)
        d2 = self.ds.distance()
        return d2 - d1