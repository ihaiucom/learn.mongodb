using ET;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

public class Test
{
    public static void InsertOnce()
    {
        // start-insert-one
        // Generates a new restaurant document
        Restaurant newRestaurant = new()
        {
            Name = "Mongo's Pizza",
            RestaurantId = "12345",
            Cuisine = "Pizza",
            Address = new()
            {
                Street = "Pizza St",
                ZipCode = "10003"
            },
            Borough = "Manhattan",
        };
        
        MongoHelp.I.InsertOnce(newRestaurant);
    }

    public static async Task Insert10()
    {
        for (int i = 0; i < 10; i++)
        {
            await InsertOnceAsync($"ZF {i}");
        }
    }
    
    public static async Task InsertOnceAsync(string Name =  "Mongo's Pizza")
    {
        // start-insert-one
        // Generates a new restaurant document
        Restaurant newRestaurant = new()
        {
            Name = Name,
            RestaurantId = "12345",
            Cuisine = "Pizza",
            Address = new()
            {
                Street = "Pizza St",
                ZipCode = "10003"
            },
            Borough = "Manhattan",
        };
        
        await MongoHelp.I.InsertOneAsync(newRestaurant);
    }

    public static async Task InsertManyAsync()
    {
        var restaurantsList = GenerateDocuments();
        await MongoHelp.I.InsertManyAsync(restaurantsList);
    }

    private static List<Restaurant> GenerateDocuments()
    {
        // Generates 5 new restaurant documents
        var restaurantsList = new List<Restaurant>();
        for (int i = 1; i <= 5; i++)
        {
            Restaurant newRestaurant = new()
            {
                Name = "Mongo's Pizza",
                RestaurantId = $"12345-{i}",
                Cuisine = "Pizza",
                Address = new()
                {
                    Street = "Pizza St",
                    ZipCode = "10003"
                },
                Borough = "Manhattan",
            };

            restaurantsList.Add(newRestaurant);
        }

        return restaurantsList;
    }

    public static void DeleteOne()
    {
        // start-delete-one
        // Generates a filter to find the restaurant
        FilterDefinition<Restaurant> filter = Builders<Restaurant>.Filter.Eq(r => r.Name, "Mongo's Pizza");
        
        // Deletes the restaurant
        var result = MongoHelp.I.DeleteOne(filter);
        Console.WriteLine($"DeleteOne: DeletedCount= {result.DeletedCount} restaurant, IsAcknowledged:{result.IsAcknowledged}");
    }
    public static void DeleteOne2()
    {
       
        var result = MongoHelp.I.DeleteOne2<Restaurant>(d => d.Name == "Mongo's Pizza");
        Console.WriteLine($"DeleteOne2: DeletedCount= {result.DeletedCount} restaurant, IsAcknowledged:{result.IsAcknowledged}");
    }
    
    public static async Task DeleteOneAsync()
    {
        var result = await MongoHelp.I.DeleteOneAsync<Restaurant>(d => d.Name == "Mongo's Pizza");
        Console.WriteLine($"DeleteOneAsync: DeletedCount= {result.DeletedCount} restaurant, IsAcknowledged:{result.IsAcknowledged}");
    }
    
    public static async Task DeleteManyAsync()
    {
        var result = await MongoHelp.I.DeleteManyAsync<Restaurant>(d => d.Name == "Mongo's Pizza");
        Console.WriteLine($"DeleteOneAsync: DeletedCount= {result.DeletedCount} restaurant, IsAcknowledged:{result.IsAcknowledged}");
    }
    
    public static async Task DeleteManyAsync2()
    {
        var result = await MongoHelp.I.DeleteManyAsync<Restaurant>(d => true);
        Console.WriteLine($"DeleteOneAsync: DeletedCount= {result.DeletedCount} restaurant, IsAcknowledged:{result.IsAcknowledged}");
    }

