using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace packing_plant_manager
{
    //klasa od poszukiwania sumatry lub adobe reader XI
    class FindProgram
    {
        public string findPDFprogram(string pdf_name)
        {
            List<string> list_disk = new List<string>();
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach(DriveInfo disk in allDrives)
            {
                list_disk.Add(disk.Name);
            }

            string fullName = "Not found";
            for (int i = 0; i < list_disk.Count; i++) {
                if (Directory.Exists(list_disk[i] + "\\Program Files\\" + pdf_name))
                {
                    fullName = list_disk[i] + @"Program Files\" + pdf_name;
                    break;
                }
                else if (Directory.Exists(list_disk[i] + "\\Program Files (x86)\\" + pdf_name))
                {
                    fullName = list_disk[i] + @"Program Files (x86)\" + pdf_name;
                    break;
                }
                else
                    fullName = "Brak programu na tym dysku w domyślnej ścieżce";
        }
            return fullName;
        }
    }
}
