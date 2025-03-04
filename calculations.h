#ifndef CALCULATIONS_H
#define CALCULATIONS_H

#include <iostream>
#include <math.h>
#include <vector>

using namespace std;

double weight_to_kg(float &weight) {
    float weight_kg = round( (weight * 0.453592) * 100) / 100; 
    return weight_kg;
}

double height_to_cm(int &feet, float &inches) {
    float height = ((feet * 12) + inches)*2.54;
    return height;
}

int total_calories_consumed(vector<int> &calories) {
    int total = 0;
    if (calories.empty()) { return -1; }
    for (int &index : calories) { total += index; }
    return total;
}

double bmr_calc(float &weight, float &height, char &gender, int &age) {
   if (gender == 'M' || gender == 'm') {
          //double bmr = (10 * weight_to_kg(weight)) + (6.25 * (height)) - (5 * age) + 5;
            double bmr = 88.362 + (13.397 * weight) + (4.799 * height) - (5.677 * age);
    return bmr;} //calorie formula for men

    else {
          //double bmr = (10 * weight_to_kg(weight)) + (6.25 * (height)) - (5 * age) - 16;
        double bmr = 655.1 + (9.563 * weight) + (1.850 * height) - (4.676 * age);
    return bmr; //else calorie formula for women
    }
}

double calculateCalories(double &bmr, int &activityLevel) {
    double activityMultipliers[5] = {1.2, 1.375, 1.55, 1.725, 1.9};
    if (activityLevel < 1 || activityLevel > 5) return -1; // Invalid input
    return bmr * activityMultipliers[activityLevel - 1];
}

void calculateMacros(double calories){
    double protein = calories * .4;
    double carbs = calories * .3;
    double fats = calories * .3;
}

void calculateRanges(double calories, double &protein, double &carbs, double &fats) {
    //calorie range
    double calorieLower = calories * .95;
    double calorieUpper = calories * 1.05;
    //protein range
    double proteinLower = protein * .95;
    double proteinUpper = protein * 1.05;
    //carb range
    double carbLower = carbs * .95;
    double carbUpper = carbs * 1.05;
    //fats range
    double fatLower = fats * .95;
    double fatUpper = fats * 1.05;
}

#endif