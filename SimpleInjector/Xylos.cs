using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

// Xylos Theme.
// Made by AeroRev9.
// 9 Controls.

internal sealed class Helpers {
    public enum RoundingStyle : byte {
        All,
        Top,
        Bottom,
        Left,
        Right,
        TopRight,
        BottomRight
    }

    public static void CenterString(Graphics G, string T, Font F, Color C, Rectangle R) {
        SizeF sizeF = G.MeasureString(T, F);
        using (SolidBrush solidBrush = new SolidBrush(C)) {
            G.DrawString(T, F, solidBrush, checked(new Point((int)Math.Round(unchecked((double)R.Width / 2.0 - (double)(sizeF.Width / 2f))), (int)Math.Round(unchecked((double)R.Height / 2.0 - (double)(sizeF.Height / 2f))))));
        }
    }

    public static Color ColorFromHex(string Hex) {
        return Color.FromArgb(checked((int)long.Parse(string.Format("FFFFFFFFFF{0}", Hex.Substring(1)), NumberStyles.HexNumber)));
    }

    public static Rectangle FullRectangle(Size S, bool Subtract) {
        Rectangle result;
        if (Subtract) {
            result = checked(new Rectangle(0, 0, S.Width - 1, S.Height - 1));
        } else {
            result = new Rectangle(0, 0, S.Width, S.Height);
        }
        return result;
    }

    public static GraphicsPath RoundRect(Rectangle Rect, int Rounding, Helpers.RoundingStyle Style = RoundingStyle.All) {
        GraphicsPath graphicsPath = new GraphicsPath();
        checked {
            int num = Rounding * 2;
            graphicsPath.StartFigure();
            bool flag = Rounding == 0;
            GraphicsPath result;
            if (flag) {
                graphicsPath.AddRectangle(Rect);
                graphicsPath.CloseAllFigures();
                result = graphicsPath;
            } else {
                switch (Style) {
                    case RoundingStyle.All:
                        graphicsPath.AddArc(new Rectangle(Rect.X, Rect.Y, num, num), -180f, 90f);
                        graphicsPath.AddArc(new Rectangle(Rect.Width - num + Rect.X, Rect.Y, num, num), -90f, 90f);
                        graphicsPath.AddArc(new Rectangle(Rect.Width - num + Rect.X, Rect.Height - num + Rect.Y, num, num), 0f, 90f);
                        graphicsPath.AddArc(new Rectangle(Rect.X, Rect.Height - num + Rect.Y, num, num), 90f, 90f);
                        break;
                    case RoundingStyle.Top:
                        graphicsPath.AddArc(new Rectangle(Rect.X, Rect.Y, num, num), -180f, 90f);
                        graphicsPath.AddArc(new Rectangle(Rect.Width - num + Rect.X, Rect.Y, num, num), -90f, 90f);
                        graphicsPath.AddLine(new Point(Rect.X + Rect.Width, Rect.Y + Rect.Height), new Point(Rect.X, Rect.Y + Rect.Height));
                        break;
                    case RoundingStyle.Bottom:
                        graphicsPath.AddLine(new Point(Rect.X, Rect.Y), new Point(Rect.X + Rect.Width, Rect.Y));
                        graphicsPath.AddArc(new Rectangle(Rect.Width - num + Rect.X, Rect.Height - num + Rect.Y, num, num), 0f, 90f);
                        graphicsPath.AddArc(new Rectangle(Rect.X, Rect.Height - num + Rect.Y, num, num), 90f, 90f);
                        break;
                    case RoundingStyle.Left:
                        graphicsPath.AddArc(new Rectangle(Rect.X, Rect.Y, num, num), -180f, 90f);
                        graphicsPath.AddLine(new Point(Rect.X + Rect.Width, Rect.Y), new Point(Rect.X + Rect.Width, Rect.Y + Rect.Height));
                        graphicsPath.AddArc(new Rectangle(Rect.X, Rect.Height - num + Rect.Y, num, num), 90f, 90f);
                        break;
                    case RoundingStyle.Right:
                        graphicsPath.AddLine(new Point(Rect.X, Rect.Y + Rect.Height), new Point(Rect.X, Rect.Y));
                        graphicsPath.AddArc(new Rectangle(Rect.Width - num + Rect.X, Rect.Y, num, num), -90f, 90f);
                        graphicsPath.AddArc(new Rectangle(Rect.Width - num + Rect.X, Rect.Height - num + Rect.Y, num, num), 0f, 90f);
                        break;
                    case RoundingStyle.TopRight:
                        graphicsPath.AddLine(new Point(Rect.X, Rect.Y + 1), new Point(Rect.X, Rect.Y));
                        graphicsPath.AddArc(new Rectangle(Rect.Width - num + Rect.X, Rect.Y, num, num), -90f, 90f);
                        graphicsPath.AddLine(new Point(Rect.X + Rect.Width, Rect.Y + Rect.Height - 1), new Point(Rect.X + Rect.Width, Rect.Y + Rect.Height));
                        graphicsPath.AddLine(new Point(Rect.X + 1, Rect.Y + Rect.Height), new Point(Rect.X, Rect.Y + Rect.Height));
                        break;
                    case RoundingStyle.BottomRight:
                        graphicsPath.AddLine(new Point(Rect.X, Rect.Y + 1), new Point(Rect.X, Rect.Y));
                        graphicsPath.AddLine(new Point(Rect.X + Rect.Width - 1, Rect.Y), new Point(Rect.X + Rect.Width, Rect.Y));
                        graphicsPath.AddArc(new Rectangle(Rect.Width - num + Rect.X, Rect.Height - num + Rect.Y, num, num), 0f, 90f);
                        graphicsPath.AddLine(new Point(Rect.X + 1, Rect.Y + Rect.Height), new Point(Rect.X, Rect.Y + Rect.Height));
                        break;
                }
                graphicsPath.CloseAllFigures();
                result = graphicsPath;
            }
            return result;
        }
    }
}
public class XylosTabControl : TabControl {
    private Graphics G;

    private Rectangle Rect;

    private int _OverIndex;

    public bool FirstHeaderBorder {
        get;
        set;
    }

