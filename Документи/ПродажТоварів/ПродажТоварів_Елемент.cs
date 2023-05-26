

/*
        ПродажТоварів_Елемент.cs
        Елемент
*/

using Gtk;

using AccountingSoftware;

using StorageAndTrade_1_0;
using StorageAndTrade_1_0.Константи;
using StorageAndTrade_1_0.Довідники;
using StorageAndTrade_1_0.Документи;
using StorageAndTrade_1_0.Перелічення;

namespace StorageAndTrade
{
    class ПродажТоварів_Елемент : ДокументЕлемент
    {
        public ПродажТоварів_Objest ПродажТоварів_Objest { get; set; } = new ПродажТоварів_Objest();

        #region Fields

        DateTimeControl ДатаДок = new DateTimeControl();

        Entry НомерДок = new Entry() { /* WidthRequest = 500 */ };

        Entry Коментар = new Entry() {  WidthRequest = 920  };
        Номенклатура_PointerControl Номенклатура = new Номенклатура_PointerControl() { Caption = "Номенклатура", WidthPresentation = 300 };
        Склад_PointerControl Склад = new Склад_PointerControl() { Caption = "Склад", WidthPresentation = 300 };

        NumericControl Кількість = new NumericControl();

        NumericControl Сума = new NumericControl();

        #endregion

        #region TabularParts

        #endregion

        public ПродажТоварів_Елемент() : base()
        {
            CreateDocName(ПродажТоварів_Const.FULLNAME, НомерДок, ДатаДок);


            CreateField(HBoxComment, "Коментар:", Коментар);


            FillComboBoxes();
        }

        void FillComboBoxes()
        {

        }

        protected override void CreateContainer1(VBox vBox)
        {
            //Номенклатура
            CreateField(vBox, null, Номенклатура);

            //Склад
            CreateField(vBox, null, Склад);
        }

        protected override void CreateContainer2(VBox vBox)
        {
            //Кількість
            CreateField(vBox, "Кількість:", Кількість);

            //Сума
            CreateField(vBox, "Сума:", Сума);
        }

        protected override void CreateContainer3(VBox vBox)
        {

        }

        protected override void CreateContainer4(VBox vBox)
        {

        }

        #region Присвоєння / зчитування значень

        public override void SetValue()
        {
            if (IsNew)
                ПродажТоварів_Objest.New();

            ДатаДок.Value = ПродажТоварів_Objest.ДатаДок;
            НомерДок.Text = ПродажТоварів_Objest.НомерДок;
            Коментар.Text = ПродажТоварів_Objest.Коментар;
            Номенклатура.Pointer = ПродажТоварів_Objest.Номенклатура;
            Кількість.Value = ПродажТоварів_Objest.Кількість;
            Сума.Value = ПродажТоварів_Objest.Сума;
            Склад.Pointer = ПродажТоварів_Objest.Склад;

        }

        protected override void GetValue()
        {
            ПродажТоварів_Objest.ДатаДок = ДатаДок.Value;
            ПродажТоварів_Objest.НомерДок = НомерДок.Text;
            ПродажТоварів_Objest.Коментар = Коментар.Text;
            ПродажТоварів_Objest.Номенклатура = Номенклатура.Pointer;
            ПродажТоварів_Objest.Кількість = Кількість.Value;
            ПродажТоварів_Objest.Сума = Сума.Value;
            ПродажТоварів_Objest.Склад = Склад.Pointer;

        }

        #endregion

        protected override bool Save()
        {
            bool isSave = false;

            try
            {
                isSave = ПродажТоварів_Objest.Save();
            }
            catch (Exception ex)
            {
                MsgError(ex);
                return false;
            }



            UnigueID = ПродажТоварів_Objest.UnigueID;
            Caption = ПродажТоварів_Objest.Назва;

            return isSave;
        }

        protected override bool SpendTheDocument(bool spendDoc)
        {
            if (spendDoc)
            {
                bool isSpend = ПродажТоварів_Objest.SpendTheDocument(ПродажТоварів_Objest.ДатаДок);

                if (!isSpend)
                    ФункціїДляПовідомлень.ВідкритиТермінал();

                return isSpend;
            }
            else
            {
                ПродажТоварів_Objest.ClearSpendTheDocument();

                return true;
            }
        }
    }
}
