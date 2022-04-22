namespace DocuStorage.Helpers;

[Flags]
public enum Roles
{
    Root = 0,
    Admin = 1,
    Manager = 2,
    User = 4,
}

