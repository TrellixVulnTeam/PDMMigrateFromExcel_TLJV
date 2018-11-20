# -*- coding: utf-8 -*-
"""
Created on Thu Oct 25 16:15:32 2018

@author: jevans
"""
import random

arrPick = [5, 28, 62, 65, 70, 5]

def genNumbers():
    arrDrawn = []
    intBall = 0
    j = 0
    for i in range(0,5):
        intBall = random.randint(1, 70)
        while intBall in arrDrawn:
            j += 1
            #print(str(j) + ": " + str(intBall))
            intBall = random.randint(1, 70)
        arrDrawn.append(intBall)
    
    arrDrawn.append(random.randint(1,25))
    return arrDrawn

def checkPick(arrPick, arrDrawn):
    match = 0
    megaBall = 0
    for pick in arrPick[0:5]:
        if(pick in arrDrawn[0:5]):
            match += 1
    if(arrPick[5] == arrDrawn[5]):
        megaBall = 1
    return [match, megaBall]

def runSim(arrPick):
    n = 0
    arrDrawn = genNumbers()
    while checkPick(arrPick, arrDrawn) != [5, 1]:
        n += 1
        if (n % 1000000 == 0):
            print(str(n) + ": " + str(arrDrawn))
        arrDrawn = genNumbers()
        
    print("num of iterations = " + str(n))
arrDrawn = genNumbers()