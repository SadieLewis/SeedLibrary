using SeedLibrary.Models;

namespace SeedLibrary.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SeedContext context)
        {
            context.Database.EnsureCreated();

            if (context.SeedPackets.Any())
            {
                return;
            }

            var commonNames = new CommonName[]
            {
                new CommonName { Name = "Arugula" },
                new CommonName { Name = "Artichoke" },
                new CommonName { Name = "Bean" },
                new CommonName { Name = "Asparagus" },
                new CommonName { Name = "Beet" },
                new CommonName { Name = "Bok Choy" },
                new CommonName { Name = "Broccoli" },
                new CommonName { Name = "Carrot" }
            };
            context.CommonNames.AddRange(commonNames);
            context.SaveChanges();

            var varieties = new Variety[]
            {
                new Variety { VarietyName = "Common", Depth = "1/4 inch", CommonNameName = "Arugula" },
                new Variety { VarietyName = "Green Globe", Depth = "1/4 inch", CommonNameName = "Artichoke" },
                new Variety { VarietyName = "Calima", Depth = "1/4 inch", CommonNameName = "Bean" },
                new Variety { VarietyName = "Mary Washington", Depth = "1/4 inch", CommonNameName = "Asparagus" },
                new Variety { VarietyName = "Detroit Dark Red", Depth = "1/4 inch", CommonNameName = "Beet" },
                new Variety { VarietyName = "Green Sprouting Calabrese", Depth = "1/4 inch", CommonNameName = "Bok Choy" },
                new Variety { VarietyName = "Catskill", Depth = "1/4 inch", CommonNameName = "Broccoli" },
                new Variety { VarietyName = "Red Cored Chantenay", Depth = "1/4 inch", CommonNameName = "Carrot" }
            };
            context.Varieties.AddRange(varieties);
            context.SaveChanges();

            var sources = new Source[]
            {
                new Source { Id = 1, SourceName = "Cool Farms" },
                new Source { Id = 2, SourceName = "Rad Farms" },
                new Source { Id = 3, SourceName = "Rich Farms" },
                new Source { Id = 4, SourceName = "A Farm" }
            };
            context.Sources.AddRange(sources);
            context.SaveChanges();

            var dates = new PlantingDate[]
            {
                new PlantingDate { Id = 1, StartMonth = 1, EndMonth = 3},
                new PlantingDate { Id = 3, StartMonth = 3, EndMonth = 6},
                new PlantingDate { Id = 2, StartMonth = 9, EndMonth = 10 }
            };
            context.PlantingDates.AddRange(dates);
            context.SaveChanges();

            var seedPackets = new SeedPacket[]
            {
                new SeedPacket { SeedId = 1, Count = 3, Note = "Succession-plant for continued harvest.", VarietyName = "Common"},
                new SeedPacket { SeedId = 2, Count = 3, Note = "Succession-plant for continued harvest.", VarietyName = "Green Globe"},
                new SeedPacket { SeedId = 3, Count = 3, Note = "Succession-plant for continued harvest.", VarietyName = "Calima"},
                new SeedPacket { SeedId = 4, Count = 3, Note = "Succession-plant for continued harvest.", VarietyName = "Mary Washington"},
                new SeedPacket { SeedId = 5, Count = 3, Note = "Succession-plant for continued harvest.", VarietyName = "Detroit Dark Red"},
                new SeedPacket { SeedId = 6, Count = 3, Note = "Succession-plant for continued harvest.", VarietyName = "Green Sprouting Calabrese"},
                new SeedPacket { SeedId = 7, Count = 3, Note = "Succession-plant for continued harvest.", VarietyName = "Catskill"},
                new SeedPacket { SeedId = 8, Count = 3, Note = "Succession-plant for continued harvest.", VarietyName = "Red Cored Chantenay"}
            };
            context.SeedPackets.AddRange(seedPackets);
            context.SaveChanges();

            var donations = new Donation[]
            {
                new Donation { SourceId = 1, SeedId = 1, Year = 2023 },
                new Donation { SourceId = 1, SeedId = 2, Year = 2021 },
                new Donation { SourceId = 2, SeedId = 3, Year = 2022 },
                new Donation { SourceId = 2, SeedId = 4, Year = 2024 },
                new Donation { SourceId = 1, SeedId = 5, Year = 2024 },
                new Donation { SourceId = 1, SeedId = 6, Year = 2025 },
                new Donation { SourceId = 3, SeedId = 7, Year = 2025 },
                new Donation { SourceId = 4, SeedId = 8, Year = 2020 }
            };
            context.Donations.AddRange(donations);
            context.SaveChanges();

            var growings = new Growing[]
            {
                new Growing { PlantingDatesId = 1, SeedId = 1 },
                new Growing { PlantingDatesId = 1, SeedId = 2 },
                new Growing { PlantingDatesId = 1, SeedId = 3 },
                new Growing { PlantingDatesId = 1, SeedId = 4 },
                new Growing { PlantingDatesId = 1, SeedId = 5 },
                new Growing { PlantingDatesId = 2, SeedId = 6 },
                new Growing { PlantingDatesId = 2, SeedId = 7 },
                new Growing { PlantingDatesId = 3, SeedId = 8 }
            };
            context.Growings.AddRange(growings);
            context.SaveChanges();
        }
    }
}