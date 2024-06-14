using TravelAgency.Domain.Models;

namespace TravelAgency.Infrastructure
{
    public class DataSeeder
    {
        private readonly DatabaseContext _dbContext;
        private readonly string _dataFilePath;

        public DataSeeder(DatabaseContext _dbContext)
        {
            this._dbContext = _dbContext;

            var baseDirectory = AppContext.BaseDirectory;
            var projectRoot = Directory.GetParent(baseDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
            _dataFilePath = Path.Combine(projectRoot, "TravelAgency.Infrastructure", "Data", "hotel_names.txt");
        }
        public void Seed()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();

            if (!_dbContext.Database.CanConnect())
            {
                return;
            }

            var Spain = new Country("Spain");
            var France = new Country("France");
            var Italy = new Country("Italy");
            var Portugal = new Country("Portugal");
            var Poland = new Country("Poland");
            var Germany = new Country("Germany");
            var Austria = new Country("Austria");
            var Belgium = new Country("Belgium");
            var Turkey = new Country("Turkey");
            var England = new Country("England");
            var Ireland = new Country("Ireland");

            var Countries = new List<Country>()
                {
                    Spain, France, Italy, Portugal, Germany, Poland, Austria, Belgium, Turkey,  England, Ireland
                };

            var Cities = new List<City>()
                {
                    new City("Barcelona", Spain),
                    new City("Valencia", Spain),
                    new City("Madryt", Spain),
                    new City("Cracow", Poland),
                    new City("Warsaw", Poland),
                    new City("Sopot", Poland),
                    new City("Milan", Italy ),
                    new City("Rome", Italy ),
                    new City("Venice", Italy),
                    new City("Aveiro", Portugal),
                    new City("Porto", Portugal),
                    new City("Lisboa", Portugal),
                    new City("Paris", France),
                    new City("Lyon", France),
                    new City("Nicea", France),
                    new City("Berlin", Germany),
                    new City("Munich", Germany),
                    new City("Bonn", Germany),
                    new City("Vienna", Austria),
                    new City("Graz", Austria),
                    new City("Salzburg", Austria),
                    new City("Antalya", Turkey),
                    new City("Istanbul", Turkey),
                    new City("Ankara", Turkey),
                    new City("London", England),
                    new City("Nottingham", England),
                    new City("Southampton", England),
                    new City("Dublin", Ireland),
                    new City("Galway", Ireland),
                    new City("Cork", Ireland),
                    new City("Brussels", Belgium),
                    new City("Charleroi", Belgium),
                    new City("Antwerp", Belgium),
            };

            var Hotels = new List<Hotel>();

            if (!File.Exists(_dataFilePath))
            {
                throw new FileNotFoundException($"Data file not found at {_dataFilePath}");
            }


            using (StreamReader sr = File.OpenText(_dataFilePath))
            {
                int i = 0;
                int max = 10;

                string? s = sr.ReadLine();
                while ((s != null) & (i++ < max))
                {
                    Random random = new Random();
                    var index = random.Next(Cities.Count);
                    var hotel = new Hotel()
                    {
                        Name = s,
                        Rate = random.Next(1, 6),
                        City = Cities[index],
                    };
                    Hotels.Add(hotel);
                    s = sr.ReadLine();
                }
            }

            for (int i = 0; i < 20; i++)
            {
                Random random = new Random();
                // To
                var indexHotel = random.Next(Hotels.Count);
                // From
                var indexCity = random.Next(Cities.Count);
                var dateFrom = DateTime.Now.AddDays(random.Next(10, 30));
                var days = random.Next(5, 14);
                var price = random.Next(300, 700) * days;

                var offer = new Offer()
                {
                    Hotel = Hotels[indexHotel],
                    Price = price,
                    DateFrom = dateFrom,
                    DateTo = dateFrom.AddDays(days),
                    DepartureCity = Cities[indexCity],
                };
                _dbContext.Offer.Add(offer);
            }

            _dbContext.SaveChanges();

        }
    }
}
