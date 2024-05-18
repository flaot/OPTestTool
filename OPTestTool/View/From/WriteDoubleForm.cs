
namespace OPTestTool
{
    public partial class WriteDoubleForm : Form
    {
        public WriteDoubleForm()
        {
            InitializeComponent();
        }

        public double Context()
        {
            if (double.TryParse(Txt_Value.Text, out var val))
                return val;
            else
                return 0f;
        }

    }
}
