﻿using ObjectOrientedDesign_SmartCops_DrinkTypes.Enums;
using ObjectOrientedDesign_SmartCops_DrinkTypes.Factories;
using ObjectOrientedDesign_SmartCops_DrinkTypes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace WPF_ObjectOrientedDemonstration_SmartCops_Drinks
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly List<string> _drinkCategories;
		private readonly List<string> _fruitTypes;
		private readonly List<string> _sodaTypes;
		private readonly List<string> _alcoholTypes;
		private readonly List<string> _juiceTypes;

		private DrinkCategories _selectedDrinkCategory;

		private readonly List<BaseDrinkModel> _createdDrinkList = new List<BaseDrinkModel>();

		public MainWindow()
		{
			InitializeComponent();

			_drinkCategories = Enum.GetValues(typeof(DrinkCategories)).Cast<DrinkCategories>().Select(v => v.ToString()).ToList();
			_fruitTypes = Enum.GetValues(typeof(FruitTypes)).Cast<FruitTypes>().Select(v => v.ToString()).ToList();
			_sodaTypes = Enum.GetValues(typeof(SodaTypes)).Cast<SodaTypes>().Select(v => v.ToString()).ToList();
			_alcoholTypes = Enum.GetValues(typeof(AlcoholTypes)).Cast<AlcoholTypes>().Select(v => v.ToString()).ToList();
			_juiceTypes = Enum.GetValues(typeof(JuiceTypes)).Cast<JuiceTypes>().Select(v => ExtensionHelpers.ToFriendlyString(v)).ToList();

			ComboBox_DrinkCategories.ItemsSource = _drinkCategories;
			ListBox_Fruits.ItemsSource = _fruitTypes;
		}
		#region Window Events

		private void ComboBox_DrinkCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			string selectedCategory = (sender as ComboBox).SelectedItem as string;

			if (string.Equals(selectedCategory, "Soda"))
			{
				CheckBox_IsCarbonated.IsChecked = true;
				CheckBox_IsCarbonated.IsEnabled = false;
			}
			else
			{
				CheckBox_IsCarbonated.IsChecked = false;
				CheckBox_IsCarbonated.IsEnabled = true;
			}

			if (string.Equals(selectedCategory, "Juice"))
			{
				ListBox_Fruits.IsEnabled = true;
			}
			else
			{
				ListBox_Fruits.IsEnabled = false;
				ListBox_Fruits.SelectedItems.Clear();
			}

			if (Enum.IsDefined(typeof(DrinkCategories), selectedCategory))
			{
				ComboBox_DrinkTypes.IsEnabled = true;
			}
			else
			{
				ComboBox_DrinkTypes.IsEnabled = false;
			}

			switch (selectedCategory)
			{
				case "Alcoholic":
					AlcoholicCategorySelected();
					break;
				case "Juice":
					JuiceCategorySelected();
					break;
				case "Soda":
					SodaCategorySelected();
					break;
				default:
					throw new NotImplementedException("The drink category was not found!");
			}
		}

		private void ComboBox_DrinkTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (ComboBox_DrinkTypes.SelectedIndex > -1)
			{
				Button_Build.IsEnabled = true;
			}
			else
			{
				Button_Build.IsEnabled = false;
			}
		}

		private void TextBox_AlcoholContent_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
		{
			Regex regex = new Regex("[^0-9]+");
			e.Handled = regex.IsMatch(e.Text);
		}

		private void Button_Clear_Click(object sender, RoutedEventArgs e)
		{
			_createdDrinkList.Clear();
			DisplayResults();
		}

		private void CheckBox_Checked(object sender, RoutedEventArgs e)
		{
			Label_AlcoholContent.Visibility = Visibility.Visible;
			TextBox_AlcoholContent.Visibility = Visibility.Visible;
		}

		private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			Label_AlcoholContent.Visibility = Visibility.Hidden;
			TextBox_AlcoholContent.Visibility = Visibility.Hidden;
			TextBox_AlcoholContent.Text = "0";
		}

		private void Button_Build_Click(object sender, RoutedEventArgs e)
		{
			AddDrink();
			DisplayResults();
		}
		#endregion

		#region Drink Category Selected
		private void AlcoholicCategorySelected()
		{
			_selectedDrinkCategory = DrinkCategories.Alcoholic;
			ComboBox_DrinkTypes.ItemsSource = _alcoholTypes;
		}

		private void JuiceCategorySelected()
		{
			_selectedDrinkCategory = DrinkCategories.Juice;
			ComboBox_DrinkTypes.ItemsSource = _juiceTypes;
		}

		private void SodaCategorySelected()
		{
			_selectedDrinkCategory = DrinkCategories.Soda;
			ComboBox_DrinkTypes.ItemsSource = _sodaTypes;
		}
		#endregion

		#region Add Drink and Display Results
		private void AddDrink()
		{

			switch (_selectedDrinkCategory)
			{
				case DrinkCategories.Alcoholic:
					_createdDrinkList.Add(DrinkFactory.BuildDrink(_selectedDrinkCategory, ComboBox_DrinkTypes.SelectedItem.ToString(), CheckBox_IsCarbonated.IsChecked.Value, int.Parse(TextBox_AlcoholContent.Text)));
					break;
				case DrinkCategories.Juice:
					_createdDrinkList.Add(DrinkFactory.BuildDrink(_selectedDrinkCategory, ComboBox_DrinkTypes.SelectedItem.ToString(), CheckBox_IsCarbonated.IsChecked.Value, int.Parse(TextBox_AlcoholContent.Text), ListBox_Fruits.SelectedItems.Cast<string>().ToList()));
					break;
				case DrinkCategories.Soda:
					_createdDrinkList.Add(DrinkFactory.BuildDrink(_selectedDrinkCategory, ComboBox_DrinkTypes.SelectedItem.ToString(), CheckBox_IsCarbonated.IsChecked.Value, int.Parse(TextBox_AlcoholContent.Text)));
					break;
				default:
					throw new NotImplementedException("Selected category doesn't exist while adding the drink!");
			}
		}

		private void DisplayResults()
		{
			TextBox_Results.Text = "";

			Label_CurrentDrinkAmount.Content = $"You currently have {_createdDrinkList.Count} drinks.";
			int index = 1;

			foreach (BaseDrinkModel drink in _createdDrinkList)
			{
				TextBox_Results.AppendText(index.ToString() + ". - ");
				TextBox_Results.AppendText(drink.GetDescription());
				TextBox_Results.AppendText(Environment.NewLine);
				index++;
			}
		}

		#endregion

	}
}
