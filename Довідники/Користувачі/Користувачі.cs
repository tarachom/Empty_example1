

/*     
        Користувачі.cs
        Список
*/

using AccountingSoftware;
using StorageAndTrade_1_0.Довідники;
using ТабличніСписки = StorageAndTrade_1_0.Довідники.ТабличніСписки;

namespace StorageAndTrade
{
    public class Користувачі : ДовідникЖурнал
    {
        public Користувачі() : base()
        {
            TreeViewGrid.Model = ТабличніСписки.Користувачі_Записи.Store;
            ТабличніСписки.Користувачі_Записи.AddColumns(TreeViewGrid);
        }

        #region Override

        public override void LoadRecords()
        {
            ТабличніСписки.Користувачі_Записи.SelectPointerItem = SelectPointerItem;
            ТабличніСписки.Користувачі_Записи.DirectoryPointerItem = DirectoryPointerItem;

            ТабличніСписки.Користувачі_Записи.Where.Clear();

            ТабличніСписки.Користувачі_Записи.LoadRecords();

            if (ТабличніСписки.Користувачі_Записи.SelectPath != null)
                TreeViewGrid.SetCursor(ТабличніСписки.Користувачі_Записи.SelectPath, TreeViewGrid.Columns[0], false);

            TreeViewGrid.GrabFocus();
        }

        protected override void LoadRecords_OnSearch(string searchText)
        {
            searchText = searchText.ToLower().Trim();

            if (searchText.Length < 1)
                return;

            searchText = "%" + searchText.Replace(" ", "%") + "%";

            ТабличніСписки.Користувачі_Записи.Where.Clear();

            //Назва
            ТабличніСписки.Користувачі_Записи.Where.Add(
                new Where(Користувачі_Const.Назва, Comparison.LIKE, searchText) { FuncToField = "LOWER" });

            ТабличніСписки.Користувачі_Записи.LoadRecords();

            if (ТабличніСписки.Користувачі_Записи.FirstPath != null)
                TreeViewGrid.SetCursor(ТабличніСписки.Користувачі_Записи.FirstPath, TreeViewGrid.Columns[0], false);
        }

        protected override void OpenPageElement(bool IsNew, UnigueID? unigueID = null)
        {
            if (IsNew)
            {
                Program.GeneralForm?.CreateNotebookPage($"{Користувачі_Const.FULLNAME} *", () =>
                {
                    Користувачі_Елемент page = new Користувачі_Елемент
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
                Користувачі_Objest Користувачі_Objest = new Користувачі_Objest();
                if (Користувачі_Objest.Read(unigueID))
                {
                    Program.GeneralForm?.CreateNotebookPage($"{Користувачі_Objest.Назва}", () =>
                    {
                        Користувачі_Елемент page = new Користувачі_Елемент
                        {
                            CallBack_LoadRecords = CallBack_LoadRecords,
                            IsNew = false,
                            Користувачі_Objest = Користувачі_Objest,
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
            Користувачі_Objest Користувачі_Objest = new Користувачі_Objest();
            if (Користувачі_Objest.Read(unigueID))
                Користувачі_Objest.SetDeletionLabel(!Користувачі_Objest.DeletionLabel);
            else
                Message.Error(Program.GeneralForm, "Не вдалось прочитати!");
        }

        protected override UnigueID? Copy(UnigueID unigueID)
        {
            Користувачі_Objest Користувачі_Objest = new Користувачі_Objest();
            if (Користувачі_Objest.Read(unigueID))
            {
                Користувачі_Objest Користувачі_Objest_Новий = Користувачі_Objest.Copy(true);
                Користувачі_Objest_Новий.Save();

                return Користувачі_Objest_Новий.UnigueID;
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
    