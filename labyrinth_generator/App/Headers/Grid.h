#ifndef LABYRINTHGENERATOR_GRID_H
#define LABYRINTHGENERATOR_GRID_H


class Grid
{

private:

    int *width;
    int *height;
    int *seed;
    char ***grid; // zadny strach, to je pointer na 2D pole pointeru na 1D pole char pointeru

    char tileData[6] = {
            0x0F, // byte representation of the first tile: 0b0000_1111
            0x02, // byte representation of the second tile: 0b0000_0010
            0x06, // byte representation of the third tile: 0b0000_0110
            0x0C, // byte representation of the fourth tile: 0b0000_1100
            0x09, // byte representation of the fifth tile: 0b0000_1001
            0x05 // byte representation of the sixth tile: 0b0000_0101
    };
    void allocateAndFill();
    void deallocateGrid();
    static bool checkTopCompatibility(const char *topTileData, const char *currentTileData);
    static bool checkLeftCompatibility(const char *leftTileData, const char *currentTileData);
    int getCompatibleTileId(const int *rowId, const int *colId);

public:

    Grid(const int *width, const int *height, const int *seed);
    ~Grid();
    char getGridData_ByRowAndColIndex(const int *row, const int *col);
    [[maybe_unused]] char getTileData_ByTileId(const int *tileId);
    void printGrid();


};

#endif //LABYRINTHGENERATOR_GRID_H
