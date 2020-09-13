using System.Diagnostics;
using System.Windows.Forms;

namespace SimpleInjector {
    public class TextBoxListener : TraceListener {
        RichTextBox _box;
        string _data;
        public TextBoxListener(string initializeData) {
            _data = initializeData;
        }

        private bool Init() {
            if (_box != null && _box.IsDisposed) {
                _box = null;
            }
            if (_box == null) {
                foreach (Form f in Application.OpenForms) {
                    Search(f);
                }
            }
            return _box != null && !_box.IsDisposed;
        }

        private void Search(Control f) {
            foreach (Control c in f.Controls) {
                if (c.Name == _data && c is RichTextBox) {
                    _box = (RichTextBox)c;
                    break;
                } else if (c.Controls.Count > 0) {
                    Search(c);
                }
            }
        }

        public override void WriteLine(string message) {
            if (Init()) {
                _box.Text = _box.Text + message + "\r\n";
            }
        }

        public override void Write(string message) {
            if (Init()) {
                _box.Text = _box.Text + message;
            }
        }
    }
}
