

/*     
        Номенклатура_PointerControl.cs
        PointerControl (Список)
*/

using Gtk;

using AccountingSoftware;
using StorageAndTrade_1_0.Довідники;

namespace StorageAndTrade
{
    class Номенклатура_PointerControl : PointerControl
    {
        public Номенклатура_PointerControl()
        {
            pointer = new Номенклатура_Pointer();
            WidthPresentation = 300;
            Caption = $"{Номенклатура_Const.FULLNAME}:";
        }

        Номенклатура_Pointer pointer;
        public Номенклатура_Pointer Pointer
        {
            get
            {
                return pointer;
            }
            set
            {
                pointer = value;
                Presentation = (pointer != null ? pointer.GetPresentation() : "");
            }
        }

        protected override void OpenSelect(object? sender, EventArgs args)
        {
            Popover PopoverSmallSelect = new Popover((Button)sender!) { Position = PositionType.Bottom, BorderWidth = 2 };

            if (BeforeClickOpenFunc != null)
                BeforeClickOpenFunc.Invoke();

            Номенклатура_ШвидкийВибір page = new Номенклатура_ШвидкийВибір() { PopoverParent = PopoverSmallSelect, DirectoryPointerItem = Pointer.UnigueID };
            page.CallBack_OnSelectPointer = (UnigueID selectPointer) =>
            {
                Pointer = new Номенклатура_Pointer(selectPointer);

                if (AfterSelectFunc != null)
                    AfterSelectFunc.Invoke();
            };

            PopoverSmallSelect.Add(page);
            PopoverSmallSelect.ShowAll();

            page.LoadRecords();
        }

        protected override void OnClear(object? sender, EventArgs args)
        {
            Pointer = new Номенклатура_Pointer();

            if (AfterSelectFunc != null)
                AfterSelectFunc.Invoke();
        }
    }
}
    