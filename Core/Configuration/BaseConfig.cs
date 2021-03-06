using Newtonsoft.Json;

namespace Core.Configuration
{
    /// <summary>
    /// The root of TPP-Core-Configuration.
    /// Contains all configurations shared between all modes.
    /// </summary>
    // properties need setters for deserialization
    // ReSharper disable AutoPropertyCanBeMadeGetOnly.Local
    public sealed class BaseConfig : ConfigBase
    {
        [JsonProperty("$schema")] public static string Schema { get; private set; } = "./config.schema.json";

        /* directory under which log files will be created. null for no log files. */
        public string? LogPath { get; private set; } = null;

        /* connection details for mongodb */
        public string MongoDbConnectionUri { get; private set; } = "mongodb://localhost:27017/?replicaSet=rs0";
        public string MongoDbDatabaseName { get; private set; } = "tpp3";

        public ChatConfig Chat { get; private set; } = new ChatConfig();

        /* currency amounts for brand new users (new entries in the database) */
        public int StartingPokeyen { get; private set; } = 100;
        public int StartingTokens { get; private set; } = 0;
    }
}
