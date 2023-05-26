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

using StorageAndTrade_1_0;
using Константи = StorageAndTrade_1_0.Константи;
using StorageAndTrade_1_0.Довідники;
using StorageAndTrade_1_0.РегістриНакопичення;

namespace StorageAndTrade
{
    class Звіт_Товари : VBox
    {
        Notebook reportNotebook;

        #region Filters

        DateTimeControl ДатаПочатокПеріоду = new DateTimeControl() { OnlyDate = true, Value = DateTime.Parse($"01.{DateTime.Now.Month}.{DateTime.Now.Year}") };
        DateTimeControl ДатаКінецьПеріоду = new DateTimeControl() { OnlyDate = true, Value = DateTime.Now };

        Номенклатура_PointerControl Номенклатура = new Номенклатура_PointerControl();
        Склад_PointerControl Склад = new Склад_PointerControl();

        struct ПараметриФільтр
        {
            public DateTime ДатаПочатокПеріоду;
            public DateTime ДатаКінецьПеріоду;
            public Номенклатура_Pointer Номенклатура;
            public Склад_Pointer Склад;
        }

        #endregion

        public Звіт_Товари() : base()
        {
            //Кнопки
            HBox hBoxTop = new HBox();
            PackStart(hBoxTop, false, false, 10);

            //2
            Button bOstatok = new Button("Залишки");
            bOstatok.Clicked += OnReport_Залишки;

            hBoxTop.PackStart(bOstatok, false, false, 10);

            //3
            Button bOborot = new Button("Залишки та обороти");
            bOborot.Clicked += OnReport_ЗалишкиТаОбороти;

            hBoxTop.PackStart(bOborot, false, false, 10);

            //4
            Button bDocuments = new Button("Документи");
            bDocuments.Clicked += OnReport_Документи;

            hBoxTop.PackStart(bDocuments, false, false, 10);

            CreateFilters();

            reportNotebook = new Notebook() { Scrollable = true, EnablePopup = true, BorderWidth = 0, ShowBorder = false };
            reportNotebook.TabPos = PositionType.Top;
            PackStart(reportNotebook, true, true, 0);

            ShowAll();
        }

        #region Filters

        void CreateFilters()
        {
            HBox hBoxContainer = new HBox();

            Expander expander = new Expander("Відбори") { Expanded = true };
            expander.Add(hBoxContainer);

            //Container1
            VBox vBoxContainer1 = new VBox() { WidthRequest = 500 };
            hBoxContainer.PackStart(vBoxContainer1, false, false, 5);

            CreateContainer1(vBoxContainer1);

            //Container2
            VBox vBoxContainer2 = new VBox() { WidthRequest = 500 };
            hBoxContainer.PackStart(vBoxContainer2, false, false, 5);

            CreateContainer2(vBoxContainer2);

            PackStart(expander, false, false, 10);
        }

        void CreateContainer1(VBox vBox)
        {
            //Період
            HBox hBoxPeriod = new HBox() { Halign = Align.End };
            hBoxPeriod.PackStart(new Label("Період з "), false, false, 5);
            hBoxPeriod.PackStart(ДатаПочатокПеріоду, false, false, 5);
            hBoxPeriod.PackStart(new Label(" по "), false, false, 5);
            hBoxPeriod.PackStart(ДатаКінецьПеріоду, false, false, 5);
            vBox.PackStart(hBoxPeriod, false, false, 5);

            //Номенклатура
            HBox hBoxNomenklatura = new HBox() { Halign = Align.End };
            vBox.PackStart(hBoxNomenklatura, false, false, 5);

            hBoxNomenklatura.PackStart(Номенклатура, false, false, 5);
        }

        void CreateContainer2(VBox vBox)
        {

        }

        #endregion

        ПараметриФільтр СформуватиФільтр()
        {
            return new ПараметриФільтр()
            {
                ДатаПочатокПеріоду = ДатаПочатокПеріоду.ПочатокДня(),
                ДатаКінецьПеріоду = ДатаКінецьПеріоду.КінецьДня(),
                Номенклатура = Номенклатура.Pointer,
                Склад = Склад.Pointer
            };
        }

