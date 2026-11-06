namespace vrms_api.Models
{
    public class RoleModel
    {
        public string Name { get; set; }
        public string Note { get; set; }
    }
    public class RoleAddModel:RoleModel
    {

    }
    public class RoleEditModel : RoleModel
    {
        public int Id { get; set; }
    }
    public class RoleViewModel : RoleEditModel { }
    public class RoleListModel : RoleEditModel { }
    public class UpdateRolePermissionsModel
    {
    }
    public class RolePermissionsViewModel
    {
    }
}
