
#include "../headers/loadAndProcess_Requests.h"

int loadAndProcess_Requests(const Node*** matrix, const IdosData* idosData)
{
    char buffer[BUFFER_SIZE];
    unsigned long requestCount;

    if(fgets(buffer, BUFFER_SIZE, stdin) == NULL)
    {
        return 1; // sus
    }
    if(EOF == sscanf(buffer, "%lu", &requestCount)) // NOLINT(cert-err34-c)
    {
        return 2; // sus
    }

    for(unsigned long i = 0; i < requestCount; i++)
    {
        int res = processInputLine(matrix, idosData);
        if(res != 0)
        {
            return res;
        }
    }

    return 0; // return 0 on success
}