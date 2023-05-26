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

Функції для документів

Функції для шапки
Контекстне меню для табличної частини

*/

using Gtk;

using System.Reflection;

using AccountingSoftware;

using Конфа = StorageAndTrade_1_0;
using Довідники = StorageAndTrade_1_0.Довідники;
using Документи = StorageAndTrade_1_0.Документи;
using Перелічення = StorageAndTrade_1_0.Перелічення;
using РегістриВідомостей = StorageAndTrade_1_0.РегістриВідомостей;

namespace StorageAndTrade
{
    /// <summary>
    /// Спільні функції для документів
    /// </summary>
    class ФункціїДляДокументів
    {
        /// <summary>
        /// Функція відкриває список докуменів і позиціонує на вибраний елемент
        /// </summary>
        /// <param name="typeDoc">Тип документу</param>
        /// <param name="unigueID">Елемент для позиціювання</param>
        /// <param name="periodWhere">Період</param>
        /// <param name="insertPage">Вставити сторінку</param>
        public static void ВідкритиДокументВідповідноДоВиду(string typeDoc, UnigueID? unigueID, Перелічення.ТипПеріодуДляЖурналівДокументів periodWhere = 0, bool insertPage = true)
        {
            Assembly ExecutingAssembly = Assembly.GetExecutingAssembly();

            //Простір імен програми
            string NameSpacePage = "StorageAndTrade";

            //Простір імен конфігурації
            string NameSpaceConfig = "StorageAndTrade_1_0.Документи";

            object? listPage;

            try
            {
                listPage = ExecutingAssembly.CreateInstance($"{NameSpacePage}.{typeDoc}");
            }
            catch (Exception ex)
            {
                Message.Error(Program.GeneralForm, ex.Message);
                return;
            }

            if (listPage != null)
            {
                //Документ який потрібно виділити в списку
                listPage.GetType().GetProperty("SelectPointerItem")?.SetValue(listPage, unigueID);

                //Заголовок журналу з константи конфігурації
                string listName = "Список документів";
                {
                    Type? documentConst = Type.GetType($"{NameSpaceConfig}.{typeDoc}_Const");
                    if (documentConst != null)
                        listName = documentConst.GetField("FULLNAME")?.GetValue(null)?.ToString() ?? listName;
                }

                Program.GeneralForm?.CreateNotebookPage(listName, () => { return (Widget)listPage; }, insertPage);

                listPage.GetType().GetProperty("PeriodWhere")?.SetValue(listPage, (periodWhere != 0 ? periodWhere : Перелічення.ТипПеріодуДляЖурналівДокументів.ВесьПеріод));
                listPage.GetType().InvokeMember("SetValue", BindingFlags.InvokeMethod, null, listPage, null);
            }
        }

        /// <summary>
        /// Функція обєднує дві дати (з пешої дата, з другої час)
        /// </summary>
        /// <param name="дата">Дата</param>
        /// <param name="час">Час</param>
        /// <returns>Обєднана дата</returns>
        public static DateTime ОбєднатиДатуТаЧас(DateTime дата, DateTime час)
        {
            return new DateTime(дата.Year, дата.Month, дата.Day, час.Hour, час.Minute, час.Second);
        }
    }
}
