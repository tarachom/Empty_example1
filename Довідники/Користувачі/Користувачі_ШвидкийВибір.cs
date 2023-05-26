

/*     
        Користувачі_ШвидкийВибір.cs
        ШвидкийВибір
*/

using Gtk;

using AccountingSoftware;
using StorageAndTrade_1_0.Довідники;
using ТабличніСписки = StorageAndTrade_1_0.Довідники.ТабличніСписки;

namespace StorageAndTrade
{
    class Користувачі_ШвидкийВибір : ДовідникШвидкийВибір
    {
        public Користувачі_ШвидкийВибір() : base()
        {
            TreeViewGrid.Model = ТабличніСписки.Користувачі_ЗаписиШвидкийВибір.Store;
            ТабличніСписки.Користувачі_ЗаписиШвидкийВибір.AddColumns(TreeViewGrid);

            //Сторінка
            {
                LinkButton linkPage = new LinkButton($" {Користувачі_Const.FULLNAME}") { Halign = Align.Start, Image = new Image(AppContext.BaseDirectory + "images/doc.png"), AlwaysShowImage = true };
                linkPage.Clicked += (object? sender, EventArgs args) =>
                {
                    Користувачі page = new Користувачі()
                    {
                        DirectoryPointerItem = DirectoryPointerItem,
                        CallBack_OnSelectPointer = CallBack_OnSelectPointer
                    };

                    Program.GeneralForm?.CreateNotebookPage($"Вибір - {Користувачі_Const.FULLNAME}", () => { return page; }, true);

                    page.LoadRecords();
                };

                HBoxTop.PackStart(linkPage, false, false, 10);
            }

            //Новий
            {
                LinkButton linkNew = new LinkButton("Новий");
                linkNew.Clicked += (object? sender, EventArgs args) =>
                {
                    Користувачі_Елемент page = new Користувачі_Елемент
                    {
                        IsNew = true,
                        CallBack_OnSelectPointer = CallBack_OnSelectPointer
                    };

                    Program.GeneralForm?.CreateNotebookPage($"{Користувачі_Const.FULLNAME} *", () => { return page; }, true);

                    page.SetValue();
                };

                HBoxTop.PackStart(linkNew, false, false, 0);
            }
        }

        public override void LoadRecords()
        {
            ТабличніСписки.Користувачі_ЗаписиШвидкийВибір.DirectoryPointerItem = DirectoryPointerItem;

            ТабличніСписки.Користувачі_ЗаписиШвидкийВибір.Where.Clear();

            ТабличніСписки.Користувачі_ЗаписиШвидкийВибір.LoadRecords();

            if (ТабличніСписки.Користувачі_ЗаписиШвидкийВибір.SelectPath != null)
                TreeViewGrid.SetCursor(ТабличніСписки.Користувачі_ЗаписиШвидкийВибір.SelectPath, TreeViewGrid.Columns[0], false);
        }

        protected override void LoadRecords_OnSearch(string searchText)
        {
            searchText = searchText.ToLower().Trim();

            if (searchText.Length < 1)
                return;

            searchText = "%" + searchText.Replace(" ", "%") + "%";

            ТабличніСписки.Користувачі_ЗаписиШвидкийВибір.Where.Clear();

            //Код
            ТабличніСписки.Користувачі_ЗаписиШвидкийВибір.Where.Add(
                new Where(Користувачі_Const.Код, Comparison.LIKE, searchText) { FuncToField = "LOWER" });

            //Назва
            ТабличніСписки.Користувачі_ЗаписиШвидкийВибір.Where.Add(
                new Where(Comparison.OR, Користувачі_Const.Назва, Comparison.LIKE, searchText) { FuncToField = "LOWER" });

            ТабличніСписки.Користувачі_ЗаписиШвидкийВибір.LoadRecords();
        }
    }
}
    