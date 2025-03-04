#include <iostream>
#include <string>
#include "entries.h"
#include <cstdlib>

Entry::Entry(std::string food_name, int calories) {
    Entry_Type entry;
    entry.calories = calories;
    entry.food_name = food_name;
    int id;

    for (int i = 0; i <= 6; i++) {
        int randomId = rand() % 10;
        id += randomId;
    }

    entry.id = id;
    std::cout << id << std::endl;
    entries.push_back(entry);
}

void Entry::deleteEntry(Entry_Type entry) {
    for (int i = 0; i <= entries.size(); i++) {
        if (entries.at(i).food_name == entry.food_name) {
            entries.erase(entries.begin() + i);
            return;
        }
    }
}

void Entry::displayEntries() const {

}