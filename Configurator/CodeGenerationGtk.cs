
/*
Copyright (C) 2019-2023 TARAKHOMYN YURIY IVANOVYCH
All rights reserved.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

/*
Автор:    Тарахомин Юрій Іванович
Адреса:   Україна, м. Львів
Сайт:     accounting.org.ua
*/
  
/*
 *
 * Конфігурації "Нова конфігурація"
 * Автор 
  
 * Дата конфігурації: 26.05.2023 13:45:25
 *
 *
 * Цей код згенерований в Конфігураторі 3. Шаблон Gtk.xslt
 *
 */

using Gtk;
using AccountingSoftware;

namespace StorageAndTrade_1_0.Довідники.ТабличніСписки
{
    
    #region DIRECTORY "Користувачі"
    
      
    /* ТАБЛИЦЯ */
    public class Користувачі_Записи
    {
        string Image 
        {
            get
            {
                return AppContext.BaseDirectory + "images/" + (DeletionLabel ? "doc_delete.png" : "doc.png");
            }
        }

        bool DeletionLabel = false;
        string ID = "";
        
        string Код = "";
        string Назва = "";

        Array ToArray()
        {
            return new object[] { new Gdk.Pixbuf(Image), ID
            /* */ , Код, Назва };
        }

        public static ListStore Store = new ListStore(typeof(Gdk.Pixbuf) /* Image */, typeof(string) /* ID */
            , typeof(string) /* Код */
            , typeof(string) /* Назва */
            );

        public static void AddColumns(TreeView treeView)
        {
            treeView.AppendColumn(new TreeViewColumn("", new CellRendererPixbuf(), "pixbuf", 0)); /* { Ypad = 4 } */
            treeView.AppendColumn(new TreeViewColumn("ID", new CellRendererText(), "text", 1) { Visible = false });
            /* */
            treeView.AppendColumn(new TreeViewColumn("Код", new CellRendererText() { Xpad = 4 }, "text", 2) { MinWidth = 20, Resizable = true, SortColumnId = 2 } ); /*Код*/
            treeView.AppendColumn(new TreeViewColumn("Назва", new CellRendererText() { Xpad = 4 }, "text", 3) { MinWidth = 20, Resizable = true, SortColumnId = 3 } ); /*Назва*/
            
            //Пустишка
            treeView.AppendColumn(new TreeViewColumn());
        }

        public static List<Where> Where { get; set; } = new List<Where>();

        public static UnigueID? DirectoryPointerItem { get; set; }
        public static UnigueID? SelectPointerItem { get; set; }
        public static TreePath? FirstPath;
        public static TreePath? SelectPath;
        public static TreePath? CurrentPath;

        public static void LoadRecords()
        {
            Store.Clear();
            FirstPath = SelectPath = CurrentPath = null;

            Довідники.Користувачі_Select Користувачі_Select = new Довідники.Користувачі_Select();
            Користувачі_Select.QuerySelect.Field.AddRange(
                new string[]
                { "deletion_label" /*Помітка на видалення*/
                    , Довідники.Користувачі_Const.Код /* 1 */
                    , Довідники.Користувачі_Const.Назва /* 2 */
                    
                });

            /* Where */
            Користувачі_Select.QuerySelect.Where = Where;

            
              /* ORDER */
              Користувачі_Select.QuerySelect.Order.Add(Довідники.Користувачі_Const.Назва, SelectOrder.ASC);
            

            /* SELECT */
            Користувачі_Select.Select();
            while (Користувачі_Select.MoveNext())
            {
                Довідники.Користувачі_Pointer? cur = Користувачі_Select.Current;

                if (cur != null)
                {
                    Користувачі_Записи Record = new Користувачі_Записи
                    {
                        ID = cur.UnigueID.ToString(),
                        DeletionLabel = (bool)cur.Fields?["deletion_label"]!, /*Помітка на видалення*/
                        Код = cur.Fields?[Користувачі_Const.Код]?.ToString() ?? "", /**/
                        Назва = cur.Fields?[Користувачі_Const.Назва]?.ToString() ?? "" /**/
                        
                    };

                    TreeIter CurrentIter = Store.AppendValues(Record.ToArray());
                    CurrentPath = Store.GetPath(CurrentIter);

                    if (FirstPath == null)
                        FirstPath = CurrentPath;

                    if (DirectoryPointerItem != null || SelectPointerItem != null)
                    {
                        string UidSelect = SelectPointerItem != null ? SelectPointerItem.ToString() : DirectoryPointerItem!.ToString();

                        if (Record.ID == UidSelect)
                            SelectPath = CurrentPath;
                    }
                }
            }
        }
    }
	    
    /* ТАБЛИЦЯ */
    public class Користувачі_ЗаписиШвидкийВибір
    {
        string Image 
        {
            get
            {
                return AppContext.BaseDirectory + "images/" + (DeletionLabel ? "doc_delete.png" : "doc.png");
            }
        }

        bool DeletionLabel = false;
        string ID = "";
        
        string Код = "";
        string Назва = "";

        Array ToArray()
        {
            return new object[] { new Gdk.Pixbuf(Image), ID
            /* */ , Код, Назва };
        }

        public static ListStore Store = new ListStore(typeof(Gdk.Pixbuf) /* Image */, typeof(string) /* ID */
            , typeof(string) /* Код */
            , typeof(string) /* Назва */
            );

        public static void AddColumns(TreeView treeView)
        {
            treeView.AppendColumn(new TreeViewColumn("", new CellRendererPixbuf(), "pixbuf", 0)); /* { Ypad = 4 } */
            treeView.AppendColumn(new TreeViewColumn("ID", new CellRendererText(), "text", 1) { Visible = false });
            /* */
            treeView.AppendColumn(new TreeViewColumn("Код", new CellRendererText() { Xpad = 4 }, "text", 2) { MinWidth = 20, Resizable = true, SortColumnId = 2 } ); /*Код*/
            treeView.AppendColumn(new TreeViewColumn("Назва", new CellRendererText() { Xpad = 4 }, "text", 3) { MinWidth = 20, Resizable = true, SortColumnId = 3 } ); /*Назва*/
            
            //Пустишка
            treeView.AppendColumn(new TreeViewColumn());
        }

        public static List<Where> Where { get; set; } = new List<Where>();

        public static UnigueID? DirectoryPointerItem { get; set; }
        public static UnigueID? SelectPointerItem { get; set; }
        public static TreePath? FirstPath;
        public static TreePath? SelectPath;
        public static TreePath? CurrentPath;

        public static void LoadRecords()
        {
            Store.Clear();
            FirstPath = SelectPath = CurrentPath = null;

            Довідники.Користувачі_Select Користувачі_Select = new Довідники.Користувачі_Select();
            Користувачі_Select.QuerySelect.Field.AddRange(
                new string[]
                { "deletion_label" /*Помітка на видалення*/
                    , Довідники.Користувачі_Const.Код /* 1 */
                    , Довідники.Користувачі_Const.Назва /* 2 */
                    
                });

            /* Where */
            Користувачі_Select.QuerySelect.Where = Where;

            
              /* ORDER */
              Користувачі_Select.QuerySelect.Order.Add(Довідники.Користувачі_Const.Назва, SelectOrder.ASC);
            

            /* SELECT */
            Користувачі_Select.Select();
            while (Користувачі_Select.MoveNext())
            {
                Довідники.Користувачі_Pointer? cur = Користувачі_Select.Current;

                if (cur != null)
                {
                    Користувачі_ЗаписиШвидкийВибір Record = new Користувачі_ЗаписиШвидкийВибір
                    {
                        ID = cur.UnigueID.ToString(),
                        DeletionLabel = (bool)cur.Fields?["deletion_label"]!, /*Помітка на видалення*/
                        Код = cur.Fields?[Користувачі_Const.Код]?.ToString() ?? "", /**/
                        Назва = cur.Fields?[Користувачі_Const.Назва]?.ToString() ?? "" /**/
                        
                    };

                    TreeIter CurrentIter = Store.AppendValues(Record.ToArray());
                    CurrentPath = Store.GetPath(CurrentIter);

                    if (FirstPath == null)
                        FirstPath = CurrentPath;

                    if (DirectoryPointerItem != null || SelectPointerItem != null)
                    {
                        string UidSelect = SelectPointerItem != null ? SelectPointerItem.ToString() : DirectoryPointerItem!.ToString();

                        if (Record.ID == UidSelect)
                            SelectPath = CurrentPath;
                    }
                }
            }
        }
    }
	    
