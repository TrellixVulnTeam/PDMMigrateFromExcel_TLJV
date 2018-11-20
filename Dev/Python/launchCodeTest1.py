# -*- coding: utf-8 -*-
"""
Created on Fri Apr 27 13:15:58 2018

@author: jevans
"""
import random

def convertToMatrix(n, string):
    # accepts a string of values, i.e "1 0 1 1" and returns a dictionary containing array of spaces with row number as key
    # example: convertToMatrix(4, "0 0 0 0 1 1 1 1 0 0 0 0 1 1 1 1")
    #          returns {1: [0,0,0,0], 2: [1, 1, 1, 1], 3: [0, 0, 0, 0], 4: [1, 1, 1, 1]}
    matrix = {}
    ctr = 0
    arr = string.split(" ")
    arr = list(map(int, arr))
    for x in range(1, n+1):
        matrix[x] = arr[ctr: ctr+n]
        ctr += n
    return matrix

def genRandomGrid(n):
    # generates a random grid of 1's and 0's for testing purposes
    for i in range(0, n*n):
        if i == 0:
            parkingStr = str(random.randint(0,1)) + " "
        elif i == n*n - 1:
            parkingStr += str(random.randint(0,1))
        else:
            parkingStr += str(random.randint(0,1)) + " "
    return parkingStr

def carParking(n, available):
    #
    # Assigns parking spaces.  
    # Accepts n: string, number of rows/columns
    #        available: string containing grid information, i.e. "1 0 0 1 1 1..."
    # Returns array containing row and space number, [row, space].  If lot is full this returns [0,0]
    matrix = convertToMatrix(n, available)
    #print(matrix)

    def printMatrix(matrix, n):
        colStr = ""
        for i in range(1, n+1):
            colStr += "C" + str(i) + " "
        print("       " + colStr)
        
        for x in range(1, n+1):
            rowStr = ""
            
            for el in matrix[x]:
                rowStr += " " + str(el) + " "                
            print("Row " + str(x) + ": " + rowStr)
        return
            
        

    def getPosition(arrSpace):
        ctr = 0
        for pos in arrSpace:
            if pos == 0:
                return ctr + 1
            ctr += 1
        return 0
    
    def getRow(matrix, n):
        maxSpaces = 0
        maxRow = 0
        for x in range(n, 1, -1):
            cnt = 0
            for space in matrix[x]:
                if space == 0:
                    cnt += 1
            if cnt > maxSpaces:
                maxSpaces = cnt
                maxRow = x
        return maxRow
    
    
    printMatrix(matrix, n)
    row = getRow(matrix, n)
    if row == 0:
        # if row is set = 0 then lot is full
        pos = 0
    else:
        # if lot is not full then determine position
        pos = getPosition(matrix[row])
    return [row, pos]
                    


n = 5

string = "1 1 1 0 0 1 1 1 0 0 1 1 0 0 0 1 0 0 0 0 1 1 1 0 0"
#string = "1 1 1 1 1 1 1 1 1"
#string = genRandomGrid(n)
arr = carParking(n, string)
if arr == [0,0]:
    print("Attendant: 'Lot is full, maybe try another one.'")
else:
    print( "Attendant: 'Please park in location, (R" + str(arr[0]) + ", C" + str(arr[1]) + ").'")


        

