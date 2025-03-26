#ifndef REMINDER_H
#define REMINDER_H

#include <iostream>
#include <vector>
#include <fstream>
#include <cstdlib>
#include <ctime>
#include "json.hpp" 
<<<<<<< HEAD
#include <calculations.h>
=======
#include "calculations.h"
>>>>>>> 1232889aa7320af528fdac13077c128be215a6e9

using json = nlohmann::json;
using namespace std;

class Reminder {
private:
    json remindersData;

    void loadReminders() {
        ifstream file("reminders.json");
        if (file.is_open()) {
            file >> remindersData;
            file.close();
        } else {
            cerr << "Error: Could not open reminders.json\n";
        }
    }

    string getRandomReminder(const string& category) {
        if (remindersData.contains(category)) {
            vector<string> reminders = remindersData[category].get<vector<string>>();
            if (!reminders.empty()) {
                return reminders[rand() % reminders.size()];
            }
        }
        return "No reminder found.";
    }

public:
    Reminder() {
        srand(time(0));  
        loadReminders();
    }

    void checkCalorieIntake(int calorieGoal) {
<<<<<<< HEAD
        int caloriesConsumed = total_calories_consumed();
=======
        vector<int> calories = {500, 600, 700}; // Example values, replace with actual logic
        int caloriesConsumed = total_calories_consumed(calories);

>>>>>>> 1232889aa7320af528fdac13077c128be215a6e9
        if (caloriesConsumed > calorieGoal) {
            cout << getRandomReminder("manyCalorieWarnings") << endl;
        } else if (caloriesConsumed < calorieGoal) {
            cout << getRandomReminder("lowCalorieWarnings") << endl;
        } else {
            cout << "Great job! You stayed within your calorie goal." << endl;
        }
    }

    void checkWeightChange(double oldWeight, double newWeight) {
        if (newWeight > oldWeight) {
            cout << getRandomReminder("weightGainWarnings") << endl;
        } else if (newWeight < oldWeight) {
            cout << getRandomReminder("weightLossWarnings") << endl;
        } else {
            cout << "Your weight is stable. Keep maintaining your lifestyle." << endl;
        } 
    }

    void randomReminders(int startHour){
        time_t now = time(nullptr);
        tm *localTime = localtime(&now);
        int currentHour = localTime -> tm_hour;
        if (currentHour == startHour){
        }
<<<<<<< HEAD
        // for 
=======

>>>>>>> 1232889aa7320af528fdac13077c128be215a6e9
    }
};

#endif
