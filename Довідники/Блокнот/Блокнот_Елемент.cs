

/*
        Блокнот_Елемент.cs
        Елемент
*/

using Gtk;

using StorageAndTrade_1_0.Довідники;
using StorageAndTrade_1_0.Документи;
using StorageAndTrade_1_0.Перелічення;

namespace StorageAndTrade
{
    class Блокнот_Елемент : ДовідникЕлемент
    {
        public Блокнот_Objest Блокнот_Objest { get; set; } = new Блокнот_Objest();

        #region Fields

        Entry Код = new Entry() { WidthRequest = 500 };

        Entry Назва = new Entry() { WidthRequest = 500 };

        TextView Запис = new TextView();

        #endregion

        #region TabularParts

        #endregion

        public Блокнот_Елемент() : base()
        {

        }

        protected override void CreatePack1(VBox vBox)
        {

            CreateField(vBox, "Код:", Код);

            CreateField(vBox, "Назва:", Назва);

            CreateFieldView(vBox, "Запис:", Запис, 500, 200);

        }

        protected override void CreatePack2(VBox vBox)
        {

        }

        #region Присвоєння / зчитування значень

        public override void SetValue()
        {
            if (IsNew)
                Блокнот_Objest.New();

            Код.Text = Блокнот_Objest.Код;
            Назва.Text = Блокнот_Objest.Назва;
            Запис.Buffer.Text = Блокнот_Objest.Запис;

        }

        protected override void GetValue()
        {
            UnigueID = Блокнот_Objest.UnigueID;
            Caption = Назва.Text;

            Блокнот_Objest.Код = Код.Text;
            Блокнот_Objest.Назва = Назва.Text;
            Блокнот_Objest.Запис = Запис.Buffer.Text;

        }

        #endregion

        protected override void Save()
        {
            try
            {
                Блокнот_Objest.Save();
            }
            catch (Exception ex)
            {
                MsgError(ex);
            }


        }
    }
}
