using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Manurizer.Controls
{
	/// <summary>
	/// Interaction logic for TextEdit.xaml
	/// </summary>
	public partial class TextEdit : UserControl
	{
		public TextEdit()
		{
			InitializeComponent();
		}


		public string LabelText
		{
			get { return (string)GetValue(LabelTextProperty); }
			set { SetValue(LabelTextProperty, value); }
		}

		public static readonly DependencyProperty LabelTextProperty =
			DependencyProperty.Register("LabelText", typeof(string), typeof(TextEdit),
				new PropertyMetadata(""));


		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		public static readonly DependencyProperty TextProperty =
			DependencyProperty.Register("Text", typeof(string), typeof(TextEdit),
				new FrameworkPropertyMetadata("")
				{
					BindsTwoWayByDefault = true,
					DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
				});


		public CharacterCasing CharacterCasting
		{
			get { return (CharacterCasing)GetValue(CharacterCastingProperty); }
			set { SetValue(CharacterCastingProperty, value); }
		}

		public static readonly DependencyProperty CharacterCastingProperty =
				DependencyProperty.Register("CharacterCasting", typeof(CharacterCasing), typeof(TextEdit),
					new PropertyMetadata(CharacterCasing.Normal));
	}
}
