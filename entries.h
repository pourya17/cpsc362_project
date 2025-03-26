#ifndef ENTRY_H
#define ENTRY_H
#include <iostream>
#include <vector>
#include <string>

// Entry class used for creating or deleting entries.
// Ex: You ate cereal, so you add a new entry with the quantity, calories, etc

struct Entry_Type {
<<<<<<< HEAD
  std::string food_name;
  int id = 100000;
  int calories, potassium, sodium, fiber, sugars,
  protein, vitaminA, vitaminC, calcium, iron;
=======
  string food_name;
  int id = 100000;
  int calories;
>>>>>>> 1232889aa7320af528fdac13077c128be215a6e9
};

class Entry {

    public:
      Entry(std::string food_name, int calories);
      void deleteEntry(Entry_Type entry);
      void displayEntries() const;
    //  Entry getEntry();
<<<<<<< HEAD
      std::vector<Entry_Type> entries;
=======
      vector<Entry_Type> entries;
>>>>>>> 1232889aa7320af528fdac13077c128be215a6e9

};


<<<<<<< HEAD
#endif
=======
#endif
>>>>>>> 1232889aa7320af528fdac13077c128be215a6e9
