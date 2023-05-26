

/*
        ПоступленняТоварів_Елемент.cs
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
    class ПоступленняТоварів_Елемент : ДокументЕлемент
    {
        public ПоступленняТоварів_Objest ПоступленняТоварів_Objest { get; set; } = new ПоступленняТоварів_Objest();

        #region Fields

        DateTimeControl ДатаДок = new DateTimeControl();

        Entry НомерДок = new Entry() { /* WidthRequest = 500 */ };

        Entry Коментар = new Entry() { WidthRequest = 920 };
        Номенклатура_PointerControl Номенклатура = new Номенклатура_PointerControl() { Caption = "Номенклатура", WidthPresentation = 300 };
        Склад_PointerControl Склад = new Склад_PointerControl() { Caption = "Склад", WidthPresentation = 300 };
        NumericControl Кількість = new NumericControl();
        NumericControl Сума = new NumericControl();
        Користувачі_PointerControl Автор = new Користувачі_PointerControl() { Caption = "Автор", WidthPresentation = 300 };
        CompositePointerControl Основа = new CompositePointerControl();

        #endregion

        #region TabularParts

        #endregion

        public ПоступленняТоварів_Елемент() : base()
        {
            CreateDocName(ПоступленняТоварів_Const.FULLNAME, НомерДок, ДатаДок);


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
            //Автор
            CreateField(vBox, null, Автор);

            //Основа
            CreateField(vBox, null, Основа);
        }

        protected override void CreateContainer4(VBox vBox)
        {

        }

        #region Присвоєння / зчитування значень

        public override void SetValue()
        {
            if (IsNew)
                ПоступленняТоварів_Objest.New();

            ДатаДок.Value = ПоступленняТоварів_Objest.ДатаДок;
            НомерДок.Text = ПоступленняТоварів_Objest.НомерДок;
            Коментар.Text = ПоступленняТоварів_Objest.Коментар;
            Номенклатура.Pointer = ПоступленняТоварів_Objest.Номенклатура;
            Склад.Pointer = ПоступленняТоварів_Objest.Склад;
            Кількість.Value = ПоступленняТоварів_Objest.Кількість;
            Сума.Value = ПоступленняТоварів_Objest.Сума;
            Автор.Pointer = ПоступленняТоварів_Objest.Автор;
            Основа.Pointer = ПоступленняТоварів_Objest.Основа;
        }

        protected override void GetValue()
        {
            ПоступленняТоварів_Objest.ДатаДок = ДатаДок.Value;
            ПоступленняТоварів_Objest.НомерДок = НомерДок.Text;
            ПоступленняТоварів_Objest.Коментар = Коментар.Text;
            ПоступленняТоварів_Objest.Номенклатура = Номенклатура.Pointer;
            ПоступленняТоварів_Objest.Склад = Склад.Pointer;
            ПоступленняТоварів_Objest.Кількість = Кількість.Value;
            ПоступленняТоварів_Objest.Сума = Сума.Value;
            ПоступленняТоварів_Objest.Автор = Автор.Pointer;
            ПоступленняТоварів_Objest.Основа = Основа.Pointer;
        }

        #endregion

        protected override bool Save()
        {
            bool isSave = false;

            try
            {
                isSave = ПоступленняТоварів_Objest.Save();
            }
            catch (Exception ex)
            {
                MsgError(ex);
                return false;
            }



            UnigueID = ПоступленняТоварів_Objest.UnigueID;
            Caption = ПоступленняТоварів_Objest.Назва;

            return isSave;
        }

        protected override bool SpendTheDocument(bool spendDoc)
        {
            if (spendDoc)
            {
                bool isSpend = ПоступленняТоварів_Objest.SpendTheDocument(ПоступленняТоварів_Objest.ДатаДок);

                if (!isSpend)
                    ФункціїДляПовідомлень.ВідкритиТермінал();

                return isSpend;
            }
            else
            {
                ПоступленняТоварів_Objest.ClearSpendTheDocument();

                return true;
            }
        }
    }
}
