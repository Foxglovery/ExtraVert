// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Net;
using System.Security.AccessControl;
using System.Threading.Channels;

List<Plant> plants = new List<Plant>()
{
    new Plant()
    {
        Species = "Darling Mums",
        LightNeeds = 4,
        AskingPrice = 30.00M,
        City = "Pensacola",
        ZIP = 35642,
        Sold = false,
        AvailableUntil = new DateTime(2024,10,5)
    },
    new Plant()
    {
        Species = "Corlingping",
        LightNeeds = 3,
        AskingPrice = 45.99M,
        City = "Shelvyblaum",
        ZIP = 35645,
        Sold = false,
        AvailableUntil = new DateTime(2024,2,12)
    },
    new Plant()
    {
        Species = "Darkwing Buck",
        LightNeeds = 1,
        AskingPrice = 30.00M,
        City = "Partank",
        ZIP = 35232,
        Sold = true,
        AvailableUntil = new DateTime(2024,6,10)
    },
    new Plant()
    {
        Species = "Flingplint",
        LightNeeds = 3,
        AskingPrice = 99.99M,
        City = "Florsham",
        ZIP = 35642,
        Sold = false,
        AvailableUntil = new DateTime(2023,12,1)
    },
    new Plant()
    {
        Species = "Dragmunt",
        LightNeeds = 5,
        AskingPrice = 300.00M,
        City = "Plenistrad",
        ZIP = 98675,
        Sold = false,
        AvailableUntil = new DateTime(2023,11,1)
    },
};
//stack overflow saved me here, this generates a random seed to generate the random number with
Plant availablePlant = null;
while (availablePlant == null)
{
    Random random = new Random(Guid.NewGuid().GetHashCode());
    int randomIndex = random.Next(1, plants.Count);
    if (!plants[randomIndex].Sold)
    {
        availablePlant = plants[randomIndex];
    }
}

