﻿//************************************************************************************************
// Copyright © 2020 Steven M Cohn.  All rights reserved.
//************************************************************************************************

#pragma warning disable CS3003 // Type is not CLS-compliant

namespace River.OneMoreAddIn.Dialogs
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading;
	using Resx = River.OneMoreAddIn.Properties.Resources;


	internal partial class PhoneticsDialog : LocalizableForm
	{
		private class LanguageItem
		{
			public string Code;
			public string Name;
		}

		private readonly List<LanguageItem> languages;


		public PhoneticsDialog()
		{
			InitializeComponent();

			if (NeedsLocalizing())
			{
				Text = Resx.PhoneticsDialog_Text;

				Localize(new string[]
				{
					"wordLabel",
					"languageLabel",
					"okButton",
					"cancelButton"
				});
			}

			languages =
				(from a in Resx.PhoneticsDialog_languages.Split('\n')
				 let b = a.Split(',')
				 orderby b[1]
				 select new LanguageItem
				 {
					 Code = b[0],
					 Name = b[1]
				 })
				.ToList();

			languagesBox.Items.Clear();
			foreach (var language in languages)
			{
				languagesBox.Items.Add(language.Name);
			}

			var index = languages.FindIndex(l => 
				l.Code.StartsWith(Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName));

			languagesBox.SelectedIndex = index < 0 ? 0 : index;
		}


		public string Word
		{
			set => wordBox.Text = value;
			get => wordBox.Text;
		}


		public string Language
		{
			get => languages[languagesBox.SelectedIndex].Code;
			set => languagesBox.SelectedIndex = languages.FindIndex(l => l.Code == value);
		}
	}
}
