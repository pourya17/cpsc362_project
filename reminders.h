#ifndef REMINDER_H
#define REMINDER_H

#include <iostream>
#include <vector>
#include <fstream>
#include <cstdlib>
#include <ctime>
#include "json.hpp" 

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

    void checkCalorieIntake(double caloriesConsumed, double calorieGoal) {
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
};

#endif
