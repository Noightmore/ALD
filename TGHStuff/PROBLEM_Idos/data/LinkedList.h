#ifndef PROBLEM_IDOS_LINKEDLIST_H
#define PROBLEM_IDOS_LINKEDLIST_H

#include "Connection.h"

typedef struct Node
{
    Connection* connection_value;
    struct Node* next_connection;
} Node;

#endif //PROBLEM_IDOS_LINKEDLIST_H
