#include "piece.h"


Piece::Piece(double x, double y, char value, bool lucky):Tile(x,y,value)
{
    isLucky=lucky;
    itsTileImage.load(":/Images/coin1.png");
    Q_ASSERT(!itsTileImage.isNull());
}
bool Piece::getFalling()
{
    return isLucky;
}
void Piece::draw(QPainter *aPainter, int value)
{
    if(value==0)
    {
        itsTileImage.load(":/Images/coin1.png");
    }
    else if(value==25)
    {
        itsTileImage.load(":/Images/coin2.png");
    }
    else if(value==50)
    {
        itsTileImage.load(":/Images/coin3.png");
    }
    else if(value==75)
    {
        itsTileImage.load(":/Images/coin4.png");
    }
    else if(value==100)
    {
        itsTileImage.load(":/Images/coin3.png");
    }
    else if(value==125)
    {
        itsTileImage.load(":/Images/coin2.png");
    }
    Q_ASSERT(!itsTileImage.isNull());
    itsTileImage = itsTileImage.scaled(QSize(48,48));
     aPainter->drawImage(itsX,itsY,itsTileImage);
}