    private int OverIndex {
        get {
            return _OverIndex;
        }
        set {
            _OverIndex = value;
            Invalidate();
        }
    }

    public XylosTabControl() {
        _OverIndex = -1;
        DoubleBuffered = true;
        Alignment = TabAlignment.Left;
        SizeMode = TabSizeMode.Fixed;
        ItemSize = new Size(40, 180);
    }

    protected override void OnCreateControl() {
        base.OnCreateControl();
        SetStyle(ControlStyles.UserPaint, true);
    }

    protected override void OnControlAdded(ControlEventArgs e) {
        base.OnControlAdded(e);
        e.Control.BackColor = Color.White;
        e.Control.ForeColor = Helpers.ColorFromHex("#7C858E");
        e.Control.Font = new Font("Segoe UI", 9f);
    }

    protected override void OnPaint(PaintEventArgs e) {
        G = e.Graphics;
        G.SmoothingMode = SmoothingMode.HighQuality;
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        base.OnPaint(e);
        G.Clear(Helpers.ColorFromHex("#33373B"));
        checked {
            int num = TabPages.Count - 1;
            for (int i = 0; i <= num; i++) {
                Rect = GetTabRect(i);
                bool flag = Conversions.ToString(TabPages[i].Tag) != "Divider";
                if (flag) {
                    bool flag2 = SelectedIndex == i;
                    if (flag2) {
                        using (SolidBrush solidBrush = new SolidBrush(Helpers.ColorFromHex("#2B2F33"))) {
                            using (SolidBrush solidBrush2 = new SolidBrush(Helpers.ColorFromHex("#BECCD9"))) {
                                using (Font font = new Font("Segoe UI semibold", 9f)) {
                                    G.FillRectangle(solidBrush, new Rectangle(Rect.X - 5, Rect.Y + 1, Rect.Width + 7, Rect.Height));
                                    G.DrawString(TabPages[i].Text, font, solidBrush2, new Point(Rect.X + 50 + (ItemSize.Height - 180), Rect.Y + 12));
                                }
                            }
                        }
                    } else {
                        using (SolidBrush solidBrush3 = new SolidBrush(Helpers.ColorFromHex("#919BA6"))) {
                            using (Font font2 = new Font("Segoe UI semibold", 9f)) {
                                G.DrawString(TabPages[i].Text, font2, solidBrush3, new Point(Rect.X + 50 + (ItemSize.Height - 180), Rect.Y + 12));
                            }
                        }
                    }
                    bool flag3 = OverIndex != -1 && SelectedIndex != OverIndex && OverIndex == i;
                    if (flag3) {
                        using (SolidBrush solidBrush4 = new SolidBrush(Helpers.ColorFromHex("#2F3338"))) {
                            using (SolidBrush solidBrush5 = new SolidBrush(Helpers.ColorFromHex("#919BA6"))) {
                                using (Font font3 = new Font("Segoe UI semibold", 9f)) {
                                    G.FillRectangle(solidBrush4, new Rectangle(GetTabRect(OverIndex).X - 5, GetTabRect(OverIndex).Y + 1, GetTabRect(OverIndex).Width + 7, GetTabRect(OverIndex).Height));
                                    G.DrawString(TabPages[OverIndex].Text, font3, solidBrush5, new Point(GetTabRect(OverIndex).X + 50 + (ItemSize.Height - 180), GetTabRect(OverIndex).Y + 12));
                                }
                            }
                        }
                    }

                    Color clr = Helpers.ColorFromHex("#919BA6");
                    if (SelectedIndex == i)
                        clr = Helpers.ColorFromHex("#BCC1C6");

                    PrivateFontCollection pfc = new PrivateFontCollection();
                    byte[] fontBytes = SimpleInjector.Properties.Resources.fa_regular_400;

                    var fontData = Marshal.AllocCoTaskMem(fontBytes.Length);
                    Marshal.Copy(fontBytes, 0, fontData, fontBytes.Length);

                    pfc.AddMemoryFont(fontData, fontBytes.Length);
                    using (Brush b = new SolidBrush(clr)) {
                        using (Font fa = new Font(pfc.Families[0], 16, FontStyle.Regular, GraphicsUnit.Pixel)) {
                            G.TextRenderingHint = TextRenderingHint.AntiAlias;
                            StringFormat stringFormat = new StringFormat();
                            stringFormat.Alignment = StringAlignment.Center;
                            stringFormat.LineAlignment = StringAlignment.Center;
                            G.DrawString(Conversions.ToString(TabPages[i].Tag), fa, b, Rect.X + 25 + (ItemSize.Height - 180) + 12, (int)Math.Round(Rect.Y + ((Rect.Height / 2.0) - 9.0)) + 12, stringFormat);
                            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                        }
                    }
                } else {
                    using (SolidBrush solidBrush6 = new SolidBrush(Helpers.ColorFromHex("#6A7279"))) {
                        using (Font font4 = new Font("Segoe UI", 7f, FontStyle.Bold)) {
                            using (Pen pen = new Pen(Helpers.ColorFromHex("#2B2F33"))) {
                                bool firstHeaderBorder = FirstHeaderBorder;
                                if (firstHeaderBorder) {
                                    G.DrawLine(pen, new Point(Rect.X - 5, Rect.Y + 1), new Point(Rect.Width + 7, Rect.Y + 1));
                                } else {
                                    bool flag8 = i != 0;
                                    if (flag8) {
                                        G.DrawLine(pen, new Point(Rect.X - 5, Rect.Y + 1), new Point(Rect.Width + 7, Rect.Y + 1));
                                    }
                                }
                                G.DrawString(TabPages[i].Text.ToUpper(), font4, solidBrush6, new Point(Rect.X + 25 + (ItemSize.Height - 180), Rect.Y + 16));
                            }
                        }
                    }
                }
            }
        }
    }

    protected override void OnSelecting(TabControlCancelEventArgs e) {
        base.OnSelecting(e);
        bool flag = !Information.IsNothing(e.TabPage);
        if (flag) {
            bool flag2 = Conversions.ToString(e.TabPage.Tag) == "Divider";
            if (flag2) {
                e.Cancel = true;
            } else {
                OverIndex = -1;
            }
        }
    }

