cmake_minimum_required(VERSION 3.24)
project(PROBLEM_Idos C)

set(CMAKE_C_STANDARD 23)

add_executable(PROBLEM_Idos src/main.c src/connectionMatrix/include/treeNode.h src/mainApp/include/loadInitialData.h src/mainApp/include/idosData.h src/mainApp/libs/loadInitialData.c src/connectionMatrix/include/connection.h src/mainApp/include/macros.h src/connectionMatrix/include/addConnection.h src/connectionMatrix/include/initConnectionMatrix.h src/connectionMatrix/libs/initConnectionMatrix.c src/connectionMatrix/libs/addConnection.c src/connectionMatrix/include/createNewTreeNode.h src/connectionMatrix/libs/createNewTreeNode.c tests/libs/test_loadInitialData.c tests/runTests.c tests/include/test_loadInitialData.h tests/runTests.h)
