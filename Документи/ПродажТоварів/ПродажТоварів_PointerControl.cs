

/*     
        ПродажТоварів_PointerControl.cs
        PointerControl
*/

using AccountingSoftware;
using StorageAndTrade_1_0.Документи;

namespace StorageAndTrade
{
    class ПродажТоварів_PointerControl : PointerControl
    {
        public ПродажТоварів_PointerControl()
        {
            pointer = new ПродажТоварів_Pointer();
            WidthPresentation = 300;
            Caption = $"{ПродажТоварів_Const.FULLNAME}:";
        }

        ПродажТоварів_Pointer pointer;
        public ПродажТоварів_Pointer Pointer
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
            ПродажТоварів page = new ПродажТоварів();

            page.DocumentPointerItem = Pointer.UnigueID;
            page.CallBack_OnSelectPointer = (UnigueID selectPointer) =>
            {
                Pointer = new ПродажТоварів_Pointer(selectPointer);
            };

            Program.GeneralForm?.CreateNotebookPage($"Вибір - {ПродажТоварів_Const.FULLNAME}", () => { return page; }, true);

            if (UseWherePeriod)
                page.SetValue();
            else
                page.LoadRecords();
        }

        protected override void OnClear(object? sender, EventArgs args)
        {
            Pointer = new ПродажТоварів_Pointer();
        }
    }
}
    