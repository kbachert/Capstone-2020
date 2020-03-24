﻿using DataTransferObjects;
using DataAccessInterfaces;
using LogicLayerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace LogicLayer
{
	/// <summary>
	/// Creator: Cash Carlson
	/// Created: 03/19/2020
	/// Approver: Rob Holmes
	/// 
	/// Logic Manager for Sales Data
	/// </summary>
	public class SalesDataManager : ISalesDataManager
	{
		private ISalesDataAccessor _salesDataAccessor;

		/// <summary>
		/// Creator: Cash Carlson
		/// Created: 03/19/2020
		/// Approver: Rob Holmes
		/// 
		/// Default Constructor for Sales Data Manager
		/// </summary>
		public SalesDataManager()
		{
			_salesDataAccessor = new SalesDataAccessor();
		}

		/// <summary>
		/// Creator: Cash Carlson
		/// Created: 03/19/2020
		/// Approver: Rob Holmes
		/// 
		/// Constructor for testing
		/// </summary>
		/// <param name="salesDataAccessor"></param>
		public SalesDataManager(ISalesDataAccessor salesDataAccessor)
		{
			_salesDataAccessor = salesDataAccessor;
		}

		/// <summary>
		/// Creator: Cash Carlson
		/// Created: 03/19/2020
		/// Approver: Rob Holmes
		/// 
		/// Retrieve all Total Sales Data
		/// </summary>
		/// <returns></returns>
		public List<SalesDataVM> RetrieveAllTotalSalesData()
		{
			try
			{
				return _salesDataAccessor.RetrieveAllTotalSalesData();
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Data not found.", ex);
			}
		}
	}
}