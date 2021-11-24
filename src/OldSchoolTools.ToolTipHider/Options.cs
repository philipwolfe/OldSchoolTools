using Microsoft.VisualStudio.Shell;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace OldSchoolTools.QuickInfoHider
{
	public class Options : DialogPage
	{
		internal bool isDirty = false;

		protected override IWin32Window Window => new OptionsControl { Options = this };

		[Category("Hide all Quick Info popups")]
		[DisplayName("Disable All Quick Info")]
		[Description("Hides all Quick Info popups regardless of the individual settings.")]
		[DefaultValue(false)]
		public bool DisableAllQuickInfo { get; set; } = false;

		[Category("Hide Quick Info popup for built-in types")]
		[DisplayName("System.Boolean")]
		[Description("Hides the Quick Info popup for System.Boolean")]
		[DefaultValue(false)]
		public bool DisableBoolean { get; set; } = false;
		[Category("Hide Quick Info popup for built-in types")]
		[DisplayName("System.Byte")]
		[Description("Hides the Quick Info popup for System.Byte")]
		[DefaultValue(false)]
		public bool DisableByte { get; set; } = false;
		[Category("Hide Quick Info popup for built-in types")]
		[DisplayName("System.SByte")]
		[Description("Hides the Quick Info popup for System.SByte")]
		[DefaultValue(false)]
		public bool DisableSByte { get; set; } = false;
		[Category("Hide Quick Info popup for built-in types")]
		[DisplayName("System.Char")]
		[Description("Hides the Quick Info popup for System.Char")]
		[DefaultValue(false)]
		public bool DisableChar { get; set; } = false;
		[Category("Hide Quick Info popup for built-in types")]
		[DisplayName("System.DateTime")]
		[Description("Hides the Quick Info popup for System.DateTime")]
		[DefaultValue(false)]
		public bool DisableDateTime { get; set; } = false;
		[Category("Hide Quick Info popup for built-in types")]
		[DisplayName("System.Decimal")]
		[Description("Hides the Quick Info popup for System.Decimal")]
		[DefaultValue(false)]
		public bool DisableDecimal { get; set; } = false;
		[Category("Hide Quick Info popup for built-in types")]
		[DisplayName("System.Double")]
		[Description("Hides the Quick Info popup for System.Double")]
		[DefaultValue(false)]
		public bool DisableDouble { get; set; } = false;
		[Category("Hide Quick Info popup for built-in types")]
		[DisplayName("System.Single")]
		[Description("Hides the Quick Info popup for System.Single")]
		[DefaultValue(false)]
		public bool DisableSingle { get; set; } = false;
		[Category("Hide Quick Info popup for built-in types")]
		[DisplayName("System.String")]
		[Description("Hides the Quick Info popup for System.String")]
		[DefaultValue(false)]
		public bool DisableString { get; set; } = false;
		[Category("Hide Quick Info popup for built-in types")]
		[DisplayName("System.Int32")]
		[Description("Hides the Quick Info popup for System.Int32")]
		[DefaultValue(false)]
		public bool DisableInt32 { get; set; } = false;
		[Category("Hide Quick Info popup for built-in types")]
		[DisplayName("System.UInt32")]
		[Description("Hides the Quick Info popup for System.UInt32")]
		[DefaultValue(false)]
		public bool DisableUInt32 { get; set; } = false;
		[Category("Hide Quick Info popup for built-in types")]
		[DisplayName("System.IntPtr")]
		[Description("Hides the Quick Info popup for System.IntPtr")]
		[DefaultValue(false)]
		public bool DisableIntPtr { get; set; } = false;
		[Category("Hide Quick Info popup for built-in types")]
		[DisplayName("System.UIntPtr")]
		[Description("Hides the Quick Info popup for System.UIntPtr")]
		[DefaultValue(false)]
		public bool DisableUIntPtr { get; set; } = false;
		[Category("Hide Quick Info popup for built-in types")]
		[DisplayName("System.Int64")]
		[Description("Hides the Quick Info popup for System.Int64")]
		[DefaultValue(false)]
		public bool DisableInt64 { get; set; } = false;
		[Category("Hide Quick Info popup for built-in types")]
		[DisplayName("System.UInt64")]
		[Description("Hides the Quick Info popup for System.UInt64")]
		[DefaultValue(false)]
		public bool DisableUInt64 { get; set; } = false;
		[Category("Hide Quick Info popup for built-in types")]
		[DisplayName("System.Int16")]
		[Description("Hides the Quick Info popup for System.Int16")]
		[DefaultValue(false)]
		public bool DisableInt16 { get; set; } = false;
		[Category("Hide Quick Info popup for built-in types")]
		[DisplayName("System.UInt16")]
		[Description("Hides the Quick Info popup for System.UInt16")]
		[DefaultValue(false)]
		public bool DisableUInt16 { get; set; } = false;
		[Category("Hide Quick Info popup for built-in types")]
		[DisplayName("System.Object")]
		[Description("Hides the Quick Info popup for System.Object")]
		[DefaultValue(false)]
		public bool DisableObject { get; set; } = false;

		[Category("Hide Quick Info popup for these declarations")]
		[DisplayName("Class")]
		[Description("Hides the Quick Info popup for Class declarations")]
		[DefaultValue(false)]
		public bool DisableClass { get; set; } = false;
		[Category("Hide Quick Info popup for these declarations")]
		[DisplayName("Delegate")]
		[Description("Hides the Quick Info popup for Delegate declarations")]
		[DefaultValue(false)]
		public bool DisableDelegate { get; set; } = false;
		[Category("Hide Quick Info popup for these declarations")]
		[DisplayName("Dynamic")]
		[Description("Hides the Quick Info popup for Dynamic declarations")]
		[DefaultValue(false)]
		public bool DisableDynamic { get; set; } = false;
		[Category("Hide Quick Info popup for these declarations")]
		[DisplayName("Enum")]
		[Description("Hides the Quick Info popup for Enum declarations")]
		[DefaultValue(false)]
		public bool DisableEnum { get; set; } = false;
		[Category("Hide Quick Info popup for these declarations")]
		[DisplayName("Event")]
		[Description("Hides the Quick Info popup for Event declarations")]
		[DefaultValue(false)]
		public bool DisableEvent { get; set; } = false;
		[Category("Hide Quick Info popup for these declarations")]
		[DisplayName("Property")]
		[Description("Hides the Quick Info popup for Property declarations")]
		[DefaultValue(false)]
		public bool DisableProperty { get; set; } = false;
		[Category("Hide Quick Info popup for these declarations")]
		[DisplayName("Struct")]
		[Description("Hides the Quick Info popup for Struct declarations")]
		[DefaultValue(false)]
		public bool DisableStruct { get; set; } = false;

		[Category("Hide Quick Info popups for types in this list")]
		[DisplayName("Custom Types")]
		[DefaultValue("")]
		public BindingList<string> Types { get; set; } = new BindingList<string>() { };

		#region Declarations
		private IEnumerable<string> allDelcarations;
		internal IEnumerable<string> Declarations
		{
			get
			{
				if (isDirty || allDelcarations == null)
				{
					allDelcarations = LoadAllDeclarations();
					isDirty = false;
				}

				return allDelcarations;
			}
		}

		private IEnumerable<string> LoadAllDeclarations()
		{
			var declarations = new List<string>();
			if (DisableClass) declarations.Add("class");
			if (DisableDelegate) declarations.Add("delegate");
			if (DisableDynamic) declarations.Add("dynamic");
			if (DisableEnum) declarations.Add("enum");
			if (DisableEvent) declarations.Add("event");
			if (DisableProperty) declarations.Add("property");
			if (DisableStruct) declarations.Add("struct");

			return declarations;
		}
		#endregion

		#region All Types
		private IEnumerable<string> allHiddenTypes;
		internal IEnumerable<string> AllHiddenTypes
		{
			get
			{
				if (isDirty || allHiddenTypes == null)
				{
					allHiddenTypes = LoadAllHiddenTypes();
					isDirty = false;
				}
				return allHiddenTypes;
			}
		}

		private IEnumerable<string> LoadAllHiddenTypes()
		{
			var hiddenTypes = new List<string>();
			if (DisableBoolean) hiddenTypes.Add("System.Boolean");
			if (DisableByte) hiddenTypes.Add("System.Byte");
			if (DisableSByte) hiddenTypes.Add("System.SByte");
			if (DisableChar) hiddenTypes.Add("System.Char");
			if (DisableDateTime) hiddenTypes.Add("System.DateTime");
			if (DisableDecimal) hiddenTypes.Add("System.Decimal");
			if (DisableDouble) hiddenTypes.Add("System.Double");
			if (DisableSingle) hiddenTypes.Add("System.Single");
			if (DisableString) hiddenTypes.Add("System.String");
			if (DisableInt32) hiddenTypes.Add("System.Int32");
			if (DisableUInt32) hiddenTypes.Add("System.UInt32");
			if (DisableIntPtr) hiddenTypes.Add("System.IntPtr");
			if (DisableUIntPtr) hiddenTypes.Add("System.UIntPtr");
			if (DisableInt64) hiddenTypes.Add("System.Int64");
			if (DisableUInt64) hiddenTypes.Add("System.UInt64");
			if (DisableInt16) hiddenTypes.Add("System.Int16");
			if (DisableUInt16) hiddenTypes.Add("System.UInt16");
			foreach (var type in Types)
				hiddenTypes.Add(type);

			return hiddenTypes;
		}
		#endregion
	}
}
