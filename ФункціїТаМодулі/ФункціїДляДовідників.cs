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
 

*/

using Gtk;

using System.Reflection;

using AccountingSoftware;

using Довідники = StorageAndTrade_1_0.Довідники;
using StorageAndTrade_1_0.Довідники;

using Перелічення = StorageAndTrade_1_0.Перелічення;
using Константи = StorageAndTrade_1_0.Константи;

using StorageAndTrade_1_0.Документи;

namespace StorageAndTrade
{
    /// <summary>
    /// Спільні функції для довідників 
    /// </summary>
    class ФункціїДляДовідників
    {
        /// <summary>
        /// Функція відкриває список довідника і позиціонує на вибраний елемент
        /// </summary>
        /// <param name="typeDir">Тип</param>
        /// <param name="unigueID">Елемент для позиціонування</param>
        public static void ВідкритиДовідникВідповідноДоВиду(string typeDir, UnigueID? unigueID, bool insertPage = true)
        {
            Assembly ExecutingAssembly = Assembly.GetExecutingAssembly();

            //Простір імен програми
            string NameSpacePage = "StorageAndTrade";

            //Простір імен конфігурації
            string NameSpaceConfig = "StorageAndTrade_1_0.Довідники";

            object? listPage;

            try
            {
                listPage = ExecutingAssembly.CreateInstance($"{NameSpacePage}.{typeDir}");
            }
            catch (Exception ex)
            {
                Message.Error(Program.GeneralForm, ex.Message);
                return;
            }

            if (listPage != null)
            {
                //Довідник який потрібно виділити в списку
                listPage.GetType().GetProperty("SelectPointerItem")?.SetValue(listPage, unigueID);

                //Заголовок журналу з константи конфігурації
                string listName = "Список документів";
                {
                    Type? documentConst = Type.GetType($"{NameSpaceConfig}.{typeDir}_Const");
                    if (documentConst != null)
                        listName = documentConst.GetField("FULLNAME")?.GetValue(null)?.ToString() ?? listName;
                }

                Program.GeneralForm?.CreateNotebookPage(listName, () => { return (Widget)listPage; }, insertPage);

                listPage.GetType().InvokeMember("LoadRecords", BindingFlags.InvokeMethod, null, listPage, null);
            }
        }
    }
}
