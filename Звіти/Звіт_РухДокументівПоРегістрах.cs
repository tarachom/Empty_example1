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

using AccountingSoftware;
using StorageAndTrade_1_0;

namespace StorageAndTrade
{
    class Звіт_РухДокументівПоРегістрах : VBox
    {
        public Звіт_РухДокументівПоРегістрах() : base()
        {
            ShowAll();
        }

        public void CreateReport(DocumentPointer ДокументВказівник)
        {
            List<string> allowRegisterAccumulation = Config.Kernel!.Conf.Documents[ДокументВказівник.TypeDocument].AllowRegisterAccumulation;

            Dictionary<string, object> paramQuery = new Dictionary<string, object>();
            paramQuery.Add("ДокументВказівник", ДокументВказівник.UnigueID.UGuid);

            foreach (string regAccumName in allowRegisterAccumulation)
            {
                bool exist = false;
                string blockCaption = "";

                string[] columnsName = new string[] { };
                List<Dictionary<string, object>> listRow = new List<Dictionary<string, object>>();

                Dictionary<string, string> visibleColumn = new Dictionary<string, string>();
                Dictionary<string, string>? dataColumn = null;
                Dictionary<string, string>? typeColumn = null;
                Dictionary<string, float>? textAlignColumn = null;
                Dictionary<string, TreeCellDataFunc>? funcColumn = null;

                switch (regAccumName)
                {
                    case "Товари":
                        {
                            exist = true;
                            blockCaption = "Товари";

                            visibleColumn = РухДокументівПоРегістрах.Товари_ВидиміКолонки();
                            dataColumn = РухДокументівПоРегістрах.Товари_КолонкиДаних();
                            typeColumn = РухДокументівПоРегістрах.Товари_ТипиДаних();
                            textAlignColumn = РухДокументівПоРегістрах.Товари_ПозиціяТекстуВКолонці();
                            funcColumn = РухДокументівПоРегістрах.Товари_ФункціяДляКолонки();

                            Config.Kernel.DataBase.SelectRequest(РухДокументівПоРегістрах.Товари_Запит, paramQuery, out columnsName, out listRow);

                            break;
                        }
                    default:
                        {
                            exist = false;
                            break;
                        }
                }

                if (exist)
                {
                    ListStore listStore;
                    ФункціїДляЗвітів.СтворитиМодельДаних(out listStore, columnsName);

                    TreeView treeView = new TreeView(listStore);
                    treeView.ButtonPressEvent += ФункціїДляЗвітів.OpenPageDirectoryOrDocument;

                    ФункціїДляЗвітів.СтворитиКолонкиДляДерева(treeView, columnsName, visibleColumn, dataColumn, typeColumn, textAlignColumn, funcColumn);
                    ФункціїДляЗвітів.ЗаповнитиМодельДаними(listStore, columnsName, listRow);

                    WriteBlock(blockCaption, treeView);
                }
            }
        }

        void WriteBlock(string blockName, TreeView treeView)
        {
            VBox vBox = new VBox();

            Expander expander = new Expander(blockName) { Expanded = true };
            expander.Add(vBox);

            HBox hBox = new HBox();
            vBox.PackStart(hBox, false, false, 10);

            hBox.PackStart(treeView, false, false, 10);

            PackStart(expander, false, false, 10);

            ShowAll();
        }

    }
}