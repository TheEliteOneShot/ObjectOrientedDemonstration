using ObjectOrientedDesign_SmartCops_DrinkTypes.Enums;

namespace ObjectOrientedDesign_SmartCops_DrinkTypes.Interfaces
{
	public interface IDrink
	{
		public DrinkCategories DrinkCategory { get; set; }
		public string DrinkType { get; set; }
		public string Description { get; set; }
		public string GetDescription()
		{
			return Description;
		}
	}
}
