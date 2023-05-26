
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
  
 * Дата конфігурації: 26.05.2023 13:59:03
 *
 *
 * Цей код згенерований в Конфігураторі 3. Шаблон CodeGeneration.xslt
 *
 */

using AccountingSoftware;
using System.Xml;

namespace StorageAndTrade_1_0
{
    public static class Config
    {
        public static Kernel? Kernel { get; set; }
		
        public static void ReadAllConstants()
        {
            Константи.Системні.ReadAll();
            Константи.ЖурналиДокументів.ReadAll();
            Константи.ПриЗапускуПрограми.ReadAll();
            Константи.НумераціяДовідників.ReadAll();
            Константи.НумераціяДокументів.ReadAll();
            Константи.ЗначенняЗаЗамовчуванням.ReadAll();
            
        }
    }

    public class Functions
    {
        /*
          Функція для типу який задається користувачем.
          Повертає презентацію для uuidAndText.
          В @pointer - повертає групу (Документи або Довідники)
            @type - повертає назву типу
        */
        public static string CompositePointerPresentation(UuidAndText uuidAndText, out string pointer, out string type)
        {
            pointer = type = "";

            if (uuidAndText.IsEmpty() || String.IsNullOrEmpty(uuidAndText.Text) || uuidAndText.Text.IndexOf(".") == -1)
                return "";

            string[] pointer_and_type = uuidAndText.Text.Split(".", StringSplitOptions.None);

            if (pointer_and_type.Length == 2)
            {
                pointer = pointer_and_type[0];
                type = pointer_and_type[1];

                if (pointer == "Документи")
                {
                    
                    switch (type)
                    {
                        
                        case "ПоступленняТоварів": return new Документи.ПоступленняТоварів_Pointer(uuidAndText.Uuid).GetPresentation();
                        
                        case "ПродажТоварів": return new Документи.ПродажТоварів_Pointer(uuidAndText.Uuid).GetPresentation();
                        
                    }
                    
                }
                else if (pointer == "Довідники")
                {
                    
                    switch (type)
                    {
                        
                        case "Користувачі": return new Довідники.Користувачі_Pointer(uuidAndText.Uuid).GetPresentation();
                        
                        case "Блокнот": return new Довідники.Блокнот_Pointer(uuidAndText.Uuid).GetPresentation();
                        
                        case "Номенклатура": return new Довідники.Номенклатура_Pointer(uuidAndText.Uuid).GetPresentation();
                        
                        case "Склад": return new Довідники.Склад_Pointer(uuidAndText.Uuid).GetPresentation();
                        
                    }
                    
                }
            }

            return "";
        }
    }
}

namespace StorageAndTrade_1_0.Константи
{
    
	  #region CONSTANTS BLOCK "Системні"
    public static class Системні
    {
        public static void ReadAll()
        {
            
            Dictionary<string, object> fieldValue = new Dictionary<string, object>();
            bool IsSelect = Config.Kernel!.DataBase.SelectAllConstants("tab_constants",
                 new string[] { "col_a2", "col_a9" }, fieldValue);
            
            if (IsSelect)
            {
                m_ЗупинитиФоновіЗадачі_Const = (fieldValue["col_a2"] != DBNull.Value) ? (bool)fieldValue["col_a2"] : false;
                m_ПовідомленняТаПомилки_Const = fieldValue["col_a9"].ToString() ?? "";
                
            }
			      
        }
        
        
        static bool m_ЗупинитиФоновіЗадачі_Const = false;
        public static bool ЗупинитиФоновіЗадачі_Const
        {
            get 
            {
                return m_ЗупинитиФоновіЗадачі_Const;
            }
            set
            {
                m_ЗупинитиФоновіЗадачі_Const = value;
                Config.Kernel!.DataBase.SaveConstants("tab_constants", "col_a2", m_ЗупинитиФоновіЗадачі_Const);
            }
        }
        
        static string m_ПовідомленняТаПомилки_Const = "";
        public static string ПовідомленняТаПомилки_Const
        {
            get 
            {
                return m_ПовідомленняТаПомилки_Const;
            }
            set
            {
                m_ПовідомленняТаПомилки_Const = value;
                Config.Kernel!.DataBase.SaveConstants("tab_constants", "col_a9", m_ПовідомленняТаПомилки_Const);
            }
        }
        
        
        public class ПовідомленняТаПомилки_Помилки_TablePart : ConstantsTablePart
        {
            public ПовідомленняТаПомилки_Помилки_TablePart() : base(Config.Kernel!, "tab_a02",
                 new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_a5", "col_a6" }) 
            {
                Records = new List<Record>();
            }
            
            public const string TABLE = "tab_a02";
            
            public const string Дата = "col_a1";
            public const string НазваПроцесу = "col_a2";
            public const string Обєкт = "col_a3";
            public const string ТипОбєкту = "col_a4";
            public const string НазваОбєкту = "col_a5";
            public const string Повідомлення = "col_a6";
            public List<Record> Records { get; set; }
        
            public void Read()
            {
                Records.Clear();
                base.BaseRead();

                foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
                {
                    Record record = new Record();
                    record.UID = (Guid)fieldValue["uid"];
                    
                    record.Дата = (fieldValue["col_a1"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a1"].ToString() ?? DateTime.MinValue.ToString()) : DateTime.MinValue;
                    record.НазваПроцесу = fieldValue["col_a2"].ToString() ?? "";
                    record.Обєкт = (fieldValue["col_a3"] != DBNull.Value) ? (Guid)fieldValue["col_a3"] : Guid.Empty;
                    record.ТипОбєкту = fieldValue["col_a4"].ToString() ?? "";
                    record.НазваОбєкту = fieldValue["col_a5"].ToString() ?? "";
                    record.Повідомлення = fieldValue["col_a6"].ToString() ?? "";
                    
                    Records.Add(record);
                }
            
                base.BaseClear();
            }
        
            public void Save(bool clear_all_before_save /*= true*/) 
            {
                base.BaseBeginTransaction();
                
                if (clear_all_before_save)
                    base.BaseDelete();

                foreach (Record record in Records)
                {
                    Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                    fieldValue.Add("col_a1", record.Дата);
                    fieldValue.Add("col_a2", record.НазваПроцесу);
                    fieldValue.Add("col_a3", record.Обєкт);
                    fieldValue.Add("col_a4", record.ТипОбєкту);
                    fieldValue.Add("col_a5", record.НазваОбєкту);
                    fieldValue.Add("col_a6", record.Повідомлення);
                    
                    record.UID = base.BaseSave(record.UID, fieldValue);
                }
                
                base.BaseCommitTransaction();
            }
        
            public void Delete()
            {
                base.BaseDelete();
            }
            
            public class Record : ConstantsTablePartRecord
            {
                public DateTime Дата { get; set; } = DateTime.MinValue;
                public string НазваПроцесу { get; set; } = "";
                public Guid Обєкт { get; set; } = new Guid();
                public string ТипОбєкту { get; set; } = "";
                public string НазваОбєкту { get; set; } = "";
                public string Повідомлення { get; set; } = "";
                
            }
        }
               
    }
    #endregion
    
	  #region CONSTANTS BLOCK "ЖурналиДокументів"
    public static class ЖурналиДокументів
    {
        public static void ReadAll()
        {
            
            Dictionary<string, object> fieldValue = new Dictionary<string, object>();
            bool IsSelect = Config.Kernel!.DataBase.SelectAllConstants("tab_constants",
                 new string[] { "col_a8" }, fieldValue);
            
            if (IsSelect)
            {
                m_ОсновнийТипПеріоду_Const = (fieldValue["col_a8"] != DBNull.Value) ? (Перелічення.ТипПеріодуДляЖурналівДокументів)fieldValue["col_a8"] : 0;
                
            }
			      
        }
        
        
        static Перелічення.ТипПеріодуДляЖурналівДокументів m_ОсновнийТипПеріоду_Const = 0;
        public static Перелічення.ТипПеріодуДляЖурналівДокументів ОсновнийТипПеріоду_Const
        {
            get 
            {
                return m_ОсновнийТипПеріоду_Const;
            }
            set
            {
                m_ОсновнийТипПеріоду_Const = value;
                Config.Kernel!.DataBase.SaveConstants("tab_constants", "col_a8", (int)m_ОсновнийТипПеріоду_Const);
            }
        }
             
    }
    #endregion
    
	  #region CONSTANTS BLOCK "ПриЗапускуПрограми"
    public static class ПриЗапускуПрограми
    {
        public static void ReadAll()
        {
            
        }
        
             
    }
    #endregion
    
	  #region CONSTANTS BLOCK "НумераціяДовідників"
    public static class НумераціяДовідників
    {
        public static void ReadAll()
        {
            
            Dictionary<string, object> fieldValue = new Dictionary<string, object>();
            bool IsSelect = Config.Kernel!.DataBase.SelectAllConstants("tab_constants",
                 new string[] { "col_a1", "col_a3", "col_a4", "col_a7" }, fieldValue);
            
            if (IsSelect)
            {
                m_Користувачі_Const = (fieldValue["col_a1"] != DBNull.Value) ? (int)fieldValue["col_a1"] : 0;
                m_Блокнот_Const = (fieldValue["col_a3"] != DBNull.Value) ? (int)fieldValue["col_a3"] : 0;
                m_Номенклатура_Const = (fieldValue["col_a4"] != DBNull.Value) ? (int)fieldValue["col_a4"] : 0;
                m_Склад_Const = (fieldValue["col_a7"] != DBNull.Value) ? (int)fieldValue["col_a7"] : 0;
                
            }
			      
        }
        
        
        static int m_Користувачі_Const = 0;
        public static int Користувачі_Const
        {
            get 
            {
                return m_Користувачі_Const;
            }
            set
            {
                m_Користувачі_Const = value;
                Config.Kernel!.DataBase.SaveConstants("tab_constants", "col_a1", m_Користувачі_Const);
            }
        }
        
        static int m_Блокнот_Const = 0;
        public static int Блокнот_Const
        {
            get 
            {
                return m_Блокнот_Const;
            }
            set
            {
                m_Блокнот_Const = value;
                Config.Kernel!.DataBase.SaveConstants("tab_constants", "col_a3", m_Блокнот_Const);
            }
        }
        
        static int m_Номенклатура_Const = 0;
        public static int Номенклатура_Const
        {
            get 
            {
                return m_Номенклатура_Const;
            }
            set
            {
                m_Номенклатура_Const = value;
                Config.Kernel!.DataBase.SaveConstants("tab_constants", "col_a4", m_Номенклатура_Const);
            }
        }
        
        static int m_Склад_Const = 0;
        public static int Склад_Const
        {
            get 
            {
                return m_Склад_Const;
            }
            set
            {
                m_Склад_Const = value;
                Config.Kernel!.DataBase.SaveConstants("tab_constants", "col_a7", m_Склад_Const);
            }
        }
             
    }
    #endregion
    
	  #region CONSTANTS BLOCK "НумераціяДокументів"
    public static class НумераціяДокументів
    {
        public static void ReadAll()
        {
            
            Dictionary<string, object> fieldValue = new Dictionary<string, object>();
            bool IsSelect = Config.Kernel!.DataBase.SelectAllConstants("tab_constants",
                 new string[] { "col_a5", "col_a6" }, fieldValue);
            
            if (IsSelect)
            {
                m_ПоступленняТоварів_Const = (fieldValue["col_a5"] != DBNull.Value) ? (int)fieldValue["col_a5"] : 0;
                m_ПродажТоварів_Const = (fieldValue["col_a6"] != DBNull.Value) ? (int)fieldValue["col_a6"] : 0;
                
            }
			      
        }
        
        
        static int m_ПоступленняТоварів_Const = 0;
        public static int ПоступленняТоварів_Const
        {
            get 
            {
                return m_ПоступленняТоварів_Const;
            }
            set
            {
                m_ПоступленняТоварів_Const = value;
                Config.Kernel!.DataBase.SaveConstants("tab_constants", "col_a5", m_ПоступленняТоварів_Const);
            }
        }
        
        static int m_ПродажТоварів_Const = 0;
        public static int ПродажТоварів_Const
        {
            get 
            {
                return m_ПродажТоварів_Const;
            }
            set
            {
                m_ПродажТоварів_Const = value;
                Config.Kernel!.DataBase.SaveConstants("tab_constants", "col_a6", m_ПродажТоварів_Const);
            }
        }
             
    }
    #endregion
    
	  #region CONSTANTS BLOCK "ЗначенняЗаЗамовчуванням"
    public static class ЗначенняЗаЗамовчуванням
    {
        public static void ReadAll()
        {
            
            Dictionary<string, object> fieldValue = new Dictionary<string, object>();
            bool IsSelect = Config.Kernel!.DataBase.SelectAllConstants("tab_constants",
                 new string[] { "col_b1" }, fieldValue);
            
            if (IsSelect)
            {
                m_ОсновнийСклад_Const = new Довідники.Склад_Pointer(fieldValue["col_b1"]);
                
            }
			      
        }
        
        
        static Довідники.Склад_Pointer m_ОсновнийСклад_Const = new Довідники.Склад_Pointer();
        public static Довідники.Склад_Pointer ОсновнийСклад_Const
        {
            get 
            {
                return m_ОсновнийСклад_Const.Copy();
            }
            set
            {
                m_ОсновнийСклад_Const = value;
                Config.Kernel!.DataBase.SaveConstants("tab_constants", "col_b1", m_ОсновнийСклад_Const.UnigueID.UGuid);
            }
        }
             
    }
    #endregion
    
}

namespace StorageAndTrade_1_0.Довідники
{
    