    protected override void OnMouseMove(MouseEventArgs e) {
        base.OnMouseMove(e);
        checked {
            int num = TabPages.Count - 1;
            for (int i = 0; i <= num; i++) {
                bool flag = GetTabRect(i).Contains(e.Location) & SelectedIndex != i & Conversions.ToString(TabPages[i].Tag) != "Divider";
                if (flag) {
                    OverIndex = i;
                    break;
                }
                OverIndex = -1;
            }
        }
    }

    protected override void OnMouseLeave(EventArgs e) {
        base.OnMouseLeave(e);
        OverIndex = -1;
    }
}
[DefaultEvent("TextChanged")]
public class XylosTextBox : Control {
    public enum MouseState : byte {
        None,
        Over,
        Down
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never), AccessedThroughProperty("TB"), CompilerGenerated]
    private TextBox _TB;

    private Graphics G;

    private XylosTextBox.MouseState State;

    private bool IsDown;

    private bool _EnabledCalc;

    private bool _allowpassword;

    private int _maxChars;

    private HorizontalAlignment _textAlignment;

    private bool _multiLine;

    private bool _readOnly;

    public virtual TextBox TB {
        [CompilerGenerated]
        get {
            return _TB;
        }
        [CompilerGenerated]
        [MethodImpl(MethodImplOptions.Synchronized)]
        set {
            EventHandler value2 = delegate (object a0, EventArgs a1) {
                TextChangeTb();
            };
            TextBox tB = _TB;
            if (tB != null) {
                tB.TextChanged -= value2;
            }
            _TB = value;
            tB = _TB;
            if (tB != null) {
                tB.TextChanged += value2;
            }
        }
    }

    public new bool Enabled {
        get {
            return EnabledCalc;
        }
        set {
            TB.Enabled = value;
            _EnabledCalc = value;
            Invalidate();
        }
    }

    [DisplayName("Enabled")]
    public bool EnabledCalc {
        get {
            return _EnabledCalc;
        }
        set {
            Enabled = value;
            Invalidate();
        }
    }

    public bool UseSystemPasswordChar {
        get {
            return _allowpassword;
        }
        set {
            TB.UseSystemPasswordChar = UseSystemPasswordChar;
            _allowpassword = value;
            Invalidate();
        }
    }

    public int MaxLength {
        get {
            return _maxChars;
        }
        set {
            _maxChars = value;
            TB.MaxLength = MaxLength;
            Invalidate();
        }
    }

    public HorizontalAlignment TextAlign {
        get {
            return _textAlignment;
        }
        set {
            _textAlignment = value;
            Invalidate();
        }
    }

    public bool MultiLine {
        get {
            return _multiLine;
        }
        set {
            _multiLine = value;
            TB.Multiline = value;
            OnResize(EventArgs.Empty);
            Invalidate();
        }
    }

    public bool ReadOnly {
        get {
            return _readOnly;
        }
        set {
            _readOnly = value;
            bool flag = TB != null;
            if (flag) {
                TB.ReadOnly = value;
            }
        }
    }

    protected override void OnTextChanged(EventArgs e) {
        base.OnTextChanged(e);
        Invalidate();
    }

    protected override void OnBackColorChanged(EventArgs e) {
        base.OnBackColorChanged(e);
        Invalidate();
    }

    protected override void OnForeColorChanged(EventArgs e) {
        base.OnForeColorChanged(e);
        TB.ForeColor = ForeColor;
        Invalidate();
    }

    protected override void OnFontChanged(EventArgs e) {
        base.OnFontChanged(e);
        TB.Font = Font;
    }

    protected override void OnGotFocus(EventArgs e) {
        base.OnGotFocus(e);
        TB.Focus();
    }

    private void TextChangeTb() {
        Text = TB.Text;
    }

    private void TextChng() {
        TB.Text = Text;
    }

    public void NewTextBox() {
        TextBox tB = TB;
        tB.Text = string.Empty;
        tB.BackColor = Color.White;
        tB.ForeColor = Helpers.ColorFromHex("#7C858E");
        tB.TextAlign = HorizontalAlignment.Left;
        tB.BorderStyle = BorderStyle.None;
        tB.Location = new Point(3, 3);
        tB.Font = new Font("Segoe UI", 9f);
        tB.Size = checked(new Size(Width - 3, Height - 3));
        tB.UseSystemPasswordChar = UseSystemPasswordChar;
    }

    public XylosTextBox() {
        TextChanged += delegate (object a0, EventArgs a1) {
            TextChng();
        };
        TB = new TextBox();
        _allowpassword = false;
        _maxChars = 32767;
        _multiLine = false;
        _readOnly = false;
        NewTextBox();
        Controls.Add(TB);
        SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        TextAlign = HorizontalAlignment.Left;
        ForeColor = Helpers.ColorFromHex("#7C858E");
        Font = new Font("Segoe UI", 9f);
        Size = new Size(130, 29);
        Enabled = true;
    }

    protected override void OnPaint(PaintEventArgs e) {
        G = e.Graphics;
        G.SmoothingMode = SmoothingMode.HighQuality;
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        base.OnPaint(e);
        G.Clear(Color.White);
        bool enabled = Enabled;
        if (enabled) {
            TB.ForeColor = Helpers.ColorFromHex("#7C858E");
            bool flag = State == MouseState.Down;
            if (flag) {
                using (Pen pen = new Pen(Helpers.ColorFromHex("#78B7E6"))) {
                    G.DrawPath(pen, Helpers.RoundRect(Helpers.FullRectangle(Size, true), 12, Helpers.RoundingStyle.All));
                }
            } else {
                using (Pen pen2 = new Pen(Helpers.ColorFromHex("#D0D5D9"))) {
                    G.DrawPath(pen2, Helpers.RoundRect(Helpers.FullRectangle(Size, true), 12, Helpers.RoundingStyle.All));
                }
            }
        } else {
            TB.ForeColor = Helpers.ColorFromHex("#7C858E");
            using (Pen pen3 = new Pen(Helpers.ColorFromHex("#E1E1E2"))) {
                G.DrawPath(pen3, Helpers.RoundRect(Helpers.FullRectangle(Size, true), 12, Helpers.RoundingStyle.All));
            }
        }
        TB.TextAlign = TextAlign;
        TB.UseSystemPasswordChar = UseSystemPasswordChar;
    }

    protected override void OnResize(EventArgs e) {
        base.OnResize(e);
        bool flag = !MultiLine;
        checked {
            if (flag) {
                int height = TB.Height;
                TB.Location = new Point(10, (int)Math.Round(unchecked((double)Height / 2.0 - (double)height / 2.0 - 0.0)));
                TB.Size = new Size(Width - 20, height);
            } else {
                TB.Location = new Point(10, 10);
                TB.Size = new Size(Width - 20, Height - 20);
            }
        }
    }

    protected override void OnEnter(EventArgs e) {
        base.OnEnter(e);
        State = MouseState.Down;
        Invalidate();
    }

    protected override void OnLeave(EventArgs e) {
        base.OnLeave(e);
        State = MouseState.None;
        Invalidate();
    }
}
public class XylosButton : Control {
    public enum MouseState : byte {
        None,
        Over,
        Down
    }

