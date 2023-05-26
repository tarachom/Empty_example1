

/*
        Склад_Елемент.cs
        Елемент
*/

using Gtk;

using StorageAndTrade_1_0.Довідники;
using StorageAndTrade_1_0.Документи;
using StorageAndTrade_1_0.Перелічення;

namespace StorageAndTrade
{
    class Склад_Елемент : ДовідникЕлемент
    {
        public Склад_Objest Склад_Objest { get; set; } = new Склад_Objest();


        #region Fields

        Entry Код = new Entry() { WidthRequest = 500 };

        Entry Назва = new Entry() { WidthRequest = 500 };

        #endregion

        #region TabularParts

        #endregion

        public Склад_Елемент() : base()
        {

        }

        protected override void CreatePack1(VBox vBox)
        {

            //Код
            CreateField(vBox, "Код:", Код);

            //Назва
            CreateField(vBox, "Назва:", Назва);

        }

        protected override void CreatePack2(VBox vBox)
        {

        }

        #region Присвоєння / зчитування значень

        public override void SetValue()
        {

            if (IsNew)
                Склад_Objest.New();
            Код.Text = Склад_Objest.Код;
            Назва.Text = Склад_Objest.Назва;

        }

        protected override void GetValue()
        {
            Склад_Objest.Код = Код.Text;
            Склад_Objest.Назва = Назва.Text;

        }

        #endregion

        protected override void Save()
        {
            try
            {
                Склад_Objest.Save();
            }
            catch (Exception ex)
            {
                MsgError(ex);
            }



            UnigueID = Склад_Objest.UnigueID;
            Caption = Назва.Text;
        }
    }
}
