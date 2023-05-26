

/*     
        Склад_ШвидкийВибір.cs
        ШвидкийВибір
*/

using Gtk;

using AccountingSoftware;
using StorageAndTrade_1_0.Довідники;
using ТабличніСписки = StorageAndTrade_1_0.Довідники.ТабличніСписки;

namespace StorageAndTrade
{
    class Склад_ШвидкийВибір : ДовідникШвидкийВибір
    {
        public Склад_ШвидкийВибір() : base()
        {
            TreeViewGrid.Model = ТабличніСписки.Склад_ЗаписиШвидкийВибір.Store;
            ТабличніСписки.Склад_ЗаписиШвидкийВибір.AddColumns(TreeViewGrid);

            //Сторінка
            {
                LinkButton linkPage = new LinkButton($" {Склад_Const.FULLNAME}") { Halign = Align.Start, Image = new Image(AppContext.BaseDirectory + "images/doc.png"), AlwaysShowImage = true };
                linkPage.Clicked += (object? sender, EventArgs args) =>
                {
                    Склад page = new Склад()
                    {
                        DirectoryPointerItem = DirectoryPointerItem,
                        CallBack_OnSelectPointer = CallBack_OnSelectPointer
                    };

                    Program.GeneralForm?.CreateNotebookPage($"Вибір - {Склад_Const.FULLNAME}", () => { return page; }, true);

                    page.LoadRecords();
                };

                HBoxTop.PackStart(linkPage, false, false, 10);
            }

            //Новий
            {
                LinkButton linkNew = new LinkButton("Новий");
                linkNew.Clicked += (object? sender, EventArgs args) =>
                {
                    Склад_Елемент page = new Склад_Елемент
                    {
                        IsNew = true,
                        CallBack_OnSelectPointer = CallBack_OnSelectPointer
                    };

                    Program.GeneralForm?.CreateNotebookPage($"{Склад_Const.FULLNAME} *", () => { return page; }, true);

                    page.SetValue();
                };

                HBoxTop.PackStart(linkNew, false, false, 0);
            }
        }

        public override void LoadRecords()
        {
            ТабличніСписки.Склад_ЗаписиШвидкийВибір.DirectoryPointerItem = DirectoryPointerItem;

            ТабличніСписки.Склад_ЗаписиШвидкийВибір.Where.Clear();

            ТабличніСписки.Склад_ЗаписиШвидкийВибір.LoadRecords();

            if (ТабличніСписки.Склад_ЗаписиШвидкийВибір.SelectPath != null)
                TreeViewGrid.SetCursor(ТабличніСписки.Склад_ЗаписиШвидкийВибір.SelectPath, TreeViewGrid.Columns[0], false);
        }

        protected override void LoadRecords_OnSearch(string searchText)
        {
            searchText = searchText.ToLower().Trim();

            if (searchText.Length < 1)
                return;

            searchText = "%" + searchText.Replace(" ", "%") + "%";

            ТабличніСписки.Склад_ЗаписиШвидкийВибір.Where.Clear();

            //Код
            ТабличніСписки.Склад_ЗаписиШвидкийВибір.Where.Add(
                new Where(Склад_Const.Код, Comparison.LIKE, searchText) { FuncToField = "LOWER" });

            //Назва
            ТабличніСписки.Склад_ЗаписиШвидкийВибір.Where.Add(
                new Where(Comparison.OR, Склад_Const.Назва, Comparison.LIKE, searchText) { FuncToField = "LOWER" });

            ТабличніСписки.Склад_ЗаписиШвидкийВибір.LoadRecords();
        }
    }
}
    