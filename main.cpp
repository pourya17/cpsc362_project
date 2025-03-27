#include <iostream>
#include "calculations.h"
#include "entries.h"
#include "reminders.h"
#include <string>

using namespace std;



int main() {
    float height, inches;
    int feet;
    float weight, weight_kg;
    double protein, fats, carbs, baseCalories, adjustedCalories, calorieUpper, calorieLower, proteinLower, proteinUpper, carbLower, carbUpper, fatLower, fatUpper, fatCalories, proteinCalories, carbCalories;
    int activityLevel, age;
    char gender, fitnessGoal;

    // User Input
    std::cout << "Enter your weight (lbs): ";
    std::cin >> weight;
    std::cout << "Enter your height (feet inches): ";
    std::cin >> feet >> inches;
    std::cout << "Enter your gender (M/F): ";
    std::cin >> gender;
    std::cout << "Enter your age: ";
    std::cin >> age;
    std::cout << "Enter your activity level (1-5): ";
    std::cout << "\n1. Sedentary (little to no exercise)";
    std::cout << "\n2. Lightly active (light exercise 1-3 days per week)";
    std::cout << "\n3. Moderately active (moderate exercise 3-5 days per week)";
    std::cout << "\n4. Very active (hard exercise 6-7 days per week)";
    std::cout << "\n5. Super active (very intense exercise daily)\n";
    std::cin >> activityLevel;

   //convert weight and height to metric
    weight_kg = weight_to_kg(weight);
    height = height_to_cm(feet, inches);

    // Calculate BMR and Caloric Needs for Weight Maintenance 
       double bmr = bmr_calc(weight_kg, height, gender, age);
       double calories = calculateCalories(bmr, activityLevel);
       void calculateMacros(double calories, double &protein, double &carbs, double &fats); //calc prot carb fat
       void calculateRanges(double calories, double &protein, double &carbs, double &fats); //calc ranges of each macro
       void inputMacroBreakdown(double totalCalories); //ask for custom goals
 //goal
    double adjustCalories(double baseCalories);

    if (calories == -1) {
        cout << "Invalid activity level entered." << endl;
    } else {
       std::cout << "Your estimated daily nutritional needs:\n"; 
       std::cout << "Calories:" << calories << " kcal\n";
       std::cout << "Protein: " << protein << " g\n";
       std::cout << "Carbs: " << carbs << " g\n";
       std::cout << "Fats: " << fats << " g\n";
    }

    return 0;
}
