﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataTransferObjects;
using LogicLayer;
using LogicLayerInterfaces;

namespace WPFPresentationLayer.InventoryPages
{
	/// <summary>
	/// CREATED BY: Matt Deaton
	/// DATE: 2020-03-06
	/// CHECKED BY:
	/// 
	/// View for handling the Shelter Inventory
	/// Interaction logic for ShelterInventory.xaml
	/// </summary>
	/// <remarks>
	/// UPDATED BY:
	/// UPDATED:
	/// CHANGE:
	/// </remarks>
	public partial class ShelterInventory : Page
	{
		private IItemManager _itemManager;


		public ShelterInventory()
		{
			_itemManager = new ItemManager();
			InitializeComponent();
		}

		/// <summary>
		/// CREATED BY: Matt Deaton
		/// DATE: 2020-03-06
		/// CHECKED BY:
		/// 
		/// Method that loads the list of Shelter Items into the Data Grid on page load.
		/// 
		/// </summary>
		/// <remarks>
		/// UPDATED BY:
		/// UPDATED:
		/// CHANGE:
		/// </remarks>
		/// <param name="sender"></param>
		/// <param name="e"></param>

		private void dgViewShelterInventory_AutoGeneratedColumns(object sender, EventArgs e)
		{
			dgViewShelterInventory.Columns[0].Header = "Item ID";
			dgViewShelterInventory.Columns[1].Header = "Item Name";
			dgViewShelterInventory.Columns[2].Header = "Quantity";
			dgViewShelterInventory.Columns[3].Header = "Shelter Threshold";
			dgViewShelterInventory.Columns[4].Header = "Item Category";
			dgViewShelterInventory.Columns[5].Header = "Description";
			dgViewShelterInventory.Columns[5].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
			dgViewShelterInventory.Columns.RemoveAt(6); // Removes the Active Field from DataGrid
			dgViewShelterInventory.Columns.RemoveAt(6); // Removes the ShelterItem Field from DataGrid
		}// dgViewShelterInventory_AutoGeneratedColumns()

		/// <summary>
		/// CREATED BY: Matt Deaton
		/// DATE: 2020-03-06
		/// CHECKED BY:
		/// 
		/// Method that loads the list of Shelter Items into the Data Grid on page load.
		/// 
		/// </summary>
		/// <remarks>
		/// UPDATED BY:
		/// UPDATED:
		/// CHANGE:
		/// </remarks>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgViewShelterInventory_Loaded(object sender, RoutedEventArgs e)
		{
			dgViewShelterInventory.ItemsSource = _itemManager.RetrieveShelterItems();
			btnAllShelterInventory.Visibility = Visibility.Hidden;
			btnNeededItems.Visibility = Visibility.Visible;
		}// End dgViewShelterInventory_Loaded()

		/// <summary>
		/// CREATED BY: Matt Deaton
		/// DATE: 2020-03-06
		/// CHECKED BY:
		/// 
		/// Method that loads the list of Needed Shelter Items into the Data Grid 
		/// Button Click.
		/// 
		/// </summary>
		/// <remarks>
		/// UPDATED BY:
		/// UPDATED:
		/// CHANGE:
		/// </remarks>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnNeededItems_Click(object sender, RoutedEventArgs e)
		{
			dgViewShelterInventory.ItemsSource = _itemManager.RetrieveNeededShelterItems();
			btnAllShelterInventory.Visibility = Visibility.Visible;
			btnNeededItems.Visibility = Visibility.Hidden;
			lblShelterInvetoryHeader.Content = "Needed Shelter Items";
		}// End btnNeededItems_Click()

		/// <summary>
		/// CREATED BY: Matt Deaton
		/// DATE: 2020-03-06
		/// CHECKED BY:
		/// 
		/// Button that calls the RetrieveShelterItems methdod.
		/// Only visible during search or needed donations screens are used.
		/// 
		/// </summary>
		/// <remarks>
		/// UPDATED BY:
		/// UPDATED:
		/// CHANGE:
		/// </remarks>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAllShelterInventory_Click(object sender, RoutedEventArgs e)
		{
			dgViewShelterInventory.ItemsSource = _itemManager.RetrieveShelterItems();
			txtShelterItemSearch.Text = "";
			btnAllShelterInventory.Visibility = Visibility.Hidden;
			btnNeededItems.Visibility = Visibility.Visible;
			lblShelterInvetoryHeader.Content = "Shelter Inventory";
		}// End btnAllShelterInventory_Click()

		private void TxtShelterItemSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			List<Item> shelterItems = _itemManager.RetrieveShelterItems();
			List<Item> foundShelter = new List<Item>();
			foreach (var item in shelterItems)
			{
				if (item.ItemCategoryID.ToLower().Contains(txtShelterItemSearch.Text.ToLower()))
				{
					foundShelter.Add(item);
				}
			}
			dgViewShelterInventory.ItemsSource = foundShelter;

			if (txtShelterItemSearch.Text == "")
			{
				btnAllShelterInventory.Visibility = Visibility.Hidden;
				btnNeededItems.Visibility = Visibility.Visible;
			}
			else
			{
				btnAllShelterInventory.Visibility = Visibility.Visible;
				btnNeededItems.Visibility = Visibility.Hidden;
			}
		}// End TxtShelterItemSearch_TextChanged()

		private void btnAddDonation_Click(object sender, RoutedEventArgs e)
		{
			this.NavigationService?.Navigate(new AddDonatedItem());
		}// End btnAddDonation_Click()

		private void dgViewShelterInventory_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			Item shelterItem = (Item)dgViewShelterInventory.SelectedItem;
			this.NavigationService?.Navigate(new UpdateShelterItem(shelterItem, _itemManager));
		}
	}// End class ShelterInventory
}