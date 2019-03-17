using System;

public class AnalogTwo
{
    public AnalogTwo()
	{
	}
    public List<CurrentCar> GetCurrentCar()
    {
        RandomObject roj = new RandomObject();
        List<CurrentCar> currentCars = new List<CurrentCar>();
        CurrentCar cCar = new CurrentCar();
        cCar.carNUben = "粤T" + roj.RandCode(5);
        cCar.entelTime = DateTime.Now.ToString();
        Thread.Sleep(3000);
        cCar.outTime = DateTime.Now.ToString();
        cCar.Asite = "1";
        cCar.Bsite = "2";
        currentCars.ADD(cCar);
    }
}
