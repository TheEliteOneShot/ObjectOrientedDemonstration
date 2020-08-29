using ObjectOrientedDesign_SmartCops_DrinkTypes.Enums;
using ObjectOrientedDesign_SmartCops_DrinkTypes.Models;
using System;
using System.Collections.Generic;

namespace ObjectOrientedDesign_SmartCops_DrinkTypes.Factories
{
	public class DrinkFactory
	{
		public static BaseDrinkModel BuildDrink(DrinkCategories category, string drinkType, bool isCarbonated, int alcoholContent)
		{
			return category switch
			{
				DrinkCategories.Alcoholic => new AlcoholicDrinkModel(drinkType, isCarbonated, alcoholContent),
				DrinkCategories.Soda => new SodaDrinkModel(drinkType, alcoholContent),
				_ => throw new NotImplementedException("Drink category does not exist in the factory!")
			};
		}

		public static BaseDrinkModel BuildDrink(DrinkCategories category, string drinkType, bool isCarbonated, int alcoholContent, List<string> fruitTypes)
		{
			return category switch
			{
				DrinkCategories.Juice => new JuiceDrinkModel(drinkType, isCarbonated, alcoholContent, fruitTypes),
				_ => throw new NotImplementedException("Drink category does not exist in the factory!")
			};
		}
	}
}
