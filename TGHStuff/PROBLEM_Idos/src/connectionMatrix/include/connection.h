#ifndef PROBLEM_IDOS_CONNECTION_H
#define PROBLEM_IDOS_CONNECTION_H

typedef struct connection connection;

struct connection {
        unsigned int *departure_time;
        unsigned int *time_it_takes;
};

#endif //PROBLEM_IDOS_CONNECTION_H
