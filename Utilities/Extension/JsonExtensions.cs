using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extension
{

    public class JsonExtensions
    {
        public static readonly JsonSerializerSettings SerializerSettings = new()
        {
            //设置首字母小写
            ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),
            //忽略循环引用
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            //设置时间格式
            //DateFormatString = "yyyy-MM-dd HH:mm:ss"
        };
    }

}
