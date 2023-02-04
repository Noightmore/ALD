cmake_minimum_required(VERSION 3.23)
project(PROBLEM_Idos C)

set(CMAKE_C_STANDARD 23)

add_executable(PROBLEM_Idos main.c data/Node.h tools/headers/loadData.h tools/loadData.c tools/headers/loadInitialData.h tools/loadInitialData.c tools/headers/initMatrix.h tools/initMatrix.c data/IdosData.h data/LinkedList.h)
