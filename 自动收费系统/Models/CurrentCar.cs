using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 自动收费系统.Models
{
    public class CurrentCar
    {

        int carId { get; set; }
        string carNUben { get; set; }
        string entelTime { get; set; }
        string outTime { get; set; }
        int ASite { get; set; }
        int BSite { get; set; }
        int Carsum { get; set; }
    }
}