        HBox ВідобразитиФільтр(string typeReport, ПараметриФільтр Фільтр)
        {
            HBox hBoxCaption = new HBox();

            string text = "";

            switch (typeReport)
            {
                case "Залишки":
                    {
                        text += "Без періоду; ";
                        break;
                    }
                case "ЗалишкиТаОбороти":
                case "Документи":
                    {
                        text += "З <b>" +
                            Фільтр.ДатаПочатокПеріоду.ToString("dd.MM.yyyy") + "</b> по <b>" +
                            Фільтр.ДатаКінецьПеріоду.ToString("dd.MM.yyyy") + "</b>; ";
                        break;
                    }
            }

            if (!Фільтр.Номенклатура.IsEmpty())
                text += "Номенклатура: <b>" + Фільтр.Номенклатура.GetPresentation() + "</b>; ";

            if (!Фільтр.Склад.IsEmpty())
                text += "Склад: <b>" + Фільтр.Склад.GetPresentation() + "</b>; ";

            hBoxCaption.PackStart(new Label(text) { Wrap = true, UseMarkup = true }, false, false, 2);

            return hBoxCaption;
        }

        void OnReport_Залишки(object? sender, EventArgs args)
        {
            Залишки(СформуватиФільтр());
        }

        void OnReport_ЗалишкиТаОбороти(object? sender, EventArgs args)
        {
            ЗалишкиТаОбороти(СформуватиФільтр());
        }

        void OnReport_Документи(object? sender, EventArgs args)
        {
            Документи(СформуватиФільтр());
        }

        void Залишки(object? Параметри, bool refreshPage = false)
        {
            ПараметриФільтр Фільтр = Параметри != null ? (ПараметриФільтр)Параметри : new ПараметриФільтр();

            #region SELECT

            bool isExistParent = false;

            string query = $@"
SELECT 
    Товари.{Товари_Залишки_TablePart.Номенклатура} AS Номенклатура, 
    Довідник_Номенклатура.{Номенклатура_Const.Назва} AS Номенклатура_Назва, 
    Товари.{Товари_Залишки_TablePart.Склад} AS Склад, 
    Довідник_Склад.{Склад_Const.Назва} AS Склад_Назва, 
    ROUND(SUM(Товари.{Товари_Залишки_TablePart.Кількість}), 2) AS Кількість,
    ROUND(SUM(Товари.{Товари_Залишки_TablePart.Сума}), 2) AS Сума
FROM 
    {Товари_Залишки_TablePart.TABLE} AS Товари

    LEFT JOIN {Номенклатура_Const.TABLE} AS Довідник_Номенклатура ON Довідник_Номенклатура.uid = 
        Товари.{Товари_Залишки_TablePart.Номенклатура}

    LEFT JOIN {Склад_Const.TABLE} AS Довідник_Склад ON Довідник_Склад.uid = 
        Товари.{Товари_Залишки_TablePart.Склад}
";
            #region WHERE

            //Відбір по вибраному елементу Номенклатура
            if (!Фільтр.Номенклатура.IsEmpty())
            {
                query += isExistParent ? "AND" : "WHERE";
                isExistParent = true;

                query += $@"
Довідник_Номенклатура.uid = '{Фільтр.Номенклатура.UnigueID}'
";
            }

            //Відбір по вибраному елементу Склад
            if (!Фільтр.Склад.IsEmpty())
            {
                query += isExistParent ? "AND" : "WHERE";
                isExistParent = true;

                query += $@"
Довідник_Склад.uid = '{Фільтр.Склад.UnigueID}'
";
            }

            #endregion

            query += $@"
GROUP BY Номенклатура, Номенклатура_Назва, 
         Склад, Склад_Назва

HAVING
    SUM(Товари.{Товари_Залишки_TablePart.Кількість}) != 0 OR
    SUM(Товари.{Товари_Залишки_TablePart.Сума}) != 0

ORDER BY Номенклатура_Назва
";
            #endregion

            Dictionary<string, string> ВидиміКолонки = new Dictionary<string, string>();
            ВидиміКолонки.Add("Номенклатура_Назва", "Номенклатура");
            ВидиміКолонки.Add("Склад_Назва", "Склад");
            ВидиміКолонки.Add("Кількість", "Кількість");
            ВидиміКолонки.Add("Сума", "Сума");

            Dictionary<string, string> КолонкиДаних = new Dictionary<string, string>();
            КолонкиДаних.Add("Номенклатура_Назва", "Номенклатура");
            КолонкиДаних.Add("Склад_Назва", "Склад");

            Dictionary<string, string> ТипиДаних = new Dictionary<string, string>();
            ТипиДаних.Add("Номенклатура_Назва", Номенклатура_Const.POINTER);
            ТипиДаних.Add("Склад_Назва", Склад_Const.POINTER);

            Dictionary<string, float> ПозиціяТекстуВКолонці = new Dictionary<string, float>();
            ПозиціяТекстуВКолонці.Add("Кількість", 1);
            ПозиціяТекстуВКолонці.Add("Сума", 1);

            Dictionary<string, TreeCellDataFunc> ФункціяДляКолонки = new Dictionary<string, TreeCellDataFunc>();
            ФункціяДляКолонки.Add("Кількість", ФункціїДляЗвітів.ФункціяДляКолонкиВідємнеЧислоЧервоним);
            ФункціяДляКолонки.Add("Сума", ФункціїДляЗвітів.ФункціяДляКолонкиВідємнеЧислоЧервоним);

            Dictionary<string, object> paramQuery = new Dictionary<string, object>();

            string[] columnsName;
            List<Dictionary<string, object>> listRow;

            Config.Kernel!.DataBase.SelectRequest(query, paramQuery, out columnsName, out listRow);

            ListStore listStore;
            ФункціїДляЗвітів.СтворитиМодельДаних(out listStore, columnsName);

            TreeView treeView = new TreeView(listStore);
            treeView.ButtonPressEvent += ФункціїДляЗвітів.OpenPageDirectoryOrDocument;

            ФункціїДляЗвітів.СтворитиКолонкиДляДерева(treeView, columnsName, ВидиміКолонки, КолонкиДаних, ТипиДаних, ПозиціяТекстуВКолонці, ФункціяДляКолонки);
            ФункціїДляЗвітів.ЗаповнитиМодельДаними(listStore, columnsName, listRow);

            ФункціїДляЗвітів.CreateReportNotebookPage(reportNotebook, "Залишки", ВідобразитиФільтр("Залишки", Фільтр), treeView, Залишки, Фільтр, refreshPage);
        }

