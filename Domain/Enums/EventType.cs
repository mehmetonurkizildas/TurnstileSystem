using System.ComponentModel;

namespace System
{
    public enum EventType
    {
        [Description("Giriş Yaptı")]
        Login = 1,
        [Description("Çıkış Yaptı")]
        Logout = 2,
    }
}