    #endregion
    
    #region DIRECTORY "Блокнот"
    
      
    /* ТАБЛИЦЯ */
    public class Блокнот_Записи
    {
        string Image 
        {
            get
            {
                return AppContext.BaseDirectory + "images/" + (DeletionLabel ? "doc_delete.png" : "doc.png");
            }
        }

        bool DeletionLabel = false;
        string ID = "";
        
        string Код = "";
        string Назва = "";

        Array ToArray()
        {
            return new object[] { new Gdk.Pixbuf(Image), ID
            /* */ , Код, Назва };
        }

        public static ListStore Store = new ListStore(typeof(Gdk.Pixbuf) /* Image */, typeof(string) /* ID */
            , typeof(string) /* Код */
            , typeof(string) /* Назва */
            );

        public static void AddColumns(TreeView treeView)
        {
            treeView.AppendColumn(new TreeViewColumn("", new CellRendererPixbuf(), "pixbuf", 0)); /* { Ypad = 4 } */
            treeView.AppendColumn(new TreeViewColumn("ID", new CellRendererText(), "text", 1) { Visible = false });
            /* */
            treeView.AppendColumn(new TreeViewColumn("Код", new CellRendererText() { Xpad = 4 }, "text", 2) { MinWidth = 20, Resizable = true, SortColumnId = 2 } ); /*Код*/
            treeView.AppendColumn(new TreeViewColumn("Назва", new CellRendererText() { Xpad = 4 }, "text", 3) { MinWidth = 20, Resizable = true, SortColumnId = 3 } ); /*Назва*/
            
            //Пустишка
            treeView.AppendColumn(new TreeViewColumn());
        }

        public static List<Where> Where { get; set; } = new List<Where>();

        public static UnigueID? DirectoryPointerItem { get; set; }
        public static UnigueID? SelectPointerItem { get; set; }
        public static TreePath? FirstPath;
        public static TreePath? SelectPath;
        public static TreePath? CurrentPath;

        public static void LoadRecords()
        {
            Store.Clear();
            FirstPath = SelectPath = CurrentPath = null;

            Довідники.Блокнот_Select Блокнот_Select = new Довідники.Блокнот_Select();
            Блокнот_Select.QuerySelect.Field.AddRange(
                new string[]
                { "deletion_label" /*Помітка на видалення*/
                    , Довідники.Блокнот_Const.Код /* 1 */
                    , Довідники.Блокнот_Const.Назва /* 2 */
                    
                });

            /* Where */
            Блокнот_Select.QuerySelect.Where = Where;

            
              /* ORDER */
              Блокнот_Select.QuerySelect.Order.Add(Довідники.Блокнот_Const.Код, SelectOrder.ASC);
            

            /* SELECT */
            Блокнот_Select.Select();
            while (Блокнот_Select.MoveNext())
            {
                Довідники.Блокнот_Pointer? cur = Блокнот_Select.Current;

                if (cur != null)
                {
                    Блокнот_Записи Record = new Блокнот_Записи
                    {
                        ID = cur.UnigueID.ToString(),
                        DeletionLabel = (bool)cur.Fields?["deletion_label"]!, /*Помітка на видалення*/
                        Код = cur.Fields?[Блокнот_Const.Код]?.ToString() ?? "", /**/
                        Назва = cur.Fields?[Блокнот_Const.Назва]?.ToString() ?? "" /**/
                        
                    };

                    TreeIter CurrentIter = Store.AppendValues(Record.ToArray());
                    CurrentPath = Store.GetPath(CurrentIter);

                    if (FirstPath == null)
                        FirstPath = CurrentPath;

                    if (DirectoryPointerItem != null || SelectPointerItem != null)
                    {
                        string UidSelect = SelectPointerItem != null ? SelectPointerItem.ToString() : DirectoryPointerItem!.ToString();

                        if (Record.ID == UidSelect)
                            SelectPath = CurrentPath;
                    }
                }
            }
        }
    }
	    
    /* ТАБЛИЦЯ */
    public class Блокнот_ЗаписиШвидкийВибір
    {
        string Image 
        {
            get
            {
                return AppContext.BaseDirectory + "images/" + (DeletionLabel ? "doc_delete.png" : "doc.png");
            }
        }

        bool DeletionLabel = false;
        string ID = "";
        
        string Код = "";
        string Назва = "";

        Array ToArray()
        {
            return new object[] { new Gdk.Pixbuf(Image), ID
            /* */ , Код, Назва };
        }

        public static ListStore Store = new ListStore(typeof(Gdk.Pixbuf) /* Image */, typeof(string) /* ID */
            , typeof(string) /* Код */
            , typeof(string) /* Назва */
            );

        public static void AddColumns(TreeView treeView)
        {
            treeView.AppendColumn(new TreeViewColumn("", new CellRendererPixbuf(), "pixbuf", 0)); /* { Ypad = 4 } */
            treeView.AppendColumn(new TreeViewColumn("ID", new CellRendererText(), "text", 1) { Visible = false });
            /* */
            treeView.AppendColumn(new TreeViewColumn("Код", new CellRendererText() { Xpad = 4 }, "text", 2) { MinWidth = 20, Resizable = true, SortColumnId = 2 } ); /*Код*/
            treeView.AppendColumn(new TreeViewColumn("Назва", new CellRendererText() { Xpad = 4 }, "text", 3) { MinWidth = 20, Resizable = true, SortColumnId = 3 } ); /*Назва*/
            
            //Пустишка
            treeView.AppendColumn(new TreeViewColumn());
        }

        public static List<Where> Where { get; set; } = new List<Where>();

        public static UnigueID? DirectoryPointerItem { get; set; }
        public static UnigueID? SelectPointerItem { get; set; }
        public static TreePath? FirstPath;
        public static TreePath? SelectPath;
        public static TreePath? CurrentPath;

        public static void LoadRecords()
        {
            Store.Clear();
            FirstPath = SelectPath = CurrentPath = null;

            Довідники.Блокнот_Select Блокнот_Select = new Довідники.Блокнот_Select();
            Блокнот_Select.QuerySelect.Field.AddRange(
                new string[]
                { "deletion_label" /*Помітка на видалення*/
                    , Довідники.Блокнот_Const.Код /* 1 */
                    , Довідники.Блокнот_Const.Назва /* 2 */
                    
                });

            /* Where */
            Блокнот_Select.QuerySelect.Where = Where;

            

            /* SELECT */
            Блокнот_Select.Select();
            while (Блокнот_Select.MoveNext())
            {
                Довідники.Блокнот_Pointer? cur = Блокнот_Select.Current;

                if (cur != null)
                {
                    Блокнот_ЗаписиШвидкийВибір Record = new Блокнот_ЗаписиШвидкийВибір
                    {
                        ID = cur.UnigueID.ToString(),
                        DeletionLabel = (bool)cur.Fields?["deletion_label"]!, /*Помітка на видалення*/
                        Код = cur.Fields?[Блокнот_Const.Код]?.ToString() ?? "", /**/
                        Назва = cur.Fields?[Блокнот_Const.Назва]?.ToString() ?? "" /**/
                        
                    };

                    TreeIter CurrentIter = Store.AppendValues(Record.ToArray());
                    CurrentPath = Store.GetPath(CurrentIter);

                    if (FirstPath == null)
                        FirstPath = CurrentPath;

                    if (DirectoryPointerItem != null || SelectPointerItem != null)
                    {
                        string UidSelect = SelectPointerItem != null ? SelectPointerItem.ToString() : DirectoryPointerItem!.ToString();

                        if (Record.ID == UidSelect)
                            SelectPath = CurrentPath;
                    }
                }
            }
        }
    }
	    
