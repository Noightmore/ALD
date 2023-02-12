cmake_minimum_required(VERSION 3.23)
project(PROBLEM_Idos C)
set(CMAKE_C_FLAGS "${CMAKE_C_FLAGS} -O3")


set(CMAKE_C_STANDARD 23)

FIND_PACKAGE ( PkgConfig REQUIRED )
PKG_CHECK_MODULES( GTK REQUIRED gtk+-3.0 )
INCLUDE_DIRECTORIES ( ${GTK_INCLUDE_DIRS} )
SET ( FOOBAR_LIBS ${GTK_LIBRARIES} )

add_executable(PROBLEM_Idos main.c data/Connection.h tools/headers/loadInitialData.h tools/functions/loadInitialData.c tools/headers/connectionMatrix/initConnectionMatrix.h tools/functions/connectionMatrix/initConnectionMatrix.c data/IdosData.h data/LinkedList.h data/Macros.h tools/headers/connectionMatrix/populateConnectionMatrix.h tools/functions/connectionMatrix/populateConnectionMatrix.c tools/headers/connectionMatrix/createNode.h tools/functions/connectionMatrix/createNode.c tools/headers/connectionMatrix/addConnection.h tools/functions/connectionMatrix/addConnection.c tools/headers/connectionMatrix/matrixVisualizer.h tools/functions/connectionMatrix/matrixVisualizer.c tools/functions/loadAndProcess_Requests.c tools/headers/loadAndProcess_Requests.h tools/functions/processInputLine.c tools/headers/processInputLine.h tools/headers/dijkstraSolver/findShortestPath.h tools/functions/dijkstraSolver/findShortestPath.c data/PathNode.h)
