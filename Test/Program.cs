// See https://aka.ms/new-console-template for more information
using Domain.Adapter.State;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

State state = new State();



string str = "{\r\n  \"connected\":1,\r\n  \"version\":\"1.0.0\",\r\n  \"mac\":\"MAC地址\"\r\n}\r\n";


var ss=JsonDocument.Parse(str);
var s=JsonConvert.SerializeObject(state, Formatting.None);

Console.WriteLine("Hello, World!");