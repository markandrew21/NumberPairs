//Code sample by Mark Luffred 
// markandrew21@gmail.com
// 315-632-0002


using System;

namespace CarPricer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                AssertCarValue(25313.40m, 35000m, 3 * 12, 50000, 1, 1);
                AssertCarValue(19688.20m, 35000m, 3 * 12, 150000, 1, 1);
                AssertCarValue(19688.20m, 35000m, 3 * 12, 250000, 1, 1);
                AssertCarValue(20090.00m, 35000m, 3 * 12, 250000, 1, 0);
                AssertCarValue(21657.02m, 35000m, 3 * 12, 250000, 0, 1);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }

        }

        private static void AssertCarValue(decimal expectValue, decimal purchaseValue, int ageInMonths, int numberOfMiles, int numberOfPreviousOwners, int numberOfCollisions)
        {
            Car car = new Car
            {
                AgeInMonths = ageInMonths,
                NumberOfCollisions = numberOfCollisions,
                NumberOfMiles = numberOfMiles,
                NumberOfPreviousOwners = numberOfPreviousOwners,
                PurchaseValue = purchaseValue
            };
            PriceDeterminator priceDeterminator = new PriceDeterminator();
            var carPrice = priceDeterminator.DetermineCarPrice(car);
            Console.WriteLine("Actual Car Value: $" + carPrice.ToString("F")); //Actual Car Value: $88888.88 
        }
    }

    public class Car
    {
        public decimal PurchaseValue { get; set; }

        public int AgeInMonths { get; set; }

        public int NumberOfMiles { get; set; }

        public int NumberOfPreviousOwners { get; set; }

        public int NumberOfCollisions { get; set; }
    }

    //    Each factor should be off of the result of the previous value in the order of
    //        1. AGE
    //        2. MILES
    //        3. PREVIOUS OWNER
    //        4. COLLISION
    //        
    //    E.g., Start with the current value of the car, then adjust for age, take that  
    //    result then adjust for miles, then collision, and finally previous owner. 
    //    Note that if previous owner, had a positive effect, then it should be applied 
    //    AFTER step 4. If a negative effect, then BEFORE step 4.
    
    public class PriceDeterminator
    {
        public decimal DetermineCarPrice(Car car)
        {
            double valueByAge = CalculatePriceByAge(car);
            double valueByMiles = CalculatePriceByMiles(valueByAge, car);
            double valueByOwner = CalculatePriceByPreviousOwners(valueByMiles, car);
            double valueByCollision = CalculatePriceByCollision(valueByOwner, car);
            return Convert.ToDecimal(valueByCollision);
        }

        //AGE: Given the number of months of how old the car is, reduce its value one-half (0.5) percent.
        //     After 10 years, it's value cannot be reduced further by age. This is not cumulative.
        
        public double CalculatePriceByAge(Car car)
        {
            double value = (double)car.PurchaseValue;
            if (car.AgeInMonths > 10 * 12)
            {
                value = value * (1 - 10 * 12 * 0.005);
            }
            else
            {
                value = value * (1 - car.AgeInMonths * 0.005);
            }

            return value;
        }

        //MILES: For every 1,000 miles on the car, reduce its value by one-fifth of a percent (0.2). 
        //       Do not consider remaining miles. After 150,000 miles, it's value cannot be reduced further by miles.

        public double CalculatePriceByMiles(double value, Car car)
        {
            if (car.NumberOfMiles > 150000)
            {
                value = value * (1 - 150000 / 1000 * 0.002);
            }
            else
            {
                value = value * (1 - car.NumberOfMiles / 1000 * 0.002);
            }

            return value;
        }

        //PREVIOUS OWNER: If the car has had more than 2 previous owners, reduce its value by twenty-five (25) percent. 
        //                If the car has had no previous owners, add ten (10) percent of the FINAL car value at the end.

        
        public double CalculatePriceByPreviousOwners(double value, Car car)
        {
            if (car.NumberOfPreviousOwners > 2)
            {
                value = value * 0.75;
            }
            if (car.NumberOfPreviousOwners == 0)
            {
                value = value * 1.1;
            }

            return value;
        }

        //COLLISION: For every reported collision the car has been in, 
        //           remove two (2) percent of it's value up to five (5) collisions.

        public double CalculatePriceByCollision(double value, Car car)
        {
            if (car.NumberOfCollisions > 5)
            {
                value = 0;
            }
            else
            {
                value = value * (1 - 0.2 * car.NumberOfCollisions);
            }

            return value;
        }
    }
}