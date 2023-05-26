
/*     
        ПоступленняТоварів.cs
        Список
*/

using Gtk;

using AccountingSoftware;

using ТабличніСписки = StorageAndTrade_1_0.Документи.ТабличніСписки;
using StorageAndTrade_1_0.Документи;
using StorageAndTrade_1_0.Перелічення;

namespace StorageAndTrade
{
    public class ПоступленняТоварів : ДокументЖурнал
    {
        public ПоступленняТоварів() : base()
        {
            TreeViewGrid.Model = ТабличніСписки.ПоступленняТоварів_Записи.Store;
            ТабличніСписки.ПоступленняТоварів_Записи.AddColumns(TreeViewGrid);
        }

        #region Override

        public override void LoadRecords()
        {
            ТабличніСписки.ПоступленняТоварів_Записи.SelectPointerItem = SelectPointerItem;
            ТабличніСписки.ПоступленняТоварів_Записи.DocumentPointerItem = DocumentPointerItem;

            ТабличніСписки.ПоступленняТоварів_Записи.LoadRecords();

            if (ТабличніСписки.ПоступленняТоварів_Записи.SelectPath != null)
                TreeViewGrid.SetCursor(ТабличніСписки.ПоступленняТоварів_Записи.SelectPath, TreeViewGrid.Columns[0], false);
            else if (ТабличніСписки.ПоступленняТоварів_Записи.CurrentPath != null)
                TreeViewGrid.SetCursor(ТабличніСписки.ПоступленняТоварів_Записи.CurrentPath, TreeViewGrid.Columns[0], false);

            TreeViewGrid.GrabFocus();
        }

        protected override void LoadRecords_OnSearch(string searchText)
        {
            searchText = searchText.ToLower().Trim();

            if (searchText.Length < 1)
                return;

            searchText = "%" + searchText.Replace(" ", "%") + "%";

            ТабличніСписки.ПоступленняТоварів_Записи.Where.Clear();

            //Назва
            ТабличніСписки.ПоступленняТоварів_Записи.Where.Add(
                new Where(ПоступленняТоварів_Const.Назва, Comparison.LIKE, searchText) { FuncToField = "LOWER" });

            ТабличніСписки.ПоступленняТоварів_Записи.LoadRecords();

            if (ТабличніСписки.ПоступленняТоварів_Записи.FirstPath != null)
                TreeViewGrid.SetCursor(ТабличніСписки.ПоступленняТоварів_Записи.FirstPath, TreeViewGrid.Columns[0], false);

            TreeViewGrid.GrabFocus();
        }

        protected override void OpenPageElement(bool IsNew, UnigueID? unigueID = null)
        {
            if (IsNew)
            {
                Program.GeneralForm?.CreateNotebookPage($"{ПоступленняТоварів_Const.FULLNAME} *", () =>
                {
                    ПоступленняТоварів_Елемент page = new ПоступленняТоварів_Елемент
                    {
                        CallBack_LoadRecords = CallBack_LoadRecords,
                        IsNew = true
                    };

                    page.SetValue();

                    return page;
                });
            }
            else if (unigueID != null)
            {
                ПоступленняТоварів_Objest ПоступленняТоварів_Objest = new ПоступленняТоварів_Objest();
                if (ПоступленняТоварів_Objest.Read(unigueID))
                {
                    Program.GeneralForm?.CreateNotebookPage($"{ПоступленняТоварів_Objest.Назва}", () =>
                    {
                        ПоступленняТоварів_Елемент page = new ПоступленняТоварів_Елемент
                        {
                            CallBack_LoadRecords = CallBack_LoadRecords,
                            IsNew = false,
                            ПоступленняТоварів_Objest = ПоступленняТоварів_Objest,
                        };

                        page.SetValue();

                        return page;
                    });
                }
                else
                    Message.Error(Program.GeneralForm, "Не вдалось прочитати!");
            }
        }

        protected override void SetDeletionLabel(UnigueID unigueID)
        {
            ПоступленняТоварів_Objest ПоступленняТоварів_Objest = new ПоступленняТоварів_Objest();
            if (ПоступленняТоварів_Objest.Read(unigueID))
                ПоступленняТоварів_Objest.SetDeletionLabel(!ПоступленняТоварів_Objest.DeletionLabel);
            else
                Message.Error(Program.GeneralForm, "Не вдалось прочитати!");
        }

        protected override UnigueID? Copy(UnigueID unigueID)
        {
            ПоступленняТоварів_Objest ПоступленняТоварів_Objest = new ПоступленняТоварів_Objest();
            if (ПоступленняТоварів_Objest.Read(unigueID))
            {
                ПоступленняТоварів_Objest ПоступленняТоварів_Objest_Новий = ПоступленняТоварів_Objest.Copy(true);
                ПоступленняТоварів_Objest_Новий.Save();

                return ПоступленняТоварів_Objest_Новий.UnigueID;
            }
            else
            {
                Message.Error(Program.GeneralForm, "Не вдалось прочитати!");
                return null;
            }
        }

        protected override void PeriodWhereChanged()
        {
            ТабличніСписки.ПоступленняТоварів_Записи.ДодатиВідбірПоПеріоду(Enum.Parse<ТипПеріодуДляЖурналівДокументів>(ComboBoxPeriodWhere.ActiveId));
            LoadRecords();
        }

        protected override void SpendTheDocument(UnigueID unigueID, bool spendDoc)
        {
            ПоступленняТоварів_Pointer ПоступленняТоварів_Pointer = new ПоступленняТоварів_Pointer(unigueID);
            ПоступленняТоварів_Objest? ПоступленняТоварів_Objest = ПоступленняТоварів_Pointer.GetDocumentObject(true);
            if (ПоступленняТоварів_Objest == null) return;

            if (spendDoc)
            {
                if (!ПоступленняТоварів_Objest.SpendTheDocument(ПоступленняТоварів_Objest.ДатаДок))
                    ФункціїДляПовідомлень.ВідкритиТермінал();
            }
            else
                ПоступленняТоварів_Objest.ClearSpendTheDocument();
        }

        protected override DocumentPointer? ReportSpendTheDocument(UnigueID unigueID)
        {
            return new ПоступленняТоварів_Pointer(unigueID);
        }

        protected override void ExportXML(UnigueID unigueID)
        {
            string pathToSave = System.IO.Path.Combine(AppContext.BaseDirectory, $"{ПоступленняТоварів_Const.FULLNAME}_{unigueID}.xml");
            ПоступленняТоварів_Export.ToXmlFile(new ПоступленняТоварів_Pointer(unigueID), pathToSave);
        }

        #endregion
    }
}
