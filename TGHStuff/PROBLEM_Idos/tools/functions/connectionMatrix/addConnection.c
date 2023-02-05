#include "../../headers/connectionMatrix/addConnection.h"

int addConnection(Node** connectionPosition, const unsigned long* start_time, const unsigned long* time_it_takes)
{
    if(*connectionPosition == NULLPTR)
    {
        *connectionPosition = createNode(start_time, time_it_takes);
        return 1;
    }

    Node *currentNode = *connectionPosition;
    while(currentNode->next_connection != NULLPTR)
    {
        currentNode = currentNode->next_connection;
    }

    currentNode->next_connection = createNode(start_time, time_it_takes);
    return 0;
}