#include "../../headers/connectionMatrix/createNode.h"

Node* createNode(const unsigned long* start_time, const unsigned long* time_it_takes)
{
    Node* node = malloc(sizeof(Node));

    node->connection_value = malloc(sizeof(Connection*));
    node->connection_value->start_time = malloc(sizeof(unsigned long));
    node->connection_value->time_it_takes = malloc(sizeof(unsigned long));

    *node->connection_value->start_time = *start_time;
    *node->connection_value->time_it_takes = *time_it_takes;
    node->next_connection = NULLPTR;

    return node;
}