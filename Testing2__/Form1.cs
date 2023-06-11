using PlayerInventory;

namespace Testing2__
{
	public partial class Form1 : Form
	{
		public static Dictionary<Material, Color> Materials = new Dictionary<Material, Color>();

		private Inventory inventory;
		private VisualItemSlot[] slots;

		public ItemStack AddItem(ItemStack e)
		{
			var outp = inventory.AddItem(e);
			UpdateVisual();
			return outp;
		}
		public void SetItem(int slot, ItemStack e)
		{
			inventory.SetItem(slot, e);
			UpdateVisual();
		}
		public int RemoveItems(ItemStack e)
		{
			var outp = inventory.RemoveItems(e);
			UpdateVisual();
			return outp;
		}
		public ItemStack GetItem(int slot) => inventory.GetItem(slot);

		public Form1(int size)
		{
			Materials.Add(Material.Empty, Color.White);
			Materials.Add(Material.Wooden_Sword, Color.Red);
			Materials.Add(Material.Stone_Sword, Color.DarkRed);
			Materials.Add(Material.Shield, Color.Gold);
			Materials.Add(Material.Potion_Heal, Color.Green);
			Materials.Add(Material.Potion_Regeneration, Color.DarkGreen);

			Materials.Add(Material.Stone, Color.DarkGray);
			Materials.Add(Material.Wood, Color.Brown);
			Materials.Add(Material.Keks, Color.Orange);
			InitializeComponent();

			inventory = new Inventory(size);
			slots = new VisualItemSlot[size];
			Point coord = new Point(10, 10);
			int inLine = 7, space = 20;
			int count = 0;
			for (int i = 0; i < size; ++i)
			{
				slots[i] = new VisualItemSlot(i, ref inventory, coord);
				Controls.Add(slots[i]);

				if (count >= inLine)
				{
					coord.X = 10;
					coord.Y += slots[i].Height + space;
					count = 0;
				}
				else
				{
					coord.X += slots[i].Width + space;
					count++;
				}
			}

			// Fill inventory will some random stuff

			SetItem(0, new ItemStack(Material.Stone, 32, 64));
			SetItem(1, new ItemStack(Material.Stone, 46, 64));
			
			BackColor = Color.Black;

			Initialize();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		public void UpdateVisual()
		{
			foreach (var v in slots)
			{
				v.UpdateVisual(ref inventory);
			}
		}

		public static void Log(object s)
		{
			if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debug.WriteLine(s.ToString());
			Console.WriteLine(s.ToString());
		}
	}

	public class VisualItemSlot : Button
	{
		public int Slot;


		public VisualItemSlot(int Slot, ref Inventory ReferenceInv, Point coord)
		{
			this.Slot = Slot;
			this.Width = 100;
			this.Height = 100;
			this.Location = coord;
			this.Enabled = false;
			this.FlatStyle = FlatStyle.Flat;
			UpdateVisual(ref ReferenceInv);
		}

		public void UpdateVisual(ref Inventory inventory)
		{
			this.BackColor = Form1.Materials[inventory.GetItem(Slot).Material];
			this.Text = "" + inventory.GetItem(Slot).Amount;
		}
	}
}