    #endregion
    
    #region DIRECTORY "Номенклатура"
    
      
    /* ТАБЛИЦЯ */
    public class Номенклатура_Записи
    {
        string Image 
        {
            get
            {
                return AppContext.BaseDirectory + "images/" + (DeletionLabel ? "doc_delete.png" : "doc.png");
            }
        }

        bool DeletionLabel = false;
        string ID = "";
        
        string Код = "";
        string Назва = "";

        Array ToArray()
        {
            return new object[] { new Gdk.Pixbuf(Image), ID
            /* */ , Код, Назва };
        }

        public static ListStore Store = new ListStore(typeof(Gdk.Pixbuf) /* Image */, typeof(string) /* ID */
            , typeof(string) /* Код */
            , typeof(string) /* Назва */
            );

        public static void AddColumns(TreeView treeView)
        {
            treeView.AppendColumn(new TreeViewColumn("", new CellRendererPixbuf(), "pixbuf", 0)); /* { Ypad = 4 } */
            treeView.AppendColumn(new TreeViewColumn("ID", new CellRendererText(), "text", 1) { Visible = false });
            /* */
            treeView.AppendColumn(new TreeViewColumn("Код", new CellRendererText() { Xpad = 4 }, "text", 2) { MinWidth = 20, Resizable = true, SortColumnId = 2 } ); /*Код*/
            treeView.AppendColumn(new TreeViewColumn("Назва", new CellRendererText() { Xpad = 4 }, "text", 3) { MinWidth = 20, Resizable = true, SortColumnId = 3 } ); /*Назва*/
            
            //Пустишка
            treeView.AppendColumn(new TreeViewColumn());
        }

        public static List<Where> Where { get; set; } = new List<Where>();

        public static UnigueID? DirectoryPointerItem { get; set; }
        public static UnigueID? SelectPointerItem { get; set; }
        public static TreePath? FirstPath;
        public static TreePath? SelectPath;
        public static TreePath? CurrentPath;

        public static void LoadRecords()
        {
            Store.Clear();
            FirstPath = SelectPath = CurrentPath = null;

            Довідники.Номенклатура_Select Номенклатура_Select = new Довідники.Номенклатура_Select();
            Номенклатура_Select.QuerySelect.Field.AddRange(
                new string[]
                { "deletion_label" /*Помітка на видалення*/
                    , Довідники.Номенклатура_Const.Код /* 1 */
                    , Довідники.Номенклатура_Const.Назва /* 2 */
                    
                });

            /* Where */
            Номенклатура_Select.QuerySelect.Where = Where;

            

            /* SELECT */
            Номенклатура_Select.Select();
            while (Номенклатура_Select.MoveNext())
            {
                Довідники.Номенклатура_Pointer? cur = Номенклатура_Select.Current;

                if (cur != null)
                {
                    Номенклатура_Записи Record = new Номенклатура_Записи
                    {
                        ID = cur.UnigueID.ToString(),
                        DeletionLabel = (bool)cur.Fields?["deletion_label"]!, /*Помітка на видалення*/
                        Код = cur.Fields?[Номенклатура_Const.Код]?.ToString() ?? "", /**/
                        Назва = cur.Fields?[Номенклатура_Const.Назва]?.ToString() ?? "" /**/
                        
                    };

                    TreeIter CurrentIter = Store.AppendValues(Record.ToArray());
                    CurrentPath = Store.GetPath(CurrentIter);

                    if (FirstPath == null)
                        FirstPath = CurrentPath;

                    if (DirectoryPointerItem != null || SelectPointerItem != null)
                    {
                        string UidSelect = SelectPointerItem != null ? SelectPointerItem.ToString() : DirectoryPointerItem!.ToString();

                        if (Record.ID == UidSelect)
                            SelectPath = CurrentPath;
                    }
                }
            }
        }
    }
	    
    /* ТАБЛИЦЯ */
    public class Номенклатура_ЗаписиШвидкийВибір
    {
        string Image 
        {
            get
            {
                return AppContext.BaseDirectory + "images/" + (DeletionLabel ? "doc_delete.png" : "doc.png");
            }
        }

        bool DeletionLabel = false;
        string ID = "";
        
        string Код = "";
        string Назва = "";

        Array ToArray()
        {
            return new object[] { new Gdk.Pixbuf(Image), ID
            /* */ , Код, Назва };
        }

        public static ListStore Store = new ListStore(typeof(Gdk.Pixbuf) /* Image */, typeof(string) /* ID */
            , typeof(string) /* Код */
            , typeof(string) /* Назва */
            );

        public static void AddColumns(TreeView treeView)
        {
            treeView.AppendColumn(new TreeViewColumn("", new CellRendererPixbuf(), "pixbuf", 0)); /* { Ypad = 4 } */
            treeView.AppendColumn(new TreeViewColumn("ID", new CellRendererText(), "text", 1) { Visible = false });
            /* */
            treeView.AppendColumn(new TreeViewColumn("Код", new CellRendererText() { Xpad = 4 }, "text", 2) { MinWidth = 20, Resizable = true, SortColumnId = 2 } ); /*Код*/
            treeView.AppendColumn(new TreeViewColumn("Назва", new CellRendererText() { Xpad = 4 }, "text", 3) { MinWidth = 20, Resizable = true, SortColumnId = 3 } ); /*Назва*/
            
            //Пустишка
            treeView.AppendColumn(new TreeViewColumn());
        }

        public static List<Where> Where { get; set; } = new List<Where>();

        public static UnigueID? DirectoryPointerItem { get; set; }
        public static UnigueID? SelectPointerItem { get; set; }
        public static TreePath? FirstPath;
        public static TreePath? SelectPath;
        public static TreePath? CurrentPath;

        public static void LoadRecords()
        {
            Store.Clear();
            FirstPath = SelectPath = CurrentPath = null;

            Довідники.Номенклатура_Select Номенклатура_Select = new Довідники.Номенклатура_Select();
            Номенклатура_Select.QuerySelect.Field.AddRange(
                new string[]
                { "deletion_label" /*Помітка на видалення*/
                    , Довідники.Номенклатура_Const.Код /* 1 */
                    , Довідники.Номенклатура_Const.Назва /* 2 */
                    
                });

            /* Where */
            Номенклатура_Select.QuerySelect.Where = Where;

            

            /* SELECT */
            Номенклатура_Select.Select();
            while (Номенклатура_Select.MoveNext())
            {
                Довідники.Номенклатура_Pointer? cur = Номенклатура_Select.Current;

                if (cur != null)
                {
                    Номенклатура_ЗаписиШвидкийВибір Record = new Номенклатура_ЗаписиШвидкийВибір
                    {
                        ID = cur.UnigueID.ToString(),
                        DeletionLabel = (bool)cur.Fields?["deletion_label"]!, /*Помітка на видалення*/
                        Код = cur.Fields?[Номенклатура_Const.Код]?.ToString() ?? "", /**/
                        Назва = cur.Fields?[Номенклатура_Const.Назва]?.ToString() ?? "" /**/
                        
                    };

                    TreeIter CurrentIter = Store.AppendValues(Record.ToArray());
                    CurrentPath = Store.GetPath(CurrentIter);

                    if (FirstPath == null)
                        FirstPath = CurrentPath;

                    if (DirectoryPointerItem != null || SelectPointerItem != null)
                    {
                        string UidSelect = SelectPointerItem != null ? SelectPointerItem.ToString() : DirectoryPointerItem!.ToString();

                        if (Record.ID == UidSelect)
                            SelectPath = CurrentPath;
                    }
                }
            }
        }
    }
	    
