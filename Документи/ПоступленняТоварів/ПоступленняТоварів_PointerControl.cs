

/*     
        ПоступленняТоварів_PointerControl.cs
        PointerControl
*/

using AccountingSoftware;
using StorageAndTrade_1_0.Документи;

namespace StorageAndTrade
{
    class ПоступленняТоварів_PointerControl : PointerControl
    {
        public ПоступленняТоварів_PointerControl()
        {
            pointer = new ПоступленняТоварів_Pointer();
            WidthPresentation = 300;
            Caption = $"{ПоступленняТоварів_Const.FULLNAME}:";
        }

        ПоступленняТоварів_Pointer pointer;
        public ПоступленняТоварів_Pointer Pointer
        {
            get
            {
                return pointer;
            }
            set
            {
                pointer = value;

                if (pointer != null)
                    Presentation = pointer.GetPresentation();
                else
                    Presentation = "";
            }
        }

        //Відбір по періоду в журналі
        public bool UseWherePeriod { get; set; } = true;

        protected override void OpenSelect(object? sender, EventArgs args)
        {
            ПоступленняТоварів page = new ПоступленняТоварів();

            page.DocumentPointerItem = Pointer.UnigueID;
            page.CallBack_OnSelectPointer = (UnigueID selectPointer) =>
            {
                Pointer = new ПоступленняТоварів_Pointer(selectPointer);
            };

            Program.GeneralForm?.CreateNotebookPage($"Вибір - {ПоступленняТоварів_Const.FULLNAME}", () => { return page; }, true);

            if (UseWherePeriod)
                page.SetValue();
            else
                page.LoadRecords();
        }

        protected override void OnClear(object? sender, EventArgs args)
        {
            Pointer = new ПоступленняТоварів_Pointer();
        }
    }
}