    public delegate void ClickEventHandler(object sender, EventArgs e);

    private Graphics G;

    private XylosButton.MouseState State;

    private bool _EnabledCalc;

    [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
    private XylosButton.ClickEventHandler ClickEvent;

    public new event XylosButton.ClickEventHandler Click {
        [CompilerGenerated]
        add {
            XylosButton.ClickEventHandler clickEventHandler = ClickEvent;
            XylosButton.ClickEventHandler clickEventHandler2;
            do {
                clickEventHandler2 = clickEventHandler;
                XylosButton.ClickEventHandler value2 = (XylosButton.ClickEventHandler)Delegate.Combine(clickEventHandler2, value);
                clickEventHandler = Interlocked.CompareExchange<XylosButton.ClickEventHandler>(ref ClickEvent, value2, clickEventHandler2);
            }
            while (clickEventHandler != clickEventHandler2);
        }
        [CompilerGenerated]
        remove {
            XylosButton.ClickEventHandler clickEventHandler = ClickEvent;
            XylosButton.ClickEventHandler clickEventHandler2;
            do {
                clickEventHandler2 = clickEventHandler;
                XylosButton.ClickEventHandler value2 = (XylosButton.ClickEventHandler)Delegate.Remove(clickEventHandler2, value);
                clickEventHandler = Interlocked.CompareExchange<XylosButton.ClickEventHandler>(ref ClickEvent, value2, clickEventHandler2);
            }
            while (clickEventHandler != clickEventHandler2);
        }
    }

    public new bool Enabled {
        get {
            return EnabledCalc;
        }
        set {
            _EnabledCalc = value;
            Invalidate();
        }
    }

    [DisplayName("Enabled")]
    public bool EnabledCalc {
        get {
            return _EnabledCalc;
        }
        set {
            Enabled = value;
            Invalidate();
        }
    }

    public XylosButton() {
        DoubleBuffered = true;
        Enabled = true;
    }

    protected override void OnPaint(PaintEventArgs e) {
        G = e.Graphics;
        G.SmoothingMode = SmoothingMode.HighQuality;
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        base.OnPaint(e);
        bool enabled = Enabled;
        if (enabled) {
            XylosButton.MouseState state = State;
            if (state != MouseState.Over) {
                if (state != MouseState.Down) {
                    using (SolidBrush solidBrush = new SolidBrush(Helpers.ColorFromHex("#F6F6F6"))) {
                        G.FillPath(solidBrush, Helpers.RoundRect(Helpers.FullRectangle(Size, true), 3, Helpers.RoundingStyle.All));
                    }
                } else {
                    using (SolidBrush solidBrush2 = new SolidBrush(Helpers.ColorFromHex("#F0F0F0"))) {
                        G.FillPath(solidBrush2, Helpers.RoundRect(Helpers.FullRectangle(Size, true), 3, Helpers.RoundingStyle.All));
                    }
                }
            } else {
                using (SolidBrush solidBrush3 = new SolidBrush(Helpers.ColorFromHex("#FDFDFD"))) {
                    G.FillPath(solidBrush3, Helpers.RoundRect(Helpers.FullRectangle(Size, true), 3, Helpers.RoundingStyle.All));
                }
            }
            using (Font font = new Font("Segoe UI", 9f)) {
                using (Pen pen = new Pen(Helpers.ColorFromHex("#C3C3C3"))) {
                    G.DrawPath(pen, Helpers.RoundRect(Helpers.FullRectangle(Size, true), 3, Helpers.RoundingStyle.All));
                    Helpers.CenterString(G, Text, font, Helpers.ColorFromHex("#7C858E"), Helpers.FullRectangle(Size, false));
                }
            }
        } else {
            using (SolidBrush solidBrush4 = new SolidBrush(Helpers.ColorFromHex("#F3F4F7"))) {
                using (Pen pen2 = new Pen(Helpers.ColorFromHex("#DCDCDC"))) {
                    using (Font font2 = new Font("Segoe UI", 9f)) {
                        G.FillPath(solidBrush4, Helpers.RoundRect(Helpers.FullRectangle(Size, true), 3, Helpers.RoundingStyle.All));
                        G.DrawPath(pen2, Helpers.RoundRect(Helpers.FullRectangle(Size, true), 3, Helpers.RoundingStyle.All));
                        Helpers.CenterString(G, Text, font2, Helpers.ColorFromHex("#D0D3D7"), Helpers.FullRectangle(Size, false));
                    }
                }
            }
        }
    }

    protected override void OnMouseEnter(EventArgs e) {
        base.OnMouseEnter(e);
        State = MouseState.Over;
        Invalidate();
    }

    protected override void OnMouseLeave(EventArgs e) {
        base.OnMouseLeave(e);
        State = MouseState.None;
        Invalidate();
    }

    protected override void OnMouseUp(MouseEventArgs e) {
        base.OnMouseUp(e);
        bool enabled = Enabled;
        if (enabled) {
            XylosButton.ClickEventHandler clickEvent = ClickEvent;
            if (clickEvent != null) {
                clickEvent(this, e);
            }
        }
        State = MouseState.Over;
        Invalidate();
    }

    protected override void OnMouseDown(MouseEventArgs e) {
        base.OnMouseDown(e);
        State = MouseState.Down;
        Invalidate();
    }
}
[DefaultEvent("CheckedChanged")]
public class XylosCheckBox : Control {
    public delegate void CheckedChangedEventHandler(object sender, EventArgs e);

