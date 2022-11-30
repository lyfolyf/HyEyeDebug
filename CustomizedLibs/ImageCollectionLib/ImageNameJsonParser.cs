using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace ImageCollectionLib
{
    public class ImageNameJsonParser
    {
        const string TC_1 = "TC_1";
        const string TC_2 = "TC_2";
        const string TC_3 = "TC_3";
        const string TC_4 = "TC_4";

        const string LCM_1 = "LCM_1";
        const string LCM_2 = "LCM_2";
        const string LCM_3 = "LCM_3";
        const string LCM_4 = "LCM_4";

        const string CORNER = "Corner_1.2.3.4";

        const string SIDE_3 = "Side_3.8";
        const string SIDE_2 = "Side_2.7";
        const string SIDE_1 = "Side_1.6";
        const string SIDE_4 = "Side_4.9";
        const string SIDE_5 = "Side_5.10";

        const string DH_1 = "DH_1";
        const string DH_2 = "DH_2";
        const string DH_3 = "DH_3";
        const string DH_4 = "DH_4";

        const string BC_1 = "BC_1";
        const string BC_2 = "BC_2";
        const string BC_3 = "BC_3";
        const string BC_4 = "BC_4";

        public static Dictionary<string, List<string>> Parse(string json)
        {
            Dictionary<string, List<string>> imageNameDic = new Dictionary<string, List<string>>();

            var rootToken = JToken.Parse(json);

            // TC
            List<string> tc1 = rootToken[TC_1]?.ToObject<List<string>>();
            imageNameDic.Add(TC_1, tc1);

            List<string> tc2 = rootToken[TC_2]?.ToObject<List<string>>();
            imageNameDic.Add(TC_2, tc2);

            List<string> tc3 = rootToken[TC_3]?.ToObject<List<string>>();
            imageNameDic.Add(TC_3, tc3);

            List<string> tc4 = rootToken[TC_4]?.ToObject<List<string>>();
            imageNameDic.Add(TC_4, tc4);

            // LCM
            List<string> lcm1 = rootToken[LCM_1]?.ToObject<List<string>>();
            imageNameDic.Add(LCM_1, lcm1);

            List<string> lcm2 = rootToken[LCM_2]?.ToObject<List<string>>();
            imageNameDic.Add(LCM_2, lcm2);

            List<string> lcm3 = rootToken[LCM_3]?.ToObject<List<string>>();
            imageNameDic.Add(LCM_3, lcm3);

            List<string> lcm4 = rootToken[LCM_4]?.ToObject<List<string>>();
            imageNameDic.Add(LCM_4, lcm4);

            // Corner
            List<string> corner = rootToken[CORNER]?.ToObject<List<string>>();
            imageNameDic.Add(CORNER, corner);

            // Side
            List<string> side1 = rootToken[SIDE_1]?.ToObject<List<string>>();
            imageNameDic.Add(SIDE_1, side1);

            List<string> side2 = rootToken[SIDE_2]?.ToObject<List<string>>();
            imageNameDic.Add(SIDE_2, side2);

            List<string> side3 = rootToken[SIDE_3]?.ToObject<List<string>>();
            imageNameDic.Add(SIDE_3, side3);

            List<string> side4 = rootToken[SIDE_4]?.ToObject<List<string>>();
            imageNameDic.Add(SIDE_4, side4);

            List<string> side5 = rootToken[SIDE_5]?.ToObject<List<string>>();
            imageNameDic.Add(SIDE_5, side5);

            // DH
            List<string> dh1 = rootToken[DH_1]?.ToObject<List<string>>();
            imageNameDic.Add(DH_1, dh1);

            List<string> dh2 = rootToken[DH_2]?.ToObject<List<string>>();
            imageNameDic.Add(DH_2, dh2);

            List<string> dh3 = rootToken[DH_3]?.ToObject<List<string>>();
            imageNameDic.Add(DH_3, dh3);

            List<string> dh4 = rootToken[DH_4]?.ToObject<List<string>>();
            imageNameDic.Add(DH_4, dh4);

            // BC
            List<string> bc1 = rootToken[BC_1]?.ToObject<List<string>>();
            imageNameDic.Add(BC_1, bc1);

            List<string> bc2 = rootToken[BC_2]?.ToObject<List<string>>();
            imageNameDic.Add(BC_2, bc2);

            List<string> bc3 = rootToken[BC_3]?.ToObject<List<string>>();
            imageNameDic.Add(BC_3, bc3);

            List<string> bc4 = rootToken[BC_4]?.ToObject<List<string>>();
            imageNameDic.Add(BC_4, bc4);

            return imageNameDic;
        }

        public static string ReadJsonString(string path)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(fileStream);
            string jsonStr = reader.ReadToEnd();

            return jsonStr;
        }
    }
}