string greeting = @"Welcome to ExtraVert
Your One Place to adopt and sell plants!";
Console.WriteLine(greeting);
string choice = null;
while (choice != "0")
{

    Console.WriteLine(@"Choose An Option:
0. Exit
1. Display All Plants
2. Post a Plant
3. Adopt a Plant
4. Remove Plant Listing
5. Plant of the Day
6. Find a Plant by Light Needs
7. View App Statistics
");

   
        choice = Console.ReadLine();

        if (choice == "0")
        {
            Console.WriteLine("So Long, Friend!");


        }
        else if (choice == "1")
        {
            ListAllPlants();
            Console.WriteLine("press any key to continue");
            Console.ReadKey();



        }
        else if (choice == "2")
        {
            PostAPlant();
            Console.WriteLine("press any key to continue");
            Console.ReadKey();
        }
        else if (choice == "3")
        {
            AdoptAPlant();
            Console.WriteLine("press any key to continue");
            Console.ReadKey();

        }
        else if (choice == "4")
        {
            DelistPlant();
            Console.WriteLine("press any key to continue");
            Console.ReadKey();
        }
        else if (choice == "5")
        {
            Console.WriteLine("The Plant of the day:");
            Console.WriteLine($"{availablePlant.Species} in {availablePlant.City} is available for ${availablePlant.AskingPrice}");
            Console.WriteLine("press any key to continue");
            Console.ReadKey();
        }
        else if (choice == "6")
        {
            FilterByLight();
            Console.WriteLine("press any key to continue");
            Console.ReadKey();

        }
        else if (choice == "7")
        {
            Console.WriteLine("ExtraVert Stats:");
            GenAppStats();
            Console.WriteLine("press any key to continue");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Please only type numbers in range.");

        }
    
    


}

void ListAllPlants()
{
    Console.WriteLine("Plants:");
    for (int i = 0; i < plants.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {plants[i].Species} in {plants[i].City} {(plants[i].Sold ? $"was sold for ${plants[i].AskingPrice}" : $"is available for ${plants[i].AskingPrice} until {plants[i].AvailableUntil}")}");
    }
}

void PostAPlant()
{
    Console.WriteLine("I'm going to ask you some planty questions now.");

    Console.WriteLine("Species?");
    string addSpecies = Console.ReadLine();
    while (string.IsNullOrEmpty(addSpecies))
    {
        Console.WriteLine("You didn't choose anything");
        addSpecies = Console.ReadLine();
    }

    Console.WriteLine("Light Needs?");
    int addLightNeeds = int.Parse(Console.ReadLine());

    Console.WriteLine("Asking Price");
    decimal addAskingPrice = decimal.Parse(Console.ReadLine());

    Console.WriteLine("City");
    string addCity = Console.ReadLine();

    Console.WriteLine("Zip Code");
    int addZIP = int.Parse(Console.ReadLine());

    Console.WriteLine(@"I will ask you for the Year, Month, and Day
    That this listing should expire.
    Use the format: (yyyy,mm,dd");
    int chosenYear, chosenMonth, chosenDay;
    DateTime availableUntil;
    while (true)
    {
        try
        {
            Console.WriteLine("Year?");
            chosenYear = int.Parse(Console.ReadLine());

            Console.WriteLine("Month?");
            chosenMonth = int.Parse(Console.ReadLine());

            Console.WriteLine("Day?");
            chosenDay = int.Parse(Console.ReadLine());
            
            availableUntil = new DateTime(chosenYear, chosenMonth, chosenDay);
            break;
            

        }
        catch (FormatException)
        {
            Console.WriteLine("That is not what we discussed. Only input numbers");
            return;
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("That is not what we discussed. Try a valid date.");
            return;
        }
    }
    
    
     



    Plant newPlant = new Plant()
    {
        Species = addSpecies,
        LightNeeds = addLightNeeds,
        AskingPrice = addAskingPrice,
        City = addCity,
        ZIP = addZIP,
        Sold = false,
        AvailableUntil = availableUntil
        };
    plants.Add(newPlant);
    Console.WriteLine("Plant added successfully");


}

//FIND A WAY TO FLIP SOLD TO TRUE

void AdoptAPlant()
{

    List<Plant> unsoldPlants = new List<Plant>();
    foreach (Plant plant in plants)
    {
        if (!plant.Sold && plant.AvailableUntil > DateTime.Now)
        {
            unsoldPlants.Add(plant);
        }
    }
    for (int i = 0; i < unsoldPlants.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {unsoldPlants[i].Species} in {unsoldPlants[i].City} {(unsoldPlants[i].Sold ? $"was sold for ${unsoldPlants[i].AskingPrice}" : $"is available for ${unsoldPlants[i].AskingPrice}")}");

    }
    Plant chosenPlant = null;
    while (chosenPlant == null)
    {
        Console.WriteLine("Enter the number of the plant you want to adopt.");
        try
        {
            int choice = int.Parse(Console.ReadLine().Trim());
            chosenPlant = unsoldPlants[choice - 1];
            Console.WriteLine($"You chose: {chosenPlant.Species}");
            chosenPlant.Sold = true;
        }
        catch (FormatException)
        {
            Console.WriteLine("please only input integers!");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Please choose an extant item, you fool");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Console.WriteLine("Do Better!");
        }
    }
}

void DelistPlant()
{
    ListAllPlants();
    Plant chosenPlant = null;
    while (chosenPlant == null)
    {
        Console.WriteLine("Please enter a plant number to Delist");
        try
        {
            int response = int.Parse(Console.ReadLine().Trim());
            chosenPlant = plants[response - 1];
            Console.WriteLine($"You Delisted {chosenPlant.Species}");
            plants.RemoveAt(response - 1);
        }
        catch (FormatException)
        {
            Console.WriteLine("Please only type integers!");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Please choose an extant item, you fool!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Console.WriteLine("Do better!");
        }
    }
}

void FilterByLight()
{
    List<Plant> wellLitPlants = new List<Plant>();
    int response;
    Console.WriteLine("On a scale of 1-5, how lit up is your place?");
    try
    {
        response = int.Parse(Console.ReadLine().Trim());
    }
    catch (FormatException)
    {
        Console.WriteLine("Please only input integers!!!");
        return;
    }

    foreach (Plant plant in plants)
    {
        if (plant.LightNeeds < response)
        {
            wellLitPlants.Add(plant);
        }
    }
    if (wellLitPlants.Count == 0)
    {
        Console.WriteLine("No Plants match your needs");
    }
    else
    {
        Console.WriteLine("Plants you can care for:");
        foreach (Plant plant in wellLitPlants)
        {
            Console.WriteLine($"{plant.Species}");
        }
    }
}
void GenAppStats()
{
    if (plants.Count == 0)
    {
        Console.WriteLine("There are no plants in this list. Add Some!");
        return;
    }

    Plant cheapestPlant = plants[0];
    foreach (Plant plant in plants)
    {
        if (plant.AskingPrice < cheapestPlant.AskingPrice)
        {
            cheapestPlant = plant;
        }
    }
    Console.WriteLine($"The cheapest plant is: {cheapestPlant.Species} in {cheapestPlant.City} at {cheapestPlant.AskingPrice}");

    List<Plant> AvailablePlants = new List<Plant>();

    foreach (Plant plant in plants)
    {
        if (!plant.Sold && plant.AvailableUntil > DateTime.Now)
        {
            AvailablePlants.Add(plant);
        }
    }
    Console.WriteLine($"# Available Plants: {AvailablePlants.Count}");

    Plant brightestPlant = plants[0];
    foreach (Plant plant in plants)
    {
        if (plant.LightNeeds > brightestPlant.LightNeeds)
        {
            brightestPlant = plant;
        }
    }
    Console.WriteLine($"The plant needing the most light: {brightestPlant.Species}");

    int totalLightNeeds = 0;
    foreach (Plant plant in plants)
    {
        totalLightNeeds += plant.LightNeeds;
    }

    double averagedLightNeeds = (double)totalLightNeeds / plants.Count;
    Console.WriteLine($"Average Light Needs:{averagedLightNeeds}");

    List<Plant> soldPlants = new List<Plant>();
    int percentageSold = 0
    foreach (Plant plant in plants)
    {
        if (plant.Sold)
        {
            soldPlants.Add(plant);
        }
    }
    percentageSold = soldPlants.Count / plants.Count * 100;
    Console.WriteLine($"{percentageSold}");

}

