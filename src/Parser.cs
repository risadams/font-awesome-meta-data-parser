using Newtonsoft.Json;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;

namespace font_awesome_meta_data_parser;

public class Parser
{

  private static readonly Dictionary<string, List<string>> iconList = new Dictionary<string, List<string>>();

  public static async Task Main(string[] args)
  {
    Console.WriteLine("FontAwesome category Parser");

    if (args.Length != 1)
    {
      Console.WriteLine("ERR: Must specify path to categories.yml");
      return;
    }

    var path = args[0];
    var meta = await File.ReadAllTextAsync(path);
    var yaml = new YamlStream();
    yaml.Load(new StringReader(meta));

    var deserailizer = new DeserializerBuilder().Build();
    var yamlObject   = deserailizer.Deserialize(new StringReader(meta));

    var serializer = new SerializerBuilder()
                     .JsonCompatible()
                     .Build();

    var json = serializer.Serialize(yamlObject);

    var cats = JsonConvert.DeserializeObject(json);
    await File.WriteAllTextAsync("out.json", cats?.ToString());
  }
}
