﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Commerce_Sv.Models.ViewModels
{
	public class ProductVM
	{
		public Product Product { get; set; }
		public IEnumerable<Product>? Products { get; set; }
		[ValidateNever]
		public IEnumerable<SelectListItem> CategoryList { get; set; }
	}
}