    [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
    private XylosCheckBox.CheckedChangedEventHandler CheckedChangedEvent;

    private bool _Checked;

    private bool _EnabledCalc;

    private Graphics G;

    private string B64Enabled;

    private string B64Disabled;

    public event XylosCheckBox.CheckedChangedEventHandler CheckedChanged {
        [CompilerGenerated]
        add {
            XylosCheckBox.CheckedChangedEventHandler checkedChangedEventHandler = CheckedChangedEvent;
            XylosCheckBox.CheckedChangedEventHandler checkedChangedEventHandler2;
            do {
                checkedChangedEventHandler2 = checkedChangedEventHandler;
                XylosCheckBox.CheckedChangedEventHandler value2 = (XylosCheckBox.CheckedChangedEventHandler)Delegate.Combine(checkedChangedEventHandler2, value);
                checkedChangedEventHandler = Interlocked.CompareExchange<XylosCheckBox.CheckedChangedEventHandler>(ref CheckedChangedEvent, value2, checkedChangedEventHandler2);
            }
            while (checkedChangedEventHandler != checkedChangedEventHandler2);
        }
        [CompilerGenerated]
        remove {
            XylosCheckBox.CheckedChangedEventHandler checkedChangedEventHandler = CheckedChangedEvent;
            XylosCheckBox.CheckedChangedEventHandler checkedChangedEventHandler2;
            do {
                checkedChangedEventHandler2 = checkedChangedEventHandler;
                XylosCheckBox.CheckedChangedEventHandler value2 = (XylosCheckBox.CheckedChangedEventHandler)Delegate.Remove(checkedChangedEventHandler2, value);
                checkedChangedEventHandler = Interlocked.CompareExchange<XylosCheckBox.CheckedChangedEventHandler>(ref CheckedChangedEvent, value2, checkedChangedEventHandler2);
            }
            while (checkedChangedEventHandler != checkedChangedEventHandler2);
        }
    }

    public bool Checked {
        get {
            return _Checked;
        }
        set {
            _Checked = value;
            Invalidate();
        }
    }

    public new bool Enabled {
        get {
            return EnabledCalc;
        }
        set {
            _EnabledCalc = value;
            bool enabled = Enabled;
            if (enabled) {
                Cursor = Cursors.Hand;
            } else {
                Cursor = Cursors.Default;
            }
            Invalidate();
        }
    }

    [DisplayName("Enabled")]
    public bool EnabledCalc {
        get {
            return _EnabledCalc;
        }
        set {
            Enabled = value;
            Invalidate();
        }
    }

    public XylosCheckBox() {
        B64Enabled = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAA00lEQVQ4T6WTwQ2CMBSG30/07Ci6gY7gxZoIiYADuAIrsIDpQQ/cHMERZBOuXHimDSWALYL01EO/L//724JmLszk6S+BCOIExFsmL50sEH4kAZxVciYuJgnacD16Plpgg8tFtYMILntQdSXiZ3aXqa1UF/yUsoDw4wKglQaZZPa4RW3JEKzO4RjEbyJaN1BL8gvWgsMp3ADeq0lRJ2FimLZNYWpmFbudUJdolXTLyG2wTmDODUiccEfgSDIIfwmMxAMStS+XHPZn7l/z6Ifk+nSzBR8zi2d9JmVXSgAAAABJRU5ErkJggg==";
        B64Disabled = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAA1UlEQVQ4T6WTzQ2CQBCF56EnLpaiXvUAJBRgB2oFtkALdEAJnoVEMIGzdEIFjNkFN4DLn+xpD/N9efMWQAsPFvL0lyBMUg8MiwzyZwuiJAuI6CyTMxezBC24EuSTBTp4xaaN6JWdqKQbge6udfB1pfbBjrMvEMZZAdCm3ilw7eO1KRmCxRyiOH0TsFUQs5KMwVLweKY7ALFKUZUTECD6qdquCxM7i9jNhLJEraQ5xZzrYJngO9crGYBbAm2SEfhHoCQGeeK+Ls1Ld+fuM0/+kPp+usWCD10idEOGa4QuAAAAAElFTkSuQmCC";
        DoubleBuffered = true;
        Enabled = true;
    }

    protected override void OnPaint(PaintEventArgs e) {
        G = e.Graphics;
        G.SmoothingMode = SmoothingMode.HighQuality;
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        base.OnPaint(e);
        G.Clear(Color.White);
        bool enabled = Enabled;
        if (enabled) {
            using (SolidBrush solidBrush = new SolidBrush(Helpers.ColorFromHex("#F3F4F7"))) {
                using (Pen pen = new Pen(Helpers.ColorFromHex("#D0D5D9"))) {
                    using (SolidBrush solidBrush2 = new SolidBrush(Helpers.ColorFromHex("#7C858E"))) {
                        using (Font font = new Font("Segoe UI", 9f)) {
                            G.FillPath(solidBrush, Helpers.RoundRect(new Rectangle(0, 0, 16, 16), 3, Helpers.RoundingStyle.All));
                            G.DrawPath(pen, Helpers.RoundRect(new Rectangle(0, 0, 16, 16), 3, Helpers.RoundingStyle.All));
                            G.DrawString(Text, font, solidBrush2, new Point(25, 0));
                        }
                    }
                }
            }
            bool @checked = Checked;
            if (@checked) {
                using (Image image = Image.FromStream(new MemoryStream(Convert.FromBase64String(B64Enabled)))) {
                    G.DrawImage(image, new Rectangle(3, 3, 11, 11));
                }
            }
        } else {
            using (SolidBrush solidBrush3 = new SolidBrush(Helpers.ColorFromHex("#F5F5F8"))) {
                using (Pen pen2 = new Pen(Helpers.ColorFromHex("#E1E1E2"))) {
                    using (SolidBrush solidBrush4 = new SolidBrush(Helpers.ColorFromHex("#D0D3D7"))) {
                        using (Font font2 = new Font("Segoe UI", 9f)) {
                            G.FillPath(solidBrush3, Helpers.RoundRect(new Rectangle(0, 0, 16, 16), 3, Helpers.RoundingStyle.All));
                            G.DrawPath(pen2, Helpers.RoundRect(new Rectangle(0, 0, 16, 16), 3, Helpers.RoundingStyle.All));
                            G.DrawString(Text, font2, solidBrush4, new Point(25, 0));
                        }
                    }
                }
            }
            bool checked2 = Checked;
            if (checked2) {
                using (Image image2 = Image.FromStream(new MemoryStream(Convert.FromBase64String(B64Disabled)))) {
                    G.DrawImage(image2, new Rectangle(3, 3, 11, 11));
                }
            }
        }
    }

    protected override void OnMouseUp(MouseEventArgs e) {
        base.OnMouseUp(e);
        bool enabled = Enabled;
        if (enabled) {
            Checked = !Checked;
            XylosCheckBox.CheckedChangedEventHandler checkedChangedEvent = CheckedChangedEvent;
            if (checkedChangedEvent != null) {
                checkedChangedEvent(this, e);
            }
        }
    }

    protected override void OnResize(EventArgs e) {
        base.OnResize(e);
        Size = new Size(Width, 18);
    }
}
public class XylosCombobox : ComboBox {
    private Graphics G;

