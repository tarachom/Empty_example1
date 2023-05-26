

/*     
        Номенклатура.cs
        Список
*/

using AccountingSoftware;
using StorageAndTrade_1_0.Довідники;
using ТабличніСписки = StorageAndTrade_1_0.Довідники.ТабличніСписки;

namespace StorageAndTrade
{
    public class Номенклатура : ДовідникЖурнал
    {
        public Номенклатура() : base()
        {
            TreeViewGrid.Model = ТабличніСписки.Номенклатура_Записи.Store;
            ТабличніСписки.Номенклатура_Записи.AddColumns(TreeViewGrid);
        }

        #region Override

        public override void LoadRecords()
        {
            ТабличніСписки.Номенклатура_Записи.SelectPointerItem = SelectPointerItem;
            ТабличніСписки.Номенклатура_Записи.DirectoryPointerItem = DirectoryPointerItem;

            ТабличніСписки.Номенклатура_Записи.Where.Clear();

            ТабличніСписки.Номенклатура_Записи.LoadRecords();

            if (ТабличніСписки.Номенклатура_Записи.SelectPath != null)
                TreeViewGrid.SetCursor(ТабличніСписки.Номенклатура_Записи.SelectPath, TreeViewGrid.Columns[0], false);

            TreeViewGrid.GrabFocus();
        }

        protected override void LoadRecords_OnSearch(string searchText)
        {
            searchText = searchText.ToLower().Trim();

            if (searchText.Length < 1)
                return;

            searchText = "%" + searchText.Replace(" ", "%") + "%";

            ТабличніСписки.Номенклатура_Записи.Where.Clear();

            //Назва
            ТабличніСписки.Номенклатура_Записи.Where.Add(
                new Where(Номенклатура_Const.Назва, Comparison.LIKE, searchText) { FuncToField = "LOWER" });

            ТабличніСписки.Номенклатура_Записи.LoadRecords();

            if (ТабличніСписки.Номенклатура_Записи.FirstPath != null)
                TreeViewGrid.SetCursor(ТабличніСписки.Номенклатура_Записи.FirstPath, TreeViewGrid.Columns[0], false);
        }

        protected override void OpenPageElement(bool IsNew, UnigueID? unigueID = null)
        {
            if (IsNew)
            {
                Program.GeneralForm?.CreateNotebookPage($"{Номенклатура_Const.FULLNAME} *", () =>
                {
                    Номенклатура_Елемент page = new Номенклатура_Елемент
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
                Номенклатура_Objest Номенклатура_Objest = new Номенклатура_Objest();
                if (Номенклатура_Objest.Read(unigueID))
                {
                    Program.GeneralForm?.CreateNotebookPage($"{Номенклатура_Objest.Назва}", () =>
                    {
                        Номенклатура_Елемент page = new Номенклатура_Елемент
                        {
                            CallBack_LoadRecords = CallBack_LoadRecords,
                            IsNew = false,
                            Номенклатура_Objest = Номенклатура_Objest,
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
            Номенклатура_Objest Номенклатура_Objest = new Номенклатура_Objest();
            if (Номенклатура_Objest.Read(unigueID))
                Номенклатура_Objest.SetDeletionLabel(!Номенклатура_Objest.DeletionLabel);
            else
                Message.Error(Program.GeneralForm, "Не вдалось прочитати!");
        }

        protected override UnigueID? Copy(UnigueID unigueID)
        {
            Номенклатура_Objest Номенклатура_Objest = new Номенклатура_Objest();
            if (Номенклатура_Objest.Read(unigueID))
            {
                Номенклатура_Objest Номенклатура_Objest_Новий = Номенклатура_Objest.Copy(true);
                Номенклатура_Objest_Новий.Save();
                
                return Номенклатура_Objest_Новий.UnigueID;
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
    