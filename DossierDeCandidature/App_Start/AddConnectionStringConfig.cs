using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace DossierDeCandidature.App_Start
{
    public class ConnectionStringConfig
    {
        public static void SetPathVariables()
        {
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = Path.GetDirectoryName(executable);
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
        }
    }
}