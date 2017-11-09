using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace todo.mobile
{
    public class Todo: SqlDataModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public long CompleteByDate { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }
    }
}
