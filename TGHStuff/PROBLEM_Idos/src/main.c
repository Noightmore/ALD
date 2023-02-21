#include "mainApp/include/idosData.h"
#include "mainApp/include/loadInitialData.h"
#include "../tests/runTests.h"
#include "connectionMatrix/include/initConnectionMatrix.h"

int main() {
        idosData* initialData =
                loadInitialData();

        initialData->connectionMatrix =
                initConnectionMatrix((const unsigned long *) *initialData->stationCount);


        // free memory
        free(initialData->stationCount);
        free(initialData->connectionCount);
        free(initialData);
        return 0;
}
