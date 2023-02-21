#include <unistd.h>
#include <stdlib.h>
#include "../../src/mainApp/include/loadInitialData.h"

// test for loading initial data
// function that call loadInitialData and then provides data to stdin
// returns 0 if successful, 1 if not
// provided data are in the following format:
// 1 2
// 1 3
// 2 3


// DOES NOT WORK
int test_loadInitialData() {
        printf("Testing loadInitialData()...\n");


        // call loadInitialData
        idosData* initialData = loadInitialData();



        if (initialData == NULL) {
                printf("Error: initialData is NULL.\n");
                return 1;
        }

        if(*initialData->stationCount != 1 ) {
                printf("Error: stationCount is not 1. (%lu)\n", *initialData->stationCount);
                if(*initialData->connectionCount != 2){
                        printf("Error: connectionCount is not 2. (%lu)\n", *initialData->connectionCount);
                }
                return 1;
        }



        printf("Test passed.\n");

        // free memory
        free(initialData);

        return 0;

}


