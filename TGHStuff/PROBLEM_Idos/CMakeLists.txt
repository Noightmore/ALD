cmake_minimum_required(VERSION 3.23)
project(PROBLEM_Idos C)

set(CMAKE_C_STANDARD 23)

add_executable(PROBLEM_Idos main.c data/Connection.h tools/headers/loadInitialData.h tools/loadInitialData.c tools/headers/initConnectionMatrix.h tools/initConnectionMatrix.c data/IdosData.h data/LinkedList.h data/Macros.h tools/headers/populateConnectionMatrix.h tools/populateConnectionMatrix.c)
