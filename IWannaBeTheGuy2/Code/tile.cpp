#include "tile.h"
#include <iostream>
#include <QDebug>
using namespace std;
Tile::Tile(double x,double y,char value)
{
    itsX=x;
    itsXOld=x;
    itsY=y;
    itsWidth=48;
    itsHeight=48;
    itsSpeed=0;
    itsValue=value;
    isFalling=false;
    //itsTileImage.setRect(itsX,itsY,itsWidth,itsHeight);
    switch(itsValue)
    {
    case '0':itsTileImage.load(":/Images/vide.png");
        break;
    case 'a':itsTileImage.load(":/Images/sol.png");
        break;
    case 'b':itsTileImage.load(":/Images/littlesolleft.png");
        break;
    case 'c':itsTileImage.load(":/Images/littlesol.png");
        break;
    case 'd':itsTileImage.load(":/Images/littlesolright.png");
        break;
    case 'e':itsTileImage.load(":/Images/solright.png");
        break;
    case 'f':itsTileImage.load(":/Images/solleft.png");
        break;
    case 'g':itsTileImage.load(":/Images/grassleft.png");
        break;
    case 'h':itsTileImage.load(":/Images/grassmid.png");
        break;
    case 'i':itsTileImage.load(":/Images/grassright.png");
        break;
    case 'j':itsTileImage.load(":/Images/treeroot.png");
        break;
    case 'k':itsTileImage.load(":/Images/treebottom.png");
        break;
    case 'l':itsTileImage.load(":/Images/treemid.png");
        break;
    case 'm':itsTileImage.load(":/Images/treetop.png");
        break;
    case 'n':itsTileImage.load(":/Images/arrowleft.png");
        break;
    case 'o':itsTileImage.load(":/Images/1cloudleft.png");
        break;
    case 'p':itsTileImage.load(":/Images/1cloud.png");
        break;
    case 'q':itsTileImage.load(":/Images/1cloudright.png");
        break;
    case 'r':itsTileImage.load(":/Images/2cloudleft.png");
        break;
    case 's':itsTileImage.load(":/Images/2cloudmid1.png");
        break;
    case 't':itsTileImage.load(":/Images/2cloudmid2.png");
        break;
    case 'u':itsTileImage.load(":/Images/2cloudmid3.png");
        break;
    case 'v':itsTileImage.load(":/Images/cloudbottomright.png");
        break;
    case 'w':itsTileImage.load(":/Images/cloudbottomleft.png");
        break;
    case 'x':itsTileImage.load(":/Images/cloudbottom.png");
        break;
    case 'y':itsTileImage.load(":/Images/cloudfill.png");
        break;
    case 'z':itsTileImage.load(":/Images/stone.png");
        break;
    case 'A':itsTileImage.load(":/Images/barrierleft.png");
        break;
    case 'B':itsTileImage.load(":/Images/barriermid.png");
        break;
    case 'C':itsTileImage.load(":/Images/barrierright.png");
        break;
    case 'D':itsTileImage.load(":/Images/signleft.png");
        break;
    case 'E':itsTileImage.load(":/Images/signmid.png");
        break;
    case 'F':itsTileImage.load(":/Images/signright.png");
        break;
    case 'G':itsTileImage.load(":/Images/trunk.png");
        break;
    case 'H':itsTileImage.load(":/Images/signstick.png");
        break;
    case 'I':itsTileImage.load(":/Images/sol_solo_grand.png");
        break;
    case 'J':itsTileImage.load(":/Images/cloud_cote_droit.png");
        break;
    case 'K':itsTileImage.load(":/Images/sol_cote_droit.png");
        break;
    case 'L':itsTileImage.load(":/Images/herbe_coin_droit.png");
        break;
    case 'M':itsTileImage.load(":/Images/treetop.png");
        break;
    case 'N':itsTileImage.load(":/Images/sol_cote_gauche.png");
        break;
    case 'O':itsTileImage.load(":/Images/sol_coin_droit_bas.png");
        break;
    case 'P':itsTileImage.load(":/Images/sol_coin_gauche_bas.png");
        break;
    case 'Q':itsTileImage.load(":/Images/sol_bas.png");
        break;
    case 'R':itsTileImage.load(":/Images/treetop.png");
        break;
    case 'S':itsTileImage.load(":/Images/arrowup.png");
        break;
    case 'T':itsTileImage.load(":/Images/arrow down.png");
        break;
    case 'U':itsTileImage.load(":/Images/arrowright.png");
        break;
    case '?':itsTileImage.load(":/Images/flag1.png");
        break;
    case 'Z':itsTileImage.load(":/Images/flag1.png");
    /*    break;
    case '6':itsTileImage.load(":flag.png");
        break;
    case '7':itsTileImage.load(":flag.png");
        break;
    case '8':itsTileImage.load(":flag.png");
        break;
    case '9':itsTileImage.load(":flag.png");
        break;*/
    }
}

Tile::~Tile()
{

}

void Tile::setX(char x)
{
    int newStart=x-'0';
    itsX+=(newStart*48);
}

void Tile::setY(int x)
{
    itsY-=x;
}

void Tile::setSpeed(int aSpeed)
{
    itsSpeed=aSpeed;
}
void Tile::calculatePosition(double value)
{
    if(getValue()=='X')
    {
        setXOld(value);
    }
    itsX= itsX -(value*48);
}
void Tile::draw(QPainter * aPainter,int value)
{
    if(getValue()=='Z' || getValue()=='?')
    {
        if(value==0)
        {
            itsTileImage.load(":/Images/flag1.png");
        }
        else if(value==18 || value==128)
        {
            itsTileImage.load(":/Images/flag2.png");
        }
        else if(value==36 || value==110)
        {
            itsTileImage.load(":/Images/flag3.png");
        }
        else if(value==55 || value==91)
        {
            itsTileImage.load(":/Images/flag4.png");
        }
        else if(value==73)
        {
            itsTileImage.load(":/Images/flag5.png");
        }
    }
    Q_ASSERT(!itsTileImage.isNull());
    itsTileImage= itsTileImage.scaled(QSize(48,48));
    aPainter->drawImage(itsX,itsY,itsTileImage);
}

void Tile::setValue(char value)
{
    itsValue=value;
}

char Tile::getValue()
{
    return itsValue;
}
bool Tile::getFalling()
{
    return isFalling;
}

void Tile::move(int i)
{

}
void Tile::setFalling(bool aBool)
{
    isFalling=aBool;

}
QRect Tile::getRect()
{
    QRect TileBis;
    if(getValue()=='X' || getValue()=='Y' || getValue()=='W')
    {
        TileBis.setRect(itsX,itsY,itsWidth+48,itsHeight);
    }
    else if(getValue()=='8')
    {
        TileBis.setRect(itsX,itsY,itsWidth,itsHeight+48);
    }
    else
    {
        TileBis.setRect(itsX,itsY,itsWidth,itsHeight);
    }
    return TileBis;
}
string Tile::getSens()
{
    return itsSens;
}

void Tile::setXOld(double x)
{
    itsXOld-=(x*48);
}
