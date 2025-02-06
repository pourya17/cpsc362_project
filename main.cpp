#include <iostream>
#include "calculations.h"
using namespace std;

double weight = 0;
double height = 0;

int main() {
    float height, weight;
    int activityLevel, age;
    char gender;

    // User Input
    cout << "Enter your weight (lbs): ";
    cin >> weight;
    cout << "Enter your height (feet, inches): ";
    cin >> height_fe;
    cout << "Enter your gender (M/F): ";
    cin >> gender;
    cout << "Enter your age";
    cin >> age;
    cout << "Enter your activity level (1-5): ";
    cout << "\n1. Sedentary (little to no exercise)";
    cout << "\n2. Lightly active (light exercise 1-3 days per week)";
    cout << "\n3. Moderately active (moderate exercise 3-5 days per week)";
    cout << "\n4. Very active (hard exercise 6-7 days per week)";
    cout << "\n5. Super active (very intense exercise daily)\n";
    cin >> activityLevel;

    // Calculate BMR and Caloric Needs
       double bmr = bmr_calc(weight, height, gender, age);
       double calories = calculateCalories(bmr, activityLevel);

    if (calories == -1) {
        cout << "Invalid activity level entered." << endl;
    } else {
        cout << "Your estimated daily calorie needs: " << calories << " kcal" << endl;
    }

    return 0;
}

