namespace PlayerInventory
{
    public class Inventory
    {
        ItemStack[] Contents;

        public static ItemStack EmptyStack = new ItemStack(Material.Empty, 0, 0);

        public Inventory(int size)
        {
            Contents = new ItemStack[size];
            for (int i = 0; i < size; ++i)
            {
                Contents[i] = EmptyStack;
            }
        }

        public ItemStack AddItem(ItemStack e)
        {
            for(int i = 0; i < Contents.Length; ++i)
			{
                if (Contents[i].Material == Material.Empty)
				{
                    Contents[i] = e;
                    return EmptyStack;
				}
                else if(Contents[i].Material == e.Material)
				{
                    int remainingSpace = Contents[i].MaxStackSize - Contents[i].Amount;
                    if(e.Amount <= remainingSpace)
                    {
                        Contents[i].Amount += e.Amount;
                        return EmptyStack;
                    }
					else
					{
                        e.Amount -= remainingSpace;
                        Contents[i].Amount += remainingSpace;
                    }
                }
			}
            // Guaranteed something left over
            return e;
        }

        public void SetItem(int slot, ItemStack e)
        {
            if (slot < 0) return;
            if (slot >= Contents.Length) return;
            Contents[slot] = e;
        }

        public int RemoveItems(ItemStack e)
        {
            for (int i = 0; i < Contents.Length; ++i)
            {
                if(e.Material == Contents[i].Material)
				{
                    if(Contents[i].Amount >= e.Amount)
					{
                        Contents[i].Amount -= e.Amount;
                        return 0;
					}
                    else
                    {
                        e.Amount -= Contents[i].Amount;
                        Contents[i] = EmptyStack;
                    }
				}
            }
            return e.Amount;
        }

        public int FirstEmptySlot()
        {
            for (int i = 0; i < Contents.Length; ++i)
            {
                if (Contents[i].Material == Material.Empty) return i;
            }
            return -1;
        }

        public int FirstSlotOfType(Material mat)
        {
            for (int i = 0; i < Contents.Length; ++i)
            {
                if (Contents[i].Material == mat) return i;
            }
            return -1;
        }

        public int CountItem(Material mat)
        {
            int count = 0;

            for (int i = 0; i < Contents.Length; ++i)
            {
                if (Contents[i].Material == mat) count += Contents[i].Amount;
            }

            return count;
        }

        public bool HasEnoughItems(Material mat, int amount) => CountItem(mat) >= amount;

        public ItemStack GetItem(int slot)
        {
            if(slot < 0) return EmptyStack;
            if(slot >= Contents.Length) return EmptyStack;
            return Contents[slot];
        }

		public override string ToString()
		{
            string s = "[";
            for (int i = 0; i < Contents.Length -1; ++i)
			{
                s += Contents[i].ToString() + ", ";
			}
            s += Contents[Contents.Length - 1].ToString() + "]";
            return s;
		}
	}

    public class ItemStack
    {
        public Material Material;
        public int Amount;
        public int MaxStackSize;

        public ItemStack(Material mat, int amount, int MaxStackSize = 64)
        {
            this.Material = mat;
            this.Amount = amount;
            this.MaxStackSize = MaxStackSize;
        }

		public override string ToString()
		{
            return "{Material: " + Material + ", Amount: " + Amount + ", MaxStackSize: " + MaxStackSize + "}";
		}
	}

    public enum Material
    {
        Empty,
        Wooden_Sword, Stone_Sword,
        Shield,
        Potion_Heal, Potion_Regeneration,
        Stone, Wood, Keks
    }
}