    #region DIRECTORY "Користувачі"
    public static class Користувачі_Const
    {
        public const string TABLE = "tab_a08";
        public const string POINTER = "Довідники.Користувачі";
        public const string FULLNAME = "Користувачі";
        public const string DELETION_LABEL = "deletion_label";
        
        public const string Код = "col_a1";
        public const string Назва = "col_a2";
        public const string КодВСпеціальнійТаблиці = "col_a3";
        public const string Коментар = "col_a4";
        public const string Заблокований = "col_a5";
    }

    public class Користувачі_Objest : DirectoryObject
    {
        public Користувачі_Objest() : base(Config.Kernel!, "tab_a08",
             new string[] { "col_a1", "col_a2", "col_a3", "col_a4", "col_a5" }) 
        {
            Код = "";
            Назва = "";
            КодВСпеціальнійТаблиці = new Guid();
            Коментар = "";
            Заблокований = false;
            
        }
        
        public void New()
        {
            BaseNew();
            Користувачі_Triggers.New(this);
            
        }

        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Код = base.FieldValue["col_a1"].ToString() ?? "";
                Назва = base.FieldValue["col_a2"].ToString() ?? "";
                КодВСпеціальнійТаблиці = (base.FieldValue["col_a3"] != DBNull.Value) ? (Guid)base.FieldValue["col_a3"] : Guid.Empty;
                Коментар = base.FieldValue["col_a4"].ToString() ?? "";
                Заблокований = (base.FieldValue["col_a5"] != DBNull.Value) ? (bool)base.FieldValue["col_a5"] : false;
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public bool Save()
        {
            Користувачі_Triggers.BeforeSave(this);
            base.FieldValue["col_a1"] = Код;
            base.FieldValue["col_a2"] = Назва;
            base.FieldValue["col_a3"] = КодВСпеціальнійТаблиці;
            base.FieldValue["col_a4"] = Коментар;
            base.FieldValue["col_a5"] = Заблокований;
            
            bool result = BaseSave();
            if (result)
            {
                Користувачі_Triggers.AfterSave(this);
                BaseWriteFullTextSearch(GetBasis(), new string[] {  });
            }
            return result;
        }

        public Користувачі_Objest Copy(bool copyTableParts = false)
        {
            Користувачі_Objest copy = new Користувачі_Objest();
            copy.Код = Код;
            copy.Назва = Назва;
            copy.КодВСпеціальнійТаблиці = КодВСпеціальнійТаблиці;
            copy.Коментар = Коментар;
            copy.Заблокований = Заблокований;
            

            copy.New();
            Користувачі_Triggers.Copying(copy, this);
            return copy;
        }

        public void SetDeletionLabel(bool label = true)
        {
            Користувачі_Triggers.SetDeletionLabel(this, label);
            base.BaseDeletionLabel(label);
        }

        public void Delete()
        {
            Користувачі_Triggers.BeforeDelete(this);
            base.BaseDelete(new string[] {  });
        }
        
        public Користувачі_Pointer GetDirectoryPointer()
        {
            return new Користувачі_Pointer(UnigueID.UGuid);
        }

        public UuidAndText GetBasis()
        {
            return new UuidAndText(UnigueID.UGuid, Користувачі_Const.POINTER);
        }

        public string GetPresentation()
        {
            return base.BasePresentation(
                new string[] { "col_a2" }
            );
        }
        
        public string Код { get; set; }
        public string Назва { get; set; }
        public Guid КодВСпеціальнійТаблиці { get; set; }
        public string Коментар { get; set; }
        public bool Заблокований { get; set; }
        
    }

    public class Користувачі_Pointer : DirectoryPointer
    {
        public Користувачі_Pointer(object? uid = null) : base(Config.Kernel!, "tab_a08")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Користувачі_Pointer(UnigueID uid, Dictionary<string, object>? fields = null) : base(Config.Kernel!, "tab_a08")
        {
            base.Init(uid, fields);
        }
        
        public Користувачі_Objest? GetDirectoryObject()
        {
            if (this.IsEmpty()) return null;
            Користувачі_Objest КористувачіObjestItem = new Користувачі_Objest();
            return КористувачіObjestItem.Read(base.UnigueID) ? КористувачіObjestItem : null;
        }

        public Користувачі_Pointer Copy()
        {
            return new Користувачі_Pointer(base.UnigueID, base.Fields) { Назва = Назва };
        }

        public string Назва { get; set; } = "";

        public string GetPresentation()
        {
            return Назва = base.BasePresentation(
                new string[] { "col_a2" }
            );
        }

        public void SetDeletionLabel(bool label = true)
        {
            Користувачі_Objest? obj = GetDirectoryObject();
            if (obj != null)
            {
                Користувачі_Triggers.SetDeletionLabel(obj, label);
                
                base.BaseDeletionLabel(label);
            }
        }
		
        public Користувачі_Pointer GetEmptyPointer()
        {
            return new Користувачі_Pointer();
        }

        public UuidAndText GetBasis()
        {
            return new UuidAndText(UnigueID.UGuid, Користувачі_Const.POINTER);
        }

        public void Clear()
        {
            Init(new UnigueID(), null);
            Назва = "";
        }
    }
    
    public class Користувачі_Select : DirectorySelect
    {
        public Користувачі_Select() : base(Config.Kernel!, "tab_a08") { }        
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Користувачі_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Користувачі_Pointer? Current { get; private set; }
        
