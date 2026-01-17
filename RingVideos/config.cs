using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoOrganizer
{
    internal class Config
    {
        public string Root { get; set; }
        public List<Site> Sites { get; set; }
    }

    internal class Site
    {
        public string Name { get; set; }
        public List<Device> Devices { get; set; }
    }

    internal class Device
    {
        public string Name { get; set; }

    }

}
