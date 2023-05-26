

/*
        Номенклатура_Елемент.cs
        Елемент
*/

using Gtk;

using StorageAndTrade_1_0.Довідники;
using StorageAndTrade_1_0.Документи;
using StorageAndTrade_1_0.Перелічення;

namespace StorageAndTrade
{
    class Номенклатура_Елемент : ДовідникЕлемент
    {
        public Номенклатура_Objest Номенклатура_Objest { get; set; } = new Номенклатура_Objest();


        #region Fields

        Entry Код = new Entry() { WidthRequest = 500 };

        Entry Назва = new Entry() { WidthRequest = 500 };

        TextView Опис = new TextView();

        #endregion

        #region TabularParts

        #endregion

        public Номенклатура_Елемент() : base()
        {

        }

        protected override void CreatePack1(VBox vBox)
        {

            //Код
            CreateField(vBox, "Код:", Код);

            //Назва
            CreateField(vBox, "Назва:", Назва);

            //Опис
            CreateFieldView(vBox, "Опис:", Опис, 500, 200);

        }

        protected override void CreatePack2(VBox vBox)
        {

        }

        #region Присвоєння / зчитування значень

        public override void SetValue()
        {

            if (IsNew)
                Номенклатура_Objest.New();
            Код.Text = Номенклатура_Objest.Код;
            Назва.Text = Номенклатура_Objest.Назва;
            Опис.Buffer.Text = Номенклатура_Objest.Опис;

        }

        protected override void GetValue()
        {
            Номенклатура_Objest.Код = Код.Text;
            Номенклатура_Objest.Назва = Назва.Text;
            Номенклатура_Objest.Опис = Опис.Buffer.Text;

        }

        #endregion

        protected override void Save()
        {
            try
            {
                Номенклатура_Objest.Save();
            }
            catch (Exception ex)
            {
                MsgError(ex);
            }



            UnigueID = Номенклатура_Objest.UnigueID;
            Caption = Назва.Text;
        }
    }
}
