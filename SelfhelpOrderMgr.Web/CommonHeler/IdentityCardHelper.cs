using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SelfhelpOrderMgr.Web.CommonHeler
{
    public class IdentityCardHelper
    {
        public static bool IsValidChineseId(string cardId)
        {
            //string pattern = @"^[1-9]\d{5}(18|19|20|21|22)?\d{2}(0[1-9]|1[0-2])(0[1-9]|[12]\d|3[01])\d{3}(\d|[Xx])$";
            //return Regex.IsMatch(id, pattern);


            string pattern = @"^\d{17}(?:\d|X)$";
            string birth = cardId.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            // 加权数组,用于验证最后一位的校验数字
            int[] arr_weight = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
            // 最后一位计算出来的校验数组，如果不等于这些数字将不正确
            string[] id_last = { "1", "0", "X", "9", "8", "7", "6", "5", "4", "3", "2" };
            int sum = 0;
            //通过循环前16位计算出最后一位的数字
            for (int i = 0; i < 17; i++) { sum += arr_weight[i] * int.Parse(cardId[i].ToString()); }
            // 实际校验位的值
            int result = sum % 11;
            // 首先18位格式检查
            if (Regex.IsMatch(cardId, pattern))
            {
                // 出生日期检查
                if (DateTime.TryParse(birth, out time))
                {
                    // 校验位检查
                    if (id_last[result] == cardId[17].ToString())
                    {
                        return true;
                    }
                    else
                    {
                        //"身份证最后一位校验错误!";
                        return false;
                    }
                }
                else
                {
                    //"出生日期验证失败!";
                    return false;
                }
            }
            else
            {
                // "身份证号格式错误!";
                return false;
            }

        }

        //验证错误原因
        public static string CheckIdenCard(string fsex, string fidenNo)
        {
            if (fidenNo.Length == 18)
            {
                //倒数第二位的单数是男，双数是女
                var chkValue = Convert.ToInt32(fidenNo.Substring(16, 1)) % 2;
                if ((chkValue == 1 && fsex != "男") || (chkValue == 0 && fsex != "女"))
                {
                    return $"Err|身份证号与性别不正确";
                }


                //身份证规则验证不正确
                if (!IdentityCardHelper.IsValidChineseId(fidenNo))
                {
                    return $"Err|身份证号码规则验证不正确";
                }

                return "";

            }

            return "";
        }
    }
}