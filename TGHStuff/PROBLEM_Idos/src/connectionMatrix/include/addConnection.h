#ifndef PROBLEM_IDOS_ADDCONNECTION_H
#define PROBLEM_IDOS_ADDCONNECTION_H

#include <stddef.h>

#include "connection.h"
#include "treeNode.h"
#include "createNewTreeNode.h"
#include "../../mainApp/include/idosData.h"

// inserts connection to connectionMatrix specified by a pointer
// and stores it at the appropriate position
// returns 0 if successful, 1 if not

int addConnection(idosData* idos_data,
                  connection* connection,
                  unsigned int start_destination,
                  unsigned int end_destination);

#endif //PROBLEM_IDOS_ADDCONNECTION_H
