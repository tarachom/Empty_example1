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

using Gtk;

using Константи = StorageAndTrade_1_0.Константи;
using StorageAndTrade_1_0.Довідники;
using StorageAndTrade_1_0.Документи;
using StorageAndTrade_1_0.РегістриНакопичення;

namespace StorageAndTrade
{
    class РухДокументівПоРегістрах
    {
        #region Товари

        public static Dictionary<string, string> Товари_ВидиміКолонки()
        {
            Dictionary<string, string> columns = new Dictionary<string, string>();

            columns.Add("income", "Рух");
            columns.Add("period", "Період");
            columns.Add("Номенклатура_Назва", "Номенклатура");
            columns.Add("Кількість", "Кількість");
            columns.Add("Сума", "Сума");

            return columns;
        }

        public static Dictionary<string, string> Товари_КолонкиДаних()
        {
            Dictionary<string, string> columns = new Dictionary<string, string>();

            columns.Add("Номенклатура_Назва", "Номенклатура");

            return columns;
        }

        public static Dictionary<string, string> Товари_ТипиДаних()
        {
            Dictionary<string, string> columns = new Dictionary<string, string>();

            columns.Add("Номенклатура_Назва", Номенклатура_Const.POINTER);

            return columns;
        }

        public static Dictionary<string, float> Товари_ПозиціяТекстуВКолонці()
        {
            Dictionary<string, float> columns = new Dictionary<string, float>();

            columns.Add("income", 0.5f);
            columns.Add("Кількість", 1);
            columns.Add("Сума", 1);

            return columns;
        }

        public static Dictionary<string, TreeCellDataFunc> Товари_ФункціяДляКолонки()
        {
            Dictionary<string, TreeCellDataFunc> columns = new Dictionary<string, TreeCellDataFunc>();

            columns.Add("Кількість", ФункціїДляЗвітів.ФункціяДляКолонкиБазоваДляЧисла);
            columns.Add("Сума", ФункціїДляЗвітів.ФункціяДляКолонкиБазоваДляЧисла);

            return columns;
        }

        public static string Товари_Запит = $@"
SELECT 
    (CASE WHEN Рег_Товари.income = true THEN '+' ELSE '-' END) AS income,
    Рег_Товари.period, 
    Рег_Товари.{Товари_Const.Номенклатура} AS Номенклатура, 
    Довідник_Номенклатура.{Номенклатура_Const.Назва} AS Номенклатура_Назва, 
    Рег_Товари.{Товари_Const.Кількість} AS Кількість,
    Рег_Товари.{Товари_Const.Сума} AS Сума
FROM 
    {Товари_Const.TABLE} AS Рег_Товари

    LEFT JOIN {Номенклатура_Const.TABLE} AS Довідник_Номенклатура ON Довідник_Номенклатура.uid = 
        Рег_Товари.{Товари_Const.Номенклатура}

WHERE
    Рег_Товари.Owner = @ДокументВказівник
ORDER BY Номенклатура_Назва
";

        #endregion
    }
}