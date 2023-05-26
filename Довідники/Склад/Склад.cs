

/*     
        Склад.cs
        Список
*/

using AccountingSoftware;
using StorageAndTrade_1_0.Довідники;
using ТабличніСписки = StorageAndTrade_1_0.Довідники.ТабличніСписки;

namespace StorageAndTrade
{
    public class Склад : ДовідникЖурнал
    {
        public Склад() : base()
        {
            TreeViewGrid.Model = ТабличніСписки.Склад_Записи.Store;
            ТабличніСписки.Склад_Записи.AddColumns(TreeViewGrid);
        }

        #region Override

        public override void LoadRecords()
        {
            ТабличніСписки.Склад_Записи.SelectPointerItem = SelectPointerItem;
            ТабличніСписки.Склад_Записи.DirectoryPointerItem = DirectoryPointerItem;

            ТабличніСписки.Склад_Записи.Where.Clear();

            ТабличніСписки.Склад_Записи.LoadRecords();

            if (ТабличніСписки.Склад_Записи.SelectPath != null)
                TreeViewGrid.SetCursor(ТабличніСписки.Склад_Записи.SelectPath, TreeViewGrid.Columns[0], false);

            TreeViewGrid.GrabFocus();
        }

        protected override void LoadRecords_OnSearch(string searchText)
        {
            searchText = searchText.ToLower().Trim();

            if (searchText.Length < 1)
                return;

            searchText = "%" + searchText.Replace(" ", "%") + "%";

            ТабличніСписки.Склад_Записи.Where.Clear();

            //Назва
            ТабличніСписки.Склад_Записи.Where.Add(
                new Where(Склад_Const.Назва, Comparison.LIKE, searchText) { FuncToField = "LOWER" });

            ТабличніСписки.Склад_Записи.LoadRecords();

            if (ТабличніСписки.Склад_Записи.FirstPath != null)
                TreeViewGrid.SetCursor(ТабличніСписки.Склад_Записи.FirstPath, TreeViewGrid.Columns[0], false);
        }

        protected override void OpenPageElement(bool IsNew, UnigueID? unigueID = null)
        {
            if (IsNew)
            {
                Program.GeneralForm?.CreateNotebookPage($"{Склад_Const.FULLNAME} *", () =>
                {
                    Склад_Елемент page = new Склад_Елемент
                    {
                        CallBack_LoadRecords = CallBack_LoadRecords,
                        IsNew = true
                    };

                    page.SetValue();

                    return page;
                }, true);
            }
            else if (unigueID != null)
            {
                Склад_Objest Склад_Objest = new Склад_Objest();
                if (Склад_Objest.Read(unigueID))
                {
                    Program.GeneralForm?.CreateNotebookPage($"{Склад_Objest.Назва}", () =>
                    {
                        Склад_Елемент page = new Склад_Елемент
                        {
                            CallBack_LoadRecords = CallBack_LoadRecords,
                            IsNew = false,
                            Склад_Objest = Склад_Objest,
                        };

                        page.SetValue();

                        return page;
                    }, true);
                }
                else
                    Message.Error(Program.GeneralForm, "Не вдалось прочитати!");
            }
        }

        protected override void SetDeletionLabel(UnigueID unigueID)
        {
            Склад_Objest Склад_Objest = new Склад_Objest();
            if (Склад_Objest.Read(unigueID))
                Склад_Objest.SetDeletionLabel(!Склад_Objest.DeletionLabel);
            else
                Message.Error(Program.GeneralForm, "Не вдалось прочитати!");
        }

        protected override UnigueID? Copy(UnigueID unigueID)
        {
            Склад_Objest Склад_Objest = new Склад_Objest();
            if (Склад_Objest.Read(unigueID))
            {
                Склад_Objest Склад_Objest_Новий = Склад_Objest.Copy(true);
                Склад_Objest_Новий.Save();
                
                return Склад_Objest_Новий.UnigueID;
            }
            else
            {
                Message.Error(Program.GeneralForm, "Не вдалось прочитати!");
                return null;
            }
        }

        #endregion
    }
}
    