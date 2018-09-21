﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles.Helpers
{
	using System.IO;
	public class FilesHelper
	{
		public static byte[] ReadFully(Stream input)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				input.CopyTo(ms);
				return ms.ToArray();
			}
		}
	}
}
