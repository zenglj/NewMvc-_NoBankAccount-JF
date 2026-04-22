using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Web.Mvc;

public class CustomJsonResult : JsonResult
{
    public override void ExecuteResult(ControllerContext context)
    {
        var response = context.HttpContext.Response;
        response.ContentType = "application/json";

        if (Data != null)
        {
            // 使用 IsoDateTimeConverter 将 DateTime 序列化为 ISO 8601 格式
            // 你也可以自定义格式，例如：DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
            var isoConverter = new IsoDateTimeConverter();
            string json = JsonConvert.SerializeObject(Data, isoConverter);
            response.Write(json);
        }
    }
}