    private Rectangle Rect;

    private bool _EnabledCalc;

    public new bool Enabled {
        get {
            return EnabledCalc;
        }
        set {
            _EnabledCalc = value;
            Invalidate();
        }
    }

    [DisplayName("Enabled")]
    public bool EnabledCalc {
        get {
            return _EnabledCalc;
        }
        set {
            base.Enabled = value;
            Enabled = value;
            Invalidate();
        }
    }

    public XylosCombobox() {
        DoubleBuffered = true;
        DropDownStyle = ComboBoxStyle.DropDownList;
        Cursor = Cursors.Hand;
        Enabled = true;
        DrawMode = DrawMode.OwnerDrawFixed;
        ItemHeight = 20;
    }

    protected override void OnCreateControl() {
        base.OnCreateControl();
        SetStyle(ControlStyles.UserPaint, true);
    }

    protected override void OnPaint(PaintEventArgs e) {
        G = e.Graphics;
        G.SmoothingMode = SmoothingMode.HighQuality;
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        base.OnPaint(e);
        G.Clear(Color.White);
        bool enabled = Enabled;
        checked {
            if (enabled) {
                using (Pen pen = new Pen(Helpers.ColorFromHex("#D0D5D9"))) {
                    using (SolidBrush solidBrush = new SolidBrush(Helpers.ColorFromHex("#7C858E"))) {
                        using (Font font = new Font("Marlett", 13f)) {
                            G.DrawPath(pen, Helpers.RoundRect(Helpers.FullRectangle(Size, true), 6, Helpers.RoundingStyle.All));
                            G.DrawString("6", font, solidBrush, new Point(Width - 22, 3));
                        }
                    }
                }
            } else {
                using (Pen pen2 = new Pen(Helpers.ColorFromHex("#E1E1E2"))) {
                    using (SolidBrush solidBrush2 = new SolidBrush(Helpers.ColorFromHex("#D0D3D7"))) {
                        using (Font font2 = new Font("Marlett", 13f)) {
                            G.DrawPath(pen2, Helpers.RoundRect(Helpers.FullRectangle(Size, true), 6, Helpers.RoundingStyle.All));
                            G.DrawString("6", font2, solidBrush2, new Point(Width - 22, 3));
                        }
                    }
                }
            }
            bool flag = !Information.IsNothing(Items);
            if (flag) {
                using (Font font3 = new Font("Segoe UI", 9f)) {
                    using (SolidBrush solidBrush3 = new SolidBrush(Helpers.ColorFromHex("#7C858E"))) {
                        bool enabled2 = Enabled;
                        if (enabled2) {
                            bool flag2 = SelectedIndex != -1;
                            if (flag2) {
                                G.DrawString(GetItemText(RuntimeHelpers.GetObjectValue(Items[SelectedIndex])), font3, solidBrush3, new Point(7, 4));
                            } else {
                                try {
                                    G.DrawString(GetItemText(RuntimeHelpers.GetObjectValue(Items[0])), font3, solidBrush3, new Point(7, 4));
                                } catch (Exception arg_272_0) {
                                    ProjectData.SetProjectError(arg_272_0);
                                    ProjectData.ClearProjectError();
                                }
                            }
                        } else {
                            using (SolidBrush solidBrush4 = new SolidBrush(Helpers.ColorFromHex("#D0D3D7"))) {
                                bool flag3 = SelectedIndex != -1;
                                if (flag3) {
                                    G.DrawString(GetItemText(RuntimeHelpers.GetObjectValue(Items[SelectedIndex])), font3, solidBrush4, new Point(7, 4));
                                } else {
                                    G.DrawString(GetItemText(RuntimeHelpers.GetObjectValue(Items[0])), font3, solidBrush4, new Point(7, 4));
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    protected override void OnDrawItem(DrawItemEventArgs e) {
        base.OnDrawItem(e);
        G = e.Graphics;
        G.SmoothingMode = SmoothingMode.HighQuality;
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        bool enabled = Enabled;
        checked {
            if (enabled) {
                e.DrawBackground();
                Rect = e.Bounds;
                try {
                    using (new Font("Segoe UI", 9f)) {
                        using (new Pen(Helpers.ColorFromHex("#D0D5D9"))) {
                            bool flag = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
                            if (flag) {
                                using (new SolidBrush(Color.White)) {
                                    using (SolidBrush solidBrush2 = new SolidBrush(Helpers.ColorFromHex("#78B7E6"))) {
                                        G.FillRectangle(solidBrush2, Rect);
                                        G.DrawString(GetItemText(RuntimeHelpers.GetObjectValue(Items[e.Index])), new Font("Segoe UI", 9f), Brushes.White, new Point(Rect.X + 5, Rect.Y + 1));
                                    }
                                }
                            } else {
                                using (SolidBrush solidBrush3 = new SolidBrush(Helpers.ColorFromHex("#7C858E"))) {
                                    G.FillRectangle(Brushes.White, Rect);
                                    G.DrawString(GetItemText(RuntimeHelpers.GetObjectValue(Items[e.Index])), new Font("Segoe UI", 9f), solidBrush3, new Point(Rect.X + 5, Rect.Y + 1));
                                }
                            }
                        }
                    }
                } catch (Exception arg_1F1_0) {
                    ProjectData.SetProjectError(arg_1F1_0);
                    ProjectData.ClearProjectError();
                }
            }
        }
    }

    protected override void OnSelectedItemChanged(EventArgs e) {
        base.OnSelectedItemChanged(e);
        Invalidate();
    }
}
public class XylosNotice : TextBox {
    private Graphics G;

    private string B64;

    public XylosNotice() {
        B64 = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAABL0lEQVQ4T5VT0VGDQBB9e2cBdGBSgTIDEr9MCw7pI0kFtgB9yFiC+KWMmREqMOnAAuDWOfAiudzhyA/svtvH7Xu7BOv5eH2atVKtwbwk0LWGGVyDqLzoRB7e3u/HJTQOdm+PGYjWNuk4ZkIW36RbkzsS7KqiBnB1Usw49DHh8oQEXMfJKhwgAM4/Mw7RIp0NeLG3ScCcR4vVhnTPnVCf9rUZeImTdKnz71VREnBnn5FKzMnX95jA2V6vLufkBQFESTq0WBXsEla7owmcoC6QJMKW2oCUePY5M0lAjK0iBAQ8TBGc2/d7+uvnM/AQNF4Rp4bpiGkRfTb2Gigx12+XzQb3D9JfBGaQzHWm7HS000RJ2i/av5fJjPDZMplErwl1GxDpMTbL1YC5lCwze52/AQFekh7wKBpGAAAAAElFTkSuQmCC";
        DoubleBuffered = true;
        Enabled = false;
        ReadOnly = true;
        BorderStyle = BorderStyle.None;
        Multiline = true;
        Cursor = Cursors.Default;
    }

    protected override void OnCreateControl() {
        base.OnCreateControl();
        SetStyle(ControlStyles.UserPaint, true);
    }

    protected override void OnPaint(PaintEventArgs e) {
        G = e.Graphics;
        G.SmoothingMode = SmoothingMode.HighQuality;
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        base.OnPaint(e);
        G.Clear(Color.White);
        using (SolidBrush solidBrush = new SolidBrush(Helpers.ColorFromHex("#FFFDE8"))) {
            using (Pen pen = new Pen(Helpers.ColorFromHex("#F2F3F7"))) {
                using (SolidBrush solidBrush2 = new SolidBrush(Helpers.ColorFromHex("#B9B595"))) {
                    using (Font font = new Font("Segoe UI", 9f)) {
                        G.FillPath(solidBrush, Helpers.RoundRect(Helpers.FullRectangle(Size, true), 3, Helpers.RoundingStyle.All));
                        G.DrawPath(pen, Helpers.RoundRect(Helpers.FullRectangle(Size, true), 3, Helpers.RoundingStyle.All));
                        G.DrawString(Text, font, solidBrush2, new Point(30, 6));
                    }
                }
            }
        }
        using (Image image = Image.FromStream(new MemoryStream(Convert.FromBase64String(B64)))) {
            G.DrawImage(image, new Rectangle(8, checked((int)Math.Round(unchecked((double)Height / 2.0 - 8.0))), 16, 16));
        }
    }

    protected override void OnMouseUp(MouseEventArgs e) {
        base.OnMouseUp(e);
    }
}
public class XylosProgressBar : Control {
    private int _Val;

    private int _Min;

    private int _Max;

    [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
    private Color _Stripes;

    [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
    private Color _BackgroundColor;

    public Color Stripes {
        get;
        set;
    }

    public Color BackgroundColor {
        get;
        set;
    }

    public int Value {
        get {
            return _Val;
        }
        set {
            _Val = value;
            Invalidate();
        }
    }

    public int Minimum {
        get {
            return _Min;
        }
        set {
            _Min = value;
            Invalidate();
        }
    }

    public int Maximum {
        get {
            return _Max;
        }
        set {
            _Max = value;
            Invalidate();
        }
    }

    public XylosProgressBar() {
        _Val = 0;
        _Min = 0;
        _Max = 100;
        Stripes = Color.DarkGreen;
        BackgroundColor = Color.Green;
        DoubleBuffered = true;
        Maximum = 100;
        Minimum = 0;
        Value = 0;
    }

    protected override void OnPaint(PaintEventArgs e) {
        Graphics graphics = e.Graphics;
        graphics.SmoothingMode = SmoothingMode.HighQuality;
        graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        base.OnPaint(e);
        graphics.Clear(Color.White);
        using (Pen pen = new Pen(Helpers.ColorFromHex("#D0D5D9"))) {
            graphics.DrawPath(pen, Helpers.RoundRect(Helpers.FullRectangle(Size, true), 6, Helpers.RoundingStyle.All));
        }
        bool flag = Value != 0;
        if (flag) {
            using (HatchBrush hatchBrush = new HatchBrush(HatchStyle.LightUpwardDiagonal, Stripes, BackgroundColor)) {
                graphics.FillPath(hatchBrush, Helpers.RoundRect(checked(new Rectangle(0, 0, (int)Math.Round(unchecked((double)Value / (double)Maximum * (double)Width - 1.0)), Height - 1)), 6, Helpers.RoundingStyle.All));
            }
        }
    }
}
[DefaultEvent("CheckedChanged")]
public class XylosRadioButton : Control {
    public delegate void CheckedChangedEventHandler(object sender, EventArgs e);

    [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
    private XylosRadioButton.CheckedChangedEventHandler CheckedChangedEvent;

    private bool _Checked;

    private bool _EnabledCalc;

    private Graphics G;

    public event XylosRadioButton.CheckedChangedEventHandler CheckedChanged {
        [CompilerGenerated]
        add {
            XylosRadioButton.CheckedChangedEventHandler checkedChangedEventHandler = CheckedChangedEvent;
            XylosRadioButton.CheckedChangedEventHandler checkedChangedEventHandler2;
            do {
                checkedChangedEventHandler2 = checkedChangedEventHandler;
                XylosRadioButton.CheckedChangedEventHandler value2 = (XylosRadioButton.CheckedChangedEventHandler)Delegate.Combine(checkedChangedEventHandler2, value);
                checkedChangedEventHandler = Interlocked.CompareExchange<XylosRadioButton.CheckedChangedEventHandler>(ref CheckedChangedEvent, value2, checkedChangedEventHandler2);
            }
            while (checkedChangedEventHandler != checkedChangedEventHandler2);
        }
        [CompilerGenerated]
        remove {
            XylosRadioButton.CheckedChangedEventHandler checkedChangedEventHandler = CheckedChangedEvent;
            XylosRadioButton.CheckedChangedEventHandler checkedChangedEventHandler2;
            do {
                checkedChangedEventHandler2 = checkedChangedEventHandler;
                XylosRadioButton.CheckedChangedEventHandler value2 = (XylosRadioButton.CheckedChangedEventHandler)Delegate.Remove(checkedChangedEventHandler2, value);
                checkedChangedEventHandler = Interlocked.CompareExchange<XylosRadioButton.CheckedChangedEventHandler>(ref CheckedChangedEvent, value2, checkedChangedEventHandler2);
            }
            while (checkedChangedEventHandler != checkedChangedEventHandler2);
        }
    }

    public bool Checked {
        get {
            return _Checked;
        }
        set {
            _Checked = value;
            Invalidate();
        }
    }

    public new bool Enabled {
        get {
            return EnabledCalc;
        }
        set {
            _EnabledCalc = value;
            bool enabled = Enabled;
            if (enabled) {
                Cursor = Cursors.Hand;
            } else {
                Cursor = Cursors.Default;
            }
            Invalidate();
        }
    }

    [DisplayName("Enabled")]
    public bool EnabledCalc {
        get {
            return _EnabledCalc;
        }
        set {
            Enabled = value;
            Invalidate();
        }
    }

    public XylosRadioButton() {
        DoubleBuffered = true;
        Enabled = true;
    }

    protected override void OnPaint(PaintEventArgs e) {
        G = e.Graphics;
        G.SmoothingMode = SmoothingMode.HighQuality;
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        base.OnPaint(e);
        G.Clear(Color.White);
        bool enabled = Enabled;
        if (enabled) {
            using (SolidBrush solidBrush = new SolidBrush(Helpers.ColorFromHex("#F3F4F7"))) {
                using (Pen pen = new Pen(Helpers.ColorFromHex("#D0D5D9"))) {
                    using (SolidBrush solidBrush2 = new SolidBrush(Helpers.ColorFromHex("#7C858E"))) {
                        using (Font font = new Font("Segoe UI", 9f)) {
                            G.FillEllipse(solidBrush, new Rectangle(0, 0, 16, 16));
                            G.DrawEllipse(pen, new Rectangle(0, 0, 16, 16));
                            G.DrawString(Text, font, solidBrush2, new Point(25, 0));
                        }
                    }
                }
            }
            bool @checked = Checked;
            if (@checked) {
                using (SolidBrush solidBrush3 = new SolidBrush(Helpers.ColorFromHex("#575C62"))) {
                    G.FillEllipse(solidBrush3, new Rectangle(4, 4, 8, 8));
                }
            }
        } else {
            using (SolidBrush solidBrush4 = new SolidBrush(Helpers.ColorFromHex("#F5F5F8"))) {
                using (Pen pen2 = new Pen(Helpers.ColorFromHex("#E1E1E2"))) {
                    using (SolidBrush solidBrush5 = new SolidBrush(Helpers.ColorFromHex("#D0D3D7"))) {
                        using (Font font2 = new Font("Segoe UI", 9f)) {
                            G.FillEllipse(solidBrush4, new Rectangle(0, 0, 16, 16));
                            G.DrawEllipse(pen2, new Rectangle(0, 0, 16, 16));
                            G.DrawString(Text, font2, solidBrush5, new Point(25, 0));
                        }
                    }
                }
            }
            bool checked2 = Checked;
            if (checked2) {
                using (SolidBrush solidBrush6 = new SolidBrush(Helpers.ColorFromHex("#BCC1C6"))) {
                    G.FillEllipse(solidBrush6, new Rectangle(4, 4, 8, 8));
                }
            }
        }
    }

    protected override void OnMouseUp(MouseEventArgs e) {
        base.OnMouseUp(e);
        bool enabled = Enabled;
        if (enabled) {
            try {
                IEnumerator enumerator = Parent.Controls.GetEnumerator();
                while (enumerator.MoveNext()) {
                    Control control = (Control)enumerator.Current;
                    bool flag = control is XylosRadioButton;
                    if (flag) {
                        ((XylosRadioButton)control).Checked = false;
                    }
                }
            } finally {

            }
            Checked = !Checked;
            XylosRadioButton.CheckedChangedEventHandler checkedChangedEvent = CheckedChangedEvent;
            if (checkedChangedEvent != null) {
                checkedChangedEvent(this, e);
            }
        }
    }

    protected override void OnResize(EventArgs e) {
        base.OnResize(e);
        Size = new Size(Width, 18);
    }
}
public class XylosSeparator : Control {
    private Graphics G;

    public XylosSeparator() {
        DoubleBuffered = true;
    }

    protected override void OnPaint(PaintEventArgs e) {
        G = e.Graphics;
        G.SmoothingMode = SmoothingMode.HighQuality;
        G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        base.OnPaint(e);
        using (Pen pen = new Pen(Helpers.ColorFromHex("#EBEBEC"))) {
            G.DrawLine(pen, new Point(0, 0), new Point(Width, 0));
        }
    }

    protected override void OnResize(EventArgs e) {
        base.OnResize(e);
        Size = new Size(Width, 2);
    }
}