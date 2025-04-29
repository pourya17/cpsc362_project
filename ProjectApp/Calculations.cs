using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProjectApp
{
    public class Calculations
    {

        public static double WeightToKg(ref float weight)
        {
            double weightKg = Math.Round((weight * 0.453592f) * 100) / 100;
            return weightKg;
        }

        public static double HeightToCm(ref int feet, ref float inches)
        {
            float height = ((feet * 12) + inches) * 2.54f;
            return height;
        }

        public static int TotalCaloriesConsumed(ref List<int> calories)
        {
            int total = 0;
            if (calories.Count == 0) { return -1; }
            foreach (int index in calories) { total += index; }
            return total;
        }

        public static double BmrCalc(ref float weight, ref float height, ref char gender, ref int age)
        {
            if (gender == 'M' || gender == 'm')
            {
                double bmr = 88.362 + (13.397 * weight) + (4.799 * height) - (5.677 * age);
                return bmr; // calorie formula for men
            }
            else
            {
                double bmr = 655.1 + (9.563 * weight) + (1.850 * height) - (4.676 * age);
                return bmr; // else calorie formula for women
            }
        }

        public static double CalculateCalories(ref double bmr, ref int activityLevel)
        {
            double[] activityMultipliers = { 1.2, 1.375, 1.55, 1.725, 1.9 };
            if (activityLevel < 1 || activityLevel > 5) return -1; // Invalid input
            return bmr * activityMultipliers[activityLevel - 1];
        }

        public static void CalculateMacros(double calories, out double protein, out double carbs, out double fats)
        {
            protein = calories * 0.4;
            carbs = calories * 0.3;
            fats = calories * 0.3;
        }

        public static void CalculateRanges(double calories, double protein, double carbs, double fats,
                                            out double calorieLower, out double calorieUpper,
                                            out double proteinLower, out double proteinUpper,
                                            out double carbLower, out double carbUpper,
                                            out double fatLower, out double fatUpper)
        {
            // calorie range
            calorieLower = calories * 0.95;
            calorieUpper = calories * 1.05;
            // protein range
            proteinLower = protein * 0.95;
            proteinUpper = protein * 1.05;
            // carb range
            carbLower = carbs * 0.95;
            carbUpper = carbs * 1.05;
            // fats range
            fatLower = fats * 0.95;
            fatUpper = fats * 1.05;
        }

    }
}