    #endregion
    
    #region DIRECTORY "Склад"
    
      
    /* ТАБЛИЦЯ */
    public class Склад_Записи
    {
        string Image 
        {
            get
            {
                return AppContext.BaseDirectory + "images/" + (DeletionLabel ? "doc_delete.png" : "doc.png");
            }
        }

        bool DeletionLabel = false;
        string ID = "";
        
        string Код = "";
        string Назва = "";

        Array ToArray()
        {
            return new object[] { new Gdk.Pixbuf(Image), ID
            /* */ , Код, Назва };
        }

        public static ListStore Store = new ListStore(typeof(Gdk.Pixbuf) /* Image */, typeof(string) /* ID */
            , typeof(string) /* Код */
            , typeof(string) /* Назва */
            );

        public static void AddColumns(TreeView treeView)
        {
            treeView.AppendColumn(new TreeViewColumn("", new CellRendererPixbuf(), "pixbuf", 0)); /* { Ypad = 4 } */
            treeView.AppendColumn(new TreeViewColumn("ID", new CellRendererText(), "text", 1) { Visible = false });
            /* */
            treeView.AppendColumn(new TreeViewColumn("Код", new CellRendererText() { Xpad = 4 }, "text", 2) { MinWidth = 20, Resizable = true, SortColumnId = 2 } ); /*Код*/
            treeView.AppendColumn(new TreeViewColumn("Назва", new CellRendererText() { Xpad = 4 }, "text", 3) { MinWidth = 20, Resizable = true, SortColumnId = 3 } ); /*Назва*/
            
            //Пустишка
            treeView.AppendColumn(new TreeViewColumn());
        }

        public static List<Where> Where { get; set; } = new List<Where>();

        public static UnigueID? DirectoryPointerItem { get; set; }
        public static UnigueID? SelectPointerItem { get; set; }
        public static TreePath? FirstPath;
        public static TreePath? SelectPath;
        public static TreePath? CurrentPath;

        public static void LoadRecords()
        {
            Store.Clear();
            FirstPath = SelectPath = CurrentPath = null;

            Довідники.Склад_Select Склад_Select = new Довідники.Склад_Select();
            Склад_Select.QuerySelect.Field.AddRange(
                new string[]
                { "deletion_label" /*Помітка на видалення*/
                    , Довідники.Склад_Const.Код /* 1 */
                    , Довідники.Склад_Const.Назва /* 2 */
                    
                });

            /* Where */
            Склад_Select.QuerySelect.Where = Where;

            

            /* SELECT */
            Склад_Select.Select();
            while (Склад_Select.MoveNext())
            {
                Довідники.Склад_Pointer? cur = Склад_Select.Current;

                if (cur != null)
                {
                    Склад_Записи Record = new Склад_Записи
                    {
                        ID = cur.UnigueID.ToString(),
                        DeletionLabel = (bool)cur.Fields?["deletion_label"]!, /*Помітка на видалення*/
                        Код = cur.Fields?[Склад_Const.Код]?.ToString() ?? "", /**/
                        Назва = cur.Fields?[Склад_Const.Назва]?.ToString() ?? "" /**/
                        
                    };

                    TreeIter CurrentIter = Store.AppendValues(Record.ToArray());
                    CurrentPath = Store.GetPath(CurrentIter);

                    if (FirstPath == null)
                        FirstPath = CurrentPath;

                    if (DirectoryPointerItem != null || SelectPointerItem != null)
                    {
                        string UidSelect = SelectPointerItem != null ? SelectPointerItem.ToString() : DirectoryPointerItem!.ToString();

                        if (Record.ID == UidSelect)
                            SelectPath = CurrentPath;
                    }
                }
            }
        }
    }
	    
    /* ТАБЛИЦЯ */
    public class Склад_ЗаписиШвидкийВибір
    {
        string Image 
        {
            get
            {
                return AppContext.BaseDirectory + "images/" + (DeletionLabel ? "doc_delete.png" : "doc.png");
            }
        }

        bool DeletionLabel = false;
        string ID = "";
        
        string Код = "";
        string Назва = "";

        Array ToArray()
        {
            return new object[] { new Gdk.Pixbuf(Image), ID
            /* */ , Код, Назва };
        }

        public static ListStore Store = new ListStore(typeof(Gdk.Pixbuf) /* Image */, typeof(string) /* ID */
            , typeof(string) /* Код */
            , typeof(string) /* Назва */
            );

        public static void AddColumns(TreeView treeView)
        {
            treeView.AppendColumn(new TreeViewColumn("", new CellRendererPixbuf(), "pixbuf", 0)); /* { Ypad = 4 } */
            treeView.AppendColumn(new TreeViewColumn("ID", new CellRendererText(), "text", 1) { Visible = false });
            /* */
            treeView.AppendColumn(new TreeViewColumn("Код", new CellRendererText() { Xpad = 4 }, "text", 2) { MinWidth = 20, Resizable = true, SortColumnId = 2 } ); /*Код*/
            treeView.AppendColumn(new TreeViewColumn("Назва", new CellRendererText() { Xpad = 4 }, "text", 3) { MinWidth = 20, Resizable = true, SortColumnId = 3 } ); /*Назва*/
            
            //Пустишка
            treeView.AppendColumn(new TreeViewColumn());
        }

        public static List<Where> Where { get; set; } = new List<Where>();

        public static UnigueID? DirectoryPointerItem { get; set; }
        public static UnigueID? SelectPointerItem { get; set; }
        public static TreePath? FirstPath;
        public static TreePath? SelectPath;
        public static TreePath? CurrentPath;

        public static void LoadRecords()
        {
            Store.Clear();
            FirstPath = SelectPath = CurrentPath = null;

            Довідники.Склад_Select Склад_Select = new Довідники.Склад_Select();
            Склад_Select.QuerySelect.Field.AddRange(
                new string[]
                { "deletion_label" /*Помітка на видалення*/
                    , Довідники.Склад_Const.Код /* 1 */
                    , Довідники.Склад_Const.Назва /* 2 */
                    
                });

            /* Where */
            Склад_Select.QuerySelect.Where = Where;

            

            /* SELECT */
            Склад_Select.Select();
            while (Склад_Select.MoveNext())
            {
                Довідники.Склад_Pointer? cur = Склад_Select.Current;

                if (cur != null)
                {
                    Склад_ЗаписиШвидкийВибір Record = new Склад_ЗаписиШвидкийВибір
                    {
                        ID = cur.UnigueID.ToString(),
                        DeletionLabel = (bool)cur.Fields?["deletion_label"]!, /*Помітка на видалення*/
                        Код = cur.Fields?[Склад_Const.Код]?.ToString() ?? "", /**/
                        Назва = cur.Fields?[Склад_Const.Назва]?.ToString() ?? "" /**/
                        
                    };

                    TreeIter CurrentIter = Store.AppendValues(Record.ToArray());
                    CurrentPath = Store.GetPath(CurrentIter);

                    if (FirstPath == null)
                        FirstPath = CurrentPath;

                    if (DirectoryPointerItem != null || SelectPointerItem != null)
                    {
                        string UidSelect = SelectPointerItem != null ? SelectPointerItem.ToString() : DirectoryPointerItem!.ToString();

                        if (Record.ID == UidSelect)
                            SelectPath = CurrentPath;
                    }
                }
            }
        }
    }
	    
    #endregion
    
}

namespace StorageAndTrade_1_0.Документи.ТабличніСписки
{
    public static class Інтерфейс
    {
        public static ComboBoxText СписокВідбірПоПеріоду()
        {
            ComboBoxText сomboBox = new ComboBoxText();

            if (Config.Kernel != null)
            {
                ConfigurationEnums ТипПеріодуДляЖурналівДокументів = Config.Kernel.Conf.Enums["ТипПеріодуДляЖурналівДокументів"];

                foreach (ConfigurationEnumField field in ТипПеріодуДляЖурналівДокументів.Fields.Values)
                    сomboBox.Append(field.Name, field.Desc);
            }

            /*сomboBox.Active = 0;*/

            return сomboBox;
        }

