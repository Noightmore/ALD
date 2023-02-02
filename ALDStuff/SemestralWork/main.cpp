#pragma clang diagnostic push
#pragma ide diagnostic ignored "cert-msc51-cpp"

#include <iostream>
#include "App/Headers/Grid.h"

#include <QApplication>
#include <QGraphicsPixmapItem>
#include <QGraphicsView>

int main([[maybe_unused]] int argc, char** argv)
{
    [[maybe_unused]] auto *app = new QApplication(argc, argv);

    // Create a QGraphicsScene and add it to the view
    auto* scene = new QGraphicsScene();
    auto* view = new QGraphicsView();
    view->setScene(scene);

    // Set the size of the grid and the size of each image
    int gridSizeW = std::stoi(argv[2]);
    int gridSizeH = std::stoi(argv[1]);
    int seed = std::stoi(argv[3]);
    double zoom = std::stoi(argv[4]);

    Grid* grid = new Grid(&gridSizeW, &gridSizeH, &seed);
    QPixmap *obrazek;

    // Load the images from file
    std::vector<QPixmap*> obrazky = {
            new QPixmap("../Img/2.png"),
            new QPixmap("../Img/5.png"),
            new QPixmap("../Img/6.png"),
            new QPixmap("../Img/9.png"),
            new QPixmap("../Img/c.png"),
            new QPixmap("../Img/f.png")
    };

    // Create a 2D array of QGraphicsPixmapItem objects
    auto **items = new QGraphicsPixmapItem*[gridSizeH];
    for (int i = 0; i < gridSizeW; i++)
    {
        items[i] = new QGraphicsPixmapItem[gridSizeW];
    }

    // Add the items to the scene and position them in a grid
    for (int i = 0; i < gridSizeH; i++)
    {
        for (int j = 0; j < gridSizeW; j++)
        {

            auto cisloGridu =  grid->getGridData_ByRowAndColIndex(&i, &j);
            switch(cisloGridu) {
                case 0x02:
                    obrazek = obrazky.at(0);
                    break;
                case 0x05:
                    obrazek = obrazky.at(1);
                    break;
                case 0x06:
                    obrazek = obrazky.at(2);
                    break;
                case 0x09:
                    obrazek = obrazky.at(3);
                    break;
                case 0x0C:
                    obrazek = obrazky.at(4);
                    break;
                case 0x0F:
                    obrazek = obrazky.at(5);
                    break;
                default:
                    obrazek = obrazky.at(0);
                    break;
            }

            (*(items + i)+j)->setPixmap(*obrazek);
            (*(items + i)+j)->setPos(j * zoom, i * zoom);
            auto scale = zoom/10000.0;
            (*(items + i)+j)->setScale(scale);
            scene->addItem((*(items + i)+j));
        }
    }
    view->show();
    //grid->printGrid();
    auto end = QApplication::exec();
    return end;
}

#pragma clang diagnostic pop