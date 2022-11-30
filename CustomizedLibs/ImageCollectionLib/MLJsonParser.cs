using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace ImageCollectionLib
{
    public class MLJsonParser
    {
        const string KEY_ID = "identifier";
        const string KEY_SN = "serialnumber";
        const string KEY_ML_RESPONSE = "ml_response";
        const string KEY_IMAGE_NAME = "image_name";
        const string KEY_DECISION = "decision";

        public static List<(string sn, string contents, string decision)> Parse(JObject jsonRoot)
        {
            string sn;
            string id = string.Empty;
            string contents = string.Empty;
            string decision = string.Empty;

            List<(string sn, string contents, string decision)> judgeList = new List<(string sn, string contents, string decision)>();


            sn = jsonRoot[KEY_SN]?.ToString();

            if (jsonRoot.ContainsKey(KEY_ID))
                id = jsonRoot[KEY_ID]?.ToString();

            JToken[] response = jsonRoot[KEY_ML_RESPONSE].ToObject<JToken[]>();

            for (int i = 0; i < response.Length; i++)
            {
                foreach (JToken token in response[i])
                {
                    JProperty prop = token.ToObject<JProperty>();

                    if (prop.Name == KEY_IMAGE_NAME)
                    {
                        contents = prop.Value?.ToString();
                    }

                    if (prop.Name == KEY_DECISION)
                    {
                        decision = prop.Value?.ToString();
                    }
                }

                string key = id == string.Empty ? sn : string.Format("{0}-{1}", sn, id);
                judgeList.Add((key, contents, decision));
            }

            return judgeList;
        }
    }
}
