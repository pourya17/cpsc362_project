#include <iostream>
#include "calculations.h"
#include "entries.h"

using namespace std;

double weight = 0;
double height = 0;

int main() {
    float height, inches;
    int feet;
    float weight, weight_kg;
    double protein, fats, carbs, calorieUpper, calorieLower, proteinLower, proteinUpper, carbLower, carbUpper, fatLower, fatUpper;
    int activityLevel, age;
    char gender;

    // User Input
    cout << "Enter your weight (lbs): ";
    cin >> weight;
    cout << "Enter your height (feet inches): ";
    cin >> feet >> inches;
    cout << "Enter your gender (M/F): ";
    cin >> gender;
    cout << "Enter your age: ";
    cin >> age;
    cout << "Enter your activity level (1-5): ";
    cout << "\n1. Sedentary (little to no exercise)";
    cout << "\n2. Lightly active (light exercise 1-3 days per week)";
    cout << "\n3. Moderately active (moderate exercise 3-5 days per week)";
    cout << "\n4. Very active (hard exercise 6-7 days per week)";
    cout << "\n5. Super active (very intense exercise daily)\n";
    cin >> activityLevel;
    weight_kg = weight_to_kg(weight);
    height = height_to_cm(feet, inches);
    // Calculate BMR and Caloric Needs for Weight Maintenance 
       double bmr = bmr_calc(weight_kg, height, gender, age);
       double calories = calculateCalories(bmr, activityLevel);
       void calculateMacros(double calories, double &protein, double &carbs, double &fats); //calc prot carb fat
       void calculateRanges(double calories, double &protein, double &carbs, double &fats); //calc ranges of each macro

    if (calories == -1) {
        cout << "Invalid activity level entered." << endl;
    } else {
        cout << "Your estimated daily nutritional needs: \n" << "Calories:" << calories << " kcal" << endl;
        cout << "Protein: " << protein << "g" << endl;
        cout << "Carbs: " << carbs << "g" << endl;
        cout << "Fats: " << fats << "g" << endl;
    }     

    Entry entry{"Cereal", 200};

   // cout << height << weight_kg;
    return 0;
}
