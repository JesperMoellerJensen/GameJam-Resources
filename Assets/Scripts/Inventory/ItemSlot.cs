using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ItemSlot {
    public Item Item { get; }
    public int StackSize;

    public ItemSlot(Item item, int stackSize) {
        Item = item;
        StackSize = stackSize;
    }
}
