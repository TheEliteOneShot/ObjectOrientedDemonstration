namespace ObjectOrientedDesign_SmartCops_DrinkTypes.Enums
{
	public enum JuiceTypes
	{
		OrangeJuice,
		AppleJuice,
		CherryJuice,
		LemonJuice,
		MangoJuice
	}

	public static class ExtensionHelpers
	{
		public static string ToFriendlyString(this JuiceTypes juiceType)
		{
			return juiceType switch
			{
				JuiceTypes.AppleJuice => "Apple Juice",
				JuiceTypes.CherryJuice => "Cherry Juice",
				JuiceTypes.MangoJuice => "Mango Juice",
				JuiceTypes.LemonJuice => "Lemon Juice",
				JuiceTypes.OrangeJuice => "Orange Juice",
				_ => "No Type Was Selected",
			};
		}
	}
}
