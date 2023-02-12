#include "../headers/processInputLine.h"
#include "../headers/dijkstraSolver/findShortestPath.h"

int processInputLine(const Node*** matrix, const IdosData* idosData)
{
    char line[BUFFER_SIZE];
    unsigned int input[3];

    if(fgets(line, BUFFER_SIZE, stdin) == NULL)
    {
        return 1; // sus
    }
    if(EOF == sscanf(line, "%d %d %d", input, input+1, input+2)) // NOLINT(cert-err34-c)
    {
        return 2; // sus
    }

    //printf("%d %d %d", input[0], input[1], input[2]);
    int result = findShortestPath(matrix, idosData, input);
    // TODO: check result, rework this
    if(result == NULL)
    {
        return 1;
    }

    // TODO: print the result path
    // create function for it

    return 0; // return 0 on success
}
