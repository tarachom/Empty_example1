

/*     
        Користувачі_PointerControl.cs
        PointerControl
*/

using Gtk;

using AccountingSoftware;
using StorageAndTrade_1_0.Довідники;

namespace StorageAndTrade
{
    class Користувачі_PointerControl : PointerControl
    {
        public Користувачі_PointerControl()
        {
            pointer = new Користувачі_Pointer();
            WidthPresentation = 300;
            Caption = $"{Користувачі_Const.FULLNAME}:";
        }

        Користувачі_Pointer pointer;
        public Користувачі_Pointer Pointer
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

            Користувачі_ШвидкийВибір page = new Користувачі_ШвидкийВибір() { PopoverParent = PopoverSmallSelect, DirectoryPointerItem = Pointer.UnigueID };
            page.CallBack_OnSelectPointer = (UnigueID selectPointer) =>
            {
                Pointer = new Користувачі_Pointer(selectPointer);

                if (AfterSelectFunc != null)
                    AfterSelectFunc.Invoke();
            };

            PopoverSmallSelect.Add(page);
            PopoverSmallSelect.ShowAll();

            page.LoadRecords();
        }

        protected override void OnClear(object? sender, EventArgs args)
        {
            Pointer = new Користувачі_Pointer();

            if (AfterSelectFunc != null)
                AfterSelectFunc.Invoke();
        }
    }
}
    