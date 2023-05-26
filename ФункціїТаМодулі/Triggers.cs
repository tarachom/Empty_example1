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
 
Модуль функцій зворотнього виклику.

1. Перед записом
2. Після запису
3. Перед видаленням
 
*/

using AccountingSoftware;
using StorageAndTrade;
using Конфа = StorageAndTrade_1_0;
using StorageAndTrade_1_0.Константи;
using StorageAndTrade_1_0.Довідники;

namespace StorageAndTrade_1_0.Довідники
{
    class Користувачі_Triggers
    {
        public static void New(Користувачі_Objest ДовідникОбєкт)
        {
            ДовідникОбєкт.Код = (++НумераціяДовідників.Користувачі_Const).ToString("D6");
        }

        public static void Copying(Користувачі_Objest ДовідникОбєкт, Користувачі_Objest Основа)
        {
            ДовідникОбєкт.Назва += " - Копія";
        }

        public static void BeforeSave(Користувачі_Objest ДовідникОбєкт)
        {

        }

        public static void AfterSave(Користувачі_Objest ДовідникОбєкт)
        {

        }

        public static void SetDeletionLabel(Користувачі_Objest ДовідникОбєкт, bool label)
        {

        }

        public static void BeforeDelete(Користувачі_Objest ДовідникОбєкт)
        {

        }
    }

    class Блокнот_Triggers
    {
        public static void New(Блокнот_Objest ДовідникОбєкт)
        {
            ДовідникОбєкт.Код = (++НумераціяДовідників.Блокнот_Const).ToString("D6");
        }

        public static void Copying(Блокнот_Objest ДовідникОбєкт, Блокнот_Objest Основа)
        {
            ДовідникОбєкт.Назва += " - Копія";
        }

        public static void BeforeSave(Блокнот_Objest ДовідникОбєкт)
        {

        }

        public static void AfterSave(Блокнот_Objest ДовідникОбєкт)
        {

        }

        public static void SetDeletionLabel(Блокнот_Objest ДовідникОбєкт, bool label)
        {

        }

        public static void BeforeDelete(Блокнот_Objest ДовідникОбєкт)
        {

        }
    }

    class Номенклатура_Triggers
    {
        public static void New(Номенклатура_Objest ДовідникОбєкт)
        {
            ДовідникОбєкт.Код = (++НумераціяДовідників.Номенклатура_Const).ToString("D6");
        }

        public static void Copying(Номенклатура_Objest ДовідникОбєкт, Номенклатура_Objest Основа)
        {
            ДовідникОбєкт.Назва += " - Копія";
        }

        public static void BeforeSave(Номенклатура_Objest ДовідникОбєкт)
        {

        }

        public static void AfterSave(Номенклатура_Objest ДовідникОбєкт)
        {

        }

        public static void SetDeletionLabel(Номенклатура_Objest ДовідникОбєкт, bool label)
        {

        }

        public static void BeforeDelete(Номенклатура_Objest ДовідникОбєкт)
        {

        }
    }

    class Склад_Triggers
    {
        public static void New(Склад_Objest ДовідникОбєкт)
        {
            ДовідникОбєкт.Код = (++НумераціяДовідників.Склад_Const).ToString("D6");
        }

        public static void Copying(Склад_Objest ДовідникОбєкт, Склад_Objest Основа)
        {
            ДовідникОбєкт.Назва += " - Копія";
        }

        public static void BeforeSave(Склад_Objest ДовідникОбєкт)
        {

        }

        public static void AfterSave(Склад_Objest ДовідникОбєкт)
        {

        }

        public static void SetDeletionLabel(Склад_Objest ДовідникОбєкт, bool label)
        {

        }

        public static void BeforeDelete(Склад_Objest ДовідникОбєкт)
        {

        }
    }

}

namespace StorageAndTrade_1_0.Документи
{
    class ПоступленняТоварів_Triggers
    {
        public static void New(ПоступленняТоварів_Objest ДокументОбєкт)
        {
            ДокументОбєкт.НомерДок = (++НумераціяДокументів.ПоступленняТоварів_Const).ToString("D8");
            ДокументОбєкт.ДатаДок = DateTime.Now;
            ДокументОбєкт.Автор = Program.Користувач;
        }

        public static void Copying(ПоступленняТоварів_Objest ДокументОбєкт, ПоступленняТоварів_Objest Основа)
        {
            ДокументОбєкт.Назва += " - Копія";
        }

        public static void BeforeSave(ПоступленняТоварів_Objest ДокументОбєкт)
        {
            ДокументОбєкт.Назва = $"{ПоступленняТоварів_Const.FULLNAME} №{ДокументОбєкт.НомерДок} від {ДокументОбєкт.ДатаДок.ToShortDateString()}";
        }

        public static void AfterSave(ПоступленняТоварів_Objest ДокументОбєкт)
        {

        }

        public static void SetDeletionLabel(ПоступленняТоварів_Objest ДокументОбєкт, bool label)
        {

        }

        public static void BeforeDelete(ПоступленняТоварів_Objest ДокументОбєкт)
        {

        }
    }

    class ПродажТоварів_Triggers
    {
        public static void New(ПродажТоварів_Objest ДокументОбєкт)
        {
            ДокументОбєкт.НомерДок = (++НумераціяДокументів.ПродажТоварів_Const).ToString("D8");
            ДокументОбєкт.ДатаДок = DateTime.Now;
            ДокументОбєкт.Автор = Program.Користувач;
        }

        public static void Copying(ПродажТоварів_Objest ДокументОбєкт, ПродажТоварів_Objest Основа)
        {
            ДокументОбєкт.Назва += " - Копія";
        }

        public static void BeforeSave(ПродажТоварів_Objest ДокументОбєкт)
        {
            ДокументОбєкт.Назва = $"{ПродажТоварів_Const.FULLNAME} №{ДокументОбєкт.НомерДок} від {ДокументОбєкт.ДатаДок.ToShortDateString()}";
        }

        public static void AfterSave(ПродажТоварів_Objest ДокументОбєкт)
        {

        }

        public static void SetDeletionLabel(ПродажТоварів_Objest ДокументОбєкт, bool label)
        {

        }

        public static void BeforeDelete(ПродажТоварів_Objest ДокументОбєкт)
        {

        }
    }

}