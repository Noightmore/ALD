#include "../include/createNewTreeNode.h"

treeNode* createNewTreeNode(connection* connection) {
    treeNode* newTreeNode = (treeNode*) malloc(sizeof(treeNode));
    newTreeNode->connection = connection;
    newTreeNode->left = NULL;
    newTreeNode->right = NULL;

    return newTreeNode;
}