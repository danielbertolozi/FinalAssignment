﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace FinalAssignment
{
	public class Program
	{
		public static void Main()
		{
			var host = new WebHostBuilder()
				.UseKestrel()
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseIISIntegration()
				.UseStartup<Startup>()
				.Build();

			host.Run();
		}
	}
}
