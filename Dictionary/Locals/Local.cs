namespace Dictionary.Locals
{
    internal abstract class Local
    {
        #region Main Window
            public abstract string MainWindowTitle { get; }
            public abstract string NewDictionary { get; }
            public abstract string Selected { get; }
            public abstract string WordColumnHeader { get; }
            public abstract string TranslationColumnHeader { get; }

            #region Message Boxes
                public abstract string MessageBoxSaveText { get; }
                public abstract string MessageBoxSaveCaption { get; }

                public abstract string MessageBoxDeleteText { get; }
                public abstract string MessageBoxDeleteCaption { get; }

                public abstract string MessageBoxUnsavedDataText { get; }
                public abstract string MessageBoxUnsavedDataCaption { get; }

                public abstract string MessageBoxUnsavedExitText { get; }
                public abstract string MessageBoxUnsavedExitCaption { get; }

            #endregion

            #region Button Names
                public abstract string ButtonLearnText { get; }
                public virtual string ButtonShuffleText { get; } = "\u292e";
                public virtual string ButtonRepeatText { get; } = "\u2b6e";
                public abstract string SelectAllButtonText { get; }
                public abstract string UnselectAllButtonText { get; }
                public abstract string SelectLastButtonText { get; }
                public abstract string SelectRandomButtonText { get; }

            #endregion

            #region Tooltips
                public abstract string EntriesNumberTextBoxTooltip { get; }
                public abstract string ButtonNewTooltip { get; }
                public abstract string ButtonOpenTooltip { get; }
                public abstract string ButtonSaveTooltip { get; }
                public abstract string ButtonAddTooltip { get; }
                public abstract string ButtonDeleteTooltip { get; }
                public abstract string ButtonSearchTooltip { get; }

                public abstract string ButtonLearnTooltip { get; }
                public abstract string ButtonShuffleTooltip { get; }
                public abstract string ButtonRepeatTooltip { get; }

            #endregion

        #endregion

        #region Trainer Window
            public virtual string ButtonNextText { get; } = "\U0001f846";
            public virtual string ButtonPreviousText { get; } = "\U0001f844";
            public abstract string WindowTrainerTitle { get; }
            public abstract string ButtonNextTooltip { get; }
            public abstract string ButtonPreviousTooltip { get; }
            public abstract string WordCheckBoxTooltip { get; }
            public abstract string TranslationCheckBoxTooltip { get; }

        #endregion

        #region Add Window
            public abstract string AddWindowTitle { get; }
            public abstract string LabelWordText { get; }
            public abstract string LabelTranslationText { get; }

        #endregion
    }
}
