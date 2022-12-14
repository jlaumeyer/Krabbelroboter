from ev3dev2.sensor.lego import UltrasonicSensor

class DistanceSensor(UltrasonicSensor):
    def distance(self):
        sum,iterations = 0,10
        for i in range(iterations):
            sum += super().distance_centimeters
        return round(sum / iterations)