

/*     
        Блокнот_PointerControl.cs
        PointerControl
*/

using Gtk;

using AccountingSoftware;
using StorageAndTrade_1_0.Довідники;

namespace StorageAndTrade
{
    class Блокнот_PointerControl : PointerControl
    {
        public Блокнот_PointerControl()
        {
            pointer = new Блокнот_Pointer();
            WidthPresentation = 300;
            Caption = $"{Блокнот_Const.FULLNAME}:";
        }

        Блокнот_Pointer pointer;
        public Блокнот_Pointer Pointer
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

            Блокнот_ШвидкийВибір page = new Блокнот_ШвидкийВибір() { PopoverParent = PopoverSmallSelect, DirectoryPointerItem = Pointer.UnigueID };
            page.CallBack_OnSelectPointer = (UnigueID selectPointer) =>
            {
                Pointer = new Блокнот_Pointer(selectPointer);

                if (AfterSelectFunc != null)
                    AfterSelectFunc.Invoke();
            };

            PopoverSmallSelect.Add(page);
            PopoverSmallSelect.ShowAll();

            page.LoadRecords();
        }

        protected override void OnClear(object? sender, EventArgs args)
        {
            Pointer = new Блокнот_Pointer();

            if (AfterSelectFunc != null)
                AfterSelectFunc.Invoke();
        }
    }
}
    