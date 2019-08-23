using MSTSC.Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSTSC.Manage.Utils
{
    public class CommonUtils
    {
        public static List<KeyValueModel> getAllRegions()
        {
            List<KeyValueModel> result = new List<KeyValueModel>();
            result.Add(new KeyValueModel("北京市", "北京市"));
            result.Add(new KeyValueModel("天津市", "天津市"));
            result.Add(new KeyValueModel("上海市", "上海市"));
            result.Add(new KeyValueModel("重庆市", "重庆市"));
            result.Add(new KeyValueModel("河北省", "河北省"));
            result.Add(new KeyValueModel("山西省", "山西省"));
            result.Add(new KeyValueModel("辽宁省", "辽宁省"));
            result.Add(new KeyValueModel("吉林省", "吉林省"));
            result.Add(new KeyValueModel("黑龙江省", "黑龙江省"));
            result.Add(new KeyValueModel("江苏省", "江苏省"));
            result.Add(new KeyValueModel("浙江省", "浙江省"));
            result.Add(new KeyValueModel("安徽省", "安徽省"));
            result.Add(new KeyValueModel("福建省", "福建省"));
            result.Add(new KeyValueModel("江西省", "江西省"));
            result.Add(new KeyValueModel("山东省", "山东省"));
            result.Add(new KeyValueModel("河南省", "河南省"));
            result.Add(new KeyValueModel("湖北省", "湖北省"));
            result.Add(new KeyValueModel("湖南省", "湖南省"));
            result.Add(new KeyValueModel("广东省", "广东省"));
            result.Add(new KeyValueModel("海南省", "海南省"));
            result.Add(new KeyValueModel("四川省", "四川省"));
            result.Add(new KeyValueModel("贵州省", "贵州省"));
            result.Add(new KeyValueModel("云南省", "云南省"));
            result.Add(new KeyValueModel("陕西省", "陕西省"));
            result.Add(new KeyValueModel("甘肃省", "甘肃省"));
            result.Add(new KeyValueModel("青海省", "青海省"));
            result.Add(new KeyValueModel("台湾省", "台湾省"));
            result.Add(new KeyValueModel("内蒙古自治区", "内蒙古自治区"));
            result.Add(new KeyValueModel("广西壮族自治区", "广西壮族自治区"));
            result.Add(new KeyValueModel("西藏自治区", "西藏自治区"));
            result.Add(new KeyValueModel("宁夏回族自治区", "宁夏回族自治区"));
            result.Add(new KeyValueModel("新疆维吾尔自治区", "新疆维吾尔自治区"));
            result.Add(new KeyValueModel("香港特别行政区", "香港特别行政区"));
            result.Add(new KeyValueModel("澳门特别行政区", "澳门特别行政区"));

            return result;
        }

    }
}
