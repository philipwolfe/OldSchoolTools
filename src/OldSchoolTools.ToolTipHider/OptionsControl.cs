using System.Windows.Forms;

namespace OldSchoolTools.QuickInfoHider
{
	public partial class OptionsControl : UserControl
	{
		private Options _options;

		internal Options Options
		{
			get
			{
				return _options;
			}
			set
			{
				_options = value;
				propertyGrid1.SelectedObject = _options;
			}
		}

		public OptionsControl()
		{
			InitializeComponent();
		}

		private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			_options.isDirty = true;
		}
	}
}
