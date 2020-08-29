using ObjectOrientedDesign_SmartCops_DrinkTypes.Enums;
using ObjectOrientedDesign_SmartCops_DrinkTypes.Interfaces;

namespace ObjectOrientedDesign_SmartCops_DrinkTypes.Models
{
	public class BaseDrinkModel : IDrink
	{
		public DrinkCategories DrinkCategory { get; set; }
		public string DrinkType { get; set; }
		public string Description { get; set; }
		public bool IsCarbonated { get; set; }
		public bool IsAlcoholic { get; set; }
		public int AlcoholContent { get; set; }

		public string GetDescription()
		{
			return Description;
		}
	}
}
