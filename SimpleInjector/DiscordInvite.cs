using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleInjector {
    public class DiscordInvite : Control {

        [Category("Appearance"), Description("The ending color of the bar.")]
        public bool Verified { get; set; }

        public DiscordInvite() {

        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);

            MinimumSize = MaximumSize = Size = new Size(215, 55);

            Graphics g = e.Graphics;

            using (Brush b = new SolidBrush(Color.FromArgb(47, 49, 54))) {
                g.FillRoundedRectangle(b, new Rectangle(0, 0, Width - 1, Height - 1), 4);
            }

            using (Brush b = new SolidBrush(Color.FromArgb(67, 181, 129))) {
                g.FillRoundedRectangle(b, new Rectangle(Width - 50, Height - 45, 50, 35), 4);
            }
        }
    }
}