    public static async Task QueryAsync()
    {
        // var one = await  MongoHelp.I.QueryOneAsync<Restaurant>(d => d.Name == "Mongo's Pizza");
        // Console.WriteLine(one);
        // Console.WriteLine(one == null);
        // Console.WriteLine("-------------#QueryOneAsync: ");
        // Console.WriteLine(one.ToBsonDocument());
        
        
        // var list = await  MongoHelp.I.QueryAsync<Restaurant>(d=> d.Name.Contains("ZF"));
        // Console.WriteLine("-------------#QueryAsync: ");
        // Console.WriteLine($"Count: {list.Count}");
        // foreach (var item in list)
        // {
        //     Console.WriteLine(item.ToBsonDocument());
        // }
        
        // var list = await  MongoHelp.I.QueryJsonAsync<Restaurant>("{Name:/ZF/}");
        // Console.WriteLine("-------------#QueryJsonAsync: ");
        // Console.WriteLine($"Count: {list.Count}");
        // foreach (var item in list)
        // {
        //     Console.WriteLine(item.ToBsonDocument());
        // }
    }

    public static async Task UpdateOneAsync()
    {
        var filter = Builders<Restaurant>.Filter.Eq(r => r.Name, "Mongo's Pizza");
        var update = Builders<Restaurant>.Update.Set(r => r.Name, "ABC").Set(r => r.Cuisine, "ZZZ");
        var result = await MongoHelp.I.UpdateOneAsync(d => d.Name == "Mongo's Pizza", update);
        Console.WriteLine($"UpdateOneAsync: ModifiedCount= {result.ModifiedCount} restaurant, IsAcknowledged:{result.IsAcknowledged}, UpsertedId：{result.UpsertedId.ToBsonDocument()}");
    }
    public static async Task UpdateManyAsync()
    {
        var update = Builders<Restaurant>.Update.Set(r => r.Name, "AAA").Set(r => r.Cuisine, "ZZZ");
        var result = await MongoHelp.I.UpdateManyAsync(d => d.Name == "ABC", update);
        Console.WriteLine($"UpdateOneAsync: ModifiedCount= {result.ModifiedCount} restaurant, IsAcknowledged:{result.IsAcknowledged}, UpsertedId：{result.UpsertedId.ToBsonDocument()}");
    }

    public static async Task TaskReplaceOneAsync()
    {
        var one = await  MongoHelp.I.QueryOneAsync<Restaurant>(d=> true);
        Console.WriteLine("-------------#QueryOneAsync: ");
        Console.WriteLine(one.ToBsonDocument());
        var oldId = one.Id;
        
        Restaurant newRestaurant = new()
        {
            Id = oldId,
            Name = one.Name,
            RestaurantId = "2222",
            Cuisine = "YYYYY",
            Address = new()
            {
                Street = "AAAAA",
                ZipCode = "BBBB"
            },
            Borough = "CCCC",
        };
        
        var result = await MongoHelp.I.ReplaceOneAsync(d => d.Id == oldId, newRestaurant, false);
        Console.WriteLine($"UpdateOneAsync: ModifiedCount= {result.ModifiedCount} restaurant, IsAcknowledged:{result.IsAcknowledged}, UpsertedId：{result.UpsertedId.ToBsonDocument()}");
    }
}

public class Restaurant
{
    public ObjectId Id { get; set; }

    public string Name { get; set; }

    [BsonElement("restaurant_id")]
    public string RestaurantId { get; set; }

    public string Cuisine { get; set; }

    public Address Address { get; set; }

    public string Borough { get; set; }

    public List<GradeEntry> Grades { get; set; }
}

public class Address
{
    public string Building { get; set; }

    [BsonElement("coord")]
    public double[] Coordinates { get; set; }

    public string Street { get; set; }

    [BsonElement("zipcode")]
    public string ZipCode { get; set; }
}

public class GradeEntry
{
    public DateTime Date { get; set; }

    public string Grade { get; set; }

    public float? Score { get; set; }
}