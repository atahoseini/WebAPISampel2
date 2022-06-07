namespace OnlineShope.Core.Entities.Security
{
    public class Permission
    {
        public int Id { get; set; }
        public string PermisionFlag { get; set; }
        public string Title { get; set; }
        public int MyProperty { get; set; }
        public int PermisionGroupId { get; set; }
        public virtual PermisionGroup PermisionGroup { get; set; }


    }


}
