using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;
namespace CommonCore.Websockets
{
    public class MessageTemplateSelector : Xamarin.Forms.DataTemplateSelector
    {
        private readonly DataTemplate incomingDataTemplate;
        private readonly DataTemplate outgoingDataTemplate;

        public MessageTemplateSelector()
        {
            // Retain instances!
            this.incomingDataTemplate = new DataTemplate(typeof(IncomingMessage));
            this.outgoingDataTemplate = new DataTemplate(typeof(OutgoingMessage));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var messageVm = item as Message;
            if (messageVm == null)
                return null;
            return messageVm.IsIncoming ? this.incomingDataTemplate : this.outgoingDataTemplate;
        }


    }

    public abstract class MessageViewCell: ViewCell
    {
        protected Label lbl;
        protected Label header;
        protected Frame frame;
        protected StackLayout contentPanel;

        public MessageViewCell()
        {
            Height = 100;

            header = new Label()
            {
                TextColor = Color.DarkGray,
                FontSize = 10
            };

            lbl = new Label() { 
                TextColor = Color.Black, 
                FontSize = 14 
            };

            frame = new Frame()
            { 
                HasShadow = true,
                CornerRadius = 3,
                WidthRequest = AppSettings.ScreenSize.Width * 0.55,
                VerticalOptions = LayoutOptions.Center,
                Content = new StackLayout(){
                    Children = { lbl }
                }
            };

            contentPanel = new StackLayout()
            {
                Children={header,frame}
            };

            SetViewContent();

        }

        protected override void OnBindingContextChanged()
        {
            if (BindingContext != null)
            {
                var msg = this.BindingContext as Message;
                header.Text = $"{msg.Name}        {msg.MessagDateTime.ToShortTimeString()}";
                lbl.Text = $"{msg.Text}";
                frame.BackgroundColor = msg.IsIncoming ? Color.LightGreen : Color.FromHex("#e4e4e4");
            }
        
            base.OnBindingContextChanged();
        }

        protected abstract void SetViewContent();
    }

    public class OutgoingMessage : MessageViewCell
    {
        protected override void SetViewContent()
        {
            frame.BackgroundColor = Color.FromHex("#e4e4e4");

            View = new StackLayout()
            {
                Margin = 5,
                Orientation = StackOrientation.Horizontal,
                Children = {
                    contentPanel,
                    new StackLayout(){
                        WidthRequest = AppSettings.ScreenSize.Width * 0.40,
                        HorizontalOptions = LayoutOptions.EndAndExpand
                    } }
            };
        }
    }
    public class IncomingMessage : MessageViewCell
    {
        protected override void SetViewContent()
        {
            frame.BackgroundColor = Color.FromHex("#e4e4e4");

            View = new StackLayout()
            {
                Margin = 5,
                Orientation = StackOrientation.Horizontal,
                Children = {
                    new StackLayout(){
                        WidthRequest = AppSettings.ScreenSize.Width * 0.40,
                        HorizontalOptions = LayoutOptions.EndAndExpand
                    }, 
                    contentPanel }
            };
        }
    }
}
