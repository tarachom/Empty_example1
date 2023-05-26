

/*     
        ПродажТоварів.cs
        Список
*/

using Gtk;

using AccountingSoftware;

using ТабличніСписки = StorageAndTrade_1_0.Документи.ТабличніСписки;
using StorageAndTrade_1_0.Документи;
using StorageAndTrade_1_0.Перелічення;

namespace StorageAndTrade
{
    public class ПродажТоварів : ДокументЖурнал
    {
        public ПродажТоварів() : base()
        {
            TreeViewGrid.Model = ТабличніСписки.ПродажТоварів_Записи.Store;
            ТабличніСписки.ПродажТоварів_Записи.AddColumns(TreeViewGrid);
        }

        #region Override

        public override void LoadRecords()
        {
            ТабличніСписки.ПродажТоварів_Записи.SelectPointerItem = SelectPointerItem;
            ТабличніСписки.ПродажТоварів_Записи.DocumentPointerItem = DocumentPointerItem;

            ТабличніСписки.ПродажТоварів_Записи.LoadRecords();

            if (ТабличніСписки.ПродажТоварів_Записи.SelectPath != null)
                TreeViewGrid.SetCursor(ТабличніСписки.ПродажТоварів_Записи.SelectPath, TreeViewGrid.Columns[0], false);
            else if (ТабличніСписки.ПродажТоварів_Записи.CurrentPath != null)
                TreeViewGrid.SetCursor(ТабличніСписки.ПродажТоварів_Записи.CurrentPath, TreeViewGrid.Columns[0], false);

            TreeViewGrid.GrabFocus();
        }

        protected override void LoadRecords_OnSearch(string searchText)
        {
            searchText = searchText.ToLower().Trim();

            if (searchText.Length < 1)
                return;

            searchText = "%" + searchText.Replace(" ", "%") + "%";

            ТабличніСписки.ПродажТоварів_Записи.Where.Clear();

            //Назва
            ТабличніСписки.ПродажТоварів_Записи.Where.Add(
                new Where(ПродажТоварів_Const.Назва, Comparison.LIKE, searchText) { FuncToField = "LOWER" });

            ТабличніСписки.ПродажТоварів_Записи.LoadRecords();

            if (ТабличніСписки.ПродажТоварів_Записи.FirstPath != null)
                TreeViewGrid.SetCursor(ТабличніСписки.ПродажТоварів_Записи.FirstPath, TreeViewGrid.Columns[0], false);

            TreeViewGrid.GrabFocus();
        }

        protected override void OpenPageElement(bool IsNew, UnigueID? unigueID = null)
        {
            if (IsNew)
            {
                Program.GeneralForm?.CreateNotebookPage($"{ПродажТоварів_Const.FULLNAME} *", () =>
                {
                    ПродажТоварів_Елемент page = new ПродажТоварів_Елемент
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
                ПродажТоварів_Objest ПродажТоварів_Objest = new ПродажТоварів_Objest();
                if (ПродажТоварів_Objest.Read(unigueID))
                {
                    Program.GeneralForm?.CreateNotebookPage($"{ПродажТоварів_Objest.Назва}", () =>
                    {
                        ПродажТоварів_Елемент page = new ПродажТоварів_Елемент
                        {
                            CallBack_LoadRecords = CallBack_LoadRecords,
                            IsNew = false,
                            ПродажТоварів_Objest = ПродажТоварів_Objest,
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
            ПродажТоварів_Objest ПродажТоварів_Objest = new ПродажТоварів_Objest();
            if (ПродажТоварів_Objest.Read(unigueID))
                ПродажТоварів_Objest.SetDeletionLabel(!ПродажТоварів_Objest.DeletionLabel);
            else
                Message.Error(Program.GeneralForm, "Не вдалось прочитати!");
        }

        protected override UnigueID? Copy(UnigueID unigueID)
        {
            ПродажТоварів_Objest ПродажТоварів_Objest = new ПродажТоварів_Objest();
            if (ПродажТоварів_Objest.Read(unigueID))
            {
                ПродажТоварів_Objest ПродажТоварів_Objest_Новий = ПродажТоварів_Objest.Copy(true);
                ПродажТоварів_Objest_Новий.Save();
                
                return ПродажТоварів_Objest_Новий.UnigueID;
            }
            else
            {
                Message.Error(Program.GeneralForm, "Не вдалось прочитати!");
                return null;
            }
        }

        protected override void PeriodWhereChanged()
        {
            ТабличніСписки.ПродажТоварів_Записи.ДодатиВідбірПоПеріоду(Enum.Parse<ТипПеріодуДляЖурналівДокументів>(ComboBoxPeriodWhere.ActiveId));
            LoadRecords();
        }

        protected override void SpendTheDocument(UnigueID unigueID, bool spendDoc)
        {
            ПродажТоварів_Pointer ПродажТоварів_Pointer = new ПродажТоварів_Pointer(unigueID);
            ПродажТоварів_Objest? ПродажТоварів_Objest = ПродажТоварів_Pointer.GetDocumentObject(true);
            if (ПродажТоварів_Objest == null) return;

            if (spendDoc)
            {
                if (!ПродажТоварів_Objest.SpendTheDocument(ПродажТоварів_Objest.ДатаДок))
                    ФункціїДляПовідомлень.ВідкритиТермінал();
            }
            else
                ПродажТоварів_Objest.ClearSpendTheDocument();
        }

        protected override DocumentPointer? ReportSpendTheDocument(UnigueID unigueID)
        {
            return new ПродажТоварів_Pointer(unigueID);
        }

        protected override void ExportXML(UnigueID unigueID)
        {
            string pathToSave = System.IO.Path.Combine(AppContext.BaseDirectory, $"{ПродажТоварів_Const.FULLNAME}_{unigueID}.xml");
            ПродажТоварів_Export.ToXmlFile(new ПродажТоварів_Pointer(unigueID), pathToSave);
        }

        #endregion
    }
}
    