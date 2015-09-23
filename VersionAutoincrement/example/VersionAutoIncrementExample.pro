QT += core
QT -= gui

TARGET = VersionAutoIncrementExample
CONFIG += console
CONFIG -= app_bundle

TEMPLATE = app

SOURCES += main.cpp

win32:RC_FILE += myApp.rc

build_nr.commands = $$PWD/VersionAutoIncrement.exe $$PWD/buildNumber.h
build_nr.depends = FORCE
QMAKE_EXTRA_TARGETS += build_nr
PRE_TARGETDEPS += build_nr
