using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace CommonCore.Websockets
{
    public class ChatPage : BoundPage<AppViewModel>
    {
        public ChatPage()
        {
            var lst = new CoreListView()
            {
                RowHeight = 100,
                HasUnevenRows=true,
                ItemTemplate = new MessageTemplateSelector(),
                SeparatorColor = Color.Transparent,
                SeparatorVisibility= SeparatorVisibility.None,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            lst.SetBinding(CoreListView.ScrollIndexProperty, "MessageViewIndex");
            lst.SetBinding(ListView.ItemsSourceProperty, "Messages");

            var entry = new Entry()
            {
                Placeholder="Message",
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            entry.SetBinding(Entry.TextProperty, "TextMessage");

            var send = new Button()
            {
                Text="Send"
            };
            send.SetBinding(Button.CommandProperty, "SendMessage");

            var bottomPanel = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children={entry,send}
            };

            Content = new StackLayout()
            {
                Children = { lst, bottomPanel }
            };
        }
    }
}
