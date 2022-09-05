namespace Dictionary.Locals
{
    internal class Ru : Local
    {
        #region Main Window
            public override string NewDictionary => "Новый словарь";
			public override string Selected => "Выбрано: ";
			public override string MainWindowTitle => "Персональный Словарь";
			public override string WordColumnHeader => "Слово";
			public override string TranslationColumnHeader => "Перевод";

            #region Message Boxes
                public override string MessageBoxSaveText => "Вы действительно хотите сохранить файл?";
                public override string MessageBoxSaveCaption => "Сохранение файла";

                public override string MessageBoxDeleteText =>
                    "Вы действительно хотите удалить выбранные записи?";
                public override string MessageBoxDeleteCaption => "Удаление";

                public override string MessageBoxUnsavedDataText =>
                    "Все несохранённые данные будут потеряны. Продолжить?";
                public override string MessageBoxUnsavedDataCaption =>
                    "Несохранённые данные будут потеряны";

                public override string MessageBoxUnsavedExitText =>
                    "Все несохранённые данные будут потеряны. " +
                    "Вы действительно хотите выйти?";
                public override string MessageBoxUnsavedExitCaption => "Выход";

            #endregion

            #region Button Names
                public override string ButtonLearnText => "Учить";
                public override string SelectAllButtonText => "Выделить всё";
                public override string UnselectAllButtonText => "Снять выделение";
                public override string SelectLastButtonText => "Выделить последние ";
                public override string SelectRandomButtonText => "Выделить случайные ";

            #endregion

            #region Tooltips
                public override string EntriesNumberTextBoxTooltip =>
                    "Число для произвольного выделения записей";
                public override string ButtonNewTooltip => "Создать новый словарь";
                public override string ButtonOpenTooltip => "Открыть словарь";
                public override string ButtonSaveTooltip => "Сохранить словарь";
                public override string ButtonAddTooltip => "Добавить новое слово в словарь";
                public override string ButtonDeleteTooltip => "Удалить слово из словаря";
                public override string ButtonSearchTooltip => "Найти слово в словаре";

                public override string ButtonLearnTooltip =>
                    "Запустить тренажёр для изучения выделенных слов";
                public override string ButtonShuffleTooltip => "Изучать слова в случайном порядке";
                public override string ButtonRepeatTooltip => "Зациклить порядок слов";

            #endregion

        #endregion

        #region Trainer Window
        public override string WindowTrainerTitle => "Тренажёр";
            public override string ButtonNextTooltip => "Следующее слово";
            public override string ButtonPreviousTooltip => "Предыдущее слово";
            public override string WordCheckBoxTooltip => "Показать/скрыть слово";
            public override string TranslationCheckBoxTooltip => "Показать/скрыть перевод";

        #endregion

        #region Add Window
            public override string AddWindowTitle => "Добавление нового слова";
            public override string LabelWordText => "Слово:";
            public override string LabelTranslationText => "Перевод:";

        #endregion
    }
}
