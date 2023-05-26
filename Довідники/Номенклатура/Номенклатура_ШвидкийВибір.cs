

/*     
        Номенклатура_ШвидкийВибір.cs
        ШвидкийВибір
*/

using Gtk;

using AccountingSoftware;
using StorageAndTrade_1_0.Довідники;
using ТабличніСписки = StorageAndTrade_1_0.Довідники.ТабличніСписки;

namespace StorageAndTrade
{
    class Номенклатура_ШвидкийВибір : ДовідникШвидкийВибір
    {
        public Номенклатура_ШвидкийВибір() : base()
        {
            TreeViewGrid.Model = ТабличніСписки.Номенклатура_ЗаписиШвидкийВибір.Store;
            ТабличніСписки.Номенклатура_ЗаписиШвидкийВибір.AddColumns(TreeViewGrid);

            //Сторінка
            {
                LinkButton linkPage = new LinkButton($" {Номенклатура_Const.FULLNAME}") { Halign = Align.Start, Image = new Image(AppContext.BaseDirectory + "images/doc.png"), AlwaysShowImage = true };
                linkPage.Clicked += (object? sender, EventArgs args) =>
                {
                    Номенклатура page = new Номенклатура()
                    {
                        DirectoryPointerItem = DirectoryPointerItem,
                        CallBack_OnSelectPointer = CallBack_OnSelectPointer
                    };

                    Program.GeneralForm?.CreateNotebookPage($"Вибір - {Номенклатура_Const.FULLNAME}", () => { return page; }, true);

                    page.LoadRecords();
                };

                HBoxTop.PackStart(linkPage, false, false, 10);
            }

            //Новий
            {
                LinkButton linkNew = new LinkButton("Новий");
                linkNew.Clicked += (object? sender, EventArgs args) =>
                {
                    Номенклатура_Елемент page = new Номенклатура_Елемент
                    {
                        IsNew = true,
                        CallBack_OnSelectPointer = CallBack_OnSelectPointer
                    };

                    Program.GeneralForm?.CreateNotebookPage($"{Номенклатура_Const.FULLNAME} *", () => { return page; }, true);

                    page.SetValue();
                };

                HBoxTop.PackStart(linkNew, false, false, 0);
            }
        }

        public override void LoadRecords()
        {
            ТабличніСписки.Номенклатура_ЗаписиШвидкийВибір.DirectoryPointerItem = DirectoryPointerItem;

            ТабличніСписки.Номенклатура_ЗаписиШвидкийВибір.Where.Clear();

            ТабличніСписки.Номенклатура_ЗаписиШвидкийВибір.LoadRecords();

            if (ТабличніСписки.Номенклатура_ЗаписиШвидкийВибір.SelectPath != null)
                TreeViewGrid.SetCursor(ТабличніСписки.Номенклатура_ЗаписиШвидкийВибір.SelectPath, TreeViewGrid.Columns[0], false);
        }

        protected override void LoadRecords_OnSearch(string searchText)
        {
            searchText = searchText.ToLower().Trim();

            if (searchText.Length < 1)
                return;

            searchText = "%" + searchText.Replace(" ", "%") + "%";

            ТабличніСписки.Номенклатура_ЗаписиШвидкийВибір.Where.Clear();

            //Код
            ТабличніСписки.Номенклатура_ЗаписиШвидкийВибір.Where.Add(
                new Where(Номенклатура_Const.Код, Comparison.LIKE, searchText) { FuncToField = "LOWER" });

            //Назва
            ТабличніСписки.Номенклатура_ЗаписиШвидкийВибір.Where.Add(
                new Where(Comparison.OR, Номенклатура_Const.Назва, Comparison.LIKE, searchText) { FuncToField = "LOWER" });

            ТабличніСписки.Номенклатура_ЗаписиШвидкийВибір.LoadRecords();
        }
    }
}
    