        void ЗалишкиТаОбороти(object? Параметри, bool refreshPage = false)
        {
            ПараметриФільтр Фільтр = Параметри != null ? (ПараметриФільтр)Параметри : new ПараметриФільтр();

            #region SELECT

            bool isExistParent = false;

            string query = $@"
WITH 
ПочатковийЗалишок AS
(
    SELECT 
        Товари.{Товари_Залишки_TablePart.Номенклатура} AS Номенклатура, 
        Товари.{Товари_Залишки_TablePart.Склад} AS Склад, 
        SUM(Товари.{Товари_Залишки_TablePart.Кількість}) AS Кількість
    FROM 
        {Товари_Залишки_TablePart.TABLE} AS Товари
    WHERE
        Товари.{Товари_Залишки_TablePart.Період} < @ПочатокПеріоду
    GROUP BY Номенклатура, Склад
),
ЗалишкиТаОборотиЗаПеріод AS
(
    SELECT 
        Товари.{Товари_ЗалишкиТаОбороти_TablePart.Номенклатура} AS Номенклатура,
        Товари.{Товари_ЗалишкиТаОбороти_TablePart.Склад} AS Склад,
        SUM(Товари.{Товари_ЗалишкиТаОбороти_TablePart.КількістьПрихід}) AS КількістьПрихід,
        SUM(Товари.{Товари_ЗалишкиТаОбороти_TablePart.КількістьРозхід}) AS КількістьРозхід,
        SUM(Товари.{Товари_ЗалишкиТаОбороти_TablePart.КількістьЗалишок}) AS КількістьЗалишок
    FROM 
        {Товари_ЗалишкиТаОбороти_TablePart.TABLE} AS Товари
    WHERE
        Товари.{Товари_ЗалишкиТаОбороти_TablePart.Період} >= @ПочатокПеріоду AND
        Товари.{Товари_ЗалишкиТаОбороти_TablePart.Період} <= @КінецьПеріоду
    GROUP BY Номенклатура, Склад
),
КінцевийЗалишок AS
(
    SELECT 
        Номенклатура,
        Склад,
        Кількість
    FROM ПочатковийЗалишок

    UNION ALL

    SELECT
        Номенклатура,
        Склад,
        КількістьЗалишок
    FROM ЗалишкиТаОборотиЗаПеріод
)
SELECT 
    Номенклатура,
    Довідник_Номенклатура.{Номенклатура_Const.Назва} AS Номенклатура_Назва,
    Склад,
    Довідник_Склад.{Склад_Const.Назва} AS Склад_Назва,
    ROUND(SUM(ПочатковийЗалишок), 2) AS ПочатковийЗалишок,
    ROUND(SUM(Прихід), 2) AS Прихід,
    ROUND(SUM(Розхід), 2) AS Розхід,
    ROUND(SUM(КінцевийЗалишок), 2) AS КінцевийЗалишок
FROM 
(
    SELECT 
        Номенклатура,
        Склад,
        Кількість AS ПочатковийЗалишок,
        0 AS Прихід,
        0 AS Розхід,
        0 AS КінцевийЗалишок
    FROM ПочатковийЗалишок

    UNION ALL

    SELECT
        Номенклатура,
        Склад,
        0 AS ПочатковийЗалишок,
        КількістьПрихід AS Прихід,
        КількістьРозхід AS Розхід,
        0 
    FROM ЗалишкиТаОборотиЗаПеріод

    UNION ALL

    SELECT 
        Номенклатура,
        Склад,
        0 AS ПочатковийЗалишок,
        0 AS Прихід,
        0 AS Розхід,
        Кількість
    FROM КінцевийЗалишок
) AS ЗалишкиТаОбороти
LEFT JOIN {Номенклатура_Const.TABLE} AS Довідник_Номенклатура ON Довідник_Номенклатура.uid = ЗалишкиТаОбороти.Номенклатура
LEFT JOIN {Склад_Const.TABLE} AS Довідник_Склад ON Довідник_Склад.uid = ЗалишкиТаОбороти.Склад
";

            #region WHERE

            //Відбір по вибраному елементу Номенклатура
            if (!Фільтр.Номенклатура.IsEmpty())
            {
                query += isExistParent ? "AND" : "WHERE";
                isExistParent = true;

                query += $@"
Довідник_Номенклатура.uid = '{Фільтр.Номенклатура.UnigueID}'
";
            }

            //Відбір по вибраному елементу Склад
            if (!Фільтр.Склад.IsEmpty())
            {
                query += isExistParent ? "AND" : "WHERE";
                isExistParent = true;

                query += $@"
Довідник_Склад.uid = '{Фільтр.Склад.UnigueID}'
";
            }

            #endregion

            query += @"
GROUP BY Номенклатура, Номенклатура_Назва, 
         Склад, Склад_Назва

HAVING SUM(Прихід) != 0 OR SUM(Розхід) != 0

ORDER BY Номенклатура_Назва
";

            #endregion

            Dictionary<string, string> ВидиміКолонки = new Dictionary<string, string>();
            ВидиміКолонки.Add("Номенклатура_Назва", "Номенклатура");
            ВидиміКолонки.Add("Склад_Назва", "Склад");
            ВидиміКолонки.Add("ПочатковийЗалишок", "На початок");
            ВидиміКолонки.Add("Прихід", "Прихід");
            ВидиміКолонки.Add("Розхід", "Розхід");
            ВидиміКолонки.Add("КінцевийЗалишок", "На кінець");

            Dictionary<string, string> КолонкиДаних = new Dictionary<string, string>();
            КолонкиДаних.Add("Номенклатура_Назва", "Номенклатура");
            КолонкиДаних.Add("Склад_Назва", "Склад");

            Dictionary<string, string> ТипиДаних = new Dictionary<string, string>();
            ТипиДаних.Add("Номенклатура_Назва", Номенклатура_Const.POINTER);
            ТипиДаних.Add("Склад_Назва", Склад_Const.POINTER);

            Dictionary<string, float> ПозиціяТекстуВКолонці = new Dictionary<string, float>();
            ПозиціяТекстуВКолонці.Add("ПочатковийЗалишок", 1);
            ПозиціяТекстуВКолонці.Add("Прихід", 1);
            ПозиціяТекстуВКолонці.Add("Розхід", 1);
            ПозиціяТекстуВКолонці.Add("КінцевийЗалишок", 1);

            Dictionary<string, TreeCellDataFunc> ФункціяДляКолонки = new Dictionary<string, TreeCellDataFunc>();
            ФункціяДляКолонки.Add("ПочатковийЗалишок", ФункціїДляЗвітів.ФункціяДляКолонкиВідємнеЧислоЧервоним);
            ФункціяДляКолонки.Add("Прихід", ФункціїДляЗвітів.ФункціяДляКолонкиВідємнеЧислоЧервоним);
            ФункціяДляКолонки.Add("Розхід", ФункціїДляЗвітів.ФункціяДляКолонкиВідємнеЧислоЧервоним);
            ФункціяДляКолонки.Add("КінцевийЗалишок", ФункціїДляЗвітів.ФункціяДляКолонкиВідємнеЧислоЧервоним);

            Dictionary<string, object> paramQuery = new Dictionary<string, object>();
            paramQuery.Add("ПочатокПеріоду", Фільтр.ДатаПочатокПеріоду);
            paramQuery.Add("КінецьПеріоду", Фільтр.ДатаКінецьПеріоду);

            string[] columnsName;
            List<Dictionary<string, object>> listRow;

            Config.Kernel!.DataBase.SelectRequest(query, paramQuery, out columnsName, out listRow);

            ListStore listStore;
            ФункціїДляЗвітів.СтворитиМодельДаних(out listStore, columnsName);

            TreeView treeView = new TreeView(listStore);
            treeView.ButtonPressEvent += ФункціїДляЗвітів.OpenPageDirectoryOrDocument;

            ФункціїДляЗвітів.СтворитиКолонкиДляДерева(treeView, columnsName, ВидиміКолонки, КолонкиДаних, ТипиДаних, ПозиціяТекстуВКолонці, ФункціяДляКолонки);
            ФункціїДляЗвітів.ЗаповнитиМодельДаними(listStore, columnsName, listRow);

            ФункціїДляЗвітів.CreateReportNotebookPage(reportNotebook, "Залишки та обороти", ВідобразитиФільтр("ЗалишкиТаОбороти", Фільтр), treeView, ЗалишкиТаОбороти, Фільтр, refreshPage);
        }

