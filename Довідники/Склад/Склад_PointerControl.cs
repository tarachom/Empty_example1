

/*     
        Склад_PointerControl.cs
        PointerControl (Список)
*/

using Gtk;

using AccountingSoftware;
using StorageAndTrade_1_0.Довідники;

namespace StorageAndTrade
{
    class Склад_PointerControl : PointerControl
    {
        public Склад_PointerControl()
        {
            pointer = new Склад_Pointer();
            WidthPresentation = 300;
            Caption = $"{Склад_Const.FULLNAME}:";
        }

        Склад_Pointer pointer;
        public Склад_Pointer Pointer
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

            Склад_ШвидкийВибір page = new Склад_ШвидкийВибір() { PopoverParent = PopoverSmallSelect, DirectoryPointerItem = Pointer.UnigueID };
            page.CallBack_OnSelectPointer = (UnigueID selectPointer) =>
            {
                Pointer = new Склад_Pointer(selectPointer);

                if (AfterSelectFunc != null)
                    AfterSelectFunc.Invoke();
            };

            PopoverSmallSelect.Add(page);
            PopoverSmallSelect.ShowAll();

            page.LoadRecords();
        }

        protected override void OnClear(object? sender, EventArgs args)
        {
            Pointer = new Склад_Pointer();

            if (AfterSelectFunc != null)
                AfterSelectFunc.Invoke();
        }
    }
}
    