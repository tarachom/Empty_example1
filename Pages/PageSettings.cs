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

Налаштування

*/

using Gtk;

using AccountingSoftware;
using StorageAndTrade_1_0;
using Константи = StorageAndTrade_1_0.Константи;
using Перелічення = StorageAndTrade_1_0.Перелічення;

namespace StorageAndTrade
{
    class PageSettings : VBox
    {

        #region Const

        //
        //ЖурналиДокументів
        //

        ComboBoxText ОсновнийТипПеріоду_ДляЖурналівДокументів = new ComboBoxText();

        #endregion

        public PageSettings() : base()
        {
            //Кнопки
            HBox hBox = new HBox();

            Button bSave = new Button("Зберегти");
            bSave.Clicked += OnSaveClick;

            hBox.PackStart(bSave, false, false, 10);

            PackStart(hBox, false, false, 10);

            FillComboBoxes();

            HPaned hPaned = new HPaned() { BorderWidth = 5, Position = 500 };

            CreatePack1(hPaned);
            CreatePack2(hPaned);

            PackStart(hPaned, false, false, 5);

            ShowAll();
        }

        void FillComboBoxes()
        {
            foreach (ConfigurationEnumField field in Config.Kernel!.Conf.Enums["ТипПеріодуДляЖурналівДокументів"].Fields.Values)
                ОсновнийТипПеріоду_ДляЖурналівДокументів.Append(field.Name, field.Desc);
        }

        void CreatePack1(HPaned hPaned)
        {
            VBox vBox = new VBox();

            CreateDefaultBlock(vBox);

            vBox.PackStart(new Separator(Orientation.Horizontal), false, false, 10);

            CreateJournalBlock(vBox);

            vBox.PackStart(new Separator(Orientation.Horizontal), false, false, 10);

            CreateLinkBlock(vBox);

            hPaned.Pack1(vBox, false, false);
        }

        void CreatePack2(HPaned hPaned)
        {
            VBox vBox = new VBox();
            hPaned.Pack2(vBox, false, false);


        }

        //Значення за замовчування
        void CreateDefaultBlock(VBox vBoxTop)
        {
            VBox vBox = new VBox();

            Expander expanderConstDefault = new Expander("Значення за замовчуванням") { Expanded = true };
            expanderConstDefault.Add(vBox);

            //Info
            HBox hBoxInfo = new HBox() { Halign = Align.Start };
            vBox.PackStart(hBoxInfo, false, false, 15);
            hBoxInfo.PackStart(new Label("Для заповненння нових документів та довідників"), false, false, 5);

            

            vBoxTop.PackStart(expanderConstDefault, false, false, 10);
        }

        void CreateJournalBlock(VBox vBoxTop)
        {
            VBox vBox = new VBox();

            Expander expander = new Expander("Журнали документів") { Expanded = true };
            expander.Add(vBox);

            //Controls
            AddCaptionAndControl(vBox, new Label("Період для журналів документів:"), ОсновнийТипПеріоду_ДляЖурналівДокументів);

            vBoxTop.PackStart(expander, false, false, 10);
        }

        void CreateLinkBlock(VBox vBoxTop)
        {
            VBox vBox = new VBox();

            Expander expander = new Expander("Додатково") { Expanded = true };
            expander.Add(vBox);

            
            vBoxTop.PackStart(expander, false, false, 0);
        }

        void AddPointerControl(VBox vBox, Widget wgPointerControl)
        {
            HBox hBox = new HBox() { Halign = Align.End };
            vBox.PackStart(hBox, false, false, 5);

            hBox.PackStart(wgPointerControl, false, false, 5);
        }

        void AddCaptionAndControl(VBox vBox, Widget wgCaption, Widget wgControl)
        {
            HBox hBox = new HBox() { Halign = Align.Start };
            vBox.PackStart(hBox, false, false, 5);

            hBox.PackStart(wgCaption, false, false, 5);
            hBox.PackStart(wgControl, false, false, 5);
        }

        void AddControl(VBox vBox, Widget wgControl)
        {
            HBox hBox = new HBox() { Halign = Align.Start };
            vBox.PackStart(hBox, false, false, 5);

            hBox.PackStart(wgControl, false, false, 5);
        }

        void AddLink(VBox vbox, string uri, EventHandler? clickAction = null)
        {
            LinkButton lb = new LinkButton(uri, " " + uri) { Halign = Align.Start, Image = new Image(AppContext.BaseDirectory + "images/doc.png"), AlwaysShowImage = true };
            vbox.PackStart(lb, false, false, 0);

            if (clickAction != null)
                lb.Clicked += clickAction;
        }

        public void SetValue()
        {
            
            //
            //ЖурналиДокументів
            //

            {
                ОсновнийТипПеріоду_ДляЖурналівДокументів.ActiveId = Константи.ЖурналиДокументів.ОсновнийТипПеріоду_Const.ToString();

                if (ОсновнийТипПеріоду_ДляЖурналівДокументів.Active == -1)
                    ОсновнийТипПеріоду_ДляЖурналівДокументів.ActiveId = Перелічення.ТипПеріодуДляЖурналівДокументів.ВесьПеріод.ToString();
            }

        }

        void GetValue()
        {
            
            //
            //ЖурналиДокументів
            //

            Константи.ЖурналиДокументів.ОсновнийТипПеріоду_Const = Enum.Parse<Перелічення.ТипПеріодуДляЖурналівДокументів>(ОсновнийТипПеріоду_ДляЖурналівДокументів.ActiveId);
        }

        void OnSaveClick(object? sender, EventArgs args)
        {
            GetValue();
        }
    }
}