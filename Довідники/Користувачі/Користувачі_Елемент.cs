

/*
        Користувачі_Елемент.cs
        Елемент
*/

using Gtk;

using StorageAndTrade_1_0.Довідники;
using StorageAndTrade_1_0.Документи;
using StorageAndTrade_1_0.Перелічення;

namespace StorageAndTrade
{
    class Користувачі_Елемент : ДовідникЕлемент
    {
        public Користувачі_Objest Користувачі_Objest { get; set; } = new Користувачі_Objest();

        #region Fields

        Entry Код = new Entry() { WidthRequest = 500 };

        Entry Назва = new Entry() { WidthRequest = 500 };

        Entry Коментар = new Entry() { WidthRequest = 500 };

        CheckButton Заблокований = new CheckButton("Заблокований");

        #endregion

        #region TabularParts

        #endregion

        public Користувачі_Елемент() : base()
        {

        }

        protected override void CreatePack1(VBox vBox)
        {

            CreateField(vBox, "Код:", Код);

            CreateField(vBox, "Назва:", Назва);

            CreateField(vBox, "Коментар:", Коментар);

            CreateField(vBox, null, Заблокований);

        }

        protected override void CreatePack2(VBox vBox)
        {

        }

        #region Присвоєння / зчитування значень

        public override void SetValue()
        {
            if (IsNew)
                Користувачі_Objest.New();

            Код.Text = Користувачі_Objest.Код;
            Назва.Text = Користувачі_Objest.Назва;

            Коментар.Text = Користувачі_Objest.Коментар;
            Заблокований.Active = Користувачі_Objest.Заблокований;

        }

        protected override void GetValue()
        {
            UnigueID = Користувачі_Objest.UnigueID;
            Caption = Назва.Text;

            Користувачі_Objest.Код = Код.Text;
            Користувачі_Objest.Назва = Назва.Text;

            Користувачі_Objest.Коментар = Коментар.Text;
            Користувачі_Objest.Заблокований = Заблокований.Active;

        }

        #endregion

        protected override void Save()
        {
            try
            {
                Користувачі_Objest.Save();
            }
            catch (Exception ex)
            {
                MsgError(ex);
            }


        }
    }
}
