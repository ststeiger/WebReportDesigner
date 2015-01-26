using System;
using System.Collections.Generic;
using System.Web;

namespace WebReportDesigner
{


    public class SecTools
    {


        //[System.Runtime.InteropServices.DllImport ("libc")]
        //public static extern uint getuid ();

        bool IsAdministrator
        {
            get
            {
                Mono.Unix.Native.Group[] groups =
                Mono.Unix.Native.Syscall.getgrouplist("username");

                // groups[0].
                // groups[0].gr_gid
                // groups[0].gr_passwd
                // groups[0].gr_name

                if (Environment.OSVersion.Platform == PlatformID.Unix)
                {
                    // Mono.Posix assembly:
                    return (Mono.Unix.Native.Syscall.getuid() == 0);
                    // Console.WriteLine("I'm running as root!");
                } // End if(Environment.OSVersion.Platform == PlatformID.Unix)

                System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
                System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);

                return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
            }

        } // End Property IsAdministrator



    }
}