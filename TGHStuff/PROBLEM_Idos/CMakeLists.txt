cmake_minimum_required(VERSION 3.23)
project(PROBLEM_Idos C)

set(CMAKE_C_STANDARD 23)

add_executable(PROBLEM_Idos main.c data/Connection.h tools/headers/loadInitialData.h tools/functions/loadInitialData.c tools/headers/connectionMatrix/initConnectionMatrix.h tools/functions/connectionMatrix/initConnectionMatrix.c data/IdosData.h data/LinkedList.h data/Macros.h tools/headers/connectionMatrix/populateConnectionMatrix.h tools/functions/connectionMatrix/populateConnectionMatrix.c tools/headers/connectionMatrix/createNode.h tools/functions/connectionMatrix/createNode.c tools/headers/connectionMatrix/addConnection.h tools/functions/connectionMatrix/addConnection.c)
