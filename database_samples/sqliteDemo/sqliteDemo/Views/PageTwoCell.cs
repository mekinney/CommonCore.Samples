using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace sqliteDemo
{
    public class PageTwoCell: ViewCell
    {
        private Label lblName;
        private Label lblPhone;

        public PageTwoCell()
        {
            try
            {
                lblName = new Label()
                {
                    FontSize = 12
                };
                lblPhone = new Label()
                {
                    TextColor = Color.Gray,
                    FontSize = 10
                };


                ContextActions.Add(new MenuItem()
                {
                    Text = "Delete",
                    IsDestructive = true,
                    Command = new Command((obj) =>
                    {
                        var p = (Person) this.BindingContext;
                        CoreDependencyService.SendViewModelMessage<AppViewModel>(CoreSettings.DeletePersonTag, p.Id);
                    })
                });

                View = new StackLayout()
                {
                    Padding = new Thickness(10, 5, 0, 5),
                    Children = {lblName, lblPhone}
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected override void OnBindingContextChanged()
        {
            if(BindingContext!=null){
                var p = (Person)this.BindingContext;
                lblName.Text = $"{p.FirstName} {p.LastName}";

                long ph;
                var parseResult = long.TryParse(p.PhoneNumber,out ph);
                var phoneDisplay = parseResult ? string.Format("{0:(###) ###-####}", ph) : "Invalid Phone Number";
                lblPhone.Text = phoneDisplay;
            }
            base.OnBindingContextChanged();
        }
    }
}