        public static void ДодатиВідбірПоПеріоду(List<Where> Where, string fieldWhere, Перелічення.ТипПеріодуДляЖурналівДокументів типПеріоду)
        {
            switch (типПеріоду)
            {
                case Перелічення.ТипПеріодуДляЖурналівДокументів.ЗПочаткуРоку:
                {
                    Where.Add(new Where(fieldWhere, Comparison.QT_EQ, new DateTime(DateTime.Now.Year, 1, 1)));
                    break;
                }
                case Перелічення.ТипПеріодуДляЖурналівДокументів.Квартал:
                {
                    DateTime ДатаТриМісцяНазад = DateTime.Now.AddMonths(-3);
                    Where.Add(new Where(fieldWhere, Comparison.QT_EQ, new DateTime(ДатаТриМісцяНазад.Year, ДатаТриМісцяНазад.Month, 1)));
                    break;
                }
                case Перелічення.ТипПеріодуДляЖурналівДокументів.ЗМинулогоМісяця:
                {
                    DateTime ДатаМісцьНазад = DateTime.Now.AddMonths(-1);
                    Where.Add(new Where(fieldWhere, Comparison.QT_EQ, new DateTime(ДатаМісцьНазад.Year, ДатаМісцьНазад.Month, 1)));
                    break;
                }
                case Перелічення.ТипПеріодуДляЖурналівДокументів.Місяць:
                {
                    Where.Add(new Where(fieldWhere, Comparison.QT_EQ, DateTime.Now.AddMonths(-1)));
                    break;
                }
                case Перелічення.ТипПеріодуДляЖурналівДокументів.ЗПочаткуМісяця:
                {
                    Where.Add(new Where(fieldWhere, Comparison.QT_EQ, new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)));
                    break;
                }
                case Перелічення.ТипПеріодуДляЖурналівДокументів.ЗПочаткуТижня:
                {
                    DateTime СімДнівНазад = DateTime.Now.AddDays(-7);
                    Where.Add(new Where(fieldWhere, Comparison.QT_EQ, new DateTime(СімДнівНазад.Year, СімДнівНазад.Month, СімДнівНазад.Day)));
                    break;
                }
                case Перелічення.ТипПеріодуДляЖурналівДокументів.ПоточнийДень:
                {
                    Where.Add(new Where(fieldWhere, Comparison.QT_EQ, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)));
                    break;
                }
            }
        }
    }

    
    #region DOCUMENT "ПоступленняТоварів"
    
      
    public class ПоступленняТоварів_Записи
    {
        string Image 
        {
            get
            {
                return AppContext.BaseDirectory + "images/" + (DeletionLabel ? "doc_delete.png" : "doc.png");
            }
        }

        bool DeletionLabel = false;
        bool Spend = false;
        string ID = "";
        
        string Назва = "";
        string ДатаДок = "";
        string НомерДок = "";
        string Коментар = "";
        string Номенклатура = "";
        string Кількість = "";
        string Сума = "";
        string Склад = "";
        string Автор = "";

        Array ToArray()
        {
            return new object[] { new Gdk.Pixbuf(Image), ID, Spend /*Проведений документ*/
            /* */ , Назва, ДатаДок, НомерДок, Коментар, Номенклатура, Кількість, Сума, Склад, Автор };
        }

        public static ListStore Store = new ListStore(typeof(Gdk.Pixbuf) /* Image */, typeof(string) /* ID */, typeof(bool) /* Spend Проведений документ*/
            , typeof(string) /* Назва */
            , typeof(string) /* ДатаДок */
            , typeof(string) /* НомерДок */
            , typeof(string) /* Коментар */
            , typeof(string) /* Номенклатура */
            , typeof(string) /* Кількість */
            , typeof(string) /* Сума */
            , typeof(string) /* Склад */
            , typeof(string) /* Автор */
            );

        public static void AddColumns(TreeView treeView)
        {
            treeView.AppendColumn(new TreeViewColumn("", new CellRendererPixbuf(), "pixbuf", 0)); /*Image*/ /* { Ypad = 0 } */
            treeView.AppendColumn(new TreeViewColumn("ID", new CellRendererText(), "text", 1) { Visible = false }); /*UID*/
            treeView.AppendColumn(new TreeViewColumn("", new CellRendererToggle(), "active", 2)); /*Проведений документ*/
            /* */
            treeView.AppendColumn(new TreeViewColumn("Назва", new CellRendererText() { Xpad = 4 }, "text", 3) { MinWidth = 20, Resizable = true } ); /*Назва*/
            treeView.AppendColumn(new TreeViewColumn("ДатаДок", new CellRendererText() { Xpad = 4 }, "text", 4) { MinWidth = 20, Resizable = true } ); /*ДатаДок*/
            treeView.AppendColumn(new TreeViewColumn("НомерДок", new CellRendererText() { Xpad = 4 }, "text", 5) { MinWidth = 20, Resizable = true } ); /*НомерДок*/
            treeView.AppendColumn(new TreeViewColumn("Коментар", new CellRendererText() { Xpad = 4 }, "text", 6) { MinWidth = 20, Resizable = true } ); /*Коментар*/
            treeView.AppendColumn(new TreeViewColumn("Номенклатура", new CellRendererText() { Xpad = 4 }, "text", 7) { MinWidth = 20, Resizable = true } ); /*Номенклатура*/
            treeView.AppendColumn(new TreeViewColumn("Кількість", new CellRendererText() { Xpad = 4 }, "text", 8) { MinWidth = 20, Resizable = true } ); /*Кількість*/
            treeView.AppendColumn(new TreeViewColumn("Сума", new CellRendererText() { Xpad = 4 }, "text", 9) { MinWidth = 20, Resizable = true } ); /*Сума*/
            treeView.AppendColumn(new TreeViewColumn("Склад", new CellRendererText() { Xpad = 4 }, "text", 10) { MinWidth = 20, Resizable = true } ); /*Склад*/
            treeView.AppendColumn(new TreeViewColumn("Автор", new CellRendererText() { Xpad = 4 }, "text", 11) { MinWidth = 20, Resizable = true } ); /*Автор*/
            
            //Пустишка
            treeView.AppendColumn(new TreeViewColumn());
        }

        public static List<Where> Where { get; set; } = new List<Where>();

        public static void ДодатиВідбірПоПеріоду(Перелічення.ТипПеріодуДляЖурналівДокументів типПеріоду)
        {
            Where.Clear();
            Інтерфейс.ДодатиВідбірПоПеріоду(Where, Документи.ПоступленняТоварів_Const.ДатаДок, типПеріоду);
        }

        public static UnigueID? DocumentPointerItem { get; set; }
        public static UnigueID? SelectPointerItem { get; set; }
        public static TreePath? FirstPath;
        public static TreePath? SelectPath;
        public static TreePath? CurrentPath;

        public static void LoadRecords()
        {
            Store.Clear();
            FirstPath = SelectPath = CurrentPath = null;

            Документи.ПоступленняТоварів_Select ПоступленняТоварів_Select = new Документи.ПоступленняТоварів_Select();
            ПоступленняТоварів_Select.QuerySelect.Field.AddRange(
                new string[]
                { "deletion_label" /*Помітка на видалення*/,
                  "spend" /*Проведений документ*/
                    , Документи.ПоступленняТоварів_Const.Назва /* 1 */
                    , Документи.ПоступленняТоварів_Const.ДатаДок /* 2 */
                    , Документи.ПоступленняТоварів_Const.НомерДок /* 3 */
                    , Документи.ПоступленняТоварів_Const.Коментар /* 4 */
                    , Документи.ПоступленняТоварів_Const.Кількість /* 5 */
                    , Документи.ПоступленняТоварів_Const.Сума /* 6 */
                    
                });

            /* Where */
            ПоступленняТоварів_Select.QuerySelect.Where = Where;

            
              /* ORDER */
              ПоступленняТоварів_Select.QuerySelect.Order.Add(Документи.ПоступленняТоварів_Const.ДатаДок, SelectOrder.ASC);
            
                /* Join Table */
                ПоступленняТоварів_Select.QuerySelect.Joins.Add(
                    new Join(Довідники.Номенклатура_Const.TABLE, Документи.ПоступленняТоварів_Const.Номенклатура, ПоступленняТоварів_Select.QuerySelect.Table, "join_tab_1"));
                
                  /* Field */
                  ПоступленняТоварів_Select.QuerySelect.FieldAndAlias.Add(
                    new NameValue<string>("join_tab_1." + Довідники.Номенклатура_Const.Назва, "join_tab_1_field_1"));
                  
                /* Join Table */
                ПоступленняТоварів_Select.QuerySelect.Joins.Add(
                    new Join(Довідники.Склад_Const.TABLE, Документи.ПоступленняТоварів_Const.Склад, ПоступленняТоварів_Select.QuerySelect.Table, "join_tab_2"));
                
                  /* Field */
                  ПоступленняТоварів_Select.QuerySelect.FieldAndAlias.Add(
                    new NameValue<string>("join_tab_2." + Довідники.Склад_Const.Назва, "join_tab_2_field_1"));
                  
                /* Join Table */
                ПоступленняТоварів_Select.QuerySelect.Joins.Add(
                    new Join(Довідники.Користувачі_Const.TABLE, Документи.ПоступленняТоварів_Const.Автор, ПоступленняТоварів_Select.QuerySelect.Table, "join_tab_3"));
                
                  /* Field */
                  ПоступленняТоварів_Select.QuerySelect.FieldAndAlias.Add(
                    new NameValue<string>("join_tab_3." + Довідники.Користувачі_Const.Назва, "join_tab_3_field_1"));
                  

            /* SELECT */
            ПоступленняТоварів_Select.Select();
            while (ПоступленняТоварів_Select.MoveNext())
            {
                Документи.ПоступленняТоварів_Pointer? cur = ПоступленняТоварів_Select.Current;

                if (cur != null)
                {
                    ПоступленняТоварів_Записи Record = new ПоступленняТоварів_Записи
                    {
                        ID = cur.UnigueID.ToString(),
                        Spend = (bool)cur.Fields?["spend"]!, /*Проведений документ*/
                        DeletionLabel = (bool)cur.Fields?["deletion_label"]!, /*Помітка на видалення*/
                        Номенклатура = cur.Fields?["join_tab_1_field_1"]?.ToString() ?? "", /**/
                        Склад = cur.Fields?["join_tab_2_field_1"]?.ToString() ?? "", /**/
                        Автор = cur.Fields?["join_tab_3_field_1"]?.ToString() ?? "", /**/
                        Назва = cur.Fields?[ПоступленняТоварів_Const.Назва]?.ToString() ?? "", /**/
                        ДатаДок = cur.Fields?[ПоступленняТоварів_Const.ДатаДок]?.ToString() ?? "", /**/
                        НомерДок = cur.Fields?[ПоступленняТоварів_Const.НомерДок]?.ToString() ?? "", /**/
                        Коментар = cur.Fields?[ПоступленняТоварів_Const.Коментар]?.ToString() ?? "", /**/
                        Кількість = cur.Fields?[ПоступленняТоварів_Const.Кількість]?.ToString() ?? "", /**/
                        Сума = cur.Fields?[ПоступленняТоварів_Const.Сума]?.ToString() ?? "" /**/
                        
                    };

                    TreeIter CurrentIter = Store.AppendValues(Record.ToArray());
                    CurrentPath = Store.GetPath(CurrentIter);

                    if (FirstPath == null)
                        FirstPath = CurrentPath;

                    if (DocumentPointerItem != null || SelectPointerItem != null)
                    {
                        string UidSelect = SelectPointerItem != null ? SelectPointerItem.ToString() : DocumentPointerItem!.ToString();

                        if (Record.ID == UidSelect)
                            SelectPath = CurrentPath;
                    }
                }
            }
        }
    }
	    
    #endregion
    
    #region DOCUMENT "ПродажТоварів"
    
      
    public class ПродажТоварів_Записи
    {
        string Image 
        {
            get
            {
                return AppContext.BaseDirectory + "images/" + (DeletionLabel ? "doc_delete.png" : "doc.png");
            }
        }

        bool DeletionLabel = false;
        bool Spend = false;
        string ID = "";
        
        string Назва = "";
        string ДатаДок = "";
        string НомерДок = "";
        string Коментар = "";
        string Номенклатура = "";
        string Кількість = "";
        string Сума = "";
        string Склад = "";
        string Автор = "";

        Array ToArray()
        {
            return new object[] { new Gdk.Pixbuf(Image), ID, Spend /*Проведений документ*/
            /* */ , Назва, ДатаДок, НомерДок, Коментар, Номенклатура, Кількість, Сума, Склад, Автор };
        }

        public static ListStore Store = new ListStore(typeof(Gdk.Pixbuf) /* Image */, typeof(string) /* ID */, typeof(bool) /* Spend Проведений документ*/
            , typeof(string) /* Назва */
            , typeof(string) /* ДатаДок */
            , typeof(string) /* НомерДок */
            , typeof(string) /* Коментар */
            , typeof(string) /* Номенклатура */
            , typeof(string) /* Кількість */
            , typeof(string) /* Сума */
            , typeof(string) /* Склад */
            , typeof(string) /* Автор */
            );

        public static void AddColumns(TreeView treeView)
        {
            treeView.AppendColumn(new TreeViewColumn("", new CellRendererPixbuf(), "pixbuf", 0)); /*Image*/ /* { Ypad = 0 } */
            treeView.AppendColumn(new TreeViewColumn("ID", new CellRendererText(), "text", 1) { Visible = false }); /*UID*/
            treeView.AppendColumn(new TreeViewColumn("", new CellRendererToggle(), "active", 2)); /*Проведений документ*/
            /* */
            treeView.AppendColumn(new TreeViewColumn("Назва", new CellRendererText() { Xpad = 4 }, "text", 3) { MinWidth = 20, Resizable = true } ); /*Назва*/
            treeView.AppendColumn(new TreeViewColumn("ДатаДок", new CellRendererText() { Xpad = 4 }, "text", 4) { MinWidth = 20, Resizable = true } ); /*ДатаДок*/
            treeView.AppendColumn(new TreeViewColumn("НомерДок", new CellRendererText() { Xpad = 4 }, "text", 5) { MinWidth = 20, Resizable = true } ); /*НомерДок*/
            treeView.AppendColumn(new TreeViewColumn("Коментар", new CellRendererText() { Xpad = 4 }, "text", 6) { MinWidth = 20, Resizable = true } ); /*Коментар*/
            treeView.AppendColumn(new TreeViewColumn("Номенклатура", new CellRendererText() { Xpad = 4 }, "text", 7) { MinWidth = 20, Resizable = true } ); /*Номенклатура*/
            treeView.AppendColumn(new TreeViewColumn("Кількість", new CellRendererText() { Xpad = 4 }, "text", 8) { MinWidth = 20, Resizable = true } ); /*Кількість*/
            treeView.AppendColumn(new TreeViewColumn("Сума", new CellRendererText() { Xpad = 4 }, "text", 9) { MinWidth = 20, Resizable = true } ); /*Сума*/
            treeView.AppendColumn(new TreeViewColumn("Склад", new CellRendererText() { Xpad = 4 }, "text", 10) { MinWidth = 20, Resizable = true } ); /*Склад*/
            treeView.AppendColumn(new TreeViewColumn("Автор", new CellRendererText() { Xpad = 4 }, "text", 11) { MinWidth = 20, Resizable = true } ); /*Автор*/
            
            //Пустишка
            treeView.AppendColumn(new TreeViewColumn());
        }

        public static List<Where> Where { get; set; } = new List<Where>();

        public static void ДодатиВідбірПоПеріоду(Перелічення.ТипПеріодуДляЖурналівДокументів типПеріоду)
        {
            Where.Clear();
            Інтерфейс.ДодатиВідбірПоПеріоду(Where, Документи.ПродажТоварів_Const.ДатаДок, типПеріоду);
        }

        public static UnigueID? DocumentPointerItem { get; set; }
        public static UnigueID? SelectPointerItem { get; set; }
        public static TreePath? FirstPath;
        public static TreePath? SelectPath;
        public static TreePath? CurrentPath;

        public static void LoadRecords()
        {
            Store.Clear();
            FirstPath = SelectPath = CurrentPath = null;

            Документи.ПродажТоварів_Select ПродажТоварів_Select = new Документи.ПродажТоварів_Select();
            ПродажТоварів_Select.QuerySelect.Field.AddRange(
                new string[]
                { "deletion_label" /*Помітка на видалення*/,
                  "spend" /*Проведений документ*/
                    , Документи.ПродажТоварів_Const.Назва /* 1 */
                    , Документи.ПродажТоварів_Const.ДатаДок /* 2 */
                    , Документи.ПродажТоварів_Const.НомерДок /* 3 */
                    , Документи.ПродажТоварів_Const.Коментар /* 4 */
                    , Документи.ПродажТоварів_Const.Кількість /* 5 */
                    , Документи.ПродажТоварів_Const.Сума /* 6 */
                    
                });

            /* Where */
            ПродажТоварів_Select.QuerySelect.Where = Where;

            
              /* ORDER */
              ПродажТоварів_Select.QuerySelect.Order.Add(Документи.ПродажТоварів_Const.ДатаДок, SelectOrder.ASC);
            
                /* Join Table */
                ПродажТоварів_Select.QuerySelect.Joins.Add(
                    new Join(Довідники.Номенклатура_Const.TABLE, Документи.ПродажТоварів_Const.Номенклатура, ПродажТоварів_Select.QuerySelect.Table, "join_tab_1"));
                
                  /* Field */
                  ПродажТоварів_Select.QuerySelect.FieldAndAlias.Add(
                    new NameValue<string>("join_tab_1." + Довідники.Номенклатура_Const.Назва, "join_tab_1_field_1"));
                  
                /* Join Table */
                ПродажТоварів_Select.QuerySelect.Joins.Add(
                    new Join(Довідники.Склад_Const.TABLE, Документи.ПродажТоварів_Const.Склад, ПродажТоварів_Select.QuerySelect.Table, "join_tab_2"));
                
                  /* Field */
                  ПродажТоварів_Select.QuerySelect.FieldAndAlias.Add(
                    new NameValue<string>("join_tab_2." + Довідники.Склад_Const.Назва, "join_tab_2_field_1"));
                  
                /* Join Table */
                ПродажТоварів_Select.QuerySelect.Joins.Add(
                    new Join(Довідники.Користувачі_Const.TABLE, Документи.ПродажТоварів_Const.Автор, ПродажТоварів_Select.QuerySelect.Table, "join_tab_3"));
                
                  /* Field */
                  ПродажТоварів_Select.QuerySelect.FieldAndAlias.Add(
                    new NameValue<string>("join_tab_3." + Довідники.Користувачі_Const.Назва, "join_tab_3_field_1"));
                  

            /* SELECT */
            ПродажТоварів_Select.Select();
            while (ПродажТоварів_Select.MoveNext())
            {
                Документи.ПродажТоварів_Pointer? cur = ПродажТоварів_Select.Current;

                if (cur != null)
                {
                    ПродажТоварів_Записи Record = new ПродажТоварів_Записи
                    {
                        ID = cur.UnigueID.ToString(),
                        Spend = (bool)cur.Fields?["spend"]!, /*Проведений документ*/
                        DeletionLabel = (bool)cur.Fields?["deletion_label"]!, /*Помітка на видалення*/
                        Номенклатура = cur.Fields?["join_tab_1_field_1"]?.ToString() ?? "", /**/
                        Склад = cur.Fields?["join_tab_2_field_1"]?.ToString() ?? "", /**/
                        Автор = cur.Fields?["join_tab_3_field_1"]?.ToString() ?? "", /**/
                        Назва = cur.Fields?[ПродажТоварів_Const.Назва]?.ToString() ?? "", /**/
                        ДатаДок = cur.Fields?[ПродажТоварів_Const.ДатаДок]?.ToString() ?? "", /**/
                        НомерДок = cur.Fields?[ПродажТоварів_Const.НомерДок]?.ToString() ?? "", /**/
                        Коментар = cur.Fields?[ПродажТоварів_Const.Коментар]?.ToString() ?? "", /**/
                        Кількість = cur.Fields?[ПродажТоварів_Const.Кількість]?.ToString() ?? "", /**/
                        Сума = cur.Fields?[ПродажТоварів_Const.Сума]?.ToString() ?? "" /**/
                        
                    };

                    TreeIter CurrentIter = Store.AppendValues(Record.ToArray());
                    CurrentPath = Store.GetPath(CurrentIter);

                    if (FirstPath == null)
                        FirstPath = CurrentPath;

                    if (DocumentPointerItem != null || SelectPointerItem != null)
                    {
                        string UidSelect = SelectPointerItem != null ? SelectPointerItem.ToString() : DocumentPointerItem!.ToString();

                        if (Record.ID == UidSelect)
                            SelectPath = CurrentPath;
                    }
                }
            }
        }
    }
	    
    #endregion
    

    //
    // Журнали
    //

    
    #region JOURNAL "Повний"
    
    public class Журнали_Повний
    {
        string Image 
        {
            get
            {
                return AppContext.BaseDirectory + "images/" + (DeletionLabel ? "doc_delete.png" : "doc.png");
            }
        }

        bool DeletionLabel = false;
        bool Spend = false;
        string ID = "";
        string Type = ""; //Тип документу
        
        string Назва = "";
        string Дата = "";
        string Номер = "";
        string Коментар = "";
        string Автор = "";

        // Масив для запису стрічки в Store
        Array ToArray()
        {
            return new object[] { new Gdk.Pixbuf(Image), ID, Type, Spend /*Проведений документ*/
            /* */ , Назва, Дата, Номер, Коментар, Автор };
        }

        // Джерело даних для списку
        public static ListStore Store = new ListStore(
          typeof(Gdk.Pixbuf) /* Image */, 
          typeof(string) /* ID */, 
          typeof(string) /* Type */, 
          typeof(bool) /* Spend Проведений документ*/
            , typeof(string) /* Назва */
            , typeof(string) /* Дата */
            , typeof(string) /* Номер */
            , typeof(string) /* Коментар */
            , typeof(string) /* Автор */
            );

        // Добавлення колонок в список
        public static void AddColumns(TreeView treeView)
        {
            treeView.AppendColumn(new TreeViewColumn("", new CellRendererPixbuf(), "pixbuf", 0)); /*Image*/ /* { Ypad = 0 } */
            treeView.AppendColumn(new TreeViewColumn("ID", new CellRendererText(), "text", 1) { Visible = false }); /*UID*/
            treeView.AppendColumn(new TreeViewColumn("Type", new CellRendererText(), "text", 2) { Visible = false }); /*Type*/
            treeView.AppendColumn(new TreeViewColumn("", new CellRendererToggle(), "active", 3)); /*Проведений документ*/
            /* */
            treeView.AppendColumn(new TreeViewColumn("Назва", new CellRendererText() { Xpad = 4 }, "text", 4) { MinWidth = 20, Resizable = true } ); /*Назва*/
            treeView.AppendColumn(new TreeViewColumn("Дата", new CellRendererText() { Xpad = 4 }, "text", 5) { MinWidth = 20, Resizable = true } ); /*Дата*/
            treeView.AppendColumn(new TreeViewColumn("Номер", new CellRendererText() { Xpad = 4 }, "text", 6) { MinWidth = 20, Resizable = true } ); /*Номер*/
            treeView.AppendColumn(new TreeViewColumn("Коментар", new CellRendererText() { Xpad = 4 }, "text", 7) { MinWidth = 20, Resizable = true } ); /*Коментар*/
            treeView.AppendColumn(new TreeViewColumn("Автор", new CellRendererText() { Xpad = 4 }, "text", 8) { MinWidth = 20, Resizable = true } ); /*Автор*/
            
            //Пустишка
            treeView.AppendColumn(new TreeViewColumn());
        }

        // Словник з відборами, ключ це Тип документу
        public static Dictionary<string, List<Where>> Where { get; set; } = new Dictionary<string, List<Where>>();

        // Добавляє відбір по періоду в словник відборів
        public static void ДодатиВідбірПоПеріоду(Перелічення.ТипПеріодуДляЖурналівДокументів типПеріоду)
        {
            Where.Clear();
            
            {
                List<Where> where = new List<Where>();
                Where.Add("ПоступленняТоварів", where);
                Інтерфейс.ДодатиВідбірПоПеріоду(where, ПоступленняТоварів_Const.ДатаДок, типПеріоду);
            }
              
            {
                List<Where> where = new List<Where>();
                Where.Add("ПродажТоварів", where);
                Інтерфейс.ДодатиВідбірПоПеріоду(where, ПродажТоварів_Const.ДатаДок, типПеріоду);
            }
              
        }

        // Список документів які входять в журнал
        public static Dictionary<string, string> AllowDocument()
        {
            Dictionary<string, string> allowDoc = new Dictionary<string, string>();
            allowDoc.Add("ПоступленняТоварів", "ПоступленняТоварів");
            allowDoc.Add("ПродажТоварів", "ПродажТоварів");
            
            return allowDoc;
        }

        public static UnigueID? SelectPointerItem { get; set; }
        public static TreePath? SelectPath;
        public static TreePath? CurrentPath;

        // Завантаження даних
        public static void LoadRecords()
        {
            Store.Clear();
            SelectPath = CurrentPath = null;
            List<string> allQuery = new List<string>();
            Dictionary<string, object> paramQuery = new Dictionary<string, object>();

          
              {
                  Query query = new Query(Документи.ПоступленняТоварів_Const.TABLE);

                  // Встановлення відбору для даного типу документу
                  if (Where.ContainsKey("ПоступленняТоварів") && Where["ПоступленняТоварів"].Count != 0) {
                      query.Where = Where["ПоступленняТоварів"];
                      foreach(Where field in query.Where)
                          paramQuery.Add(field.Alias, field.Value);
                  }

                  query.FieldAndAlias.Add(new NameValue<string>("'ПоступленняТоварів'", "type"));
                  query.Field.Add("deletion_label");
                  query.Field.Add("spend");
                  
                              query.FieldAndAlias.Add(
                                  new NameValue<string>(Документи.ПоступленняТоварів_Const.TABLE + "." + Документи.ПоступленняТоварів_Const.Назва, "Назва"));
                            
                              query.FieldAndAlias.Add(
                                  new NameValue<string>(Документи.ПоступленняТоварів_Const.TABLE + "." + Документи.ПоступленняТоварів_Const.ДатаДок, "Дата"));
                            
                              query.FieldAndAlias.Add(
                                  new NameValue<string>(Документи.ПоступленняТоварів_Const.TABLE + "." + Документи.ПоступленняТоварів_Const.НомерДок, "Номер"));
                            
                              query.FieldAndAlias.Add(
                                  new NameValue<string>(Документи.ПоступленняТоварів_Const.TABLE + "." + Документи.ПоступленняТоварів_Const.Коментар, "Коментар"));
                            
                              /* Join Table */
                              query.Joins.Add(
                                  new Join(Довідники.Користувачі_Const.TABLE, Документи.ПоступленняТоварів_Const.Автор, query.Table, "join_tab_1"));
                              
                                /* Field */
                                query.FieldAndAlias.Add(
                                  new NameValue<string>("join_tab_1." + Довідники.Користувачі_Const.Назва, "Автор"));
                              

                  allQuery.Add(query.Construct());
              }
              
              {
                  Query query = new Query(Документи.ПродажТоварів_Const.TABLE);

                  // Встановлення відбору для даного типу документу
                  if (Where.ContainsKey("ПродажТоварів") && Where["ПродажТоварів"].Count != 0) {
                      query.Where = Where["ПродажТоварів"];
                      foreach(Where field in query.Where)
                          paramQuery.Add(field.Alias, field.Value);
                  }

                  query.FieldAndAlias.Add(new NameValue<string>("'ПродажТоварів'", "type"));
                  query.Field.Add("deletion_label");
                  query.Field.Add("spend");
                  
                              query.FieldAndAlias.Add(
                                  new NameValue<string>(Документи.ПродажТоварів_Const.TABLE + "." + Документи.ПродажТоварів_Const.Назва, "Назва"));
                            
                              query.FieldAndAlias.Add(
                                  new NameValue<string>(Документи.ПродажТоварів_Const.TABLE + "." + Документи.ПродажТоварів_Const.ДатаДок, "Дата"));
                            
                              query.FieldAndAlias.Add(
                                  new NameValue<string>(Документи.ПродажТоварів_Const.TABLE + "." + Документи.ПродажТоварів_Const.НомерДок, "Номер"));
                            
                              query.FieldAndAlias.Add(
                                  new NameValue<string>(Документи.ПродажТоварів_Const.TABLE + "." + Документи.ПродажТоварів_Const.Коментар, "Коментар"));
                            
                              /* Join Table */
                              query.Joins.Add(
                                  new Join(Довідники.Користувачі_Const.TABLE, Документи.ПродажТоварів_Const.Автор, query.Table, "join_tab_1"));
                              
                                /* Field */
                                query.FieldAndAlias.Add(
                                  new NameValue<string>("join_tab_1." + Довідники.Користувачі_Const.Назва, "Автор"));
                              

                  allQuery.Add(query.Construct());
              }
              

            string unionAllQuery = string.Join("\nUNION\n", allQuery);

            unionAllQuery += "\nORDER BY Дата";
          
            string[] columnsName;
            List<Dictionary<string, object>> listRow;

            Config.Kernel!.DataBase.SelectRequest(unionAllQuery, paramQuery, out columnsName, out listRow);

            foreach (Dictionary<string, object> row in listRow)
            {
                Журнали_Повний record = new Журнали_Повний();
                record.ID = row["uid"]?.ToString() ?? "";
                record.Type = row["type"]?.ToString() ?? "";
                record.DeletionLabel = (bool)row["deletion_label"];
                record.Spend = (bool)row["spend"];
                
                    record.Назва = row["Назва"] != DBNull.Value ? (row["Назва"]?.ToString() ?? "") : "";
                
                    record.Дата = row["Дата"] != DBNull.Value ? (row["Дата"]?.ToString() ?? "") : "";
                
                    record.Номер = row["Номер"] != DBNull.Value ? (row["Номер"]?.ToString() ?? "") : "";
                
                    record.Коментар = row["Коментар"] != DBNull.Value ? (row["Коментар"]?.ToString() ?? "") : "";
                
                    record.Автор = row["Автор"] != DBNull.Value ? (row["Автор"]?.ToString() ?? "") : "";
                

                TreeIter CurrentIter = Store.AppendValues(record.ToArray());
                CurrentPath = Store.GetPath(CurrentIter);

                if (SelectPointerItem != null)
                {
                    if (record.ID == SelectPointerItem.ToString())
                        SelectPath = CurrentPath;
                }
            }
          
        }
    }
    #endregion
    
}

namespace StorageAndTrade_1_0.РегістриВідомостей.ТабличніСписки
{
    
}

  