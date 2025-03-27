#include <iostream>
#include <string>
#include "entries.h"
#include <cstdlib>
#include <random>

std::vector<Entry_Type> entries{};

Entry::Entry(std::string food_name, int calories) {
    Entry_Type entry;
    entry.calories = calories;
    entry.food_name = food_name;

    // GENERATE UNIQUE ID
    int id = 1;
    std::random_device rd;
    std::mt19937 gen(rd());
    std::uniform_int_distribution<int> dist(0, 100000);
    
    for (int i = 0; i <= 6; i++) {
        id += dist(gen);
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