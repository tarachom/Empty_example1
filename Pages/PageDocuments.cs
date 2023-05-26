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

Документи

*/

using Gtk;

using AccountingSoftware;

using StorageAndTrade_1_0;
using StorageAndTrade_1_0.Документи;

namespace StorageAndTrade
{
    class PageDocuments : VBox
    {
        public PageDocuments() : base()
        {
            //Всі Документи
            {
                HBox hBoxAll = new HBox(false, 0);
                PackStart(hBoxAll, false, false, 10);

                Expander expanderAll = new Expander("Всі документи");
                hBoxAll.PackStart(expanderAll, false, false, 5);

                VBox vBoxAll = new VBox(false, 0);
                expanderAll.Add(vBoxAll);

                vBoxAll.PackStart(new Label("Документи"), false, false, 2);

                ListBox listBox = new ListBox();
                listBox.ButtonPressEvent += (object? sender, ButtonPressEventArgs args) =>
                {
                    if (args.Event.Type == Gdk.EventType.DoubleButtonPress && listBox.SelectedRows.Length != 0)
                        ФункціїДляДокументів.ВідкритиДокументВідповідноДоВиду(listBox.SelectedRows[0].Name, new UnigueID(), 0, false);
                };

                ScrolledWindow scrollList = new ScrolledWindow() { WidthRequest = 300, HeightRequest = 300, ShadowType = ShadowType.In };
                scrollList.SetPolicy(PolicyType.Automatic, PolicyType.Automatic);
                scrollList.Add(listBox);

                vBoxAll.PackStart(scrollList, false, false, 2);

                foreach (KeyValuePair<string, ConfigurationDocuments> documents in Config.Kernel!.Conf.Documents)
                {
                    string title = String.IsNullOrEmpty(documents.Value.FullName) ? documents.Value.Name : documents.Value.FullName;

                    ListBoxRow row = new ListBoxRow() { Name = documents.Key };
                    row.Add(new Label(title) { Halign = Align.Start });

                    listBox.Add(row);
                }
            }

            //Список
            HBox hBoxList = new HBox(false, 0);
            PackStart(hBoxList, false, false, 10);

            VBox vLeft = new VBox(false, 0);
            hBoxList.PackStart(vLeft, false, false, 5);

            Link.AddLink(vLeft, $"{ПоступленняТоварів_Const.FULLNAME}", () =>
            {
                Program.GeneralForm?.CreateNotebookPage($"{ПоступленняТоварів_Const.FULLNAME}", () =>
                {
                    ПоступленняТоварів page = new ПоступленняТоварів();
                    page.LoadRecords();
                    return page;
                });
            });

            Link.AddLink(vLeft, $"{ПродажТоварів_Const.FULLNAME}", () =>
            {
                Program.GeneralForm?.CreateNotebookPage($"{ПродажТоварів_Const.FULLNAME}", () =>
                {
                    ПродажТоварів page = new ПродажТоварів();
                    page.LoadRecords();
                    return page;
                });
            });

            ShowAll();
        }
    }
}