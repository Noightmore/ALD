#include "../include/addConnection.h"


int addConnection(idosData* idos_data,
                  connection* connection,
                  unsigned int start_destination,
                  unsigned int end_destination) {

        // pointer to the pointer to the treeNode
        treeNode** current_connection_pos = &idos_data->connectionMatrix[start_destination][end_destination];

        // check if any connection already exists for this start_destination and end_destination
        if (*current_connection_pos == NULL) {
                *current_connection_pos = createNewTreeNode(connection);
                return 0;
        }

        // create separate function for this
        if (*connection->departure_time > *(*current_connection_pos)->connection->departure_time) {
                if((*current_connection_pos)->right == NULL) {
                        (*current_connection_pos)->right = createNewTreeNode(connection);
                        return 1;
                }
                current_connection_pos = &(*current_connection_pos)->right;
        }


        return 1;
}

