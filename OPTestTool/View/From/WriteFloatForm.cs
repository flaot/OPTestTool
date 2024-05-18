
namespace OPTestTool
{
    public partial class WriteFloatForm : Form
    {
        public WriteFloatForm()
        {
            InitializeComponent();
        }

        public float Context()
        {
            if (float.TryParse(Txt_Value.Text, out var val))
                return val;
            else
                return 0f;
        }

    }
}
