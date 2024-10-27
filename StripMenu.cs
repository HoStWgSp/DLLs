using System.Windows.Forms;
using System.Drawing;

namespace UserData
{
    public class StripMenu : ContextMenuStrip
    {
        private ToolStripMenuItem StripMenuItem1, StripMenuItem2, StripMenuItem3;

        public StripMenu(string Item1Text)
        {
            StripMenuItem1 = new ToolStripMenuItem()
            {
                Name = Item1Text,
                Size = new Size(116, 22),
                Text = Item1Text

            };

            Name = "StripMenu";
            Size = new System.Drawing.Size(117, 48);
            Items.AddRange(new ToolStripItem[] { StripMenuItem1 });
        }
        public StripMenu(string Item1Text, string Item2Text)
        {
            StripMenuItem1 = new ToolStripMenuItem()
            {
                Name = Item1Text,
                Size = new Size(116, 22),
                Text = Item1Text

            };
            StripMenuItem2 = new ToolStripMenuItem()
            {
                Name = Item2Text,
                Size = new Size(116, 22),
                Text = Item2Text

            };

            Name = "StripMenu";
            Size = new System.Drawing.Size(117, 48);
            Items.AddRange(new ToolStripItem[] { StripMenuItem1, StripMenuItem2 });
        }
        public StripMenu(string Item1Text, string Item2Text, string Item3Text)
        {
            StripMenuItem1 = new ToolStripMenuItem()
            {
                Name = Item1Text,
                Size = new Size(116, 22),
                Text = Item1Text

            };
            StripMenuItem2 = new ToolStripMenuItem()
            {
                Name = Item2Text,
                Size = new Size(116, 22),
                Text = Item2Text

            };
            StripMenuItem3 = new ToolStripMenuItem()
            {
                Name = Item3Text,
                Size = new Size(116, 22),
                Text = Item3Text

            };

            Name = "StripMenu";
            Size = new System.Drawing.Size(117, 48);
            Items.AddRange(new ToolStripItem[] { StripMenuItem1, StripMenuItem2, StripMenuItem3 });
        }
    }
}