        void Документи(object? Параметри, bool refreshPage = false)
        {
            ПараметриФільтр Фільтр = Параметри != null ? (ПараметриФільтр)Параметри : new ПараметриФільтр();

            #region SELECT

            bool isExistParent = false;

            string query = $@"
WITH register AS
(
     SELECT 
        Товари.period AS period,
        Товари.owner AS owner,
        Товари.income AS income,
        Товари.{Товари_Const.Номенклатура} AS Номенклатура,
        Товари.{Товари_Const.Склад} AS Склад,
        Товари.{Товари_Const.Кількість} AS Кількість,
        Товари.{Товари_Const.Сума} AS Сума
    FROM
        {Товари_Const.TABLE} AS Товари
    WHERE
        (Товари.period >= @ПочатокПеріоду AND Товари.period <= @КінецьПеріоду)
";

            #region WHERE

            isExistParent = true;

            //Відбір по вибраному елементу Номенклатура
            if (!Фільтр.Номенклатура.IsEmpty())
            {
                query += isExistParent ? "AND" : "WHERE";
                isExistParent = true;

                query += $@"
Товари.{Товари_Const.Номенклатура} = '{Фільтр.Номенклатура.UnigueID}'
";
            }

            //Відбір по вибраному елементу Склад
            if (!Фільтр.Склад.IsEmpty())
            {
                query += isExistParent ? "AND" : "WHERE";
                isExistParent = true;

                query += $@"
Товари.{Товари_Const.Склад} = '{Фільтр.Склад.UnigueID}'
";
            }

            #endregion

            query += $@"
),
documents AS
(";
            int counter = 0;
            foreach (string table in Товари_Const.AllowDocumentSpendTable)
            {
                string docType = Товари_Const.AllowDocumentSpendType[counter];

                string union = (counter > 0 ? "UNION" : "");
                query += $@"
{union}
SELECT 
    '{docType}' AS doctype,
    {table}.uid, 
    {table}.docname, 
    register.period, 
    register.income, 
    register.Кількість,
    register.Сума,
    register.Номенклатура,
    Довідник_Номенклатура.{Номенклатура_Const.Назва} AS Номенклатура_Назва,
    register.Склад,
    Довідник_Склад.{Склад_Const.Назва} AS Склад_Назва
FROM register INNER JOIN {table} ON {table}.uid = register.owner
    LEFT JOIN {Номенклатура_Const.TABLE} AS Довідник_Номенклатура ON Довідник_Номенклатура.uid = register.Номенклатура
    LEFT JOIN {Склад_Const.TABLE} AS Довідник_Склад ON Довідник_Склад.uid = register.Склад
";
                counter++;
            }

            query += $@"
)
SELECT 
    CONCAT(uid, ':', doctype) AS uid_and_text,
    uid,
    period,
    (CASE WHEN income = true THEN '+' ELSE '-' END) AS income,
    docname AS Документ,  
    Номенклатура,
    Номенклатура_Назва,
    Склад,
    Склад_Назва,
    Кількість,
    Сума
FROM documents
ORDER BY period ASC
";

            #endregion

            Dictionary<string, string> ВидиміКолонки = new Dictionary<string, string>();
            ВидиміКолонки.Add("income", "Рух");
            ВидиміКолонки.Add("Документ", "Документ");
            ВидиміКолонки.Add("Номенклатура_Назва", "Номенклатура");
            ВидиміКолонки.Add("Склад_Назва", "Склад");
            ВидиміКолонки.Add("Кількість", "Кількість");
            ВидиміКолонки.Add("Сума", "Сума");

            Dictionary<string, string> КолонкиДаних = new Dictionary<string, string>();
            КолонкиДаних.Add("Документ", "uid_and_text");
            КолонкиДаних.Add("Номенклатура_Назва", "Номенклатура");
            КолонкиДаних.Add("Склад_Назва", "Склад");

            Dictionary<string, string> ТипиДаних = new Dictionary<string, string>();
            ТипиДаних.Add("Документ", "Документи.*");
            ТипиДаних.Add("Номенклатура_Назва", Номенклатура_Const.POINTER);
            ТипиДаних.Add("Склад_Назва", Склад_Const.POINTER);

            Dictionary<string, float> ПозиціяТекстуВКолонці = new Dictionary<string, float>();
            ПозиціяТекстуВКолонці.Add("income", 0.5f);
            ПозиціяТекстуВКолонці.Add("Кількість", 1);
            ПозиціяТекстуВКолонці.Add("Сума", 1);

            Dictionary<string, TreeCellDataFunc> ФункціяДляКолонки = new Dictionary<string, TreeCellDataFunc>();
            ФункціяДляКолонки.Add("Кількість", ФункціїДляЗвітів.ФункціяДляКолонкиВідємнеЧислоЧервоним);
            ФункціяДляКолонки.Add("Сума", ФункціїДляЗвітів.ФункціяДляКолонкиВідємнеЧислоЧервоним);

            Dictionary<string, object> paramQuery = new Dictionary<string, object>();
            paramQuery.Add("ПочатокПеріоду", Фільтр.ДатаПочатокПеріоду);
            paramQuery.Add("КінецьПеріоду", Фільтр.ДатаКінецьПеріоду);

            string[] columnsName;
            List<Dictionary<string, object>> listRow;

            Config.Kernel!.DataBase.SelectRequest(query, paramQuery, out columnsName, out listRow);

            ListStore listStore;
            ФункціїДляЗвітів.СтворитиМодельДаних(out listStore, columnsName);

            TreeView treeView = new TreeView(listStore);
            treeView.ButtonPressEvent += ФункціїДляЗвітів.OpenPageDirectoryOrDocument;

            ФункціїДляЗвітів.СтворитиКолонкиДляДерева(treeView, columnsName, ВидиміКолонки, КолонкиДаних, ТипиДаних, ПозиціяТекстуВКолонці, ФункціяДляКолонки);
            ФункціїДляЗвітів.ЗаповнитиМодельДаними(listStore, columnsName, listRow);

            ФункціїДляЗвітів.CreateReportNotebookPage(reportNotebook, "Документи", ВідобразитиФільтр("Документи", Фільтр), treeView, Документи, Фільтр, refreshPage);
        }


    }
}