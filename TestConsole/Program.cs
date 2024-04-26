

using App.Business.Concrete;
using App.DataAccess.Abstract;

internal class Program
{
    private CarService carService;

    public Program(CarService carService)
    {
        this.carService = carService;
    }

    private void Main(string[] args)
    {
        foreach (var item in carService.GetAll())
        {
            Console.WriteLine(item);
        }
    }
}