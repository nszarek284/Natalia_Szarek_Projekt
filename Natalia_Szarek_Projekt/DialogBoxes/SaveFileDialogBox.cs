using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natalia_Szarek_Projekt.DialogBoxes
{
    public class SaveFileDialogBox : FileDialogBox
    {
        public SaveFileDialogBox()
        {
            fileDialog = new Microsoft.Win32.SaveFileDialog();
        }
    }
}
