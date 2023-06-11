using PlayerInventory;

namespace Testing2__
{
	internal static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
			Test();
			ApplicationConfiguration.Initialize();
			Application.Run(new Form1(24));
		}

		static void Test()
		{
			Inventory inv = new Inventory(3);
			inv.SetItem(0, new ItemStack(Material.Stone, 32, 64));
			inv.SetItem(1, new ItemStack(Material.Stone, 46, 64));
			Log(inv.GetItem(20));
			Log(inv);
			inv.AddItem(new ItemStack(Material.Stone, 64, 64));
			Log(inv);
		}

		static void Log(object s)
		{
			if(System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debug.WriteLine(s.ToString());
			Console.WriteLine(s.ToString());
		}
	}
}