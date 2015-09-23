#include <QCoreApplication>
#include "buildNumber.h"
#include <QDebug>

int main(int argc, char *argv[])
{
    QCoreApplication a(argc, argv);

    qDebug() << FULL_VER;

    return a.exec();
}

