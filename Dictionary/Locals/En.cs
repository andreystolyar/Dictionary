namespace Dictionary.Locals
{
    internal class En : Local
    {
        #region Main Window
            public override string NewDictionary => "New Dictionary";
			public override string Selected => "Selected: ";
			public override string MainWindowTitle => "Personal Dictionary";
			public override string WordColumnHeader => "Word";
			public override string TranslationColumnHeader => "Translation";

            #region Message Boxes
                public override string MessageBoxSaveText =>
					"Do you want to save your changes to the file?";
                public override string MessageBoxSaveCaption =>
                    "Confirm saving";

                public override string MessageBoxDeleteText =>
                    "Are you sure you want to delete selected items?";
                public override string MessageBoxDeleteCaption =>
                    "Delete items";

                public override string MessageBoxUnsavedDataText =>
                    "All unsaved changes will be lost. Proceed?";
                public override string MessageBoxUnsavedDataCaption =>
                    "Unsaved changes";

                public override string MessageBoxUnsavedExitText =>
                    "Are you sure you want to exit without saving changes?";
                public override string MessageBoxUnsavedExitCaption =>
                    "Confirm exit";

            #endregion

            #region Button Names
                public override string ButtonLearnText => "Learn";
                public override string SelectAllButtonText => "Select All";
                public override string UnselectAllButtonText => "Unselect All";
                public override string SelectLastButtonText => "Select last ";
                public override string SelectRandomButtonText => "Select random ";

            #endregion

            #region Tooltips
                public override string EntriesNumberTextBoxTooltip =>
                    "The number for selection";
                public override string ButtonNewTooltip => "New dictionary";
                public override string ButtonOpenTooltip => "Open a dictionary";
                public override string ButtonSaveTooltip => "Save the dictionary";
                public override string ButtonAddTooltip => "Add a new word";
                public override string ButtonDeleteTooltip => "Delete selected";
                public override string ButtonSearchTooltip => "Search for a word";

                public override string ButtonLearnTooltip =>
                    "Study selected words";
                public override string ButtonShuffleTooltip => "Study words in random order";
                public override string ButtonRepeatTooltip => "Loop the words order";

            #endregion

        #endregion

        #region Trainer Window
        public override string WindowTrainerTitle => "Trainer";
            public override string ButtonNextTooltip => "Next word";
            public override string ButtonPreviousTooltip => "Previous word";
            public override string WordCheckBoxTooltip => "Show/hide word";
            public override string TranslationCheckBoxTooltip => "Show/hide translation";

        #endregion

        #region Add Window
            public override string AddWindowTitle => "Adding a new word";
            public override string LabelWordText => "Word:";
            public override string LabelTranslationText => "Translation:";

        #endregion
    }
}