        public Користувачі_Pointer FindByField(string name, object value)
        {
            Користувачі_Pointer itemPointer = new Користувачі_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(name, value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Користувачі_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Користувачі_Pointer> directoryPointerList = new List<Користувачі_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(name, value, limit, offset)) 
                directoryPointerList.Add(new Користувачі_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
   
    #endregion
    
    #region DIRECTORY "Блокнот"
    public static class Блокнот_Const
    {
        public const string TABLE = "tab_a01";
        public const string POINTER = "Довідники.Блокнот";
        public const string FULLNAME = "Блокнот";
        public const string DELETION_LABEL = "deletion_label";
        
        public const string Код = "col_a1";
        public const string Назва = "col_a2";
        public const string Запис = "col_a3";
    }

    public class Блокнот_Objest : DirectoryObject
    {
        public Блокнот_Objest() : base(Config.Kernel!, "tab_a01",
             new string[] { "col_a1", "col_a2", "col_a3" }) 
        {
            Код = "";
            Назва = "";
            Запис = "";
            
        }
        
        public void New()
        {
            BaseNew();
            Блокнот_Triggers.New(this);
            
        }

        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Код = base.FieldValue["col_a1"].ToString() ?? "";
                Назва = base.FieldValue["col_a2"].ToString() ?? "";
                Запис = base.FieldValue["col_a3"].ToString() ?? "";
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public bool Save()
        {
            Блокнот_Triggers.BeforeSave(this);
            base.FieldValue["col_a1"] = Код;
            base.FieldValue["col_a2"] = Назва;
            base.FieldValue["col_a3"] = Запис;
            
            bool result = BaseSave();
            if (result)
            {
                Блокнот_Triggers.AfterSave(this);
                BaseWriteFullTextSearch(GetBasis(), new string[] { Запис });
            }
            return result;
        }

        public Блокнот_Objest Copy(bool copyTableParts = false)
        {
            Блокнот_Objest copy = new Блокнот_Objest();
            copy.Код = Код;
            copy.Назва = Назва;
            copy.Запис = Запис;
            

            copy.New();
            Блокнот_Triggers.Copying(copy, this);
            return copy;
        }

        public void SetDeletionLabel(bool label = true)
        {
            Блокнот_Triggers.SetDeletionLabel(this, label);
            base.BaseDeletionLabel(label);
        }

        public void Delete()
        {
            Блокнот_Triggers.BeforeDelete(this);
            base.BaseDelete(new string[] {  });
        }
        
        public Блокнот_Pointer GetDirectoryPointer()
        {
            return new Блокнот_Pointer(UnigueID.UGuid);
        }

        public UuidAndText GetBasis()
        {
            return new UuidAndText(UnigueID.UGuid, Блокнот_Const.POINTER);
        }

        public string GetPresentation()
        {
            return base.BasePresentation(
                new string[] { "col_a2" }
            );
        }
        
        public string Код { get; set; }
        public string Назва { get; set; }
        public string Запис { get; set; }
        
    }

    public class Блокнот_Pointer : DirectoryPointer
    {
        public Блокнот_Pointer(object? uid = null) : base(Config.Kernel!, "tab_a01")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Блокнот_Pointer(UnigueID uid, Dictionary<string, object>? fields = null) : base(Config.Kernel!, "tab_a01")
        {
            base.Init(uid, fields);
        }
        
        public Блокнот_Objest? GetDirectoryObject()
        {
            if (this.IsEmpty()) return null;
            Блокнот_Objest БлокнотObjestItem = new Блокнот_Objest();
            return БлокнотObjestItem.Read(base.UnigueID) ? БлокнотObjestItem : null;
        }

        public Блокнот_Pointer Copy()
        {
            return new Блокнот_Pointer(base.UnigueID, base.Fields) { Назва = Назва };
        }

        public string Назва { get; set; } = "";

        public string GetPresentation()
        {
            return Назва = base.BasePresentation(
                new string[] { "col_a2" }
            );
        }

        public void SetDeletionLabel(bool label = true)
        {
            Блокнот_Objest? obj = GetDirectoryObject();
            if (obj != null)
            {
                Блокнот_Triggers.SetDeletionLabel(obj, label);
                
                base.BaseDeletionLabel(label);
            }
        }
		
        public Блокнот_Pointer GetEmptyPointer()
        {
            return new Блокнот_Pointer();
        }

        public UuidAndText GetBasis()
        {
            return new UuidAndText(UnigueID.UGuid, Блокнот_Const.POINTER);
        }

        public void Clear()
        {
            Init(new UnigueID(), null);
            Назва = "";
        }
    }
    
    public class Блокнот_Select : DirectorySelect
    {
        public Блокнот_Select() : base(Config.Kernel!, "tab_a01") { }        
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Блокнот_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Блокнот_Pointer? Current { get; private set; }
        
        public Блокнот_Pointer FindByField(string name, object value)
        {
            Блокнот_Pointer itemPointer = new Блокнот_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(name, value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Блокнот_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Блокнот_Pointer> directoryPointerList = new List<Блокнот_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(name, value, limit, offset)) 
                directoryPointerList.Add(new Блокнот_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
   
    #endregion
    
    #region DIRECTORY "Номенклатура"
    public static class Номенклатура_Const
    {
        public const string TABLE = "tab_a03";
        public const string POINTER = "Довідники.Номенклатура";
        public const string FULLNAME = "Номенклатура";
        public const string DELETION_LABEL = "deletion_label";
        
        public const string Код = "col_a1";
        public const string Назва = "col_a2";
        public const string Опис = "col_a3";
    }

    public class Номенклатура_Objest : DirectoryObject
    {
        public Номенклатура_Objest() : base(Config.Kernel!, "tab_a03",
             new string[] { "col_a1", "col_a2", "col_a3" }) 
        {
            Код = "";
            Назва = "";
            Опис = "";
            
        }
        
        public void New()
        {
            BaseNew();
            Номенклатура_Triggers.New(this);
            
        }

        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Код = base.FieldValue["col_a1"].ToString() ?? "";
                Назва = base.FieldValue["col_a2"].ToString() ?? "";
                Опис = base.FieldValue["col_a3"].ToString() ?? "";
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public bool Save()
        {
            Номенклатура_Triggers.BeforeSave(this);
            base.FieldValue["col_a1"] = Код;
            base.FieldValue["col_a2"] = Назва;
            base.FieldValue["col_a3"] = Опис;
            
            bool result = BaseSave();
            if (result)
            {
                Номенклатура_Triggers.AfterSave(this);
                BaseWriteFullTextSearch(GetBasis(), new string[] {  });
            }
            return result;
        }

        public Номенклатура_Objest Copy(bool copyTableParts = false)
        {
            Номенклатура_Objest copy = new Номенклатура_Objest();
            copy.Код = Код;
            copy.Назва = Назва;
            copy.Опис = Опис;
            

            copy.New();
            Номенклатура_Triggers.Copying(copy, this);
            return copy;
        }

        public void SetDeletionLabel(bool label = true)
        {
            Номенклатура_Triggers.SetDeletionLabel(this, label);
            base.BaseDeletionLabel(label);
        }

        public void Delete()
        {
            Номенклатура_Triggers.BeforeDelete(this);
            base.BaseDelete(new string[] {  });
        }
        
        public Номенклатура_Pointer GetDirectoryPointer()
        {
            return new Номенклатура_Pointer(UnigueID.UGuid);
        }

        public UuidAndText GetBasis()
        {
            return new UuidAndText(UnigueID.UGuid, Номенклатура_Const.POINTER);
        }

        public string GetPresentation()
        {
            return base.BasePresentation(
                new string[] { "col_a2" }
            );
        }
        
        public string Код { get; set; }
        public string Назва { get; set; }
        public string Опис { get; set; }
        
    }

    public class Номенклатура_Pointer : DirectoryPointer
    {
        public Номенклатура_Pointer(object? uid = null) : base(Config.Kernel!, "tab_a03")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Номенклатура_Pointer(UnigueID uid, Dictionary<string, object>? fields = null) : base(Config.Kernel!, "tab_a03")
        {
            base.Init(uid, fields);
        }
        
        public Номенклатура_Objest? GetDirectoryObject()
        {
            if (this.IsEmpty()) return null;
            Номенклатура_Objest НоменклатураObjestItem = new Номенклатура_Objest();
            return НоменклатураObjestItem.Read(base.UnigueID) ? НоменклатураObjestItem : null;
        }

        public Номенклатура_Pointer Copy()
        {
            return new Номенклатура_Pointer(base.UnigueID, base.Fields) { Назва = Назва };
        }

        public string Назва { get; set; } = "";

        public string GetPresentation()
        {
            return Назва = base.BasePresentation(
                new string[] { "col_a2" }
            );
        }

        public void SetDeletionLabel(bool label = true)
        {
            Номенклатура_Objest? obj = GetDirectoryObject();
            if (obj != null)
            {
                Номенклатура_Triggers.SetDeletionLabel(obj, label);
                
                base.BaseDeletionLabel(label);
            }
        }
		
        public Номенклатура_Pointer GetEmptyPointer()
        {
            return new Номенклатура_Pointer();
        }

        public UuidAndText GetBasis()
        {
            return new UuidAndText(UnigueID.UGuid, Номенклатура_Const.POINTER);
        }

        public void Clear()
        {
            Init(new UnigueID(), null);
            Назва = "";
        }
    }
    
    public class Номенклатура_Select : DirectorySelect
    {
        public Номенклатура_Select() : base(Config.Kernel!, "tab_a03") { }        
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Номенклатура_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Номенклатура_Pointer? Current { get; private set; }
        
        public Номенклатура_Pointer FindByField(string name, object value)
        {
            Номенклатура_Pointer itemPointer = new Номенклатура_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(name, value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Номенклатура_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Номенклатура_Pointer> directoryPointerList = new List<Номенклатура_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(name, value, limit, offset)) 
                directoryPointerList.Add(new Номенклатура_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
   
    #endregion
    
    #region DIRECTORY "Склад"
    public static class Склад_Const
    {
        public const string TABLE = "tab_a11";
        public const string POINTER = "Довідники.Склад";
        public const string FULLNAME = "Склад";
        public const string DELETION_LABEL = "deletion_label";
        
        public const string Код = "col_a1";
        public const string Назва = "col_a2";
    }

    public class Склад_Objest : DirectoryObject
    {
        public Склад_Objest() : base(Config.Kernel!, "tab_a11",
             new string[] { "col_a1", "col_a2" }) 
        {
            Код = "";
            Назва = "";
            
        }
        
        public void New()
        {
            BaseNew();
            Склад_Triggers.New(this);
            
        }

        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Код = base.FieldValue["col_a1"].ToString() ?? "";
                Назва = base.FieldValue["col_a2"].ToString() ?? "";
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public bool Save()
        {
            Склад_Triggers.BeforeSave(this);
            base.FieldValue["col_a1"] = Код;
            base.FieldValue["col_a2"] = Назва;
            
            bool result = BaseSave();
            if (result)
            {
                Склад_Triggers.AfterSave(this);
                BaseWriteFullTextSearch(GetBasis(), new string[] {  });
            }
            return result;
        }

        public Склад_Objest Copy(bool copyTableParts = false)
        {
            Склад_Objest copy = new Склад_Objest();
            copy.Код = Код;
            copy.Назва = Назва;
            

            copy.New();
            Склад_Triggers.Copying(copy, this);
            return copy;
        }

        public void SetDeletionLabel(bool label = true)
        {
            Склад_Triggers.SetDeletionLabel(this, label);
            base.BaseDeletionLabel(label);
        }

        public void Delete()
        {
            Склад_Triggers.BeforeDelete(this);
            base.BaseDelete(new string[] {  });
        }
        
        public Склад_Pointer GetDirectoryPointer()
        {
            return new Склад_Pointer(UnigueID.UGuid);
        }

        public UuidAndText GetBasis()
        {
            return new UuidAndText(UnigueID.UGuid, Склад_Const.POINTER);
        }

        public string GetPresentation()
        {
            return base.BasePresentation(
                new string[] { "col_a2" }
            );
        }
        
        public string Код { get; set; }
        public string Назва { get; set; }
        
    }

    public class Склад_Pointer : DirectoryPointer
    {
        public Склад_Pointer(object? uid = null) : base(Config.Kernel!, "tab_a11")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public Склад_Pointer(UnigueID uid, Dictionary<string, object>? fields = null) : base(Config.Kernel!, "tab_a11")
        {
            base.Init(uid, fields);
        }
        
        public Склад_Objest? GetDirectoryObject()
        {
            if (this.IsEmpty()) return null;
            Склад_Objest СкладObjestItem = new Склад_Objest();
            return СкладObjestItem.Read(base.UnigueID) ? СкладObjestItem : null;
        }

        public Склад_Pointer Copy()
        {
            return new Склад_Pointer(base.UnigueID, base.Fields) { Назва = Назва };
        }

        public string Назва { get; set; } = "";

        public string GetPresentation()
        {
            return Назва = base.BasePresentation(
                new string[] { "col_a2" }
            );
        }

        public void SetDeletionLabel(bool label = true)
        {
            Склад_Objest? obj = GetDirectoryObject();
            if (obj != null)
            {
                Склад_Triggers.SetDeletionLabel(obj, label);
                
                base.BaseDeletionLabel(label);
            }
        }
		
        public Склад_Pointer GetEmptyPointer()
        {
            return new Склад_Pointer();
        }

        public UuidAndText GetBasis()
        {
            return new UuidAndText(UnigueID.UGuid, Склад_Const.POINTER);
        }

        public void Clear()
        {
            Init(new UnigueID(), null);
            Назва = "";
        }
    }
    
    public class Склад_Select : DirectorySelect
    {
        public Склад_Select() : base(Config.Kernel!, "tab_a11") { }        
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new Склад_Pointer(base.DirectoryPointerPosition.UnigueID, base.DirectoryPointerPosition.Fields); return true; } else { Current = null; return false; } }

        public Склад_Pointer? Current { get; private set; }
        
        public Склад_Pointer FindByField(string name, object value)
        {
            Склад_Pointer itemPointer = new Склад_Pointer();
            DirectoryPointer directoryPointer = base.BaseFindByField(name, value);
            if (!directoryPointer.IsEmpty()) itemPointer.Init(directoryPointer.UnigueID);
            return itemPointer;
        }
        
        public List<Склад_Pointer> FindListByField(string name, object value, int limit = 0, int offset = 0)
        {
            List<Склад_Pointer> directoryPointerList = new List<Склад_Pointer>();
            foreach (DirectoryPointer directoryPointer in base.BaseFindListByField(name, value, limit, offset)) 
                directoryPointerList.Add(new Склад_Pointer(directoryPointer.UnigueID));
            return directoryPointerList;
        }
    }
    
      
   
    #endregion
    
}

namespace StorageAndTrade_1_0.Перелічення
{
    
    #region ENUM "ТипПеріодуДляЖурналівДокументів"
    public enum ТипПеріодуДляЖурналівДокументів
    {
         ВесьПеріод = 1,
         ЗПочаткуРоку = 2,
         Квартал = 6,
         ЗМинулогоМісяця = 7,
         Місяць = 8,
         ЗПочаткуМісяця = 3,
         ЗПочаткуТижня = 4,
         ПоточнийДень = 5
    }
    #endregion
    

    public static class ПсевдонімиПерелічення
    {
    
        #region ENUM "ТипПеріодуДляЖурналівДокументів"
        public static string ТипПеріодуДляЖурналівДокументів_Alias(ТипПеріодуДляЖурналівДокументів value)
        {
            switch (value)
            {
                
                case ТипПеріодуДляЖурналівДокументів.ВесьПеріод: return "Весь період";
                
                case ТипПеріодуДляЖурналівДокументів.ЗПочаткуРоку: return "Рік (з початку року)";
                
                case ТипПеріодуДляЖурналівДокументів.Квартал: return "Квартал (три місяці)";
                
                case ТипПеріодуДляЖурналівДокументів.ЗМинулогоМісяця: return "Два місяці (з 1 числа)";
                
                case ТипПеріодуДляЖурналівДокументів.Місяць: return "Місяць";
                
                case ТипПеріодуДляЖурналівДокументів.ЗПочаткуМісяця: return "Місяць (з 1 числа)";
                
                case ТипПеріодуДляЖурналівДокументів.ЗПочаткуТижня: return "Тиждень";
                
                case ТипПеріодуДляЖурналівДокументів.ПоточнийДень: return "День";
                
                default: return "";
            }
        }

        public static ТипПеріодуДляЖурналівДокументів? ТипПеріодуДляЖурналівДокументів_FindByName(string name)
        {
            switch (name)
            {
                
                case "Весь період": return ТипПеріодуДляЖурналівДокументів.ВесьПеріод;
                
                case "Рік (з початку року)": return ТипПеріодуДляЖурналівДокументів.ЗПочаткуРоку;
                
                case "Квартал (три місяці)": return ТипПеріодуДляЖурналівДокументів.Квартал;
                
                case "Два місяці (з 1 числа)": return ТипПеріодуДляЖурналівДокументів.ЗМинулогоМісяця;
                
                case "Місяць": return ТипПеріодуДляЖурналівДокументів.Місяць;
                
                case "Місяць (з 1 числа)": return ТипПеріодуДляЖурналівДокументів.ЗПочаткуМісяця;
                
                case "Тиждень": return ТипПеріодуДляЖурналівДокументів.ЗПочаткуТижня;
                
                case "День": return ТипПеріодуДляЖурналівДокументів.ПоточнийДень;
                
                default: return null;
            }
        }

        public static List<NameValue<ТипПеріодуДляЖурналівДокументів>> ТипПеріодуДляЖурналівДокументів_List()
        {
            List<NameValue<ТипПеріодуДляЖурналівДокументів>> value = new List<NameValue<ТипПеріодуДляЖурналівДокументів>>();
            
            value.Add(new NameValue<ТипПеріодуДляЖурналівДокументів>("Весь період", ТипПеріодуДляЖурналівДокументів.ВесьПеріод));
            
            value.Add(new NameValue<ТипПеріодуДляЖурналівДокументів>("Рік (з початку року)", ТипПеріодуДляЖурналівДокументів.ЗПочаткуРоку));
            
            value.Add(new NameValue<ТипПеріодуДляЖурналівДокументів>("Квартал (три місяці)", ТипПеріодуДляЖурналівДокументів.Квартал));
            
            value.Add(new NameValue<ТипПеріодуДляЖурналівДокументів>("Два місяці (з 1 числа)", ТипПеріодуДляЖурналівДокументів.ЗМинулогоМісяця));
            
            value.Add(new NameValue<ТипПеріодуДляЖурналівДокументів>("Місяць", ТипПеріодуДляЖурналівДокументів.Місяць));
            
            value.Add(new NameValue<ТипПеріодуДляЖурналівДокументів>("Місяць (з 1 числа)", ТипПеріодуДляЖурналівДокументів.ЗПочаткуМісяця));
            
            value.Add(new NameValue<ТипПеріодуДляЖурналівДокументів>("Тиждень", ТипПеріодуДляЖурналівДокументів.ЗПочаткуТижня));
            
            value.Add(new NameValue<ТипПеріодуДляЖурналівДокументів>("День", ТипПеріодуДляЖурналівДокументів.ПоточнийДень));
            
            return value;
        }
        #endregion
    
    }
}

namespace StorageAndTrade_1_0.Документи
{
    
    #region DOCUMENT "ПоступленняТоварів"
    public static class ПоступленняТоварів_Const
    {
        public const string TABLE = "tab_a04";
        public const string POINTER = "Документи.ПоступленняТоварів";
        public const string FULLNAME = "ПоступленняТоварів";
        public const string DELETION_LABEL = "deletion_label";
        
        
        public const string Назва = "docname";
        public const string ДатаДок = "docdate";
        public const string НомерДок = "docnomer";
        public const string Коментар = "col_a1";
        public const string Номенклатура = "col_a2";
        public const string Кількість = "col_a3";
        public const string Сума = "col_a4";
        public const string Склад = "col_a5";
        public const string Автор = "col_a6";
        public const string Основа = "col_a7";
    }

    public static class ПоступленняТоварів_Export
    {
        public static void ToXmlFile(ПоступленняТоварів_Pointer ПоступленняТоварів, string pathToSave)
        {
            ПоступленняТоварів_Objest? obj = ПоступленняТоварів.GetDocumentObject(true);
            if (obj == null) return;

            XmlWriter xmlWriter = XmlWriter.Create(pathToSave, new XmlWriterSettings() { Indent = true, Encoding = System.Text.Encoding.UTF8 });
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("root");
            xmlWriter.WriteAttributeString("uid", obj.UnigueID.ToString());
            
            xmlWriter.WriteStartElement("Назва");
            xmlWriter.WriteAttributeString("type", "string");
            
                xmlWriter.WriteValue(obj.Назва);
              
            xmlWriter.WriteEndElement(); //Назва
            xmlWriter.WriteStartElement("ДатаДок");
            xmlWriter.WriteAttributeString("type", "datetime");
            
                xmlWriter.WriteValue(obj.ДатаДок);
              
            xmlWriter.WriteEndElement(); //ДатаДок
            xmlWriter.WriteStartElement("НомерДок");
            xmlWriter.WriteAttributeString("type", "string");
            
                xmlWriter.WriteValue(obj.НомерДок);
              
            xmlWriter.WriteEndElement(); //НомерДок
            xmlWriter.WriteStartElement("Коментар");
            xmlWriter.WriteAttributeString("type", "string");
            
                xmlWriter.WriteValue(obj.Коментар);
              
            xmlWriter.WriteEndElement(); //Коментар
            xmlWriter.WriteStartElement("Номенклатура");
            xmlWriter.WriteAttributeString("type", "pointer");
            
                    xmlWriter.WriteAttributeString("pointer", "Довідники.Номенклатура");
                    xmlWriter.WriteAttributeString("uid", obj.Номенклатура.UnigueID.ToString());
                    xmlWriter.WriteString(obj.Номенклатура.GetPresentation());
                  
            xmlWriter.WriteEndElement(); //Номенклатура
            xmlWriter.WriteStartElement("Кількість");
            xmlWriter.WriteAttributeString("type", "numeric");
            
                xmlWriter.WriteValue(obj.Кількість);
              
            xmlWriter.WriteEndElement(); //Кількість
            xmlWriter.WriteStartElement("Сума");
            xmlWriter.WriteAttributeString("type", "numeric");
            
                xmlWriter.WriteValue(obj.Сума);
              
            xmlWriter.WriteEndElement(); //Сума
            xmlWriter.WriteStartElement("Склад");
            xmlWriter.WriteAttributeString("type", "pointer");
            
                    xmlWriter.WriteAttributeString("pointer", "Довідники.Склад");
                    xmlWriter.WriteAttributeString("uid", obj.Склад.UnigueID.ToString());
                    xmlWriter.WriteString(obj.Склад.GetPresentation());
                  
            xmlWriter.WriteEndElement(); //Склад
            xmlWriter.WriteStartElement("Автор");
            xmlWriter.WriteAttributeString("type", "pointer");
            
                    xmlWriter.WriteAttributeString("pointer", "Довідники.Користувачі");
                    xmlWriter.WriteAttributeString("uid", obj.Автор.UnigueID.ToString());
                    xmlWriter.WriteString(obj.Автор.GetPresentation());
                  
            xmlWriter.WriteEndElement(); //Автор
            xmlWriter.WriteStartElement("Основа");
            xmlWriter.WriteAttributeString("type", "composite_pointer");
            
                xmlWriter.WriteRaw(((UuidAndText)obj.Основа).ToXml());
              
            xmlWriter.WriteEndElement(); //Основа

            xmlWriter.WriteEndElement(); //root
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }
    }

    public class ПоступленняТоварів_Objest : DocumentObject
    {
        public ПоступленняТоварів_Objest() : base(Config.Kernel!, "tab_a04", "ПоступленняТоварів",
             new string[] { "docname", "docdate", "docnomer", "col_a1", "col_a2", "col_a3", "col_a4", "col_a5", "col_a6", "col_a7" }) 
        {
            Назва = "";
            ДатаДок = DateTime.MinValue;
            НомерДок = "";
            Коментар = "";
            Номенклатура = new Довідники.Номенклатура_Pointer();
            Кількість = 0;
            Сума = 0;
            Склад = new Довідники.Склад_Pointer();
            Автор = new Довідники.Користувачі_Pointer();
            Основа = new UuidAndText();
            
        }
        
        public void New()
        {
            BaseNew();
            ПоступленняТоварів_Triggers.New(this);
            
        }

        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["docname"].ToString() ?? "";
                ДатаДок = (base.FieldValue["docdate"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["docdate"].ToString() ?? DateTime.MinValue.ToString()) : DateTime.MinValue;
                НомерДок = base.FieldValue["docnomer"].ToString() ?? "";
                Коментар = base.FieldValue["col_a1"].ToString() ?? "";
                Номенклатура = new Довідники.Номенклатура_Pointer(base.FieldValue["col_a2"]);
                Кількість = (base.FieldValue["col_a3"] != DBNull.Value) ? (decimal)base.FieldValue["col_a3"] : 0;
                Сума = (base.FieldValue["col_a4"] != DBNull.Value) ? (decimal)base.FieldValue["col_a4"] : 0;
                Склад = new Довідники.Склад_Pointer(base.FieldValue["col_a5"]);
                Автор = new Довідники.Користувачі_Pointer(base.FieldValue["col_a6"]);
                Основа = (base.FieldValue["col_a7"] != DBNull.Value) ? (UuidAndText)base.FieldValue["col_a7"] : new UuidAndText();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public bool Save()
        {
            ПоступленняТоварів_Triggers.BeforeSave(this);
            base.FieldValue["docname"] = Назва;
            base.FieldValue["docdate"] = ДатаДок;
            base.FieldValue["docnomer"] = НомерДок;
            base.FieldValue["col_a1"] = Коментар;
            base.FieldValue["col_a2"] = Номенклатура.UnigueID.UGuid;
            base.FieldValue["col_a3"] = Кількість;
            base.FieldValue["col_a4"] = Сума;
            base.FieldValue["col_a5"] = Склад.UnigueID.UGuid;
            base.FieldValue["col_a6"] = Автор.UnigueID.UGuid;
            base.FieldValue["col_a7"] = Основа;
            
            bool result = BaseSave();
            
            if (result)
            {
                ПоступленняТоварів_Triggers.AfterSave(this);
                BaseWriteFullTextSearch(GetBasis(), new string[] {  });
            }

            return result;
        }

        public bool SpendTheDocument(DateTime spendDate)
        {
            bool rezult = ПоступленняТоварів_SpendTheDocument.Spend(this);
                BaseSpend(rezult, spendDate);
                return rezult;
        }

        public void ClearSpendTheDocument()
        {
            ПоступленняТоварів_SpendTheDocument.ClearSpend(this);
            BaseSpend(false, DateTime.MinValue);
        }

        public ПоступленняТоварів_Objest Copy(bool copyTableParts = false)
        {
            ПоступленняТоварів_Objest copy = new ПоступленняТоварів_Objest();
            copy.Назва = Назва;
            copy.ДатаДок = ДатаДок;
            copy.НомерДок = НомерДок;
            copy.Коментар = Коментар;
            copy.Номенклатура = Номенклатура;
            copy.Кількість = Кількість;
            copy.Сума = Сума;
            copy.Склад = Склад;
            copy.Автор = Автор;
            copy.Основа = Основа;
            

            copy.New();
            ПоступленняТоварів_Triggers.Copying(copy, this);
            return copy;
        }

        public void SetDeletionLabel(bool label = true)
        {
            ПоступленняТоварів_Triggers.SetDeletionLabel(this, label);
            ClearSpendTheDocument();
            base.BaseDeletionLabel(label);
        }

        public void Delete()
        {
            ПоступленняТоварів_Triggers.BeforeDelete(this);
            ClearSpendTheDocument();
            base.BaseDelete(new string[] {  });
        }
        
        public ПоступленняТоварів_Pointer GetDocumentPointer()
        {
            return new ПоступленняТоварів_Pointer(UnigueID.UGuid);
        }

        public UuidAndText GetBasis()
        {
            return new UuidAndText(UnigueID.UGuid, ПоступленняТоварів_Const.POINTER);
        }
        
        public string Назва { get; set; }
        public DateTime ДатаДок { get; set; }
        public string НомерДок { get; set; }
        public string Коментар { get; set; }
        public Довідники.Номенклатура_Pointer Номенклатура { get; set; }
        public decimal Кількість { get; set; }
        public decimal Сума { get; set; }
        public Довідники.Склад_Pointer Склад { get; set; }
        public Довідники.Користувачі_Pointer Автор { get; set; }
        public UuidAndText Основа { get; set; }
        
    }
    
    public class ПоступленняТоварів_Pointer : DocumentPointer
    {
        public ПоступленняТоварів_Pointer(object? uid = null) : base(Config.Kernel!, "tab_a04", "ПоступленняТоварів")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public ПоступленняТоварів_Pointer(UnigueID uid, Dictionary<string, object>? fields = null) : base(Config.Kernel!, "tab_a04", "ПоступленняТоварів")
        {
            base.Init(uid, fields);
        }

        public string Назва { get; set; } = "";

        public string GetPresentation()
        {
            return Назва = base.BasePresentation(
              new string[] { "docname" }
            );
        }

        public bool SpendTheDocument(DateTime spendDate)
        {
            ПоступленняТоварів_Objest? obj = GetDocumentObject();
            return (obj != null ? obj.SpendTheDocument(spendDate) : false);
        }

        public void ClearSpendTheDocument()
        {
            ПоступленняТоварів_Objest? obj = GetDocumentObject();
            if (obj != null) obj.ClearSpendTheDocument();
        }

        public void SetDeletionLabel(bool label = true)
        {
            ПоступленняТоварів_Objest? obj = GetDocumentObject();
                if (obj == null) return;
                ПоступленняТоварів_Triggers.SetDeletionLabel(obj, label);
                
                if (label)
                {
                    ПоступленняТоварів_SpendTheDocument.ClearSpend(obj);
                    BaseSpend(false, DateTime.MinValue);
                }
                
            base.BaseDeletionLabel(label);
        }

        public ПоступленняТоварів_Pointer Copy()
        {
            return new ПоступленняТоварів_Pointer(base.UnigueID, base.Fields) { Назва = Назва };
        }

        public ПоступленняТоварів_Pointer GetEmptyPointer()
        {
            return new ПоступленняТоварів_Pointer();
        }

        public UuidAndText GetBasis()
        {
            return new UuidAndText(UnigueID.UGuid, ПоступленняТоварів_Const.POINTER);
        }

        public ПоступленняТоварів_Objest? GetDocumentObject(bool readAllTablePart = false)
        {
            if (this.IsEmpty()) return null;
            ПоступленняТоварів_Objest ПоступленняТоварівObjestItem = new ПоступленняТоварів_Objest();
            if (!ПоступленняТоварівObjestItem.Read(base.UnigueID)) return null;
            
            return ПоступленняТоварівObjestItem;
        }

        public void Clear()
        {
            Init(new UnigueID(), null);
            Назва = "";
        }
    }

    public class ПоступленняТоварів_Select : DocumentSelect
    {		
        public ПоступленняТоварів_Select() : base(Config.Kernel!, "tab_a04") { }
        
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new ПоступленняТоварів_Pointer(base.DocumentPointerPosition.UnigueID, base.DocumentPointerPosition.Fields); return true; } else { Current = null; return false; } }
        
        public ПоступленняТоварів_Pointer? Current { get; private set; }
    }

      
    
    #endregion
    
    #region DOCUMENT "ПродажТоварів"
    public static class ПродажТоварів_Const
    {
        public const string TABLE = "tab_a05";
        public const string POINTER = "Документи.ПродажТоварів";
        public const string FULLNAME = "ПродажТоварів";
        public const string DELETION_LABEL = "deletion_label";
        
        
        public const string Назва = "docname";
        public const string ДатаДок = "docdate";
        public const string НомерДок = "docnomer";
        public const string Коментар = "col_a1";
        public const string Номенклатура = "col_a2";
        public const string Кількість = "col_a3";
        public const string Сума = "col_a4";
        public const string Склад = "col_a5";
        public const string Автор = "col_a6";
        public const string Основа = "col_a7";
    }

    public static class ПродажТоварів_Export
    {
        public static void ToXmlFile(ПродажТоварів_Pointer ПродажТоварів, string pathToSave)
        {
            ПродажТоварів_Objest? obj = ПродажТоварів.GetDocumentObject(true);
            if (obj == null) return;

            XmlWriter xmlWriter = XmlWriter.Create(pathToSave, new XmlWriterSettings() { Indent = true, Encoding = System.Text.Encoding.UTF8 });
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("root");
            xmlWriter.WriteAttributeString("uid", obj.UnigueID.ToString());
            
            xmlWriter.WriteStartElement("Назва");
            xmlWriter.WriteAttributeString("type", "string");
            
                xmlWriter.WriteValue(obj.Назва);
              
            xmlWriter.WriteEndElement(); //Назва
            xmlWriter.WriteStartElement("ДатаДок");
            xmlWriter.WriteAttributeString("type", "datetime");
            
                xmlWriter.WriteValue(obj.ДатаДок);
              
            xmlWriter.WriteEndElement(); //ДатаДок
            xmlWriter.WriteStartElement("НомерДок");
            xmlWriter.WriteAttributeString("type", "string");
            
                xmlWriter.WriteValue(obj.НомерДок);
              
            xmlWriter.WriteEndElement(); //НомерДок
            xmlWriter.WriteStartElement("Коментар");
            xmlWriter.WriteAttributeString("type", "string");
            
                xmlWriter.WriteValue(obj.Коментар);
              
            xmlWriter.WriteEndElement(); //Коментар
            xmlWriter.WriteStartElement("Номенклатура");
            xmlWriter.WriteAttributeString("type", "pointer");
            
                    xmlWriter.WriteAttributeString("pointer", "Довідники.Номенклатура");
                    xmlWriter.WriteAttributeString("uid", obj.Номенклатура.UnigueID.ToString());
                    xmlWriter.WriteString(obj.Номенклатура.GetPresentation());
                  
            xmlWriter.WriteEndElement(); //Номенклатура
            xmlWriter.WriteStartElement("Кількість");
            xmlWriter.WriteAttributeString("type", "numeric");
            
                xmlWriter.WriteValue(obj.Кількість);
              
            xmlWriter.WriteEndElement(); //Кількість
            xmlWriter.WriteStartElement("Сума");
            xmlWriter.WriteAttributeString("type", "numeric");
            
                xmlWriter.WriteValue(obj.Сума);
              
            xmlWriter.WriteEndElement(); //Сума
            xmlWriter.WriteStartElement("Склад");
            xmlWriter.WriteAttributeString("type", "pointer");
            
                    xmlWriter.WriteAttributeString("pointer", "Довідники.Склад");
                    xmlWriter.WriteAttributeString("uid", obj.Склад.UnigueID.ToString());
                    xmlWriter.WriteString(obj.Склад.GetPresentation());
                  
            xmlWriter.WriteEndElement(); //Склад
            xmlWriter.WriteStartElement("Автор");
            xmlWriter.WriteAttributeString("type", "pointer");
            
                    xmlWriter.WriteAttributeString("pointer", "Довідники.Користувачі");
                    xmlWriter.WriteAttributeString("uid", obj.Автор.UnigueID.ToString());
                    xmlWriter.WriteString(obj.Автор.GetPresentation());
                  
            xmlWriter.WriteEndElement(); //Автор
            xmlWriter.WriteStartElement("Основа");
            xmlWriter.WriteAttributeString("type", "composite_pointer");
            
                xmlWriter.WriteRaw(((UuidAndText)obj.Основа).ToXml());
              
            xmlWriter.WriteEndElement(); //Основа

            xmlWriter.WriteEndElement(); //root
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }
    }

    public class ПродажТоварів_Objest : DocumentObject
    {
        public ПродажТоварів_Objest() : base(Config.Kernel!, "tab_a05", "ПродажТоварів",
             new string[] { "docname", "docdate", "docnomer", "col_a1", "col_a2", "col_a3", "col_a4", "col_a5", "col_a6", "col_a7" }) 
        {
            Назва = "";
            ДатаДок = DateTime.MinValue;
            НомерДок = "";
            Коментар = "";
            Номенклатура = new Довідники.Номенклатура_Pointer();
            Кількість = 0;
            Сума = 0;
            Склад = new Довідники.Склад_Pointer();
            Автор = new Довідники.Користувачі_Pointer();
            Основа = new UuidAndText();
            
        }
        
        public void New()
        {
            BaseNew();
            ПродажТоварів_Triggers.New(this);
            
        }

        public bool Read(UnigueID uid)
        {
            if (BaseRead(uid))
            {
                Назва = base.FieldValue["docname"].ToString() ?? "";
                ДатаДок = (base.FieldValue["docdate"] != DBNull.Value) ? DateTime.Parse(base.FieldValue["docdate"].ToString() ?? DateTime.MinValue.ToString()) : DateTime.MinValue;
                НомерДок = base.FieldValue["docnomer"].ToString() ?? "";
                Коментар = base.FieldValue["col_a1"].ToString() ?? "";
                Номенклатура = new Довідники.Номенклатура_Pointer(base.FieldValue["col_a2"]);
                Кількість = (base.FieldValue["col_a3"] != DBNull.Value) ? (decimal)base.FieldValue["col_a3"] : 0;
                Сума = (base.FieldValue["col_a4"] != DBNull.Value) ? (decimal)base.FieldValue["col_a4"] : 0;
                Склад = new Довідники.Склад_Pointer(base.FieldValue["col_a5"]);
                Автор = new Довідники.Користувачі_Pointer(base.FieldValue["col_a6"]);
                Основа = (base.FieldValue["col_a7"] != DBNull.Value) ? (UuidAndText)base.FieldValue["col_a7"] : new UuidAndText();
                
                BaseClear();
                return true;
            }
            else
                return false;
        }
        
        public bool Save()
        {
            ПродажТоварів_Triggers.BeforeSave(this);
            base.FieldValue["docname"] = Назва;
            base.FieldValue["docdate"] = ДатаДок;
            base.FieldValue["docnomer"] = НомерДок;
            base.FieldValue["col_a1"] = Коментар;
            base.FieldValue["col_a2"] = Номенклатура.UnigueID.UGuid;
            base.FieldValue["col_a3"] = Кількість;
            base.FieldValue["col_a4"] = Сума;
            base.FieldValue["col_a5"] = Склад.UnigueID.UGuid;
            base.FieldValue["col_a6"] = Автор.UnigueID.UGuid;
            base.FieldValue["col_a7"] = Основа;
            
            bool result = BaseSave();
            
            if (result)
            {
                ПродажТоварів_Triggers.AfterSave(this);
                BaseWriteFullTextSearch(GetBasis(), new string[] {  });
            }

            return result;
        }

        public bool SpendTheDocument(DateTime spendDate)
        {
            bool rezult = ПродажТоварів_SpendTheDocument.Spend(this);
                BaseSpend(rezult, spendDate);
                return rezult;
        }

        public void ClearSpendTheDocument()
        {
            ПродажТоварів_SpendTheDocument.ClearSpend(this);
            BaseSpend(false, DateTime.MinValue);
        }

        public ПродажТоварів_Objest Copy(bool copyTableParts = false)
        {
            ПродажТоварів_Objest copy = new ПродажТоварів_Objest();
            copy.Назва = Назва;
            copy.ДатаДок = ДатаДок;
            copy.НомерДок = НомерДок;
            copy.Коментар = Коментар;
            copy.Номенклатура = Номенклатура;
            copy.Кількість = Кількість;
            copy.Сума = Сума;
            copy.Склад = Склад;
            copy.Автор = Автор;
            copy.Основа = Основа;
            

            copy.New();
            ПродажТоварів_Triggers.Copying(copy, this);
            return copy;
        }

        public void SetDeletionLabel(bool label = true)
        {
            ПродажТоварів_Triggers.SetDeletionLabel(this, label);
            ClearSpendTheDocument();
            base.BaseDeletionLabel(label);
        }

        public void Delete()
        {
            ПродажТоварів_Triggers.BeforeDelete(this);
            ClearSpendTheDocument();
            base.BaseDelete(new string[] {  });
        }
        
        public ПродажТоварів_Pointer GetDocumentPointer()
        {
            return new ПродажТоварів_Pointer(UnigueID.UGuid);
        }

        public UuidAndText GetBasis()
        {
            return new UuidAndText(UnigueID.UGuid, ПродажТоварів_Const.POINTER);
        }
        
        public string Назва { get; set; }
        public DateTime ДатаДок { get; set; }
        public string НомерДок { get; set; }
        public string Коментар { get; set; }
        public Довідники.Номенклатура_Pointer Номенклатура { get; set; }
        public decimal Кількість { get; set; }
        public decimal Сума { get; set; }
        public Довідники.Склад_Pointer Склад { get; set; }
        public Довідники.Користувачі_Pointer Автор { get; set; }
        public UuidAndText Основа { get; set; }
        
    }
    
    public class ПродажТоварів_Pointer : DocumentPointer
    {
        public ПродажТоварів_Pointer(object? uid = null) : base(Config.Kernel!, "tab_a05", "ПродажТоварів")
        {
            base.Init(new UnigueID(uid), null);
        }
        
        public ПродажТоварів_Pointer(UnigueID uid, Dictionary<string, object>? fields = null) : base(Config.Kernel!, "tab_a05", "ПродажТоварів")
        {
            base.Init(uid, fields);
        }

        public string Назва { get; set; } = "";

        public string GetPresentation()
        {
            return Назва = base.BasePresentation(
              new string[] { "docname" }
            );
        }

        public bool SpendTheDocument(DateTime spendDate)
        {
            ПродажТоварів_Objest? obj = GetDocumentObject();
            return (obj != null ? obj.SpendTheDocument(spendDate) : false);
        }

        public void ClearSpendTheDocument()
        {
            ПродажТоварів_Objest? obj = GetDocumentObject();
            if (obj != null) obj.ClearSpendTheDocument();
        }

        public void SetDeletionLabel(bool label = true)
        {
            ПродажТоварів_Objest? obj = GetDocumentObject();
                if (obj == null) return;
                ПродажТоварів_Triggers.SetDeletionLabel(obj, label);
                
                if (label)
                {
                    ПродажТоварів_SpendTheDocument.ClearSpend(obj);
                    BaseSpend(false, DateTime.MinValue);
                }
                
            base.BaseDeletionLabel(label);
        }

        public ПродажТоварів_Pointer Copy()
        {
            return new ПродажТоварів_Pointer(base.UnigueID, base.Fields) { Назва = Назва };
        }

        public ПродажТоварів_Pointer GetEmptyPointer()
        {
            return new ПродажТоварів_Pointer();
        }

        public UuidAndText GetBasis()
        {
            return new UuidAndText(UnigueID.UGuid, ПродажТоварів_Const.POINTER);
        }

        public ПродажТоварів_Objest? GetDocumentObject(bool readAllTablePart = false)
        {
            if (this.IsEmpty()) return null;
            ПродажТоварів_Objest ПродажТоварівObjestItem = new ПродажТоварів_Objest();
            if (!ПродажТоварівObjestItem.Read(base.UnigueID)) return null;
            
            return ПродажТоварівObjestItem;
        }

        public void Clear()
        {
            Init(new UnigueID(), null);
            Назва = "";
        }
    }

    public class ПродажТоварів_Select : DocumentSelect
    {		
        public ПродажТоварів_Select() : base(Config.Kernel!, "tab_a05") { }
        
        public bool Select() { return base.BaseSelect(); }
        
        public bool SelectSingle() { if (base.BaseSelectSingle()) { MoveNext(); return true; } else { Current = null; return false; } }
        
        public bool MoveNext() { if (MoveToPosition()) { Current = new ПродажТоварів_Pointer(base.DocumentPointerPosition.UnigueID, base.DocumentPointerPosition.Fields); return true; } else { Current = null; return false; } }
        
        public ПродажТоварів_Pointer? Current { get; private set; }
    }

      
    
    #endregion
    
}

namespace StorageAndTrade_1_0.Журнали
{
    #region Journal
    public class Journal_Select: JournalSelect
    {
        public Journal_Select() : base(Config.Kernel!,
             new string[] { "tab_a04", "tab_a05"},
			       new string[] { "ПоступленняТоварів", "ПродажТоварів"}) { }

        public DocumentObject? GetDocumentObject(bool readAllTablePart = true)
        {
            if (Current == null)
                return null;

            switch (Current.TypeDocument)
            {
                case "ПоступленняТоварів": return new Документи.ПоступленняТоварів_Pointer(Current.UnigueID).GetDocumentObject(readAllTablePart);
                case "ПродажТоварів": return new Документи.ПродажТоварів_Pointer(Current.UnigueID).GetDocumentObject(readAllTablePart);
                
                default: return null;
            }
        }
    }
    #endregion

}

namespace StorageAndTrade_1_0.РегістриВідомостей
{
    
}

namespace StorageAndTrade_1_0.РегістриНакопичення
{
    public static class VirtualTablesСalculation
    {
        /* Функція повного очищення віртуальних таблиць */
        public static void ClearAll()
        {
            /*  */
        }

        /* Функція для обчислення віртуальних таблиць  */
        public static void Execute(DateTime period, string regAccumName)
        {
            
            Dictionary<string, object> paramQuery = new Dictionary<string, object>();
            paramQuery.Add("ПеріодДеньВідбір", period);

            switch(regAccumName)
            {
            
                case "Товари":
                {
                    byte transactionID = Config.Kernel!.DataBase.BeginTransaction();
                    
                    /* QueryBlock: Залишки */
                        
                    Config.Kernel!.DataBase.ExecuteSQL($@"DELETE FROM {Товари_Залишки_TablePart.TABLE} WHERE {Товари_Залишки_TablePart.TABLE}.{Товари_Залишки_TablePart.Період} = @ПеріодДеньВідбір", paramQuery, transactionID);
                        
                    Config.Kernel!.DataBase.ExecuteSQL($@"INSERT INTO {Товари_Залишки_TablePart.TABLE} ( uid, {Товари_Залишки_TablePart.Період}, {Товари_Залишки_TablePart.Номенклатура}, {Товари_Залишки_TablePart.Склад}, {Товари_Залишки_TablePart.Кількість}, {Товари_Залишки_TablePart.Сума} ) SELECT uuid_generate_v4(), date_trunc('day', Товари.period::timestamp) AS Період, Товари.{Товари_Const.Номенклатура} AS Номенклатура, Товари.{Товари_Const.Склад} AS Склад, /* Кількість */ SUM(CASE WHEN Товари.income = true THEN Товари.{Товари_Const.Кількість} ELSE -Товари.{Товари_Const.Кількість} END) AS Кількість, /* Сума */ SUM(CASE WHEN Товари.income = true THEN Товари.{Товари_Const.Сума} ELSE -Товари.{Товари_Const.Сума} END) AS Сума FROM {Товари_Const.TABLE} AS Товари WHERE date_trunc('day', Товари.period::timestamp) = @ПеріодДеньВідбір GROUP BY Період, Номенклатура, Склад HAVING /* Кількість */ SUM(CASE WHEN Товари.income = true THEN Товари.{Товари_Const.Кількість} ELSE -Товари.{Товари_Const.Кількість} END) != 0 OR /* Сума */ SUM(CASE WHEN Товари.income = true THEN Товари.{Товари_Const.Сума} ELSE -Товари.{Товари_Const.Сума} END) != 0", paramQuery, transactionID);
                        
                    /* QueryBlock: ЗалишкиТаОбороти */
                        
                    Config.Kernel!.DataBase.ExecuteSQL($@"DELETE FROM {Товари_ЗалишкиТаОбороти_TablePart.TABLE} WHERE {Товари_ЗалишкиТаОбороти_TablePart.TABLE}.{Товари_ЗалишкиТаОбороти_TablePart.Період} = @ПеріодДеньВідбір", paramQuery, transactionID);
                        
                    Config.Kernel!.DataBase.ExecuteSQL($@"INSERT INTO {Товари_ЗалишкиТаОбороти_TablePart.TABLE} ( uid, {Товари_ЗалишкиТаОбороти_TablePart.Період}, {Товари_ЗалишкиТаОбороти_TablePart.Номенклатура}, {Товари_ЗалишкиТаОбороти_TablePart.Склад}, {Товари_ЗалишкиТаОбороти_TablePart.КількістьПрихід}, {Товари_ЗалишкиТаОбороти_TablePart.КількістьРозхід}, {Товари_ЗалишкиТаОбороти_TablePart.КількістьЗалишок}, {Товари_ЗалишкиТаОбороти_TablePart.СумаПрихід}, {Товари_ЗалишкиТаОбороти_TablePart.СумаРозхід}, {Товари_ЗалишкиТаОбороти_TablePart.СумаЗалишок} ) SELECT uuid_generate_v4(), date_trunc('day', Товари.period::timestamp) AS Період, Товари.{Товари_Const.Номенклатура} AS Номенклатура, Товари.{Товари_Const.Склад} AS Склад, /* Кількість */ SUM(CASE WHEN Товари.income = true THEN Товари.{Товари_Const.Кількість} ELSE 0 END) AS КількістьПрихід, SUM(CASE WHEN Товари.income = false THEN Товари.{Товари_Const.Кількість} ELSE 0 END) AS КількістьРозхід, SUM(CASE WHEN Товари.income = true THEN Товари.{Товари_Const.Кількість} ELSE -Товари.{Товари_Const.Кількість} END) AS КількістьЗалишок, /* Сума */ SUM(CASE WHEN Товари.income = true THEN Товари.{Товари_Const.Сума} ELSE 0 END) AS СумаПрихід, SUM(CASE WHEN Товари.income = false THEN Товари.{Товари_Const.Сума} ELSE 0 END) AS СумаРозхід, SUM(CASE WHEN Товари.income = true THEN Товари.{Товари_Const.Сума} ELSE -Товари.{Товари_Const.Сума} END) AS СумаЗалишок FROM {Товари_Const.TABLE} AS Товари WHERE date_trunc('day', Товари.period::timestamp) = @ПеріодДеньВідбір GROUP BY Період, Номенклатура, Склад HAVING /* Кількість */ SUM(CASE WHEN Товари.income = true THEN Товари.{Товари_Const.Кількість} ELSE 0 END) != 0 OR SUM(CASE WHEN Товари.income = false THEN Товари.{Товари_Const.Кількість} ELSE 0 END) != 0 OR SUM(CASE WHEN Товари.income = true THEN Товари.{Товари_Const.Кількість} ELSE -Товари.{Товари_Const.Кількість} END) != 0 OR /* Сума */ SUM(CASE WHEN Товари.income = true THEN Товари.{Товари_Const.Сума} ELSE 0 END) != 0 OR SUM(CASE WHEN Товари.income = false THEN Товари.{Товари_Const.Сума} ELSE 0 END) != 0 OR SUM(CASE WHEN Товари.income = true THEN Товари.{Товари_Const.Сума} ELSE -Товари.{Товари_Const.Сума} END) != 0", paramQuery, transactionID);
                        
                    Config.Kernel!.DataBase.CommitTransaction(transactionID);
                    break;
                }
                
                    default:
                        break;
            }
            
        }

        /* Функція для обчислення підсумкових віртуальних таблиць */
        public static void ExecuteFinalCalculation(List<string> regAccumNameList)
        {
            
            foreach (string regAccumName in regAccumNameList)
                switch(regAccumName)
                {
                
                    case "Товари":
                    {
                        byte transactionID = Config.Kernel!.DataBase.BeginTransaction();
                        
                        /* QueryBlock: Підсумки */
                            
                        Config.Kernel!.DataBase.ExecuteSQL($@"DELETE FROM {Товари_Підсумки_TablePart.TABLE}", null, transactionID);
                            
                        Config.Kernel!.DataBase.ExecuteSQL($@"INSERT INTO {Товари_Підсумки_TablePart.TABLE} ( uid, {Товари_Підсумки_TablePart.Номенклатура}, {Товари_Підсумки_TablePart.Склад}, {Товари_Підсумки_TablePart.Кількість}, {Товари_Підсумки_TablePart.Сума} ) SELECT uuid_generate_v4(), Товари.{Товари_Залишки_TablePart.Номенклатура} AS Номенклатура, Товари.{Товари_Залишки_TablePart.Склад} AS Склад, /* Кількість */ SUM(Товари.{Товари_Залишки_TablePart.Кількість}) AS Кількість, /* Сума */ SUM(Товари.{Товари_Залишки_TablePart.Сума}) AS Сума FROM {Товари_Залишки_TablePart.TABLE} AS Товари GROUP BY Номенклатура, Склад HAVING /* Кількість */ SUM(Товари.{Товари_Залишки_TablePart.Кількість}) != 0 OR /* Сума */ SUM(Товари.{Товари_Залишки_TablePart.Сума}) != 0", null, transactionID);
                            
                        Config.Kernel!.DataBase.CommitTransaction(transactionID);
                        break;
                    }
                    
                        default:
                            break;
                }
            
        }
    }

    
    #region REGISTER "Товари"
    public static class Товари_Const
    {
        public const string FULLNAME = "Товари";
        public const string TABLE = "tab_a06";
		    public static readonly string[] AllowDocumentSpendTable = new string[] { "tab_a04", "tab_a05" };
		public static readonly string[] AllowDocumentSpendType = new string[] { "ПоступленняТоварів", "ПродажТоварів" };
        
        public const string Номенклатура = "col_a1";
        public const string Склад = "col_a4";
        public const string Кількість = "col_a2";
        public const string Сума = "col_a3";
    }
	
    public class Товари_RecordsSet : RegisterAccumulationRecordsSet
    {
        public Товари_RecordsSet() : base(Config.Kernel!, "tab_a06", "Товари",
             new string[] { "col_a1", "col_a4", "col_a2", "col_a3" }) 
        {
            Records = new List<Record>();
        }
		
        public List<Record> Records { get; set; }
        
        public void Read()
        {
            Records.Clear();
            
            base.BaseRead();
            
            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                record.Period = DateTime.Parse(fieldValue["period"]?.ToString() ?? DateTime.MinValue.ToString());
                record.Income = (bool)fieldValue["income"];
                record.Owner = (Guid)fieldValue["owner"];
                record.Номенклатура = new Довідники.Номенклатура_Pointer(fieldValue["col_a1"]);
                record.Склад = new Довідники.Склад_Pointer(fieldValue["col_a4"]);
                record.Кількість = (fieldValue["col_a2"] != DBNull.Value) ? (decimal)fieldValue["col_a2"] : 0;
                record.Сума = (fieldValue["col_a3"] != DBNull.Value) ? (decimal)fieldValue["col_a3"] : 0;
                
                Records.Add(record);
            }
            
            base.BaseClear();
        }
        
        public void Save(DateTime period, Guid owner) 
        {
            base.BaseBeginTransaction();
            base.BaseSelectPeriodForOwner(owner, period);
            base.BaseDelete(owner);
            foreach (Record record in Records)
            {
                record.Period = period;
                record.Owner = owner;
                Dictionary<string, object> fieldValue = new Dictionary<string, object>();
                fieldValue.Add("col_a1", record.Номенклатура.UnigueID.UGuid);
                fieldValue.Add("col_a4", record.Склад.UnigueID.UGuid);
                fieldValue.Add("col_a2", record.Кількість);
                fieldValue.Add("col_a3", record.Сума);
                
                record.UID = base.BaseSave(record.UID, period, record.Income, owner, fieldValue);
            }
            base.BaseTrigerAdd(period, owner);
            base.BaseCommitTransaction();
        }

        public void Delete(Guid owner)
        {
            base.BaseSelectPeriodForOwner(owner);
            base.BaseDelete(owner);
        }
        
        public class Record : RegisterAccumulationRecord
        {
            public Довідники.Номенклатура_Pointer Номенклатура { get; set; } = new Довідники.Номенклатура_Pointer();
            public Довідники.Склад_Pointer Склад { get; set; } = new Довідники.Склад_Pointer();
            public decimal Кількість { get; set; } = 0;
            public decimal Сума { get; set; } = 0;
            
        }
    }
    
    
    
    public class Товари_Залишки_TablePart : RegisterAccumulationTablePart
    {
        public Товари_Залишки_TablePart() : base(Config.Kernel!, "tab_a07",
              new string[] { "col_a1", "col_a2", "col_a5", "col_a3", "col_a4" }) 
        {
            Records = new List<Record>();
        }
        
        public const string TABLE = "tab_a07";
        
        public const string Період = "col_a1";
        public const string Номенклатура = "col_a2";
        public const string Склад = "col_a5";
        public const string Кількість = "col_a3";
        public const string Сума = "col_a4";
        public List<Record> Records { get; set; }
    
        public void Read()
        {
            Records.Clear();
            base.BaseRead();

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Період = (fieldValue["col_a1"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a1"].ToString() ?? DateTime.MinValue.ToString()) : DateTime.MinValue;
                record.Номенклатура = new Довідники.Номенклатура_Pointer(fieldValue["col_a2"]);
                record.Склад = new Довідники.Склад_Pointer(fieldValue["col_a5"]);
                record.Кількість = (fieldValue["col_a3"] != DBNull.Value) ? (decimal)fieldValue["col_a3"] : 0;
                record.Сума = (fieldValue["col_a4"] != DBNull.Value) ? (decimal)fieldValue["col_a4"] : 0;
                
                Records.Add(record);
            }
        
            base.BaseClear();
        }
    
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            base.BaseBeginTransaction();
            
            if (clear_all_before_save)
                base.BaseDelete();

            foreach (Record record in Records)
            {
                Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                fieldValue.Add("col_a1", record.Період);
                fieldValue.Add("col_a2", record.Номенклатура.UnigueID.UGuid);
                fieldValue.Add("col_a5", record.Склад.UnigueID.UGuid);
                fieldValue.Add("col_a3", record.Кількість);
                fieldValue.Add("col_a4", record.Сума);
                
                record.UID = base.BaseSave(record.UID, fieldValue);
            }
            
            base.BaseCommitTransaction();
        }
    
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public class Record : RegisterAccumulationTablePartRecord
        {
            public DateTime Період { get; set; } = DateTime.MinValue;
            public Довідники.Номенклатура_Pointer Номенклатура { get; set; } = new Довідники.Номенклатура_Pointer();
            public Довідники.Склад_Pointer Склад { get; set; } = new Довідники.Склад_Pointer();
            public decimal Кількість { get; set; } = 0;
            public decimal Сума { get; set; } = 0;
            
        }            
    }
    
    
    public class Товари_ЗалишкиТаОбороти_TablePart : RegisterAccumulationTablePart
    {
        public Товари_ЗалишкиТаОбороти_TablePart() : base(Config.Kernel!, "tab_a09",
              new string[] { "col_a1", "col_a2", "col_a9", "col_a3", "col_a4", "col_a5", "col_a6", "col_a7", "col_a8" }) 
        {
            Records = new List<Record>();
        }
        
        public const string TABLE = "tab_a09";
        
        public const string Період = "col_a1";
        public const string Номенклатура = "col_a2";
        public const string Склад = "col_a9";
        public const string КількістьПрихід = "col_a3";
        public const string КількістьРозхід = "col_a4";
        public const string КількістьЗалишок = "col_a5";
        public const string СумаПрихід = "col_a6";
        public const string СумаРозхід = "col_a7";
        public const string СумаЗалишок = "col_a8";
        public List<Record> Records { get; set; }
    
        public void Read()
        {
            Records.Clear();
            base.BaseRead();

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Період = (fieldValue["col_a1"] != DBNull.Value) ? DateTime.Parse(fieldValue["col_a1"].ToString() ?? DateTime.MinValue.ToString()) : DateTime.MinValue;
                record.Номенклатура = new Довідники.Номенклатура_Pointer(fieldValue["col_a2"]);
                record.Склад = new Довідники.Склад_Pointer(fieldValue["col_a9"]);
                record.КількістьПрихід = (fieldValue["col_a3"] != DBNull.Value) ? (decimal)fieldValue["col_a3"] : 0;
                record.КількістьРозхід = (fieldValue["col_a4"] != DBNull.Value) ? (decimal)fieldValue["col_a4"] : 0;
                record.КількістьЗалишок = (fieldValue["col_a5"] != DBNull.Value) ? (decimal)fieldValue["col_a5"] : 0;
                record.СумаПрихід = (fieldValue["col_a6"] != DBNull.Value) ? (decimal)fieldValue["col_a6"] : 0;
                record.СумаРозхід = (fieldValue["col_a7"] != DBNull.Value) ? (decimal)fieldValue["col_a7"] : 0;
                record.СумаЗалишок = (fieldValue["col_a8"] != DBNull.Value) ? (decimal)fieldValue["col_a8"] : 0;
                
                Records.Add(record);
            }
        
            base.BaseClear();
        }
    
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            base.BaseBeginTransaction();
            
            if (clear_all_before_save)
                base.BaseDelete();

            foreach (Record record in Records)
            {
                Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                fieldValue.Add("col_a1", record.Період);
                fieldValue.Add("col_a2", record.Номенклатура.UnigueID.UGuid);
                fieldValue.Add("col_a9", record.Склад.UnigueID.UGuid);
                fieldValue.Add("col_a3", record.КількістьПрихід);
                fieldValue.Add("col_a4", record.КількістьРозхід);
                fieldValue.Add("col_a5", record.КількістьЗалишок);
                fieldValue.Add("col_a6", record.СумаПрихід);
                fieldValue.Add("col_a7", record.СумаРозхід);
                fieldValue.Add("col_a8", record.СумаЗалишок);
                
                record.UID = base.BaseSave(record.UID, fieldValue);
            }
            
            base.BaseCommitTransaction();
        }
    
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public class Record : RegisterAccumulationTablePartRecord
        {
            public DateTime Період { get; set; } = DateTime.MinValue;
            public Довідники.Номенклатура_Pointer Номенклатура { get; set; } = new Довідники.Номенклатура_Pointer();
            public Довідники.Склад_Pointer Склад { get; set; } = new Довідники.Склад_Pointer();
            public decimal КількістьПрихід { get; set; } = 0;
            public decimal КількістьРозхід { get; set; } = 0;
            public decimal КількістьЗалишок { get; set; } = 0;
            public decimal СумаПрихід { get; set; } = 0;
            public decimal СумаРозхід { get; set; } = 0;
            public decimal СумаЗалишок { get; set; } = 0;
            
        }            
    }
    
    
    public class Товари_Підсумки_TablePart : RegisterAccumulationTablePart
    {
        public Товари_Підсумки_TablePart() : base(Config.Kernel!, "tab_a10",
              new string[] { "col_a1", "col_a4", "col_a2", "col_a3" }) 
        {
            Records = new List<Record>();
        }
        
        public const string TABLE = "tab_a10";
        
        public const string Номенклатура = "col_a1";
        public const string Склад = "col_a4";
        public const string Кількість = "col_a2";
        public const string Сума = "col_a3";
        public List<Record> Records { get; set; }
    
        public void Read()
        {
            Records.Clear();
            base.BaseRead();

            foreach (Dictionary<string, object> fieldValue in base.FieldValueList) 
            {
                Record record = new Record();
                record.UID = (Guid)fieldValue["uid"];
                
                record.Номенклатура = new Довідники.Номенклатура_Pointer(fieldValue["col_a1"]);
                record.Склад = new Довідники.Склад_Pointer(fieldValue["col_a4"]);
                record.Кількість = (fieldValue["col_a2"] != DBNull.Value) ? (decimal)fieldValue["col_a2"] : 0;
                record.Сума = (fieldValue["col_a3"] != DBNull.Value) ? (decimal)fieldValue["col_a3"] : 0;
                
                Records.Add(record);
            }
        
            base.BaseClear();
        }
    
        public void Save(bool clear_all_before_save /*= true*/) 
        {
            base.BaseBeginTransaction();
            
            if (clear_all_before_save)
                base.BaseDelete();

            foreach (Record record in Records)
            {
                Dictionary<string, object> fieldValue = new Dictionary<string, object>();

                fieldValue.Add("col_a1", record.Номенклатура.UnigueID.UGuid);
                fieldValue.Add("col_a4", record.Склад.UnigueID.UGuid);
                fieldValue.Add("col_a2", record.Кількість);
                fieldValue.Add("col_a3", record.Сума);
                
                record.UID = base.BaseSave(record.UID, fieldValue);
            }
            
            base.BaseCommitTransaction();
        }
    
        public void Delete()
        {
            base.BaseDelete();
        }
        
        public class Record : RegisterAccumulationTablePartRecord
        {
            public Довідники.Номенклатура_Pointer Номенклатура { get; set; } = new Довідники.Номенклатура_Pointer();
            public Довідники.Склад_Pointer Склад { get; set; } = new Довідники.Склад_Pointer();
            public decimal Кількість { get; set; } = 0;
            public decimal Сума { get; set; } = 0;
            
        }            
    }
    
    #endregion
  
}
  