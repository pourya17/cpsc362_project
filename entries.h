#ifndef ENTRY_H
#define ENTRY_H
#include <iostream>
#include <vector>
#include <string>

// Entry class used for creating or deleting entries.
// Ex: You ate cereal, so you add a new entry with the quantity, calories, etc

struct Entry_Type {
  string food_name;
  int id = 100000;
  int calories;
};

class Entry {

    public:
      Entry(std::string food_name, int calories);
      void deleteEntry(Entry_Type entry);
      void displayEntries() const;
    //  Entry getEntry();
      vector<Entry_Type> entries;

};


#endif
