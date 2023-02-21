#ifndef PROBLEM_IDOS_TREENODE_H
#define PROBLEM_IDOS_TREENODE_H

#include "connection.h"

typedef struct treeNode treeNode;

// left branch contains connections with smaller departure time
// right branch contains connections with bigger departure time
struct treeNode {
        connection *connection;
        treeNode *left;
        treeNode *right;
};

#endif //PROBLEM_IDOS_TREENODE_H
