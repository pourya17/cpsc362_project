#ifndef DISPLAY_H
#define DISPLAY_H

#include <iostream>
using namespace std;

void display_height_cm(double &height) {
    cout << height << " cm" << endl;
}

void display_weight_kg(double &weight) {
    cout << weight << " kg" << endl;
}

void display_total_calories(int &total) {
    cout << total << " calories" << endl;
}

#endif