#include <iostream>
#include "qgraphicsitem.h"
#include "../Headers/mainwindow.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)//,
      //view(&scene)
{
    QVBoxLayout *layout = new QVBoxLayout(this);
    layout->addWidget(&view);
    //setWindowTitle("Labyrinth");

    QGraphicsPixmapItem background = QGraphicsPixmapItem(QPixmap(":Img/2.png"));
    scene.addItem(&background);
    background.setPos(0 ,0);
    background.setZValue(-0.1);

    int width = 6;
    int height = 5;
    int seed = 4;
    int cisloGridu;
    QGraphicsPixmapItem *obrazek;

    std::vector<QGraphicsPixmapItem*> obrazky = {
        new QGraphicsPixmapItem(QPixmap(":Img/2.png")),
        new QGraphicsPixmapItem(QPixmap(":Img/5.png")),
        new QGraphicsPixmapItem(QPixmap(":Img/6.png")),
        new QGraphicsPixmapItem(QPixmap(":Img/9.png")),
        new QGraphicsPixmapItem(QPixmap(":Img/c.png")),
        new QGraphicsPixmapItem(QPixmap(":Img/f.png"))
    };


    this->grid = new Grid(&width, &height, &seed);

    for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                cisloGridu = (int) this->grid->getGridData_ByRowAndColIndex(&i, &j);
                switch(cisloGridu) {
                    case 2:
                        obrazek = obrazky[0];
                        break;
                    case 5:
                        obrazek = obrazky[1];
                        break;
                    case 6:
                        obrazek = obrazky[2];
                        break;
                    case 9:
                        obrazek = obrazky[3];
                        break;
                    case 12:
                        obrazek = obrazky[4];
                        break;
                    case 15:
                        obrazek = obrazky[5];
                        break;
                    default:
                        obrazek = obrazky[0];
                }
                obrazek->setScale(0.2);
                scene.addItem(obrazek);
                //std::cout << "Obrazek cislo: " << obrazek << std::endl;
                obrazek->setPos(i*200, j*200);
            }
        }
        this->setCentralWidget(&view);
}

MainWindow::~MainWindow()
{
    delete layout;
    if (grid != nullptr) { grid->~Grid(); }
    if (!obrazky.empty()) {
        for(int i = 0; i < 6; i++) {
            obrazky[i]->~QGraphicsPixmapItem();
        }
        obrazky.~vector();
    }
    if (scene.isActive()) { scene.~QGraphicsScene();; }
}

